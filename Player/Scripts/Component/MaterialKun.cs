using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class MaterialKun : ObjectKun
    {
        // MaterialPropertyをRuntime/Editor両方で使用する為のClass
        [System.Serializable]
        public class Property {            
            [SerializeField] bool  m_dirty;
            [SerializeField] Color m_colorValue;
            [SerializeField] string m_displayName;
            [SerializeField] UnityEngine.Rendering.ShaderPropertyFlags m_flags;
            [SerializeField] float m_floatValue;
            [SerializeField] string m_name;                        
            [SerializeField] int m_nameId;
            [SerializeField] Vector2 m_rangeLimits;            
            [SerializeField] UnityEngine.Rendering.TextureDimension m_textureDimension;
            [SerializeField] TextureKun m_textureValue;
            [SerializeField] UnityEngine.Rendering.ShaderPropertyType m_type;
            [SerializeField] Vector4 m_vectorValue;

            [SerializeField] Vector2 m_scale; // Tiling
            [SerializeField] Vector2 m_offset; // Offset

            public bool dirty{
                get{return m_dirty;}
                set{m_dirty = value;}
            }
            public Color colorValue{
                get{return m_colorValue;}
                set{m_colorValue = value;}
            }
            public string displayName {
                get {return m_displayName;}
                set {m_displayName = value;}
            }
            public UnityEngine.Rendering.ShaderPropertyFlags flags{
                get{return m_flags;}
                set{m_flags = value;}
            }
            public float floatValue{
                get{return m_floatValue;}
                set{m_floatValue = value;}
            }
            public string name{
                get{return m_name;}
                set{m_name = value;}
            }
            public int nameId {
                get{return m_nameId;}
                set{m_nameId = value;}
            }
            public Vector2 rangeLimits{
                get{return m_rangeLimits;}
                set{m_rangeLimits = value;}
            }
            public UnityEngine.Rendering.TextureDimension textureDimension{
                get{return m_textureDimension;}
                set{m_textureDimension = value;}
            }
            public TextureKun textureValue{
                get{return m_textureValue;}
                set{m_textureValue = value;}
            }
            public UnityEngine.Rendering.ShaderPropertyType type{
                get{return m_type;}
                set{m_type = value;}
            }
            public Vector4 vectorValue{
                get {return m_vectorValue;}
                set {m_vectorValue = value;}
            }
            public Vector2 scale {
                get{return m_scale;}
                set{m_scale = value;}
            }            
            public Vector2 offset {
                get{return m_offset;}
                set{m_offset = value;}
            }            


            public Property()
            {
                dirty = false;
            }
        }


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
        [SerializeField] int m_mainTexPropIdx = -1;
        int mainTexPrpoIdx {
            get{return m_mainTexPropIdx;}
            set{m_mainTexPropIdx = value;}
        }

        
        public TextureKun mainTexture{
            get{if(mainTexPrpoIdx != -1){return propertys[mainTexPrpoIdx].textureValue;} return null;}
            private set{if(mainTexPrpoIdx != -1){propertys[mainTexPrpoIdx].textureValue = value;}}
        }
        
        public Vector2 mainTextureOffset {
            get{if(mainTexPrpoIdx != -1){return propertys[mainTexPrpoIdx].offset;} return Vector2.zero;}
            set{if(mainTexPrpoIdx != -1){propertys[mainTexPrpoIdx].offset = value;}}
        }
        
        public Vector2 mainTextureScale {
            get{if(mainTexPrpoIdx != -1){return propertys[mainTexPrpoIdx].scale;} return Vector2.zero;}
            set{if(mainTexPrpoIdx != -1){propertys[mainTexPrpoIdx].scale = value;}}
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

        [SerializeField] Property[] m_propertys;
        public Property[] propertys{
            get{return m_propertys;}
            set{m_propertys = value;}
        }
        
        
        public MaterialKun():base(){}

        public MaterialKun(UnityEngine.Object obj):base(obj)
        {            
            var material = obj as Material;
            if(material){
                name = material.name;
                color = material.color;
                doubleSidedGI = material.doubleSidedGI;
                enableInstancing = material.enableInstancing;
                globalIlluminationFlags = material.globalIlluminationFlags;                                
                passCount = material.passCount;
                renderQueue = material.renderQueue;
                shaderKeywords = material.shaderKeywords;                                
                if(material.shader != null){
                    shader = new ShaderKun(material.shader);

                    if(shader.GetPropertyCount() > 0){
                        propertys = new Property[shader.GetPropertyCount()];
                        for(var i = 0; i < shader.GetPropertyCount(); i++){
                            propertys[i] = new Property();
                            
                            propertys[i].type = shader.GetPropertyType(i);
                            propertys[i].name = shader.GetPropertyName(i);
                            propertys[i].nameId = shader.GetPropertyNameId(i);
                            propertys[i].displayName = shader.GetPropertyDescription(i);
                            propertys[i].flags = shader.GetPropertyFlags(i);
                            switch(propertys[i].type){
                                case UnityEngine.Rendering.ShaderPropertyType.Range:
                                {
                                    propertys[i].rangeLimits = shader.GetPropertyRangeLimits(i);
                                    propertys[i].floatValue = material.GetFloat(propertys[i].nameId);
                                }
                                break;
                                
                                case UnityEngine.Rendering.ShaderPropertyType.Texture:
                                {
                                    var texture = material.GetTexture(propertys[i].nameId);
                                    if(texture != null){
                                        propertys[i].textureValue = new TextureKun(texture);
                                    }
                                    propertys[i].textureDimension = shader.GetPropertyTextureDimension(i);
                                    propertys[i].scale = material.GetTextureScale(propertys[i].nameId);
                                    propertys[i].offset = material.GetTextureOffset(propertys[i].nameId);
                                    if(propertys[i].name == "_MainTex"){
                                        mainTexPrpoIdx = i;
                                    }
                                }
                                break;

                                case UnityEngine.Rendering.ShaderPropertyType.Float:
                                {
                                    propertys[i].floatValue = material.GetFloat(propertys[i].nameId);
                                }
                                break;

                                case UnityEngine.Rendering.ShaderPropertyType.Vector:
                                {
                                    propertys[i].vectorValue = material.GetVector(propertys[i].nameId);
                                }
                                break;

                                case UnityEngine.Rendering.ShaderPropertyType.Color:
                                {
                                    propertys[i].colorValue = material.GetColor(propertys[i].nameId);
                                }
                                break;
                            }

                        }
                    }
                    
                }
                shaderKeywords = material.shaderKeywords;
            }
        }
        
        // TODO : Materialのwrite back
        public override bool WriteBack(Object obj)
        {
            Debug.Log("Material.WriteBack("+dirty+")");
            var result = base.WriteBack(obj);
            var material = obj as Material;
            if(material){
                if(result){ 
                    material.enableInstancing = enableInstancing;
                    material.renderQueue = renderQueue;
                    material.shaderKeywords = shaderKeywords;
                    #if false
                    material.color = color;
                    material.doubleSidedGI = doubleSidedGI;                    
                    material.globalIlluminationFlags = globalIlluminationFlags;                                                        
                    #endif
                }
                if(shader.name != material.shader.name){
                    var tmp = Shader.Find(shader.name);
                    if(tmp != null){                
                        material.shader = tmp;
                    }
                }
                if(propertys != null){
                    foreach(var prop in propertys){
                        if(prop.dirty == false){
                            continue;
                        }
                        switch(prop.type){
                            case UnityEngine.Rendering.ShaderPropertyType.Range:
                            {
                                material.SetFloat(prop.nameId,prop.floatValue);
                            }
                            break;

                            case UnityEngine.Rendering.ShaderPropertyType.Color:
                            {
                                material.SetColor(prop.nameId,prop.colorValue);
                            }
                            break;
                            
                            case UnityEngine.Rendering.ShaderPropertyType.Texture:
                            {
                                //material.SetTexture(prop.nameId,prop.textureValue);
                            }
                            break;
                            
                            case UnityEngine.Rendering.ShaderPropertyType.Float:
                            {
                                material.SetFloat(prop.nameId,prop.floatValue);
                            }
                            break;

                            case UnityEngine.Rendering.ShaderPropertyType.Vector:
                            {
                                material.SetVector(prop.nameId,prop.vectorValue);
                            }
                            break;
                        }                    
                    }
                }

                
            }
            return result;
        }

        
    }
}
