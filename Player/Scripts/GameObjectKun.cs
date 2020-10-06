namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    
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

#if false
        [SerializeField] string[] m_componentDataJsons;
        public string[] componentDataJsons {
            get{return m_componentDataJsons;}
            set{m_componentDataJsons = value;}
        }
#else
        [SerializeField] byte[][] m_componentDataBytes;
        public byte[][] componentDataBytes
        {
            get { return m_componentDataBytes; }
            set { m_componentDataBytes = value; }
        }

#endif
        private TransformKun m_transformKun;
        public TransformKun transformKun {
            get {                                
                if(m_transformKun == null){
#if true
                    if (componentDataBytes != null && componentDataBytes[0] != null)
                    {
                        var bf = new BinaryFormatter();
                        var ms = new MemoryStream(componentDataBytes[0]);
                        m_transformKun = (TransformKun)bf.Deserialize(ms);
                    }
                    else
                    {
                        m_transformKun = new TransformKun();
                    }
#else
                    m_transformKun = JsonUtility.FromJson<TransformKun>(componentDataJsons[0]);
#endif
                }
                return m_transformKun;
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
            
            var typeList = new List<BehaviourKun.ComponentKunType>();
            //var jsonList = new List<string>();            
            var bf = new BinaryFormatter();
            var components = go.GetComponents(typeof(Component));
            componentDataBytes = new byte[components.Length][];

            var i = 0;
            foreach (var component in components){
                var componentKunType = ComponentKun.GetComponentKunType(component);
                //Debug.Log("ComponentKunType: " + componentKunType);
                var systemType = ComponentKun.GetComponetKunSyetemType(componentKunType);
                var componentKun = System.Activator.CreateInstance(systemType,new object[]{component});
                typeList.Add(componentKunType);

                var ms = new MemoryStream();
                try
                {
                    bf.Serialize(ms,componentKun);
                    componentDataBytes[i] = ms.ToArray();                    
                }
                finally
                {
                    ms.Close();
                }
                i++;

                //var json = JsonUtility.ToJson(componentKun);                
                //jsonList.Add(json);                                
            }
            componentKunTypes = typeList.ToArray();
            

            dirty = false;
        }


        public void WriteBack(GameObject gameObject)
        {
            Debug.Log("GameObjectKun::StoreGameObject("+dirty+")");
            if(dirty){
                gameObject.SetActive(activeSelf);
                gameObject.isStatic = isStatic;
                gameObject.layer = layer;
                gameObject.tag = tag;
                gameObject.name = name;
            }
            for(var i = 0; i < componentKunTypes.Length; i++){                        
                var componentKunType = componentKunTypes[i];                    
                var systemType = ComponentKun.GetComponentSystemType(componentKunType);
                var component = gameObject.GetComponent(systemType);
                if(component == null){
                    Debug.LogWarning("component == null");
                    continue;
                }
#if false
                var componentKun = JsonUtility.FromJson(componentDataJsons[i],
                                                        ComponentKun.GetComponetKunSyetemType(componentKunType)) as ComponentKun;
#else
                var bf = new BinaryFormatter();
                var ms = new MemoryStream(componentDataBytes[i]);
                var componentKun = (ComponentKun)bf.Deserialize(ms);
#endif
                componentKun.WriteBack(component);
            }
            
            dirty = false;
        }


        
    }


}