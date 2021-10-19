using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utj.UnityChoseKun
{

    [System.Serializable]
    public class SceneManagerKun : ISerializerKun
    {
        SceneKun[] mSceneKuns;


        public SceneKun[] sceneKuns
        {
            get 
            { 
                if(mSceneKuns == null)
                {
                    mSceneKuns = new SceneKun[0];
                }
                return mSceneKuns; 
            }
            set { mSceneKuns = value; }
        }


        public SceneManagerKun(bool isCreate = false)
        {
            if (isCreate)
            {
                var len = SceneManager.sceneCount;
                mSceneKuns = new SceneKun[len];
                for (var i = 0; i < len; i++)
                {
                    var scene = SceneManager.GetSceneAt(i);
                    mSceneKuns[i] = new SceneKun(scene);
                }
            }
        }
        

        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            if(mSceneKuns == null)
            {
                binaryWriter.Write(-1);
            }
            else
            {
                binaryWriter.Write(mSceneKuns.Length);
                for(var i = 0; i < mSceneKuns.Length; i++)
                {
                    mSceneKuns[i].Serialize(binaryWriter);
                }
            }
        }


        public virtual void Deserialize(BinaryReader binaryReader)
        {
            var len = binaryReader.ReadInt32();
            if(len != -1)
            {
                mSceneKuns = new SceneKun[len];
                for(var i = 0; i < mSceneKuns.Length; i++)
                {
                    mSceneKuns[i] = new SceneKun();
                    mSceneKuns[i].Deserialize(binaryReader);
                }
            }
        }


        public override bool Equals(object obj)
        {
            var other = obj as SceneManagerKun;
            if(obj == null)
            {
                return false;
            }
            if(sceneKuns == null && other.sceneKuns == null)
            {
                return true;
            }
            if(sceneKuns == null && other.sceneKuns != null)
            {
                return false;
            }
            if(sceneKuns != null && other.sceneKuns == null)
            {
                return false;
            }
            if(sceneKuns.Length != other.sceneKuns.Length)
            {
                return false;
            }
            for(var i = 0; i < sceneKuns.Length; i++)
            {
                if (!SceneKun.Equals(sceneKuns[i], other.sceneKuns[i]))
                {
                    return false;
                }
            }
            return true;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public SceneKun GetSceneKunAt(int index)
        {
            return sceneKuns[index];
        }


        public SceneKun GetSceneKunHn(int handle)
        {
            for(var i = 0; i < sceneKuns.Length; i++)
            {
                if(sceneKuns[i].handle == handle)
                {
                    return sceneKuns[i];
                }
            }
            return null;
        }


        public void MoveGameObjectToScene(GameObjectKun gameObjectKun,SceneKun sceneKun)
        {            
            // RootGameObjectの場合、元のSceneから削除する
            for (var i = 0; i < sceneKuns.Length; i++)
            {
                var list = new List<GameObjectKun>(sceneKuns[i].gameObjectKuns);
                if (list.Contains(gameObjectKun))
                {
                    list.Remove(gameObjectKun);
                    sceneKuns[i].gameObjectKuns = list.ToArray();
                    break;
                }                
            }

            // 移動先のSceneへ追加
            {
                var list = new List<GameObjectKun>(sceneKun.gameObjectKuns);
                list.Add(gameObjectKun);
                sceneKun.gameObjectKuns = list.ToArray();
                gameObjectKun.transformKun.sceneHn = sceneKun.handle;
            }
        }
    }
}
