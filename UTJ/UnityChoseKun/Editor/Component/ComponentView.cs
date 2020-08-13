namespace  Utj.UnityChoseKun
{    
    // <summary> Componentの表示を行う基底クラス </summary>
    public abstract class ComponentView
    {        
        // <summary>JSONデータを設定する</summary>
        public abstract void SetJson(string json);        
        // <summary> JSONを設定する</summary>
        public abstract string GetJson();
        // <summary> OnGUIから呼び出す処理 </summary>
        public abstract void OnGUI();                    
    }
}