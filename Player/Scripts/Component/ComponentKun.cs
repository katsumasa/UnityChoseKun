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
    }
}