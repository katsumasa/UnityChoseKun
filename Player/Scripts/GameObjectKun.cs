namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;
    using System;

    [System.Serializable]
    public class GameObjectKun {

        [SerializeField] bool m_activeSelf;
        public bool activeSelf{
            get{return m_activeSelf;}
            set{m_activeSelf = value;}
        }

        [SerializeField] bool m_isStatic;
        public bool isStatic {
            get {return m_isStatic;}
            set {m_isStatic = value;}
        }

        [SerializeField] int m_layer;
        public int layer{
            get {return m_layer;}
            set {m_layer = value;}
        }

        [SerializeField] string m_tag;
        public string tag{
            get {return m_tag;}
            set {m_tag = value;}
        }

        [SerializeField] int m_instanceID;
        public int instanceID{
            get {return m_instanceID;}
            private set {m_instanceID = value;}
        }

        [SerializeField] string m_name;
        public string name {
            get {return m_name;}
            set {m_name = value;}
        }

        [SerializeField] ComponentKun.ComponentKunType [] m_componentKunTypes;
        public ComponentKun.ComponentKunType [] componentKunTypes{
            get{return m_componentKunTypes;}
            set{m_componentKunTypes = value;}
        }

        [SerializeField] ComponentKun[] m_componentKuns;
        public ComponentKun[] componentKuns
        {
            get { return m_componentKuns; }
            set { m_componentKuns = value; }
        }


        private TransformKun m_transformKun;
        public TransformKun transformKun {
            get {
                return m_componentKuns[0] as TransformKun;
            }
        }
        
        [SerializeField] bool m_dirty;
        public bool dirty {
            get{return m_dirty;}
            set{m_dirty = value;}
        }


        public GameObjectKun():this(null){}


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
                var systemType = ComponentKun.GetComponetKunSyetemType(componentKunTypes[i]);
                componentKuns[i] = System.Activator.CreateInstance(systemType, new object[] { component }) as ComponentKun;
                i++;                
            }                        
            dirty = false;
        }


        public void WriteBack(GameObject gameObject)
        {
            if(dirty){
                gameObject.SetActive(activeSelf);
                gameObject.isStatic = isStatic;
                gameObject.layer = layer;
                gameObject.tag = tag;
                gameObject.name = name;
            }

            // ComponentKun側がDirtyであるかいなかはComponentKun側に依存する
            for(var i = 0; i < componentKunTypes.Length; i++){                        
                var componentKunType = componentKunTypes[i];
                var systemType = ComponentKun.GetComponentSystemType(componentKunType);                
                var component = gameObject.GetComponent(systemType);
                if(component == null){                
                    Debug.LogWarning("component == null");
                    continue;
                }                               
                componentKuns[i].WriteBack(component);
            }
            
            dirty = false;
        }


        
    }


}