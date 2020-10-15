using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{

    [System.Serializable]
    public class AndroidView 
    {
        [SerializeField] AndroidKun m_androidKun;
        public AndroidKun androidKun
        {
            get { return m_androidKun; }
            set { m_androidKun = value; }
        }
        
        [SerializeField] Vector2 m_scrollPos;
        Vector2 scrollPos
        {
            get { return m_scrollPos; }
            set { m_scrollPos = value; }
        }
        bool isDone = false;



        public void OnGUI()
        {
            if (isDone == false)
            {
                EditorGUILayout.HelpBox("Please Pull Request.", MessageType.Info);
            }
            else
            {


                EditorGUILayout.LabelField("Permission");
                using (new EditorGUI.IndentLevelScope())
                {
                    for (var i = 0; i < androidKun.permissions.Length; i++)
                    {
                        EditorGUI.BeginChangeCheck();
                        androidKun.hasUserAuthorizedPermission[i] = EditorGUILayout.ToggleLeft(androidKun.permissions[i], androidKun.hasUserAuthorizedPermission[i]);
                        if (EditorGUI.EndChangeCheck())
                        {
                            androidKun.requestUserPermissions[i] = true;
                        }
                    }
                }
#if UNITY_2019_1_OR_NEWER
                EditorGUI.BeginChangeCheck();

                
                androidKun.isSustainedPerformanceMode = EditorGUILayout.ToggleLeft("SustainedPerformanceMode",androidKun.isSustainedPerformanceMode);

                if (EditorGUI.EndChangeCheck())
                {
                    androidKun.requestSustainedPerformanceMode = true;
                }
#endif

            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Pull"))
            {
                UnityChoseKunEditor.SendMessage<AndroidKun>(UnityChoseKun.MessageID.AndroidPull, androidKun);
            }
            if (GUILayout.Button("Push")) {
                UnityChoseKunEditor.SendMessage<AndroidKun>(UnityChoseKun.MessageID.AndroidPush, androidKun);
                
            }

            EditorGUILayout.EndHorizontal();

        }

        public void OnMessageEvent(byte[] bytes)
        {
            androidKun = UnityChoseKun.GetObject<AndroidKun>(bytes);
            isDone = true;
        }

    }

}
