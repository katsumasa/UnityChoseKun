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
       //     SkinnedMeshMeshRenderer,
    //            MeshRenderer,
            Renderer,
            Component,            
        };

        public static readonly System.Type [,] typeConverterTbls = {
            {typeof(Transform),typeof(TransformKun)},
            {typeof(Camera),typeof(CameraKun)},
            {typeof(Light),typeof(LightKun)},
            {typeof(Behaviour),typeof(BehaviourKun)},
            
            {typeof(Renderer),typeof(RendererKun)},

            
            {typeof(Component),typeof(ComponentKun)},
        };

        // <summary> ComponentからComponentKunTypeを取得する </summary>
        public static ComponentKunType GetComponentKunType(Component component)
        {
            // Note:基底クラスのチェックが後になるように記述する必要がある
            if(component is Transform){return ComponentKunType.Transform;}
            if(component is Camera){return ComponentKunType.Camera;}
            if(component is Light){return ComponentKunType.Light;}
            if(component is Behaviour){return ComponentKunType.Behaviour;}
            if(component is Renderer){return ComponentKunType.Renderer;}

            if(component is Component){return ComponentKunType.Component;}
            return ComponentKunType.Invalid;
        }

        // <summary> ComponentKunTypeからSystem.Typeを取得する </summary>
        public static System.Type GetComponentSystemType(ComponentKunType behaviorKunType)
        {            
            return typeConverterTbls[(int)behaviorKunType,0];
        }

        // <summary> ComponentからSystemTypeを取得する </summary>
        public static System.Type GetComponentSystemType(Component component)
        {            
            return GetComponentSystemType(GetComponentKunType(component));            
        }

        public static System.Type GetComponetKunSyetemType(ComponentKunType behaviorKunType)
        {
            return typeConverterTbls[(int)behaviorKunType,1];
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