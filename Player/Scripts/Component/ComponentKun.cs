namespace  Utj.UnityChoseKun 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    
    [System.Serializable]
    public class ComponentKun {                    
        public enum ComponentKunType {            
            Transform = 0,
            Camera,
            Light,
            Behaviour,
            Component,

            Invalid,
        };

        public static readonly System.Type [,] typeConverterTbls = {
            {typeof(Transform),typeof(TransformKun)},
            {typeof(Camera),typeof(CameraKun)},
            {typeof(Light),typeof(LightKun)},
            {typeof(Behaviour),typeof(BehaviourKun)},


            
            {typeof(Component),typeof(ComponentKun)},
        };


        public static ComponentKunType GetComponentKunType(Component component)
        {
            if(component is Transform){return ComponentKunType.Transform;}
            if(component is Camera){return ComponentKunType.Camera;}
            if(component is Light){return ComponentKunType.Light;}
            if(component is Behaviour){return ComponentKunType.Behaviour;}
            if(component is Component){return ComponentKunType.Component;}
            return ComponentKunType.Invalid;
        }

        public static System.Type GetComponentSystemType(ComponentKunType behaviorKunType)
        {            
            return typeConverterTbls[(int)behaviorKunType,0];
        }

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


        public string name;
        public ComponentKunType componentKunType; 

        public ComponentKun():this(null){}
        public ComponentKun(Component component)
        {
            componentKunType = ComponentKunType.Component;
            if(component!=null){
                name = component.ToString();
            }
        }

        public virtual void WriteBack(Component component)
        {
            //...
        }
        
    }
}