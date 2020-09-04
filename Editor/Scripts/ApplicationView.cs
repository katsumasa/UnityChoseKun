using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class ApplicationView
    {
        [SerializeField] ApplicationKun m_applicationKun;
        public ApplicationKun applicationKun
        {
            get { if (m_applicationKun == null) { m_applicationKun = new ApplicationKun(); } return m_applicationKun; }
            private set { m_applicationKun = value; }
        }
        bool isDone = false;
        [SerializeField] Vector2 m_scrollPos;
        Vector2 scrollPos
        {
            get { return m_scrollPos; }
            set { m_scrollPos = value; }
        }



        public void OnGUI()
        {
            if (isDone == false)
            {
                EditorGUILayout.HelpBox("Please Pull Request.", MessageType.Info);
            }
            else
            {
                EditorGUILayout.BeginScrollView(scrollPos);
                EditorGUILayout.LabelField("absoluteURL", applicationKun.absoluteURL);
                EditorGUILayout.LabelField("buildGUID", applicationKun.buildGUID);
                EditorGUILayout.LabelField("cloudProjectId", applicationKun.cloudProjectId);
                EditorGUILayout.LabelField("companyName", applicationKun.companyName);
                EditorGUILayout.LabelField("consoleLogPath", applicationKun.consoleLogPath);
                EditorGUILayout.LabelField("dataPath", applicationKun.dataPath);
                EditorGUILayout.Toggle("genuineCheckAvailable", applicationKun.genuineCheckAvailable);
                if (applicationKun.genuineCheckAvailable)
                {
                    EditorGUILayout.Toggle("genuine", applicationKun.genuine);
                }
                EditorGUILayout.LabelField("identifier", applicationKun.identifier);
                EditorGUILayout.LabelField("installerName", applicationKun.installerName);
                EditorGUILayout.EnumPopup("installMode", applicationKun.installMode);
                EditorGUILayout.EnumPopup("internetReachability", applicationKun.internetReachability);
                EditorGUILayout.Toggle("isBatchMode", applicationKun.isBatchMode);
                EditorGUILayout.Toggle("isConsolePlatform", applicationKun.isConsolePlatform);
                EditorGUILayout.Toggle("isEditor", applicationKun.isEditor);
                EditorGUILayout.Toggle("isForcus", applicationKun.isFocused);
                EditorGUILayout.Toggle("isMobilePlatform", applicationKun.isMobilePlatform);
                EditorGUILayout.Toggle("isPlaying", applicationKun.isPlaying);
                EditorGUILayout.LabelField("persistentDataPath", applicationKun.persistentDataPath);
                EditorGUILayout.EnumPopup("platform", applicationKun.platform);
                EditorGUILayout.LabelField("productName", applicationKun.productName);
                EditorGUILayout.EnumPopup("sandboxType", applicationKun.sandboxType);
                EditorGUILayout.LabelField("streamingAssetsPath", applicationKun.streamingAssetsPath);
                EditorGUILayout.EnumPopup("systemLanguage", applicationKun.systemLanguage);
                EditorGUILayout.LabelField("temporaryCachePath", applicationKun.temporaryCachePath);
                EditorGUILayout.LabelField("unityVersion", applicationKun.unityVersion);
                EditorGUILayout.LabelField("version", applicationKun.version);

                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                EditorGUI.BeginChangeCheck();
                applicationKun.backgroundLoadingPriority = (ThreadPriority)EditorGUILayout.EnumPopup("backgroundLoadingPriority", applicationKun.backgroundLoadingPriority);
                applicationKun.targetFrameRate = EditorGUILayout.IntField("targetFrameRate", applicationKun.targetFrameRate);
                applicationKun.runInBackground = EditorGUILayout.Toggle("runInBackground", applicationKun.runInBackground);
                if (EditorGUI.EndChangeCheck())
                {
                    applicationKun.isDirty = true;
                }
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Pull"))
            {
                UnityChoseKunEditor.SendMessage<ScreenKun>(UnityChoseKun.MessageID.ApplicationPull,null);
            }
            if (isDone && applicationKun.isDirty) {
                if (GUILayout.Button("Push"))
                {
                    UnityChoseKunEditor.SendMessage<ScreenKun>(UnityChoseKun.MessageID.ApplicationPush, applicationKun);
                }
            }
            if (GUILayout.Button("Quit"))
            {
                UnityChoseKunEditor.SendMessage<ApplicationKun>(UnityChoseKun.MessageID.ApplicationQuit, null);
            }
            EditorGUILayout.EndHorizontal();
            
        }

        public void OnMessageEvent(string json)
        {
            applicationKun = JsonUtility.FromJson<ApplicationKun>(json);
            isDone = true;
        }
    }
}
