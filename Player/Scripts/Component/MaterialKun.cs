using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class MaterialKun : ObjectKun
    {
        /// <summary>
        /// MaterialPropertyをRuntime/Editor両方で使用する為のClass
        /// </summary>
        [System.Serializable]
        public class Property : ISerializerKun
        {
#if UNITY_2019_1_OR_NEWER
            [SerializeField] UnityEngine.Rendering.ShaderPropertyFlags m_flags;
            [SerializeField] UnityEngine.Rendering.ShaderPropertyType m_type;
#endif
            [SerializeField] UnityEngine.Rendering.TextureDimension m_textureDimension;
            [SerializeField] bool m_dirty;
            [SerializeField] int m_nameId;
            [SerializeField] float m_floatValue;
            [SerializeField] string m_displayName;
            [SerializeField] string m_name;            
            [SerializeField] ColorKun m_colorValue;
            [SerializeField] TextureKun m_textureValue;
            [SerializeField] Vector2Kun m_rangeLimits;                        
            [SerializeField] Vector2Kun m_scale; // Tiling
            [SerializeField] Vector2Kun m_offset; // Offset
            [SerializeField] Vector4Kun m_vectorValue;


#if UNITY_2019_1_OR_NEWER
            public UnityEngine.Rendering.ShaderPropertyFlags flags
            {
                get { return m_flags; }
                set { m_flags = value; }
            }
            public UnityEngine.Rendering.ShaderPropertyType type
            {
                get { return m_type; }
                set { m_type = value; }
            }
#endif
            public bool dirty
            {
                get { return m_dirty; }
                set { m_dirty = value; }
            }
            public Color colorValue
            {
                get { return m_colorValue.GetColor(); }
                set { m_colorValue = new ColorKun(value); }
            }
            public string displayName
            {
                get { return m_displayName; }
                set { m_displayName = value; }
            }
            public float floatValue
            {
                get { return m_floatValue; }
                set { m_floatValue = value; }
            }
            public string name
            {
                get { return m_name; }
                set { m_name = value; }
            }
            public int nameId
            {
                get { return m_nameId; }
                set { m_nameId = value; }
            }
            public Vector2 rangeLimits
            {
                get { return m_rangeLimits.GetVector2(); }
                set { m_rangeLimits = new Vector2Kun(value); }
            }
            public UnityEngine.Rendering.TextureDimension textureDimension
            {
                get { return m_textureDimension; }
                set { m_textureDimension = value; }
            }
            public TextureKun textureValue
            {
                get { return m_textureValue; }
                set { m_textureValue = value; }
            }

            public Vector4 vectorValue
            {
                get { return m_vectorValue.GetVector4(); }
                set { m_vectorValue = new Vector4Kun(value); }
            }

            public Vector2 scale
            {
                get { return m_scale.GetVector2(); }
                set { m_scale = new Vector2Kun(value); }
            }

            public Vector2 offset
            {
                get { return m_offset.GetVector2(); }
                set { m_offset = new Vector2Kun(value); }
            }


            public Property()
            {
                dirty = false;
                m_colorValue = new ColorKun();
                m_textureValue = new TextureKun();
                m_vectorValue = new Vector4Kun();
                m_scale = new Vector2Kun();
                m_offset = new Vector2Kun();
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="binaryWriter"></param>
            public virtual void Serialize(BinaryWriter binaryWriter)
            {
#if UNITY_2019_1_OR_NEWER
                binaryWriter.Write((int)m_flags);
                binaryWriter.Write((int)m_type);
#endif
                binaryWriter.Write((int)m_textureDimension);
                binaryWriter.Write(m_dirty);
                binaryWriter.Write(m_nameId);
                binaryWriter.Write(m_floatValue);
                binaryWriter.Write(m_displayName);
                binaryWriter.Write(m_name);
                SerializerKun.Serialize<ColorKun>(binaryWriter, m_colorValue);
                SerializerKun.Serialize<TextureKun>(binaryWriter, m_textureValue);
                SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_rangeLimits);
                SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_scale);
                SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_offset);
                SerializerKun.Serialize<Vector4Kun>(binaryWriter, m_vectorValue);
            }


            public virtual void Deserialize(BinaryReader binaryReader)
            {
#if UNITY_2019_1_OR_NEWER
                m_flags = (UnityEngine.Rendering.ShaderPropertyFlags)binaryReader.ReadInt32();
                m_type = (UnityEngine.Rendering.ShaderPropertyType)binaryReader.ReadInt32();
#endif
                m_textureDimension = (UnityEngine.Rendering.TextureDimension)binaryReader.ReadInt32();
                m_dirty = binaryReader.ReadBoolean();
                m_nameId = binaryReader.ReadInt32();
                m_floatValue = binaryReader.ReadSingle();
                m_displayName = binaryReader.ReadString();
                m_name = binaryReader.ReadString();
                m_colorValue = SerializerKun.DesirializeObject<ColorKun>(binaryReader);
                m_textureValue = SerializerKun.DesirializeObject<TextureKun>(binaryReader);
                m_rangeLimits = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
                m_scale = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
                m_offset = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
                m_vectorValue = SerializerKun.DesirializeObject<Vector4Kun>(binaryReader);
            }


            public override bool Equals(object obj)
            {
                var other = obj as Property;
                if(other == null)
                {
                    return false;
                }
#if UNITY_2019_1_OR_NEWER
                if(flags.Equals(other.flags) == false)
                {
                    return false;
                }
                if(type.Equals(other.type) == false)
                {
                    return false;
                }
#endif
                if (dirty.Equals(other.dirty) == false)
                {
                    return false;
                }

                if (!ColorKun.Equals(m_colorValue,other.m_colorValue))                
                {
                    return false;
                }

                if(!string.Equals(displayName,other.displayName))
                {
                    return false;
                }

                if(floatValue.Equals(other.floatValue) == false)
                {
                    return false;
                }

                if(!string.Equals(name,other.name))                
                {
                    return false;
                }
                if (nameId.Equals(other.nameId) == false)
                {
                    return false;
                }

                if (!Vector2Kun.Equals(m_rangeLimits, other.m_rangeLimits)){                
                    return false;
                }

                if (textureDimension.Equals(other.textureDimension) == false)
                {
                    return false;
                }

                if(!TextureKun.Equals(m_textureValue,other.m_textureValue))
                {
                    return false;
                }

                if(!Vector4Kun.Equals(m_vectorValue,other.m_vectorValue))                
                {
                    return false;
                }

                if(!Vector2Kun.Equals(m_scale,other.m_scale))                
                {
                    return false;
                }

                if(!Vector2Kun.Equals(m_offset,other.m_offset))                
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

        
        [SerializeField] bool m_doubleSidedGI;
        [SerializeField] bool m_enableInstancing;
        [SerializeField] int m_mainTexPropIdx = -1;
        [SerializeField] int m_passCount;
        [SerializeField] int m_renderQueue;
        [SerializeField] MaterialGlobalIlluminationFlags m_globalIlluminationFlags;
        [SerializeField] ColorKun m_color;
        [SerializeField] ShaderKun m_shader;
        [SerializeField] string[] m_shaderKeywords;
        [SerializeField] Property[] m_propertys;


        public Color color
        {
            get { return m_color.GetColor(); }
            set { m_color = new ColorKun(value); }
        }


        public bool doubleSidedGI
        {
            get { return m_doubleSidedGI; }
            set { m_doubleSidedGI = value; }
        }

        
        public bool enableInstancing
        {
            get { return m_enableInstancing; }
            set { m_enableInstancing = value; }
        }


        public MaterialGlobalIlluminationFlags globalIlluminationFlags
        {
            get { return m_globalIlluminationFlags; }
            set { m_globalIlluminationFlags = value; }
        }

        
        int mainTexPrpoIdx
        {
            get { return m_mainTexPropIdx; }
            set { m_mainTexPropIdx = value; }
        }


        public TextureKun mainTexture
        {
            get { if (mainTexPrpoIdx != -1) { return propertys[mainTexPrpoIdx].textureValue; } return null; }
            private set { if (mainTexPrpoIdx != -1) { propertys[mainTexPrpoIdx].textureValue = value; } }
        }

        public Vector2 mainTextureOffset
        {
            get { if (mainTexPrpoIdx != -1) { return propertys[mainTexPrpoIdx].offset; } return Vector2.zero; }
            set { if (mainTexPrpoIdx != -1) { propertys[mainTexPrpoIdx].offset = value; } }
        }

        public Vector2 mainTextureScale
        {
            get { if (mainTexPrpoIdx != -1) { return propertys[mainTexPrpoIdx].scale; } return Vector2.zero; }
            set { if (mainTexPrpoIdx != -1) { propertys[mainTexPrpoIdx].scale = value; } }
        }

        
        public int passCount
        {
            get { return m_passCount; }
            private set { m_passCount = value; }
        }

        
        public int renderQueue
        {
            get { return m_renderQueue; }
            private set { m_renderQueue = value; }
        }

        
        public ShaderKun shader
        {
            get { return m_shader; }
            set { m_shader = value; }
        }

        
        public string[] shaderKeywords
        {
            get { return m_shaderKeywords; }
            set { m_shaderKeywords = value; }
        }

        
        public Property[] propertys
        {
            get { return m_propertys; }
            set { m_propertys = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public MaterialKun() :this(null) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public MaterialKun(UnityEngine.Object obj) : base(obj)
        {
            var material = obj as Material;
            if (material)
            {
                name = material.name;
                color = material.color;
                doubleSidedGI = material.doubleSidedGI;
                enableInstancing = material.enableInstancing;
                globalIlluminationFlags = material.globalIlluminationFlags;
                passCount = material.passCount;
                renderQueue = material.renderQueue;
                shaderKeywords = material.shaderKeywords;
                if (material.shader != null)
                {
                    shader = new ShaderKun(material.shader);
#if UNITY_2019_1_OR_NEWER
                    if (shader.GetPropertyCount() > 0)
                    {
                        propertys = new Property[shader.GetPropertyCount()];
                        for (var i = 0; i < shader.GetPropertyCount(); i++)
                        {
                            propertys[i] = new Property();
                            propertys[i].flags = shader.GetPropertyFlags(i);
                            propertys[i].type = shader.GetPropertyType(i);
                            propertys[i].name = shader.GetPropertyName(i);
                            propertys[i].nameId = shader.GetPropertyNameId(i);
                            propertys[i].displayName = shader.GetPropertyDescription(i);
                            switch (propertys[i].type)
                            {
                                case UnityEngine.Rendering.ShaderPropertyType.Range:
                                    {
                                        propertys[i].rangeLimits = shader.GetPropertyRangeLimits(i);
                                        propertys[i].floatValue = material.GetFloat(propertys[i].nameId);
                                    }
                                    break;

                                case UnityEngine.Rendering.ShaderPropertyType.Texture:
                                    {
                                        var texture = material.GetTexture(propertys[i].nameId);
                                        if (texture != null)
                                        {
                                            propertys[i].textureValue = new TextureKun(texture);
                                        }
                                        propertys[i].textureDimension = shader.GetPropertyTextureDimension(i);
                                        propertys[i].scale = material.GetTextureScale(propertys[i].nameId);
                                        propertys[i].offset = material.GetTextureOffset(propertys[i].nameId);
                                        if (propertys[i].name == "_MainTex")
                                        {
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
#endif
                }
                shaderKeywords = material.shaderKeywords;
            }
        }

        // TODO : Materialのwrite back
        public override bool WriteBack(Object obj)
        {
            var result = base.WriteBack(obj);
            var material = obj as Material;
            if (material)
            {
                if (result)
                {
                    material.enableInstancing = enableInstancing;
                    material.renderQueue = renderQueue;
                    material.shaderKeywords = shaderKeywords;
#if false
                    material.color = color;
                    material.doubleSidedGI = doubleSidedGI;
                    material.globalIlluminationFlags = globalIlluminationFlags;
#endif
                }
                if (shader.name != material.shader.name)
                {
                    var tmp = Shader.Find(shader.name);
                    if (tmp != null)
                    {
                        material.shader = tmp;
                    }
                }
#if UNITY_2019_1_OR_NEWER
                if (propertys != null)
                {
                    foreach (var prop in propertys)
                    {
                        if (prop.dirty == false)
                        {
                            continue;
                        }
                        switch (prop.type)
                        {
                            case UnityEngine.Rendering.ShaderPropertyType.Range:
                                {
                                    material.SetFloat(prop.nameId, prop.floatValue);
                                }
                                break;

                            case UnityEngine.Rendering.ShaderPropertyType.Color:
                                {
                                    material.SetColor(prop.nameId, prop.colorValue);
                                }
                                break;

                            case UnityEngine.Rendering.ShaderPropertyType.Texture:
                                {
                                    var id = prop.textureValue.GetInstanceID();
                                    Texture texture;
                                    TexturePlayer.textureDict.TryGetValue(id, out texture);
                                    if (texture != null)
                                    {
                                        //Debug.Log(texture.name);
                                        material.SetTexture(prop.nameId, texture);
                                    }
                                }
                                break;

                            case UnityEngine.Rendering.ShaderPropertyType.Float:
                                {
                                    material.SetFloat(prop.nameId, prop.floatValue);
                                }
                                break;

                            case UnityEngine.Rendering.ShaderPropertyType.Vector:
                                {
                                    material.SetVector(prop.nameId, prop.vectorValue);
                                }
                                break;
                        }
                    }
                }
#endif

            }
            return result;
        }



        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_doubleSidedGI);
            binaryWriter.Write(m_enableInstancing);
            binaryWriter.Write(m_mainTexPropIdx);
            binaryWriter.Write(m_passCount);
            binaryWriter.Write(m_renderQueue);
            binaryWriter.Write((int)m_globalIlluminationFlags);
            SerializerKun.Serialize<ColorKun>(binaryWriter, m_color);
            SerializerKun.Serialize<ShaderKun>(binaryWriter, m_shader);
            SerializerKun.Serialize(binaryWriter, m_shaderKeywords);
            SerializerKun.Serialize<Property>(binaryWriter, m_propertys);                        
        }

        
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_doubleSidedGI = binaryReader.ReadBoolean();
            m_enableInstancing = binaryReader.ReadBoolean();
            m_mainTexPropIdx = binaryReader.ReadInt32();
            m_passCount = binaryReader.ReadInt32();
            m_renderQueue = binaryReader.ReadInt32();
            m_globalIlluminationFlags = (MaterialGlobalIlluminationFlags)binaryReader.ReadInt32();
            m_color = SerializerKun.DesirializeObject<ColorKun>(binaryReader);
            m_shader = SerializerKun.DesirializeObject<ShaderKun>(binaryReader);
            m_shaderKeywords = SerializerKun.DesirializeStrings(binaryReader);
            m_propertys = SerializerKun.DesirializeObjects<Property>(binaryReader);            
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as MaterialKun;
            if(other == null)
            {
                return false;
            }
            if (m_color != null && !m_color.Equals(other.m_color))
            {
                return false;
            }
            if (!m_doubleSidedGI.Equals(other.m_doubleSidedGI))
            {
                return false;
            }
            if (!m_enableInstancing.Equals(other.m_enableInstancing))
            {
                return false;
            }
            if (!m_globalIlluminationFlags.Equals(other.m_globalIlluminationFlags))
            {
                return false;
            }

            if (!m_mainTexPropIdx.Equals(other.m_mainTexPropIdx))
            {
                return false;
            }

            if (!m_passCount.Equals(other.m_passCount))
            {
                return false;
            }

            if (!m_renderQueue.Equals(other.m_renderQueue))
            {
                return false;
            }
            
            if(m_shader != null && !m_shader.Equals(other.m_shader))
            {
                return false;
            }

            if(m_shaderKeywords != null)
            {
                if(m_shaderKeywords.Length != other.m_shaderKeywords.Length)
                {
                    return false;
                }
                for(var i = 0; i < m_shaderKeywords.Length; i++)
                {
                    if (!m_shaderKeywords[i].Equals(other.m_shaderKeywords[i]))
                    {
                        return false;
                    }
                }
            }

            if(m_propertys != null)
            {
                if(m_propertys.Length != other.m_propertys.Length)
                {
                    return false;
                }
                for(var i = 0; i < m_propertys.Length; i++)
                {
                    if (!m_propertys[i].Equals(other.m_propertys[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        public override int GetHashCode()
        {
            
            return base.GetHashCode();
        }
    }
}
