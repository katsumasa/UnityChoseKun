namespace  Utj.UnityChoseKun 
{
    using System.IO;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;


    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class SceneKun : ISerializerKun
    {
        [SerializeField] public string name;
        [SerializeField] public int rootCount;
        [SerializeField] public GameObjectKun [] mGameObjectKuns;     
        
        
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
            rootCount = binaryReader.ReadInt32();
            var len = binaryReader.ReadInt32();
            if(len != -1)
            {
                mGameObjectKuns = new GameObjectKun[len];
                for(var i = 0; i < len; i++)
                {
                    mGameObjectKuns[i] = new GameObjectKun();
                    mGameObjectKuns[i].Deserialize(binaryReader);
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