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
        public ComponentKun.ComponentType [] types;
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

        private readonly System.Type [,] typeConverterTbls = {
            {typeof(Camera),typeof(CameraKun)},
            {typeof(Light),typeof(LightKun)},
        };



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
            var typeList = new List<ComponentKun.ComponentType>();
            var jsonList = new List<string>();
            var idx = 0;
                        

            for(var i = 0; i < typeConverterTbls.GetLength(0); i++)
            {
                System.Type type = typeConverterTbls[i,0];
                var components = go.GetComponents(type);
                if(components != null){
                    type = typeConverterTbls[i,1];
                    for(var j = 0; j < components.Length; j++)    
                    {                        
                        typeList.Add((ComponentKun.ComponentType)i);
                        var component = components[j];
                        var o = System.Activator.CreateInstance(type,new object[]{component});
                        jsonList.Add(JsonUtility.ToJson(o));
                        idx++;    
                    }
                    types = typeList.ToArray();
                    componentDataJsons = jsonList.ToArray();
                }
            }
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

            for(var i = 0; i < types.Length; i++){
                var index = (int)types[i];
                var type = typeConverterTbls[index,0];
                Debug.Log("type : "+type);
                var component = gameObject.GetComponent(type);
                if(component == null){
                    Debug.Log("component == null");
                    continue;
                }
                var componentKun = JsonUtility.FromJson(componentDataJsons[i],typeConverterTbls[index,1]) as ComponentKun;
                if(componentKun == null){
                    Debug.Log("componentKun == null");
                    continue;
                }
                componentKun.WriteBack(component);
            }            
        }


        public bool IsContainComponent(ComponentKun.ComponentType type)
        {
            if(types == null){
                return false;
            }
            return types.Contains(type);
        }
    }


}