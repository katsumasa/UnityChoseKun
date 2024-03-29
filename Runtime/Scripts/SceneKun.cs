﻿using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace  Utj.UnityChoseKun.Engine
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class SceneKun : ISerializerKun
    {
        public string name;
        public int mHandle;
        public int rootCount;
        public GameObjectKun [] mGameObjectKuns;
        
        
        /// <summary>
        /// 
        /// </summary>
        public GameObjectKun[] gameObjectKuns
        {
            get {                 
                if(mGameObjectKuns == null)
                {
                    mGameObjectKuns = new GameObjectKun[0];
                }
                return mGameObjectKuns;
            }

            set
            {
                mGameObjectKuns = value;
            }
        }

        public int handle
        {
            get { return mHandle; }
        }


        /// <summary>
        /// 
        /// </summary>
        public SceneKun()
        {
            name = "None";
            rootCount = 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        public SceneKun(Scene scene):this()
        {
            this.name = scene.name;
            this.mHandle = scene.handle;
            this.rootCount = scene.rootCount;
            var  list =  new List<GameObjectKun>();                        
            foreach (var obj in scene.GetRootGameObjects())
            {
                AddGameObjectKunInChildren(obj,list);             
            }
            this.gameObjectKuns = list.ToArray();            
        }


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(name);
            binaryWriter.Write(mHandle);
            binaryWriter.Write(rootCount);
            if(mGameObjectKuns == null)
            {
                binaryWriter.Write(-1);
            }
            else
            {
                binaryWriter.Write(mGameObjectKuns.Length);
                for(var i = 0; i < mGameObjectKuns.Length; i++)
                {
                    mGameObjectKuns[i].Serialize(binaryWriter);
                }
            }
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            name = binaryReader.ReadString();
            mHandle = binaryReader.ReadInt32();
            rootCount = binaryReader.ReadInt32();
            var len = binaryReader.ReadInt32();
            if(len != -1)
            {
                List<CameraKun> cameraKuns = new List<CameraKun>();

                mGameObjectKuns = new GameObjectKun[len];
                for(var i = 0; i < len; i++)
                {
                    mGameObjectKuns[i] = new GameObjectKun();
                    mGameObjectKuns[i].Deserialize(binaryReader);

                    var cameraKun = mGameObjectKuns[i].GetComponentKun<CameraKun>();
                    if(cameraKun != null)
                    {
                        cameraKuns.Add(cameraKun);
                    }
                }
                if (cameraKuns.Count > 0)
                {
                    CameraKun.allCameraList = cameraKuns;
                } 
                else
                {
                    CameraKun.allCameraList = null;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as SceneKun;
            if(other == null)
            {
                return false;
            }
            if (!string.Equals(name, other.name))
            {
                return false;
            }
            if (this.handle != other.handle)
            {
                return false;
            }
            if (!int.Equals(rootCount, other.rootCount))
            {
                return false;
            }
            if(mGameObjectKuns != null)
            {
                if(other.mGameObjectKuns == null)
                {
                    return false;
                }
                if(mGameObjectKuns.Length != other.mGameObjectKuns.Length)
                {
                    return false;
                }
                for(var i = 0; i < mGameObjectKuns.Length; i++)
                {
                    if (!GameObjectKun.Equals(mGameObjectKuns[i], other.mGameObjectKuns[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public GameObjectKun[] GetRootGameObjects()
        {
            return mGameObjectKuns;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="go"></param>
        /// <param name="list"></param>
        private void AddGameObjectKunInChildren(GameObject go,List<GameObjectKun> list)
        {            
            if(go == null || list == null){
                return;
            }            
            var gk = new GameObjectKun(go);
            list.Add(gk);
            if(go.transform != null){
                for(var i = 0; i < go.transform.childCount; i++)
                {
                    AddGameObjectKunInChildren(go.transform.GetChild(i).gameObject,list);
                }
            }            
        }
    }


}