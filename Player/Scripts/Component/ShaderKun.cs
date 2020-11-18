using System.IO;
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
        public class Property : ISerializerKun        
        {            
            [SerializeField] UnityEngine.Rendering.ShaderPropertyFlags m_flags;
            [SerializeField] UnityEngine.Rendering.TextureDimension m_textureDimension;
            [SerializeField] UnityEngine.Rendering.ShaderPropertyType m_type;            
            [SerializeField] string m_description;            
            [SerializeField] string m_name;
            [SerializeField] string m_textureDefaultName;            
            [SerializeField] int m_nameId;                        
            [SerializeField] float m_defaultFloatValue;
            [SerializeField] string[] m_attributes;
            [SerializeField] Vector2Kun m_rangeLimits;
            [SerializeField] Vector4Kun m_defaultVectorValue;

                        
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
                get{return m_rangeLimits.GetVector2();}
                set{m_rangeLimits = new Vector2Kun(value);}
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
                get{return m_defaultVectorValue.GetVector4();}
                set{m_defaultVectorValue = new Vector4Kun(value);}
            }


            public Property()
            {
                m_description = "";
                m_name = "";
                m_rangeLimits = new Vector2Kun();
                m_textureDefaultName = "";
                m_defaultVectorValue = new Vector4Kun();
            }


            public void Serialize(BinaryWriter binaryWriter)
            {
                binaryWriter.Write((uint)m_flags);
                binaryWriter.Write((int)m_textureDimension);
                binaryWriter.Write((int)m_type);
                binaryWriter.Write(m_description);
                binaryWriter.Write(m_name);
                binaryWriter.Write(m_textureDefaultName);
                binaryWriter.Write(m_nameId);
                binaryWriter.Write(m_defaultFloatValue);
                SerializerKun.Serialize(binaryWriter, m_attributes);
                SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_rangeLimits);
                SerializerKun.Serialize<Vector4Kun>(binaryWriter, m_defaultVectorValue);                
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="binaryReader"></param>
            public virtual void Deserialize(BinaryReader binaryReader)
            {
                m_flags = (UnityEngine.Rendering.ShaderPropertyFlags)binaryReader.ReadUInt32();
                m_textureDimension = (UnityEngine.Rendering.TextureDimension)binaryReader.ReadInt32();
                m_type = (UnityEngine.Rendering.ShaderPropertyType)binaryReader.ReadInt32();
                m_description = binaryReader.ReadString();
                m_name = binaryReader.ReadString();
                m_textureDefaultName = binaryReader.ReadString();
                m_nameId = binaryReader.ReadInt32();
                m_defaultFloatValue = binaryReader.ReadSingle();

                m_attributes = SerializerKun.DesirializeStrings(binaryReader);
                m_rangeLimits = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
                m_defaultVectorValue = SerializerKun.DesirializeObject<Vector4Kun>(binaryReader);                
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                var other = obj as Property;
                if(other == null)
                {
                    return false;
                }
                if (flags.Equals(other.flags) == false)
                {
                    return false;
                }
                if(attributes == null && other.attributes != null)
                {
                    return false;
                }
                if (attributes != null && other.attributes == null)
                {
                    return false;
                }
                if(attributes != null)
                {
                    if(attributes.Length != other.attributes.Length)
                    {
                        return false;
                    }
                    for(var i = 0; i < attributes.Length; i++)
                    {
                        if(attributes[i].Equals(other.attributes[i])== false)
                        {
                            return false;
                        }
                    }
                }
                if(description != other.description)
                {
                    return false;
                }
                if (!string.Equals(name, other.name)) {
                    return false;
                }
                if(nameId != other.nameId)
                {
                    return false;
                }
                if (rangeLimits.Equals(other.rangeLimits) == false)
                {
                    return false;
                }
                if(!string.Equals(textureDefaultName, other.textureDefaultName)) {                
                    return false;
                }
                if (type.Equals(other.type) == false)
                {
                    return false;
                }
                if (defaultFloatValue.Equals(other.defaultFloatValue) == false)
                {
                    return false;
                }
                if (defaultVectorValue.Equals(other.defaultVectorValue) == false)
                {
                    return false;
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
        }
#endif


#if UNITY_2019_1_OR_NEWER
        [SerializeField] Property[] m_propertys;
        [SerializeField] int        m_passCount;
#endif
        [SerializeField] bool       m_isSupported ;
        [SerializeField] int        m_maximumLOD;
        [SerializeField] int        m_renderQueue;



#if UNITY_2019_1_OR_NEWER
        Property[] propertys
        {
            get { return m_propertys; }
            set { m_propertys = value; }
        }


        public int passCount
        {
            get { return m_passCount; }
            private set { m_passCount = value; }
        }

#endif

        public bool isSupported {
            get {return m_isSupported;}
            private set {m_isSupported = value;}
        }

        
        public int maximumLOD {
            get{return m_maximumLOD;}
            set{m_maximumLOD = value;}
        }
        

        
        public int renderQueue {
            get{return m_renderQueue;}
            private set {m_renderQueue = value;}
        }
        

        /// <summary>
        /// 
        /// </summary>
        public ShaderKun():this(null){}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public ShaderKun(UnityEngine.Object obj):base(obj)
        {
            name = "";            
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


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
#if UNITY_2019_1_OR_NEWER
            if(m_propertys == null)
            {
                binaryWriter.Write(-1);
            } else
            {
                binaryWriter.Write(m_propertys.Length);
                for(var i = 0; i < m_propertys.Length; i++)
                {
                    m_propertys[i].Serialize(binaryWriter);
                }
            }
            binaryWriter.Write(m_passCount);
#endif
            binaryWriter.Write(m_isSupported);
            binaryWriter.Write(m_maximumLOD);
            binaryWriter.Write(m_renderQueue);
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
#if UNITY_2019_1_OR_NEWER

            var len = binaryReader.ReadInt32();
            if(len != -1)
            {
                m_propertys = new Property[len];
                for(var i = 0; i < len; i++)
                {
                    m_propertys[i] = new Property();
                    m_propertys[i].Deserialize(binaryReader);
                }
            }
            m_passCount = binaryReader.ReadInt32();
#endif
            m_isSupported = binaryReader.ReadBoolean();
            m_maximumLOD = binaryReader.ReadInt32();
            m_renderQueue = binaryReader.ReadInt32();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as ShaderKun;
            if(other == null)
            {
                return false;
            }
#if UNITY_2019_1_OR_NEWER
            if(propertys == null && other.propertys != null)
            {
                return false;
            }
            if (propertys != null && other.propertys == null)
            {
                return false;
            }
            if(propertys != null)
            {
                if(propertys.Length != other.propertys.Length)
                {
                    return false;
                }
                for(var i = 0; i < propertys.Length; i++)
                {
                    if (propertys[i].Equals(other.propertys[i]) == false)
                    {
                        return false;
                    }
                }
            }
#endif
            if (isSupported.Equals(other.isSupported) == false)
            {
                return false;
            }
            if (maximumLOD.Equals(other.maximumLOD) == false)
            {
                return false;
            }
            if (renderQueue.Equals(other.renderQueue) == false)
            {
                return false;
            }
            return base.Equals(obj);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}