using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;
using System.Collections.Generic;


namespace Utj.UnityChoseKun.Engine
{
    /// <summary>
    /// GameObjectをSerialize/Deserializeする為のClass
    /// </summary>
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
    public class GameObjectKun : ISerializerKun
    {

        [SerializeField] bool m_activeSelf;
        [SerializeField] bool m_isStatic;
        [SerializeField] int m_layer;
        [SerializeField] string m_tag;
        [SerializeField] int m_instanceID;
        [SerializeField] string m_name;
        [SerializeField] ComponentKun.ComponentKunType[] m_componentKunTypes;
        [SerializeField] ComponentKun[] m_componentKuns;


        public bool activeSelf{
            get{return m_activeSelf;}
            set{m_activeSelf = value;}
        }

        public bool isStatic {
            get {return m_isStatic;}
            set {m_isStatic = value;}
        }

        public int layer{
            get {return m_layer;}
            set {m_layer = value;}
        }

        public string tag{
            get {return m_tag;}
            set {m_tag = value;}
        }

        public int instanceID{
            get {return m_instanceID;}
            private set {m_instanceID = value;}
        }
        
        public string name {
            get {return m_name;}
            set {m_name = value;}
        }

        public ComponentKun.ComponentKunType [] componentKunTypes{
            get{return m_componentKunTypes;}
            set{m_componentKunTypes = value;}
        }
                       
        public ComponentKun[] componentKuns
        {
            get 
            { 
                return m_componentKuns; 
            }
            set { m_componentKuns = value; }
        }

        private TransformKun m_transformKun;
        public TransformKun transformKun {
            get {
                return componentKuns[0] as TransformKun;
            }
        }
        
        [SerializeField] bool m_dirty;
        public bool dirty {
            get{return m_dirty;}
            set{m_dirty = value;}
        }


        /// <summary>
        /// 
        /// </summary>
        public GameObjectKun():this(null){}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="go"></param>
        public GameObjectKun(GameObject go){
            if(go == null){
                return;
            }
            activeSelf = go.activeSelf;
            isStatic = go.isStatic;
            layer = go.layer;
            tag = go.tag;
            instanceID = go.GetInstanceID();
            name = go.name;                                
                                    
            var components = go.GetComponents(typeof(Component));
            componentKuns = new ComponentKun[components.Length];
            componentKunTypes = new ComponentKun.ComponentKunType[components.Length];
            var i = 0;
            foreach (var component in components){                
                componentKunTypes[i] = ComponentKun.GetComponentKunType(component);                
                componentKuns[i] = ComponentKun.Instantiate(componentKunTypes[i], component);
                componentKuns[i].gameObjectKun = this;
                i++;                
            }                        
            dirty = false;
        }


        /// <summary>
        /// 
        /// </summary>
        public void ResetDirty()
        {
            dirty = false;
            for (var i = 0; i < componentKuns.Length; i++)
            {
                componentKuns[i].dirty = false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        public void WriteBack(GameObject gameObject)
        {
            if(dirty){
                gameObject.SetActive(activeSelf);
                gameObject.isStatic = isStatic;
                gameObject.layer = layer;
                gameObject.tag = tag;
                gameObject.name = name;
            }

            // instanceIDが一致するComponetを探し出してWriteBackを実行する。
            // ※ComponentKun側がDirtyであるかいなかはGameObjectKunではなく各ComponentKun側に依存する
            var components = gameObject.GetComponents<Component>();
            for (var i = 0; i < componentKunTypes.Length; i++){                        
                var componentKunType = componentKunTypes[i];
                if(componentKunType == ComponentKun.ComponentKunType.MissingMono)
                {
                    continue;
                }                
                Component component = null;                
                for(var j = 0; j < components.Length; j++)
                {                    
                    if (components[j] != null && components[j].GetInstanceID() == componentKuns[i].instanceID)
                    {
                        component = components[j];
                        break;
                    }
                }

                if(component == null){                
                    UnityChoseKun.LogWarning("component == null");
                    continue;
                }                               
                componentKuns[i].WriteBack(component);
                componentKuns[i].dirty = false;
            }
            
            dirty = false;
        }


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_dirty);
            binaryWriter.Write(m_activeSelf);
            binaryWriter.Write(m_isStatic);
            binaryWriter.Write(m_layer);
            binaryWriter.Write(m_tag);
            binaryWriter.Write(m_instanceID);
            binaryWriter.Write(m_name);



            if (m_componentKunTypes == null)
            {
                binaryWriter.Write((int)-1);
            }
            else
            {
                binaryWriter.Write(m_componentKunTypes.Length);
                for(var i = 0; i < m_componentKunTypes.Length; i++)
                {
                    binaryWriter.Write((int)m_componentKunTypes[i]);
                }
            }

            if(m_componentKuns == null)
            {
                binaryWriter.Write((int)-1);
            } 
            else
            {
                binaryWriter.Write(m_componentKuns.Length);
                for(var i = 0; i < m_componentKuns.Length; i++)
                {                    
                    m_componentKuns[i].Serialize(binaryWriter);
                }
            }
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_dirty = binaryReader.ReadBoolean();
            m_activeSelf = binaryReader.ReadBoolean();
            m_isStatic = binaryReader.ReadBoolean();
            m_layer = binaryReader.ReadInt32();
            m_tag = binaryReader.ReadString();
            m_instanceID = binaryReader.ReadInt32();
            m_name = binaryReader.ReadString();

            var len = binaryReader.ReadInt32();
            if(len != -1)
            {
                m_componentKunTypes = new ComponentKun.ComponentKunType[len];
                for(var i = 0; i < len; i++)
                {
                    m_componentKunTypes[i] = (ComponentKun.ComponentKunType)binaryReader.ReadInt32();
                }
            }

            len = binaryReader.ReadInt32();
            if(len != -1)
            {
                m_componentKuns = new ComponentKun[len];
                for(var i = 0; i < len; i++)
                {
                    m_componentKuns[i] = ComponentKun.Instantiate(m_componentKunTypes[i]);
                    m_componentKuns[i].Deserialize(binaryReader);
                    m_componentKuns[i].gameObjectKun = this;
                }
            }
        }


