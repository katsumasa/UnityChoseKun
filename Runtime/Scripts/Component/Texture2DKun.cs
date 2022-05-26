using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine
{
    public class Texture2DKun : TextureKun
    {
        //bool m_alphaIsTransparency;
        //public bool alphaIsTransparency
        //{
        //    get { return m_alphaIsTransparency; }
        //    set { m_alphaIsTransparency = value; }
        //}

        int m_calculatedMipmapLevel;
        public int calculatedMipmapLevel
        {
            get { return m_calculatedMipmapLevel; }
        }

        int m_desiredMipmapLevel;
        public int desiredMipmapLevel
        {
            get { return m_desiredMipmapLevel; }
        }


        TextureFormat m_format;
        public TextureFormat format
        {
            get { return m_format; }
        }

        int m_loadedMipmapLevel;
        public int loadedMipmapLevel
        {
            get { return m_loadedMipmapLevel; }
        }

        int m_loadingMipmapLevel;
        public int loadingMipmapLevel
        {
            get { return m_loadingMipmapLevel; }
        }

        int m_minimumMipmapLevel;
        public int minimumMipmapLevel
        {
            get { return m_minimumMipmapLevel; }
            set { m_minimumMipmapLevel = value; }
        }

        int m_requestedMipmapLevel;
        public int requestedMipmapLevel
        {
            get { return m_requestedMipmapLevel; }
            set { m_requestedMipmapLevel = value; }
        }

        bool m_streamingMipmaps;
        public bool streamingMipmaps
        {
            get { return m_streamingMipmaps; }
        }

        int m_streamingMipmapsPriority;
        public int streamingMipmapsPriority
        {
            get { return m_streamingMipmapsPriority; }
        }

        public bool m_vtOnly;
        public bool vtOnly
        {
            get { return m_vtOnly; }
        }

        public Texture2DKun() : base(null) { }

        public Texture2DKun(Texture2D texture2D) : base(texture2D)
        {
            if (texture2D)
            {
                //m_alphaIsTransparency = texture2D.alphaIsTransparency;
                m_calculatedMipmapLevel = texture2D.calculatedMipmapLevel;
                m_desiredMipmapLevel = texture2D.desiredMipmapLevel;
                m_format = texture2D.format;
                m_loadedMipmapLevel = texture2D.loadedMipmapLevel;
                m_loadingMipmapLevel = texture2D.loadingMipmapLevel;
                m_minimumMipmapLevel = texture2D.minimumMipmapLevel;
                m_requestedMipmapLevel = texture2D.requestedMipmapLevel;
                m_streamingMipmaps = texture2D.streamingMipmaps;
                m_streamingMipmapsPriority = texture2D.streamingMipmapsPriority;
                m_vtOnly = texture2D.vtOnly;
            }
        }

        public override bool WriteBack(UnityEngine.Object obj)
        {
            if (base.WriteBack(obj))
            {
                var texture2D = obj as Texture2D;
                if (texture2D)
                {
                    //texture2D.alphaIsTransparency = m_alphaIsTransparency;
                    texture2D.minimumMipmapLevel = m_minimumMipmapLevel;
                    texture2D.requestedMipmapLevel = m_requestedMipmapLevel;
                    return true;
                }
            }
            return false;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);


            //binaryWriter.Write(m_alphaIsTransparency);
            binaryWriter.Write(m_calculatedMipmapLevel);
            binaryWriter.Write(m_desiredMipmapLevel);
            binaryWriter.Write((int)m_format);
            binaryWriter.Write(m_loadedMipmapLevel);
            binaryWriter.Write(m_loadingMipmapLevel);
            binaryWriter.Write(m_minimumMipmapLevel);
            binaryWriter.Write(m_requestedMipmapLevel);
            binaryWriter.Write(m_streamingMipmaps);
            binaryWriter.Write(m_streamingMipmapsPriority);
            binaryWriter.Write(m_vtOnly);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);

            //m_alphaIsTransparency = binaryReader.ReadBoolean();
            m_calculatedMipmapLevel = binaryReader.ReadInt32();
            m_desiredMipmapLevel = binaryReader.ReadInt32();
            m_format =  (TextureFormat)binaryReader.ReadInt32();
            m_loadedMipmapLevel = binaryReader.ReadInt32();
            m_loadingMipmapLevel = binaryReader.ReadInt32();
            m_minimumMipmapLevel = binaryReader.ReadInt32();
            m_requestedMipmapLevel = binaryReader.ReadInt32();
            m_streamingMipmaps = binaryReader.ReadBoolean();
            m_streamingMipmapsPriority = binaryReader.ReadInt32();
            m_vtOnly = binaryReader.ReadBoolean();
        }
    }
}
