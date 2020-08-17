namespace  Utj.UnityChoseKun
{   

    using System.Collections.Generic;


    // <summary> Componentの表示を行う基底クラス </summary>
    public abstract class ComponentView
    {        

        static Dictionary<ComponentKun.ComponentType,System.Type> componentViewTbls = new Dictionary<ComponentKun.ComponentType, System.Type>{
            {ComponentKun.ComponentType.Camera,typeof(CameraView)},
            {ComponentKun.ComponentType.Light,typeof(LightView)},
        };
        
        public static System.Type GetComponentViewSyetemType(ComponentKun.ComponentType componentType)
        {
            return componentViewTbls[componentType];
        }


        // <summary>JSONデータを設定する</summary>
        public abstract void SetJson(string json);        
        // <summary> JSONを設定する</summary>
        public abstract string GetJson();
        // <summary> OnGUIから呼び出す処理 </summary>
        public abstract void OnGUI();                    
    }
}