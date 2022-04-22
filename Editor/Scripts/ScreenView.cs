using System.IO;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {
        [System.Serializable]
        public class ScreenView
        {
            [SerializeField] ScreenKun m_screenKun;
            [SerializeField] bool resolutionFoldout = false;
            [SerializeField] Vector2 m_scrollPos;
            bool isDone = false;


            static ScreenView mInstance;
            public static ScreenView instance
            {
                get { 
                    if(mInstance == null)
                    {
                        mInstance = new ScreenView();
                    }
                    return mInstance;
                }
            }


            ScreenKun screenKun
            {
                get { if (m_screenKun == null) { m_screenKun = new ScreenKun(); } return m_screenKun; }
                set { m_screenKun = value; }
            }

            Vector2 scrollPos
            {
                get { return m_scrollPos; }
                set { m_scrollPos = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public void OnGUI()
            {
                if (isDone == false)
                {
                    EditorGUILayout.HelpBox("Please Pull Request.", MessageType.Info);
                }
                else
                {
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                    screenKun.autorotateToLandscapeLeft = EditorGUILayout.ToggleLeft("autorotateToLandscapeLeft", screenKun.autorotateToLandscapeLeft);
                    screenKun.autorotateToLandscapeRight = EditorGUILayout.ToggleLeft("autorotateToLandscapeRight", screenKun.autorotateToLandscapeRight);
                    screenKun.autorotateToPortrait = EditorGUILayout.ToggleLeft("autorotateToPortrait", screenKun.autorotateToPortrait);
                    screenKun.autorotateToPortraitUpsideDown = EditorGUILayout.ToggleLeft("autorotateToPortraitUpsideDown", screenKun.autorotateToPortraitUpsideDown);
                    screenKun.orientation = (ScreenOrientation)EditorGUILayout.EnumPopup("orientation", screenKun.orientation);

                    EditorGUILayout.Space();

                    screenKun.fullScreen = EditorGUILayout.ToggleLeft("fullScreen", screenKun.fullScreen);
                    screenKun.sleepTimeout = EditorGUILayout.IntField("sleepTimeout", screenKun.sleepTimeout);

#if UNITY_2019_1_OR_NEWER
                    screenKun.brightness = EditorGUILayout.FloatField("brightness", screenKun.brightness);
#endif
                    EditorGUILayout.Space();

                    EditorGUILayout.FloatField("dpi", screenKun.dpi);

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("currentResolution");
                    EditorGUI.indentLevel++;
                    EditorGUILayout.IntField("width", screenKun.currentResolutionWidth);
                    EditorGUILayout.IntField("height", screenKun.currentResolutionHeight);
                    EditorGUILayout.IntField("refreshRate", screenKun.currentResolutionRefreshRate);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();

#if UNITY_2019_1_OR_NEWER
                    EditorGUILayout.LabelField("cutouts");
                    if (screenKun.cutouts != null)
                    {
                        EditorGUI.indentLevel++;
                        for (var i = 0; i < screenKun.cutouts.Length; i++)
                        {
                            EditorGUILayout.RectField(" [" + i + "]", screenKun.cutouts[i]);
                        }
                        EditorGUI.indentLevel--;
                    }

                    EditorGUILayout.Space();
#endif


                    if (screenKun.resolutions == null)
                    {
                        EditorGUILayout.LabelField("resolutions");
                    }
                    else
                    {
                        resolutionFoldout = EditorGUILayout.Foldout(resolutionFoldout, "resolutions");
                        if (resolutionFoldout)
                        {
                            EditorGUI.indentLevel++;
                            for (var i = 0; i < screenKun.resolutions.Length; i++)
                            {
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("width       " + screenKun.resolutions[i].width);
                                EditorGUILayout.LabelField("height      " + screenKun.resolutions[i].height);
                                EditorGUILayout.LabelField("refreshRate " + screenKun.resolutions[i].refreshRate);
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.Space();
                            }
                            EditorGUI.indentLevel--;
                        }
                    }

                    EditorGUILayout.Space();

                    EditorGUILayout.RectField("safeArea", screenKun.safeArea);

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("SetScreen");
                    EditorGUI.indentLevel++;
                    screenKun.width = EditorGUILayout.IntField("width", screenKun.width);
                    screenKun.height = EditorGUILayout.IntField("height", screenKun.height);
                    screenKun.fullScreenMode = (FullScreenMode)EditorGUILayout.EnumPopup("fullScreenMode", screenKun.fullScreenMode);
                    screenKun.preferredRefreshRate = EditorGUILayout.IntField("preferredRefreshRate", screenKun.preferredRefreshRate);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
                    EditorGUILayout.EndScrollView();
                }

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Pull"))
                {
                    UnityChoseKunEditor.SendMessage<ScreenKun>(UnityChoseKun.MessageID.ScreenPull, screenKun);
                }
                if (isDone)
                {
                    if (GUILayout.Button("Push"))
                    {
                        UnityChoseKunEditor.SendMessage<ScreenKun>(UnityChoseKun.MessageID.ScreenPush, screenKun);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel = 0;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="binaryReader"></param>
            public void OnMessageEvent(BinaryReader binaryReader)
            {
                screenKun.Deserialize(binaryReader);
                isDone = true;
            }
        }
    }
}