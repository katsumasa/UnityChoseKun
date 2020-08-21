using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class MaterialKun : ObjectKun
    {
        [SerializeField] Color m_color;
        public Color color {
            get{return m_color;}
            set{m_color = value;}
        }

        [SerializeField] bool m_doubleSidedGI;
        public bool doubleSidedGI{
            get{return m_doubleSidedGI;}
            set {m_doubleSidedGI = value;}
        }

        [SerializeField] bool m_enableInstancing ;
        public bool enableInstancing{
            get{return m_enableInstancing;}
            set{m_enableInstancing = value;}
        }

        [SerializeField] MaterialGlobalIlluminationFlags m_globalIlluminationFlags ;
        public MaterialGlobalIlluminationFlags globalIlluminationFlags{
            get{return m_globalIlluminationFlags;}
            set{m_globalIlluminationFlags = value;}
        }

        [SerializeField] TextureKun m_mainTexture;
        public TextureKun mainTexture{
            get{if(m_mainTexture!=null){m_mainTexture = new TextureKun();} return m_mainTexture;}
            private set{m_mainTexture = value;}
        }
        [SerializeField] Vector2 m_mainTextureOffset ;
        public Vector2 mainTextureOffset {
            get{return m_mainTextureOffset;}
            set{m_mainTextureOffset = value;}
        }

        [SerializeField] Vector2 m_mainTextureScale ;
        public Vector2 mainTextureScale {
            get{return m_mainTextureScale;}
            set{m_mainTextureScale = value;}
        }

        [SerializeField] int m_passCount ;
        public int passCount {
            get {return m_passCount;}
            private set {m_passCount = value;}
        }

        [SerializeField]  int m_renderQueue ;
        public int renderQueue {
            get{return m_renderQueue;}
            private set{m_renderQueue = value;}
        }

        [SerializeField] ShaderKun m_shader;
        public ShaderKun shader {
            get{return m_shader;}
            set{m_shader = value;}
        }

        [SerializeField] string[] m_shaderKeywords ;
        public string[] shaderKeywords {
            get{return m_shaderKeywords;}
            set{m_shaderKeywords = value;}
        }

        public MaterialKun():base(){}

        public MaterialKun(UnityEngine.Object obj):base(obj)
        {
            var material = obj as Material;
            if(material){
                color = material.color;
                doubleSidedGI = material.doubleSidedGI;
                enableInstancing = material.enableInstancing;
                globalIlluminationFlags = material.globalIlluminationFlags;                                
                passCount = material.passCount;
                renderQueue = material.renderQueue;                                
                if(material.shader != null){
                    shader = new ShaderKun(material.shader);
                    // TODO : Material.GetTextureを使って、"_MainTex" "_BumpMap" ,"_Cube" を取得
                    if(material.shader.FindPropertyIndex("_MainTex") != -1){
                        //if(material.mainTexture != null){
                        //    mainTexture = new TextureKun(material.mainTexture);
                        //    mainTextureOffset = material.mainTextureOffset;
                        //    mainTextureOffset = material.mainTextureScale;
                        //}
                    }                    
                }
                shaderKeywords = material.shaderKeywords;
            }
        }
        // TODO : Materialのwrite back
        public override void WriteBack(Object obj)
        {
            base.WriteBack(obj);
            var material = obj as Material;
            if(material){
                material.color = color;
                material.doubleSidedGI = doubleSidedGI;
                material.enableInstancing = enableInstancing;
                material.globalIlluminationFlags = globalIlluminationFlags;
                shader.WriteBack(material.shader);
                if(mainTexture != null){
                    mainTexture.WriteBack(material.mainTexture);                
                }
                material.mainTextureOffset = mainTextureOffset;
                material.mainTextureScale = mainTextureOffset;                
                material.renderQueue = renderQueue;                
                shader.WriteBack(material.shader);
                material.shaderKeywords = shaderKeywords;
            }
        }
    }
}
