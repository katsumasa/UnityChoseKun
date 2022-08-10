using System.IO;
using System.Collections.Generic;
using UnityEngine;


namespace Utj.UnityChoseKun.Engine
{
    
    /// <summary>
    /// 
    /// </summary>
    public static class TexturePlayer
    {
        /// <summary>
        /// TextureKunPacket
        /// </summary>
        [System.Serializable]
        public class TextureKunPacket : ISerializerKun
        {
            [SerializeField] bool m_isResources;
            [SerializeField] bool m_isScene;
            [SerializeField] TextureKun[] m_textureKuns;
            
            public bool isResources{
                get{return m_isResources;}
                set{m_isResources = value;}
            }
            public bool isScene{
                get{return m_isScene;}
                set{m_isScene = value;}
            }
            public TextureKun[] textureKuns{
                get{return m_textureKuns;}
                set{m_textureKuns = value;}
            }


            /// <summary>
            /// 
            /// </summary>
            public TextureKunPacket():this(null){}
            
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="textureKuns"></param>
            public TextureKunPacket(TextureKun[] textureKuns)
            {
                this.textureKuns = textureKuns;
            }


            /// <summary>
            /// Serialize
            /// </summary>
            /// <param name="binaryWriter">BinaryWriter</param>
            public virtual void Serialize(BinaryWriter binaryWriter)
            {
                binaryWriter.Write(m_isResources);
                binaryWriter.Write(m_isScene);
                if(m_textureKuns == null)
                {
                    binaryWriter.Write(-1);
                }
                else
                {
                    binaryWriter.Write(m_textureKuns.Length);
                    for(var i = 0; i < m_textureKuns.Length; i++)
                    {
                        m_textureKuns[i].Serialize(binaryWriter);
                    }
                }
            }


            /// <summary>
            /// Deserialize
            /// </summary>
            /// <param name="binaryReader">BinaryReader</param>
            public virtual void Deserialize(BinaryReader binaryReader)
            {
                m_isResources = binaryReader.ReadBoolean();
                m_isScene = binaryReader.ReadBoolean();
                var len = binaryReader.ReadInt32();
                if(len != -1)
                {
                    m_textureKuns = new TextureKun[len];
                    for(var i = 0; i < len; i++)
                    {
                        m_textureKuns[i] = new TextureKun();
                        m_textureKuns[i].Deserialize(binaryReader);
                    }
                }
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                var other = obj as TextureKunPacket;
                if(other == null)
                {
                    return false;
                }
                if (!m_isResources.Equals(other.m_isResources))
                {
                    return false;
                }
                if (!m_isScene.Equals(other.m_isScene))
                {
                    return false;
                }
                if (m_textureKuns != null)
                {
                    if(other.m_textureKuns == null)
                    {
                        return false;
                    }
                    if(m_textureKuns.Length != other.m_textureKuns.Length)
                    {
                        return false;
                    }
                    for(var i = 0; i < m_textureKuns.Length; i++)
                    {
                        if (!TextureKun.Equals(m_textureKuns[i], other.m_textureKuns[i]))
                        {
                            return false;
                        }
                    }
                } 
                else if(other.m_textureKuns != null)
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
        

        static Dictionary<int,Texture> m_textureDict;


        public static Dictionary<int,Texture> textureDict
        {
            get{if(m_textureDict == null){m_textureDict = new Dictionary<int, Texture>();}return m_textureDict;}
            private set{m_textureDict = value;}
        }
        

        /// <summary>
        /// 
        /// </summary>
        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public static void OnMessageEventPull(BinaryReader binaryReader){

            var textureKunPacket = new TextureKunPacket();
            textureKunPacket.Deserialize(binaryReader);
            
            if(textureKunPacket.isScene){
                GetAllTextureInScene();
            }
            if(textureKunPacket.isResources){
                GetAllTextureInResources();
            }
            var textureKuns = new TextureKun[textureDict.Count];
            var i = 0;
            foreach(var texture in textureDict.Values){
                textureKuns[i++] = new TextureKun(texture);                
            }
            
            textureKunPacket = new TextureKunPacket(textureKuns);
            UnityChoseKunPlayer.SendMessage<TextureKunPacket>(UnityChoseKun.MessageID.TexturePull,textureKunPacket);
        }


        /// <summary>
        /// 
        /// </summary>
        public static void GetAllTextureInResources()
        {
            var textures = Resources.FindObjectsOfTypeAll(typeof(Texture)) as Texture[];
            foreach(var texture in textures)
            {
                if(textureDict.ContainsKey(texture.GetInstanceID()) == false){
                    textureDict.Add(texture.GetInstanceID(),texture);
                }
            }
        }
        

        /// <summary>
        /// 
        /// </summary>
        public static void GetAllTextureInScene()
        {            
            #if UNITY_2019_1_OR_NEWER
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (scene != null)
            {
                var components = new List<Renderer>();
                foreach (var go in scene.GetRootGameObjects())
                {
                    GetSComponentsInChildren<Renderer>(go, components);
                }
                foreach (var renderer in components)
                {
                    if (renderer.materials != null)
                    {
                        for (var i = 0; i < renderer.materials.Length; i++)
                        {
                            var material = renderer.materials[i];
                            if (material != null)
                            {
                                var shader = material.shader;
                                if (shader != null)
                                {
                                    var cnt = shader.GetPropertyCount();
                                    //Debug.Log(shader.name + " cnt " + cnt);
                                    for (var j = 0; j < cnt; j++)
                                    {
                                        var type = shader.GetPropertyType(j);
                                        //Debug.Log(type);
                                        if (type != UnityEngine.Rendering.ShaderPropertyType.Texture)
                                        {
                                            continue;
                                        }
                                        var nameId = shader.GetPropertyNameId(j);
                                        var texture = material.GetTexture(nameId);
                                        //Debug.Log("nameId:" + nameId);

                                        if (texture != null)
                                        {
                                            if (textureDict.ContainsKey(texture.GetInstanceID()))
                                            {
                                                continue;
                                            }
                                            textureDict.Add(texture.GetInstanceID(), texture);
                                        } else
                                        {
                                            //Debug.Log("texture == null");
                                        }
                                    }
                                }
                                else
                                {
                                    UnityChoseKun.LogWarning("shader == null");
                                }
                            } else
                            {
                                UnityChoseKun.LogWarning("material == null");
                            }
                        }
                    }
                }
            } else
            {
                UnityChoseKun.LogWarning("scene == null");
            }
            #endif
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="components"></param>
        static void GetSComponentsInChildren<T>(GameObject go,List<T> components)
        {
            if (go != null)
            {
                go.GetComponents<T>(components);
                for (var i = 0; i < go.transform.childCount; i++)
                {
                    var child = go.transform.GetChild(i).gameObject;
                    GetSComponentsInChildren<T>(child, components);
                }
            }
            else
            {
                Debug.LogWarning("go == null");
            }
        }
    }
}
