using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun {
    [System.Serializable]
    public class TextureKun : ObjectKun
    {
        [SerializeField] int m_anisoLevel ;
        public int anisoLevel{
            get{return m_anisoLevel;}
            set{m_anisoLevel = value;}
        }

        [SerializeField] UnityEngine.Rendering.TextureDimension m_dimension ;
        public UnityEngine.Rendering.TextureDimension dimension{
            get{return m_dimension;}
            protected set {m_dimension = value;}
        }

        [SerializeField]FilterMode m_filterMode ;
        public FilterMode filterMode{
            get{return m_filterMode;}
            set{m_filterMode = value;}
        }

        [SerializeField] UnityEngine.Experimental.Rendering.GraphicsFormat m_graphicsFormat ;
        public UnityEngine.Experimental.Rendering.GraphicsFormat graphicsFormat{
            get{return m_graphicsFormat;}
            set{m_graphicsFormat = value;}
        }

        [SerializeField] int m_height ;
        public int height{
            get{return m_height;}
            private set {m_height = value;}
        }
        
        [SerializeField] bool m_isReadable ;
        public bool isReadable {
            get{return m_isReadable;}
            private set{m_isReadable = value;}
        }

        [SerializeField] float m_mipMapBias ;
        public float mipMapBias {
            get{return m_mipMapBias;}
            set{m_mipMapBias = value;}
        }

        [SerializeField] int m_mipmapCount ;
        public int mipmapCount {
            get {return m_mipmapCount;}
            protected set {m_mipmapCount = value;}
        }

        [SerializeField] uint m_updateCount ;
        public uint updateCount {
            get{return m_updateCount;}
            private set{m_updateCount = value;}
        }

        [SerializeField] int m_width ;
        public int width {
            get {return m_width;}
            private set {m_width = value;}
        }

        [SerializeField] TextureWrapMode m_wrapMode ;
        public TextureWrapMode wrapMode {
            get {return m_wrapMode;}
            set {m_wrapMode = value;}
        }

        [SerializeField] TextureWrapMode m_wrapModeU ;
        public TextureWrapMode wrapModeU {
            get{return m_wrapModeU;}
            set{m_wrapModeU = value;}
        }
        [SerializeField] TextureWrapMode m_wrapModeV ;
        public TextureWrapMode wrapModeV {
            get{return m_wrapModeV;}
            set{m_wrapModeV = value;}
        }

        [SerializeField] TextureWrapMode m_wrapModeW ;
        public TextureWrapMode wrapModeW {
            get{return m_wrapModeW;}
            set{m_wrapModeW = value;}
        }

        public TextureKun():this(null){}

        public TextureKun(Object obj):base(obj)
        {
            var texture = obj as Texture;
            if(texture){
                anisoLevel = texture.anisoLevel;
                dimension = texture.dimension;
                filterMode = texture.filterMode;
                graphicsFormat = texture.graphicsFormat;
                height = texture.height;
                //imageContentsHash = texture.imageContentsHash;
                isReadable = texture.isReadable;
                mipMapBias = texture.mipMapBias;
                mipmapCount = texture.mipmapCount;
                updateCount = texture.updateCount;
                width = texture.width;
                wrapMode = texture.wrapMode;
                wrapModeU = texture.wrapModeU;
                wrapModeV = texture.wrapModeV;
                wrapModeW = texture.wrapModeW;
            }
        }

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
                    texture.wrapModeU = wrapModeU;
                    texture.wrapModeV = wrapModeV;
                    texture.wrapModeW = wrapModeW;
                }
                return true;
            }
            return false;
        }
    }
}
