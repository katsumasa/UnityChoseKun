#if UNITY_2021_2_OR_NEWER
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
namespace Utj.UnityChoseKun
{
    using Engine.Rendering.Universal;


    namespace Editor.Rendering.Universal
    {


        public class UniversalRenderPipelineGlobalSettingsView
        {

            static UniversalRenderPipelineGlobalSettingsView mInstance;

            public static UniversalRenderPipelineGlobalSettingsView instance
            {
                get
                {
                    if(mInstance == null)
                    {
                        mInstance = new UniversalRenderPipelineGlobalSettingsView();
                    }
                    return mInstance;
                }
            }


            static class Styles
            {
                public static readonly GUIContent LightLayerNames = new GUIContent("Light Layer Names (3D)");
            }

            public void OnGUI()
            {
                EditorGUILayout.LabelField(Styles.LightLayerNames);
                EditorGUILayout.Space();
                                
                if (UniversalRenderPipelineGlobalSettingsKun.instance != null &&
                    UniversalRenderPipelineGlobalSettingsKun.instance.prefixedRenderingLayerMaskNames != null)
                {
                    for (var i = 0; i < 8; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField($"Light Layer {i.ToString()}");
                        EditorGUILayout.TextField(UniversalRenderPipelineGlobalSettingsKun.instance.prefixedRenderingLayerMaskNames[i]);
                        EditorGUILayout.EndHorizontal();
                    }
                }

                if(GUILayout.Button("Pull"))
                {
                    UnityChoseKunEditor.SendMessage<UniversalRenderPipelineGlobalSettingsKun>(UnityChoseKun.MessageID.UniversalRenderPipelineGlobalSettingsPull, null);
                }

            }

            public void OnMessageEvent(BinaryReader binaryReader)
            {
                UniversalRenderPipelineGlobalSettingsKun.instance.Deserialize(binaryReader);
            }
        }
    }
}
#endif