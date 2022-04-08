using System.IO;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine.Rendering;
    using Engine.Rendering.Universal;

    namespace Editor.Rendering
    {
        using Rendering.Universal;
        

        public class GraphicsSettingsView
        {

            static class Styles
            {
                public static readonly GUIContent allConfiguredRenderPipelines = new GUIContent("AllConfiguredRenderPipelines");
                public static readonly GUIContent currentRenderPipeline = new GUIContent("CurrentRenderPipeline");
                public static readonly GUIContent defaultRenderingLayerMask = new GUIContent("DefaultRenderingLayerMask ");
                public static readonly GUIContent disableBuiltinCustomRenderTextureUpdate = new GUIContent("DisableBuiltinCustomRenderTextureUpdate");
                public static readonly GUIContent lightsUseColorTemperature = new GUIContent("LightsUseColorTemperature");
                public static readonly GUIContent defaultRenderPipeline = new GUIContent("DefaultRenderPipeline");
            }

            static GraphicsSettingsView mInstance;
            public static GraphicsSettingsView instance
            {
                get
                {
                    if (mInstance == null)
                    {
                        mInstance = new GraphicsSettingsView();
                    }
                    return mInstance;
                }
            }


            Vector2 m_scrollPos;
            UniversalRenderPipelineAssetView[] mUniversalRenderPipelineAssetViews;


            public void OnGUI()
            {         
                m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos);
                {
#if UNITY_2019_1_OR_NEWER
                    if (GraphicsSettingsKun.renderPipelineAssetType == RenderPipelineAssetType.URP)
                    {
                        EditorGUILayout.LabelField(Styles.allConfiguredRenderPipelines);
                        EditorGUI.indentLevel++;

                        if (mUniversalRenderPipelineAssetViews == null || mUniversalRenderPipelineAssetViews.Length != GraphicsSettingsKun.allConfiguredRenderPipelines.Length)
                        {
                            mUniversalRenderPipelineAssetViews = new UniversalRenderPipelineAssetView[GraphicsSettingsKun.allConfiguredRenderPipelines.Length];
                        }
                        for (var i = 0; i < GraphicsSettingsKun.allConfiguredRenderPipelines.Length; i++)
                        {
                            if (mUniversalRenderPipelineAssetViews[i] == null)
                            {
                                mUniversalRenderPipelineAssetViews[i] = new UniversalRenderPipelineAssetView();
                            }
                            mUniversalRenderPipelineAssetViews[i].DrawContent(GraphicsSettingsKun.allConfiguredRenderPipelines[i] as UniversalRenderPipelineAssetKun);
                        }
                        EditorGUI.indentLevel--;

                        EditorGUILayout.LabelField($"CurrentRenderPipeline {GraphicsSettingsKun.currentRenderPipelineIdx}:{GraphicsSettingsKun.allConfiguredRenderPipelines[GraphicsSettingsKun.currentRenderPipelineIdx].name}");
                        EditorGUILayout.LabelField($"DefaultRenderPipeline {GraphicsSettingsKun.defaultRenderPipelineIdx}:{GraphicsSettingsKun.allConfiguredRenderPipelines[GraphicsSettingsKun.defaultRenderPipelineIdx].name}");

#if UNITY_2020_3_OR_NEWER
                        EditorGUI.BeginChangeCheck();
                        var disableBuiltinCustomRenderTextureUpdate = GraphicsSettingsKun.disableBuiltinCustomRenderTextureUpdate;
                        disableBuiltinCustomRenderTextureUpdate = EditorGUILayout.ToggleLeft(Styles.disableBuiltinCustomRenderTextureUpdate, disableBuiltinCustomRenderTextureUpdate);
                        if (EditorGUI.EndChangeCheck())
                        {
                            GraphicsSettingsKun.disableBuiltinCustomRenderTextureUpdate = disableBuiltinCustomRenderTextureUpdate;
                        }
#endif

                        EditorGUI.BeginChangeCheck();
                        var logWhenShaderIsCompiled = GraphicsSettingsKun.logWhenShaderIsCompiled;
                        logWhenShaderIsCompiled = EditorGUILayout.ToggleLeft("LogWhenShaderIsCompiled ", logWhenShaderIsCompiled);
                        if (EditorGUI.EndChangeCheck())
                        {
                            GraphicsSettingsKun.logWhenShaderIsCompiled = logWhenShaderIsCompiled;
                        }

                        EditorGUI.BeginChangeCheck();
                        var realtimeDirectRectangularAreaLights = GraphicsSettingsKun.realtimeDirectRectangularAreaLights;
                        realtimeDirectRectangularAreaLights = EditorGUILayout.ToggleLeft("RealtimeDirectRectangularAreaLights", realtimeDirectRectangularAreaLights);
                        if (EditorGUI.EndChangeCheck())
                        {
                            GraphicsSettingsKun.realtimeDirectRectangularAreaLights = realtimeDirectRectangularAreaLights;
                        }

#if UNITY_2020_1_OR_NEWER
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("VideoShadersIncludeMode");
                        var videoShadersIncludeMode = GraphicsSettingsKun.videoShadersIncludeMode;
                        videoShadersIncludeMode = (UnityEngine.Rendering.VideoShadersIncludeMode)EditorGUILayout.EnumPopup(videoShadersIncludeMode);
                        EditorGUILayout.EndHorizontal();
#endif
                    }

                    EditorGUILayout.Space(10);
#endif

                        EditorGUI.BeginChangeCheck();
                    var lightsUseColorTemperature = GraphicsSettingsKun.lightsUseColorTemperature;
                    lightsUseColorTemperature = EditorGUILayout.ToggleLeft(Styles.lightsUseColorTemperature, lightsUseColorTemperature, GUILayout.ExpandWidth(true));
                    if (EditorGUI.EndChangeCheck())
                    {
                        GraphicsSettingsKun.lightsUseColorTemperature = lightsUseColorTemperature;
                    }

                    EditorGUI.BeginChangeCheck();
                    var lightsUseLinearIntensity = GraphicsSettingsKun.lightsUseLinearIntensity;
                    lightsUseLinearIntensity = EditorGUILayout.ToggleLeft("LightsUseLinearIntensity", lightsUseLinearIntensity);
                    if (EditorGUI.EndChangeCheck())
                    {
                        GraphicsSettingsKun.lightsUseLinearIntensity = lightsUseLinearIntensity;
                    }


                    EditorGUI.BeginChangeCheck();
                    var transparencySortMode = GraphicsSettingsKun.transparencySortMode;
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("TransparencySortMode");
                    transparencySortMode = (TransparencySortMode)EditorGUILayout.EnumPopup(transparencySortMode);
                    EditorGUILayout.EndHorizontal();
                    if (EditorGUI.EndChangeCheck())
                    {
                        GraphicsSettingsKun.transparencySortMode = transparencySortMode;
                    }

                    EditorGUI.BeginChangeCheck();
                    var transparencySortAxis = GraphicsSettingsKun.transparencySortAxis.GetVector3();
                    transparencySortAxis = EditorGUILayout.Vector3Field("TransparencySortAxis", transparencySortAxis);
                    if (EditorGUI.EndChangeCheck())
                    {
                        GraphicsSettingsKun.transparencySortAxis = new Vector3Kun(transparencySortAxis);
                    }

                    EditorGUI.BeginChangeCheck();
                    var useScriptableRenderPipelineBatching = GraphicsSettingsKun.useScriptableRenderPipelineBatching;
                    useScriptableRenderPipelineBatching = EditorGUILayout.ToggleLeft("UseScriptableRenderPipelineBatching", useScriptableRenderPipelineBatching);
                    if (EditorGUI.EndChangeCheck())
                    {
                        GraphicsSettingsKun.useScriptableRenderPipelineBatching = useScriptableRenderPipelineBatching;
                    }

                    if (GraphicsSettingsKun.isDirty)
                    {

                        UnityChoseKunEditor.SendMessage<GraphicsSettingsKun>(UnityChoseKun.MessageID.GraphicsSettingsPush, GraphicsSettingsKun.instance);
                        GraphicsSettingsKun.isDirty = false;
                    }


                }
                EditorGUILayout.EndScrollView();
                
                
                if (GUILayout.Button("PULL"))
                {
                    UnityChoseKunEditor.SendMessage<GraphicsSettingsKun>(UnityChoseKun.MessageID.GraphicsSettingsPull,null);
                }
                EditorGUILayout.Space();
            }            
        }
    }
}
