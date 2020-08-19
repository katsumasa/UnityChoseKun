using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Test : EditorWindow
{
    public int ofst1 = 24;
    public int ofst2 = 12;

    public static Test window;

    private static class Styles{
        public static readonly GUIContent LightContent = new GUIContent((Texture2D)EditorGUIUtility.Load("d_AreaLight Icon"));
    }
    
    bool toggle;

    [MenuItem("Window/Test")]
        static void Create()
        {
            if(window == null){
                window = (Test)EditorWindow.GetWindow(typeof(Test));
            }
            
            window.wantsMouseMove = true;
            window.autoRepaintOnSceneChange = true;
            window.Show();
        }
    
    void OnGUI()
    {   
        EditorGUILayout.BeginHorizontal();
        //var rect = EditorGUILayout.GetControlRect();
        //rect.Set(rect.x,rect.y,32,rect.height);
        EditorGUILayout.Foldout(true,Styles.LightContent,true);
        //EditorGUI.Foldout(rect,true,Styles.LightContent,true);
        toggle = EditorGUILayout.ToggleLeft("Light",toggle);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        
        
        
    }
}
