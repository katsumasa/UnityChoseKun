using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class AssetPacket<T> : ISerializerKun where T : ObjectKun ,new(){
        [SerializeField] bool m_isResources;
        [SerializeField] bool m_isScene;
        [SerializeField] T[] m_assetKuns;

        public bool isResources
        {
            get { return m_isResources; }
            set { m_isResources = value; }
        }
        public bool isScene
        {
            get { return m_isScene; }
            set { m_isScene = value; }
        }
        public T[] assetKuns
        {
            get { return m_assetKuns; }
            set { m_assetKuns = value; }
        }


        public AssetPacket(T[] assetKuns)
        {
            this.assetKuns = assetKuns;
        }


        public AssetPacket() : this(null) { }
        

       
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_isResources);
            binaryWriter.Write(m_isScene);
            if(m_assetKuns == null)
            {
                binaryWriter.Write(-1);
            }
            else
            {
                binaryWriter.Write(m_assetKuns.Length);
                for(var i = 0; i < m_assetKuns.Length; i++)
                {
                    m_assetKuns[i].Serialize(binaryWriter);
                }
            }
        }

        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_isResources = binaryReader.ReadBoolean();
            m_isScene = binaryReader.ReadBoolean();
            var len = binaryReader.ReadInt32();
            if (len != -1)
            {
                m_assetKuns = new T[len];
                for (var i = 0; i < len; i++)
                {
                    m_assetKuns[i] = new T();
                    m_assetKuns[i].Deserialize(binaryReader);
                }
            }
        }    
    }
}