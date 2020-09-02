using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    
    
    [System.Serializable]
    public class ShaderKun : ObjectKun
    {
        // [Note]
        // Unity2019以前はShaderのPropertyにはUnityEditor.ShaderUtilからのみアクセス可能であった為、
        // Runtimeではアクセス出来ませんでした。
        #if UNITY_2019_1_OR_NEWER
        [System.Serializable]
        public class Property {            
            [SerializeField] UnityEngine.Rendering.ShaderPropertyFlags m_flags;                        
            [SerializeField] string[] m_attributes;
            [SerializeField] string m_description;            
            [SerializeField] string m_name;
            [SerializeField] int m_nameId;
            [SerializeField] Vector2 m_rangeLimits;
            [SerializeField]string m_textureDefaultName;
            [SerializeField]UnityEngine.Rendering.TextureDimension m_textureDimension;
            [SerializeField]UnityEngine.Rendering.ShaderPropertyType m_type;
            [SerializeField] float m_defaultFloatValue;
            [SerializeField] Vector4 m_defaultVectorValue;
                        
            public UnityEngine.Rendering.ShaderPropertyFlags flags{
                get {return m_flags;}
                set {m_flags = value;}
            }            
            public string[] attributes{
                get{return m_attributes;}
                set{m_attributes = value;}
            }
            public string description{
                get{return m_description;}
                set{m_description = value;}
            }                        
            public  string name {
                get{return m_name;}
                set{m_name = value;}
            }
            public int nameId{
                get{return m_nameId;}
                set{m_nameId = value;}
            }
            public Vector2 rangeLimits{
                get{return m_rangeLimits;}
                set{m_rangeLimits = value;}
            }
            public string textureDefaultName{
                get{return m_textureDefaultName;}
                set{m_textureDefaultName = value;}
            }
            public UnityEngine.Rendering.TextureDimension textureDimension{
                get{return m_textureDimension;}
                set{m_textureDimension = value;}
            }
            public UnityEngine.Rendering.ShaderPropertyType type{
                get{return m_type;}
                set{m_type = value;}
            }
            public float defaultFloatValue {
                get{return m_defaultFloatValue;}
                set{m_defaultFloatValue = value;}
            }
            public Vector4 defaultVectorValue {
                get{return m_defaultVectorValue;}
                set{m_defaultVectorValue = value;}
            }
        }

        [SerializeField] Property[] m_propertys;
        Property[] propertys {
            get{return m_propertys;}
            set{m_propertys = value;}
        }

        [SerializeField] int m_passCount ;
        public int passCount {
            get{return m_passCount;}
            private set{m_passCount = value;}
        }
        #endif

        [SerializeField] bool m_isSupported ;
        public bool isSupported {
            get {return m_isSupported;}
            private set {m_isSupported = value;}
        }

        [SerializeField] int m_maximumLOD ;
        public int maximumLOD {
            get{return m_maximumLOD;}
            set{m_maximumLOD = value;}
        }
        

        [SerializeField] int m_renderQueue ;
        public int renderQueue {
            get{return m_renderQueue;}
            private set {m_renderQueue = value;}
        }
        

        public ShaderKun():this(null){}
        public ShaderKun(UnityEngine.Object obj):base(obj)
        {
            // TODO : プロパティの表示
            var shader = obj as Shader;
            if(shader){
                name = shader.name;
                isSupported = shader.isSupported;
                maximumLOD = shader.maximumLOD;                
                renderQueue = shader.renderQueue;
                
                #if UNITY_2019_1_OR_NEWER
                passCount = shader.passCount;
                var propertyCount = shader.GetPropertyCount();
                if(propertyCount > 0){
                    propertys = new Property[propertyCount];                    
                    for(var i = 0; i < propertyCount; i++){
                        propertys[i] = new Property();             
                        propertys[i].flags = shader.GetPropertyFlags(i);             
                        propertys[i].type = shader.GetPropertyType(i);
                        propertys[i].attributes = shader.GetPropertyAttributes(i);
                        propertys[i].description = shader.GetPropertyDescription(i);                                                
                        propertys[i].name = shader.GetPropertyName(i);
                        propertys[i].nameId = shader.GetPropertyNameId(i);
                        switch(propertys[i].type){
                            case UnityEngine.Rendering.ShaderPropertyType.Range:
                            {
                                propertys[i].rangeLimits = shader.GetPropertyRangeLimits(i);
                            }
                            break;
                            case UnityEngine.Rendering.ShaderPropertyType.Texture:
                            {
                                propertys[i].textureDefaultName = shader.GetPropertyTextureDefaultName(i);
                                propertys[i].textureDimension = shader.GetPropertyTextureDimension(i);
                            }
                            break;

                            case UnityEngine.Rendering.ShaderPropertyType.Float:
                            {
                                propertys[i].defaultFloatValue = shader.GetPropertyDefaultFloatValue(i);
                            }
                            break;

                            case UnityEngine.Rendering.ShaderPropertyType.Vector:
                            {
                                propertys[i].defaultVectorValue = shader.GetPropertyDefaultVectorValue(i);
                            }
                            break;

                            case UnityEngine.Rendering.ShaderPropertyType.Color:
                            {
                                //
                            }
                            break;
                        }
                    }
                }
                #endif
            }
        }

        #if UNITY_2019_1_OR_NEWER
        public int GetPropertyCount (){
            if(propertys == null){
                return 0;
            }
            return propertys.Length;
        }

        public UnityEngine.Rendering.ShaderPropertyFlags GetPropertyFlags (int propertyIndex)
        {
            return propertys[propertyIndex].flags;
        }           

        public string[] GetPropertyAttributes (int propertyIndex)
        {
            return propertys[propertyIndex].attributes;
        }
        
        public string GetPropertyDescription (int propertyIndex)
        {
            return propertys[propertyIndex].description;
        }

        public string GetPropertyName (int propertyIndex)
        {
            return propertys[propertyIndex].name;
        }

        public int GetPropertyNameId (int propertyIndex)
        {
            return propertys[propertyIndex].nameId;            
        }

        public Vector2 GetPropertyRangeLimits (int propertyIndex)
        {
            return propertys[propertyIndex].rangeLimits;
        }

        public string GetPropertyTextureDefaultName (int propertyIndex)
        {
            return propertys[propertyIndex].textureDefaultName;
        }

        public UnityEngine.Rendering.TextureDimension GetPropertyTextureDimension (int propertyIndex)
        {
            return propertys[propertyIndex].textureDimension;
        }

        public UnityEngine.Rendering.ShaderPropertyType GetPropertyType (int propertyIndex)
        {
            return propertys[propertyIndex].type;
        }

        public float GetPropertyDefaultFloatValue (int propertyIndex)
        {
            return propertys[propertyIndex].defaultFloatValue;
        }

        public Vector4 GetPropertyDefaultVectorValue (int propertyIndex)
        {
            return propertys[propertyIndex].defaultVectorValue;
        }
        #endif
    }
}