        public override bool Equals(object obj)
        {
            var other = obj as GameObjectKun;
            if(other == null)
            {
                return false;
            }

            if (!bool.Equals(m_activeSelf, other.m_activeSelf))
            {
                return false;
            }
            if (!bool.Equals(m_isStatic, other.m_isStatic))
            {
                return false;
            }
            if (!int.Equals(m_layer, other.m_layer))
            {
                return false;
            }
            if (!string.Equals(m_tag, other.m_tag))
            {
                return false;
            }
            if (!int.Equals(m_instanceID, other.m_instanceID))
            {
                return false;
            }
            if(m_componentKunTypes != null)
            {
                if(other.m_componentKunTypes == null)
                {
                    return false;
                }
                if (!Equals(m_componentKunTypes.Length, other.m_componentKunTypes.Length))
                {
                    return false;
                }
                for(var i = 0; i < m_componentKunTypes.Length; i++)
                {
                    if (!m_componentKunTypes[i].Equals(other.m_componentKunTypes[i]))
                    {
                        return false;
                    }
                }
            }
            if(m_componentKuns != null)
            {
                if(other.m_componentKuns == null)
                {
                    return false;
                }
                if(m_componentKuns.Length != other.m_componentKuns.Length)
                {
                    return false;
                }
                for(var i = 0; i < m_componentKuns.Length; i++)
                {
                    if (!m_componentKuns[i].Equals(other.m_componentKuns[i]))
                    {
                        return false;
                    }
                }
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

        public T AddComponent<T>() where T : ComponentKun,new()
        {
            var componentKun = new T();
            
            
            componentKun.gameObjectKun = this;


            if (componentKuns == null)
            {
                componentKuns = new ComponentKun[1];
                componentKuns[0] = componentKun;

                componentKunTypes = new ComponentKun.ComponentKunType[1];
                componentKunTypes[0] = componentKun.componentKunType;
            }
            else
            {
                var list = new List<ComponentKun>(componentKuns);
                list.Add(componentKun);
                componentKuns = list.ToArray();

                var types = new List<ComponentKun.ComponentKunType>(componentKunTypes);
                types.Add(componentKun.componentKunType);
                componentKunTypes = types.ToArray();
            }
                        

            return componentKun;
        }


        public ComponentKun GetComponentKun(Type type)
        {
            foreach (var componentKun in componentKuns)
            {
                if(componentKun.GetType() == type)
                {
                    return componentKun;
                }                                
            }
            return null;
        }


        public T GetComponentKun<T>()where T : ComponentKun
        {
            foreach (var componentKun in componentKuns)
            {
                if (componentKun.GetType() == typeof(T))
                {
                    return (T)componentKun;
                }
            }
            return null;
        }

    }


}