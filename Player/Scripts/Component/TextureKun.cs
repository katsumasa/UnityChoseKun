using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Utj.UnityChoseKun {
    [System.Serializable]
    public class TextureKun : ObjectKun
    {
#if UNITY_2019_1_OR_NEWER
        [SerializeField] UnityEngine.Experimental.Rendering.GraphicsFormat m_graphicsFormat ;
        [SerializeField] int m_mipmapCount;
 #endif
        [SerializeField] int m_anisoLevel ;
        [SerializeField] UnityEngine.Rendering.TextureDimension m_dimension;
        [SerializeField] FilterMode m_filterMode;
        [SerializeField] int m_height;
        [SerializeField] bool m_isReadable;
        [SerializeField] float m_mipMapBias;
        [SerializeField] uint m_updateCount;
        [SerializeField] int m_width;
        [SerializeField] TextureWrapMode m_wrapMode;
        [SerializeField] TextureWrapMode m_wrapModeU;
        [SerializeField] TextureWrapMode m_wrapModeV;
        [SerializeField] TextureWrapMode m_wrapModeW;


#if UNITY_2019_1_OR_NEWER
        public UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat
        {
            get { return m_graphicsFormat; }
            set { m_graphicsFormat = value; }
        }


        public int mipmapCount
        {
            get { return m_mipmapCount; }
            protected set { m_mipmapCount = value; }
        }
#endif


        public int anisoLevel{
            get{return m_anisoLevel;}
            set{m_anisoLevel = value;}
        }
        
        public UnityEngine.Rendering.TextureDimension dimension{
            get{return m_dimension;}
            protected set {m_dimension = value;}
        }
                
        public FilterMode filterMode{
            get{return m_filterMode;}
            set{m_filterMode = value;}
        }
                        
        public int height{
            get{return m_height;}
            private set {m_height = value;}
        }
                
        public bool isReadable {
            get{return m_isReadable;}
            set{m_isReadable = value;}
        }

        public float mipMapBias {
            get{return m_mipMapBias;}
            set{m_mipMapBias = value;}
        }        

        public uint updateCount {
            get{return m_updateCount;}
            private set{m_updateCount = value;}
        }

        public int width {
            get {return m_width;}
            private set {m_width = value;}
        }

        public TextureWrapMode wrapMode {
            get {return m_wrapMode;}
            set {m_wrapMode = value;}
        }

        public TextureWrapMode wrapModeU {
            get{return m_wrapModeU;}
            set{m_wrapModeU = value;}
        }
        
        public TextureWrapMode wrapModeV {
            get{return m_wrapModeV;}
            set{m_wrapModeV = value;}
        }

        public TextureWrapMode wrapModeW {
            get{return m_wrapModeW;}
            set{m_wrapModeW = value;}
        }


        /// <summary>
        /// 
        /// </summary>
        public TextureKun():this(null){}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public TextureKun(Object obj):base(obj)
        {
            var texture = obj as Texture;
            if(texture){
#if UNITY_2019_1_OR_NEWER
                graphicsFormat = texture.graphicsFormat;
                mipmapCount = texture.mipmapCount;
#endif
                anisoLevel = texture.anisoLevel;
                dimension = texture.dimension;
                filterMode = texture.filterMode;                
                height = texture.height;
                //imageContentsHash = texture.imageContentsHash;
                isReadable = texture.isReadable;
                mipMapBias = texture.mipMapBias;                
                updateCount = texture.updateCount;
                width = texture.width;
                wrapMode = texture.wrapMode;
                wrapModeU = texture.wrapModeU;
                wrapModeV = texture.wrapModeV;
                wrapModeW = texture.wrapModeW;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool WriteBack(UnityEngine.Object obj)
        {
            if(base.WriteBack(obj)){
                var texture = obj as Texture;
                if(texture){
                    texture.anisoLevel = anisoLevel;
                    texture.dimension = dimension;
                    texture.filterMode = filterMode;                                                        
                    texture.mipMapBias = mipMapBias;                                                
                    texture.wrapMode = wrapMode;
                    //texture.wrapModeU = wrapModeU;
                    //texture.wrapModeV = wrapModeV;
                    texture.wrapModeW = wrapModeW;
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);

#if UNITY_2019_1_OR_NEWER
            binaryWriter.Write((int)m_graphicsFormat);
            binaryWriter.Write(m_mipmapCount);
#endif
            binaryWriter.Write(m_anisoLevel);
            binaryWriter.Write((int)m_dimension);
            binaryWriter.Write((int)m_filterMode);
            binaryWriter.Write(m_height);
            binaryWriter.Write(m_isReadable);
            binaryWriter.Write(m_mipMapBias);
            binaryWriter.Write(m_updateCount);
            binaryWriter.Write(m_width);
            binaryWriter.Write((int)m_wrapMode);
            binaryWriter.Write((int)m_wrapModeU);
            binaryWriter.Write((int)m_wrapModeV);
            binaryWriter.Write((int)m_wrapModeW);
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
#if UNITY_2019_1_OR_NEWER
            m_graphicsFormat = (UnityEngine.Experimental.Rendering.GraphicsFormat)binaryReader.ReadInt32();
            m_mipmapCount = binaryReader.ReadInt32();
#endif
            m_anisoLevel = binaryReader.ReadInt32();
            m_dimension = (UnityEngine.Rendering.TextureDimension)binaryReader.ReadInt32();
            m_filterMode = (FilterMode)binaryReader.ReadInt32();
            m_height = binaryReader.ReadInt32();
            m_isReadable = binaryReader.ReadBoolean();
            m_mipMapBias = binaryReader.ReadSingle();
            m_updateCount = binaryReader.ReadUInt32();
            m_width = binaryReader.ReadInt32();
            m_wrapMode = (TextureWrapMode)binaryReader.ReadInt32();
            m_wrapModeU = (TextureWrapMode)binaryReader.ReadInt32();
            m_wrapModeV = (TextureWrapMode)binaryReader.ReadInt32();
            m_wrapModeW = (TextureWrapMode)binaryReader.ReadInt32();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as TextureKun;
            if(other == null)
            {
                return false;
            }
            if(mipmapCount.Equals(other.mipmapCount) == false)
            {
                return false;
            }
            if (anisoLevel.Equals(other.anisoLevel) == false)
            {
                return false;
            }
            if (dimension.Equals(other.dimension) == false)
            {
                return false;
            }
            if (filterMode.Equals(other.filterMode) == false)
            {
                return false;
            }
            if (height.Equals(other.height) == false)
            {
                return false;
            }
            if (isReadable.Equals(other.isReadable) == false)
            {
                return false;
            }
            if (mipMapBias.Equals(other.mipMapBias) == false)
            {
                return false;
            }
            if(updateCount.Equals(other.updateCount) == false)
            {
                return false;
            }
            if (width.Equals(other.width) == false)
            {
                return false;
            }
            if(wrapMode.Equals(other.wrapMode)== false)
            {
                return false;
            }
            if (wrapModeU.Equals(other.wrapModeU) == false)
            {
                return false;
            }
            if (wrapModeV.Equals(other.wrapModeV) == false)
            {
                return false;
            }
            if (wrapModeW.Equals(other.wrapModeW) == false)
            {
                return false;
            }

            return base.Equals(obj);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
