using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utj.UnityChoseKun.Engine.Rendering
{
    namespace Universal
    {

        public class UniversalRenderPipelineGlobalSettingsKun : RenderPipelineGlobalSettingsKun
        {
            static UniversalRenderPipelineGlobalSettingsKun mInstance;

            public static UniversalRenderPipelineGlobalSettingsKun instance
            {
                get 
                { 
                    if(mInstance == null)
                    {
                        mInstance = new UniversalRenderPipelineGlobalSettingsKun(null);
                    }
                    return mInstance; 
                }
            }

            public static readonly string defaultAssetName = "UniversalRenderPipelineGlobalSettings";


            string[] m_RenderingLayerNames;
            string[] renderingLayerNames
            {
                get
                {
                    return m_RenderingLayerNames;
                }
            }

            string[] m_PrefixedRenderingLayerNames;
            string[] prefixedRenderingLayerNames
            {
                get { return m_PrefixedRenderingLayerNames; }
            }

            string[] m_PrefixedLightLayerNames = null;
            public string[] prefixedLightLayerNames
            {
                get { return m_PrefixedLightLayerNames; }
            }

            /// <summary>Names used for display of rendering layer masks.</summary>
            public string[] renderingLayerMaskNames => renderingLayerNames;
            /// <summary>Names used for display of rendering layer masks with a prefix.</summary>
            public string[] prefixedRenderingLayerMaskNames => prefixedRenderingLayerNames;



            public UniversalRenderPipelineGlobalSettingsKun(RenderPipelineGlobalSettings renderPipelineGlobalSettings):base(renderPipelineGlobalSettings)
            {                
                if(renderPipelineGlobalSettings == null)
                {
                    return;
                }
                var type = renderPipelineGlobalSettings.GetType();
                if (type == null)
                {
                    return;
                }
                var prop = type.GetProperty("instance");
                if (prop == null)
                {
                    return;
                }
                var _instance = prop.GetValue(null);
                if (_instance == null)
                {
                    return;
                }
                prop = type.GetProperty("renderingLayerNames", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (prop != null)
                {
                    m_RenderingLayerNames = (string[])prop.GetValue(_instance);
                }
                prop = type.GetProperty("prefixedRenderingLayerNames", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (prop != null)
                {
                    m_PrefixedRenderingLayerNames = (string[])prop.GetValue(_instance);
                }
                prop = type.GetProperty("prefixedLightLayerNames");
                if (prop != null)
                {
                    m_PrefixedLightLayerNames = (string[])prop.GetValue(_instance);
                }
            }

            public override void Serialize(BinaryWriter binaryWriter)
            {
                base.Serialize(binaryWriter);
                SerializerKun.Serialize(binaryWriter, m_RenderingLayerNames);
                SerializerKun.Serialize(binaryWriter, m_PrefixedRenderingLayerNames);
                SerializerKun.Serialize(binaryWriter, m_PrefixedLightLayerNames);
            }

            public override void Deserialize(BinaryReader binaryReader)
            {
                base.Deserialize(binaryReader);
                m_RenderingLayerNames = SerializerKun.DesirializeStrings(binaryReader);
                m_PrefixedRenderingLayerNames = SerializerKun.DesirializeStrings(binaryReader);
                m_PrefixedLightLayerNames = SerializerKun.DesirializeStrings(binaryReader);                
            }        
        }


        public class UniversalRenderPipelineGlobalSettingsPlayer
        {
            public static void OnMessageEventPull(BinaryReader binaryReader)
            {
                if(GraphicsSettings.defaultRenderPipeline == null)
                {
                    return;
                }
                var type = GraphicsSettings.defaultRenderPipeline.GetType();
                if(type.FullName != "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset")
                {
                    return;
                }
                var assembly = System.Reflection.Assembly.Load("Unity.RenderPipelines.Universal.Runtime");
                if(assembly == null)
                {
                    return;
                }
                type = assembly.GetType("UnityEngine.Rendering.Universal.UniversalRenderPipelineGlobalSettings");
                if(type == null)
                {
                    return;
                }
                var prop = type.GetProperty("instance");
                if(prop == null)
                {
                    return;
                }
                var renderPipelineGlobalSettings = prop.GetValue(null) as RenderPipelineGlobalSettings;
                var universalRenderPipelineGlobalSettingsKun = new UniversalRenderPipelineGlobalSettingsKun(renderPipelineGlobalSettings);
                //
                UnityChoseKunPlayer.SendMessage<UniversalRenderPipelineGlobalSettingsKun>(UnityChoseKun.MessageID.UniversalRenderPipelineGlobalSettingsPull, universalRenderPipelineGlobalSettingsKun);
            }


            public static void OnMessageEventPush(BinaryReader binaryReader)
            {

            }
        }
    }
}