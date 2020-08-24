namespace  Utj.UnityChoseKun 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    
    [System.Serializable]
    public class ComponentKun : ObjectKun{                    
        // <summary>定数の定義</summary>
        // <summary> ComponentKunのタイプの定義 </summary>
        public enum ComponentKunType {            
            Invalid = -1,
            Transform = 0,
            Camera,
            Light,            
            Behaviour,
            SkinnedMeshMeshRenderer,
            MeshRenderer,
            Renderer,
            Component,            
        };

        
        class ComponentPair {
            public System.Type componentType;
            public System.Type componenKunType;
            public ComponentPair(System.Type componentType,System.Type componenKunType){
                this.componentType = componentType;
                this.componenKunType = componenKunType;
            }
        }

        static readonly Dictionary<ComponentKunType,ComponentPair> componentPairDict = new Dictionary<ComponentKunType, ComponentPair>() 
        {
            {ComponentKunType.Transform,new ComponentPair(typeof(Transform),typeof(TransformKun))},
            {ComponentKunType.Camera,new ComponentPair(typeof(Camera),typeof(CameraKun))},
            {ComponentKunType.Light,new ComponentPair(typeof(Light),typeof(LightKun))},
            {ComponentKunType.SkinnedMeshMeshRenderer,new ComponentPair(typeof(SkinnedMeshRenderer),typeof(SkinnedMeshRendererKun))},            
            {ComponentKunType.MeshRenderer,new ComponentPair(typeof(MeshRenderer),typeof(MeshRendererKun))},
            {ComponentKunType.Renderer,new ComponentPair(typeof(Renderer),typeof(RendererKun))},
            {ComponentKunType.Behaviour,new ComponentPair(typeof(Behaviour),typeof(BehaviourKun))},
            {ComponentKunType.Component,new ComponentPair(typeof(Component),typeof(ComponentKun))},
        };
        
        // <summary> ComponentからComponentKunTypeを取得する </summary>
        public static ComponentKunType GetComponentKunType(Component component)
        {
            // Note:基底クラスのチェックが後になるように記述する必要がある
            if(component is Transform){return ComponentKunType.Transform;}
            if(component is Camera){return ComponentKunType.Camera;}
            if(component is Light){return ComponentKunType.Light;}
            if(component is Behaviour){return ComponentKunType.Behaviour;}
            if(component is MeshRenderer){return ComponentKunType.MeshRenderer;}
            if(component is SkinnedMeshRenderer){return ComponentKunType.SkinnedMeshMeshRenderer;}
            if(component is Renderer){return ComponentKunType.Renderer;}

            if(component is Component){return ComponentKunType.Component;}
            return ComponentKunType.Invalid;
        }

        // <summary> ComponentKunTypeからSystem.Typeを取得する </summary>
        public static System.Type GetComponentSystemType(ComponentKunType componentKunType)
        {            
            return componentPairDict[componentKunType].componentType;
        }

        // <summary> ComponentからSystemTypeを取得する </summary>
        public static System.Type GetComponentSystemType(Component component)
        {            
            return GetComponentSystemType(GetComponentKunType(component));            
        }

        public static System.Type GetComponetKunSyetemType(ComponentKunType componentKunType)
        {
            return componentPairDict[componentKunType].componenKunType;
        }

        public static System.Type GetComponetKunSyetemType(Component component)
        {            
            return GetComponetKunSyetemType(GetComponentKunType(component));           
        }
        
        [SerializeField] protected ComponentKunType m_componentKunType; 
        public ComponentKunType componentKunType{
            get{return m_componentKunType;}
            protected set{m_componentKunType = value;}
        }

        public ComponentKun():this(null){}
        public ComponentKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.Component;
        }

        public virtual void WriteBack(Component component)
        {
            //...
        }
        
    }
}