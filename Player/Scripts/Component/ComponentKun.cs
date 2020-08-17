namespace  Utj.UnityChoseKun 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // <summary> ComponentをEditor/Playerの両方でシリアライズ/デシリアライズを行う処理 </summary>
    [System.Serializable]
    public abstract class ComponentKun {
        public enum ComponentType {
            Camera = 0,
            Light,
        };
        // -----------------------
        public ComponentType type;                

        

        public abstract void WriteBack(Component component);


        static readonly System.Type [,] typeConverterTbls = {
            {typeof(Camera),typeof(CameraKun)},
            {typeof(Light),typeof(LightKun)},
        };

        public static System.Type GetSystemType(ComponentType componentType,bool isComponentKun)
        {
            return typeConverterTbls[(int)componentType,isComponentKun?1:0];
        }

        

    }
}