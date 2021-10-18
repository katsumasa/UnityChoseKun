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
    }
}
