using System.IO;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{

    [System.Serializable]
    public class AndroidView 
    {
        [SerializeField] AndroidKun m_androidKun;
        [SerializeField] Vector2 m_scrollPos;

        public AndroidKun androidKun
        {
            get { 
                if(m_androidKun == null)
                {
                    m_androidKun = new AndroidKun();
                }
                return m_androidKun; 
            }
                        
            set
            { 
                m_androidKun = value; 
            }
        }
        

        Vector2 scrollPos
        {
            get { return m_scrollPos; }
            set { m_scrollPos = value; }
        }
        bool isDone = false;


        static AndroidView mInstance;
        public static AndroidView instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new AndroidView();
                }
                return mInstance;
            }
        }


        public void OnGUI()
        {
            if (isDone == false)
            {
                EditorGUILayout.HelpBox("Please Pull Request.", MessageType.Info);
            }
            else
            {
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
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
                EditorGUILayout.EndScrollView();
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Pull"))
            {
                UnityChoseKunEditor.SendMessage(UnityChoseKun.MessageID.AndroidPull);
            }
            EditorGUILayout.EndHorizontal();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEvent(BinaryReader binaryReader)
        {
            androidKun.Deserialize(binaryReader);
            isDone = true;
        }

    }

}
