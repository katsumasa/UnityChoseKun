using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    public class CubemapKun : TextureKun
    {
        int m_desiredMipmapLevel;
        TextureFormat m_format;
        int m_loadedMipmapLevel;
        int m_loadingMipmapLevel;
        int m_requestedMipmapLevel;
        bool m_streamingMipmaps;
        int m_streamingMipmapsPriority;

        public int desiredMipmapLevel
        {
            get { return m_desiredMipmapLevel; }
        }
        public TextureFormat format
        {
            get { return m_format; }
        }
        public int loadedMipmapLevel
        {
            get { return m_loadedMipmapLevel; }
        }
        public int loadingMipmapLevel
        {
            get { return m_loadingMipmapLevel; }
        }
        public int requestedMipmapLevel
        {
            get
            {
                return m_requestedMipmapLevel;
            }
            set
            {
                m_requestedMipmapLevel = value;
            }
        }
        public bool streamingMipmaps
        {
            get
            {
                return m_streamingMipmaps;
            }

        }
        public int streamingMipmapsPriority
        {
            get
            {
                return m_streamingMipmapsPriority;
            }
        }

        public CubemapKun() : this(null) { }

        public CubemapKun(Object obj) : base(obj)
        {
            var cubemap = obj as Cubemap;
            if (cubemap)
            {
                m_desiredMipmapLevel = cubemap.desiredMipmapLevel;
                m_format = cubemap.format;
                m_loadedMipmapLevel = cubemap.loadedMipmapLevel;
                m_loadingMipmapLevel = cubemap.loadingMipmapLevel;
                m_requestedMipmapLevel = cubemap.requestedMipmapLevel;
                m_streamingMipmaps = cubemap.streamingMipmaps;
                m_streamingMipmapsPriority = cubemap.streamingMipmapsPriority;
            }
        }

        public override bool WriteBack(UnityEngine.Object obj)
        {
            if (base.WriteBack(obj))
            {
                var cubemap = obj as Cubemap;
                if (cubemap)
                {
                    cubemap.requestedMipmapLevel = m_requestedMipmapLevel;
                }
                return true;
            }
            return false;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_desiredMipmapLevel);
            binaryWriter.Write((int)m_format);
            binaryWriter.Write(m_loadedMipmapLevel);
            binaryWriter.Write(m_loadingMipmapLevel);
            binaryWriter.Write(m_requestedMipmapLevel);
            binaryWriter.Write(m_streamingMipmaps);
            binaryWriter.Write(m_streamingMipmapsPriority);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_desiredMipmapLevel = binaryReader.ReadInt32();
            m_format = (TextureFormat)binaryReader.ReadInt32();
            m_loadedMipmapLevel = binaryReader.ReadInt32();
            m_loadingMipmapLevel = binaryReader.ReadInt32();
            m_requestedMipmapLevel = binaryReader.ReadInt32();
            m_streamingMipmaps = binaryReader.ReadBoolean();
            m_streamingMipmapsPriority = binaryReader.ReadInt32();
        }

        public override bool Equals(object obj)
        {
            var other = obj as CubemapKun;
            if(obj == null)
            {
                return false;
            }
            if (!m_desiredMipmapLevel.Equals(other.desiredMipmapLevel))
            {
                return false;
            }
            if (!m_format.Equals(other.format))
            {
                return false;
            }
            if (!m_loadedMipmapLevel.Equals(other.loadedMipmapLevel))
            {
                return false;
            }
            if (!m_loadingMipmapLevel.Equals(other.loadingMipmapLevel))
            {
                return false;
            }
            if (!requestedMipmapLevel.Equals(other.requestedMipmapLevel))
            {
                return false;
            }
            if (!m_streamingMipmaps.Equals(other.m_streamingMipmaps))
            {
                return false;
            }
            if (!m_streamingMipmapsPriority.Equals(other.m_streamingMipmapsPriority))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
