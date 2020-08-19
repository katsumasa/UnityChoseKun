namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    
    [System.Serializable]
    public class GameObjectKun {
        public bool activeSelf;
        public bool isStatic;
        public int layer;
        public string tag;
        public int instanceID;
        public string name;
        
        public string transformJson;
        public ComponentKun.ComponentKunType [] componentKunTypes;
        public string[] componentDataJsons;

        private TransformKun m_transformKun;
        public TransformKun transformKun {
            get {
                if(m_transformKun == null){
                    m_transformKun = JsonUtility.FromJson<TransformKun>(transformJson);
                }
                return m_transformKun;
            }
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
            transformJson = JsonUtility.ToJson(new TransformKun(go.transform));                
            var typeList = new List<BehaviourKun.ComponentKunType>();
            var jsonList = new List<string>();
            
            var components = go.GetComponents(typeof(Component));
            foreach(var component in components){
                var componentKunType = ComponentKun.GetComponentKunType(component);
                if(componentKunType == ComponentKun.ComponentKunType.Transform){
                    continue;
                }
                var systemType = ComponentKun.typeConverterTbls[(int)componentKunType,1];   
                var componentKun = System.Activator.CreateInstance(systemType,new object[]{component});
                var json = JsonUtility.ToJson(componentKun);
                typeList.Add(componentKunType);
                jsonList.Add(json);                                
            }
            componentKunTypes = typeList.ToArray();
            componentDataJsons = jsonList.ToArray();            
        }


        public void StoreGameObject(GameObject gameObject)
        {
            Debug.Log("GameObjectKun::StoreGameObject");

            gameObject.SetActive(activeSelf);
            gameObject.isStatic = isStatic;
            gameObject.layer = layer;
            gameObject.tag = tag;
            gameObject.name = name;

            var transFormKun = JsonUtility.FromJson<TransformKun>(transformJson);
            if(transFormKun != null){
                transFormKun.WriteBack(gameObject.transform);
            }

            for(var i = 0; i < componentKunTypes.Length; i++){                        
                var componentKunType = componentKunTypes[i];
                var systemType = ComponentKun.GetComponentSystemType(componentKunType);
                var component = gameObject.GetComponent(systemType);
                if(component == null){
                    Debug.LogWarning("component == null");
                    continue;
                }
                var componentKun = JsonUtility.FromJson(componentDataJsons[i],
                                                        ComponentKun.GetComponetKunSyetemType(componentKunType)) as ComponentKun;
                componentKun.WriteBack(component);
            }                        
        }


        
    }


}