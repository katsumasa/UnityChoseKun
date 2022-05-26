using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;


namespace Utj.UnityChoseKun.Engine.Rendering
{
    namespace Universal {
        /// <summary>
        /// 
        /// </summary>
        public class UniversalRenderPipelineKun : RenderPipelineKun
        {
            static UniversalRenderPipelineKun mInstance;

            static UniversalRenderPipelineKun instance
            {
                get
                {
                    if(mInstance == null)
                    {
                        mInstance = new UniversalRenderPipelineKun();
                    }
                    return mInstance;
                }
            }


            public const string k_ShaderTagName = "UniversalPipeline";

            public static float maxShadowBias
            {
                get => 10.0f;
            }

            public static float minRenderScale
            {
                get => 0.1f;
            }

            public static float maxRenderScale
            {
                get => 2.0f;
            }

            public static int maxPerObjectLights
            {
                // No support to bitfield mask and int[] in gles2. Can't index fast more than 4 lights.
                // Check Lighting.hlsl for more details.
                get => (SystemInfoKun.graphicsDeviceType == GraphicsDeviceType.OpenGLES2) ? 4 : 8;
            }


            internal const int k_MaxVisibleAdditionalLightsMobileShaderLevelLessThan45 = 16;
            internal const int k_MaxVisibleAdditionalLightsMobile = 32;
            internal const int k_MaxVisibleAdditionalLightsNonMobile = 256;



            int m_maxVisibleAdditionalLights;
            UniversalRenderPipelineAssetKun m_asset;


            public static int maxVisibleAdditionalLights
            {
                get
                {
                    return instance.m_maxVisibleAdditionalLights;
                }
            }

            public static UniversalRenderPipelineAssetKun asset
            {
                get
                {
                    return instance.m_asset;
                }
            }

            public UniversalRenderPipelineKun(): this(false) { }
            


            public UniversalRenderPipelineKun(bool isSet):base()
            {                
                if(isSet)
                {
                    var assembly = System.Reflection.Assembly.Load("Unity.RenderPipelines.Universal.Runtime");
                    if (assembly == null)
                    {
                        return;
                    }
                    var type = assembly.GetType("UnityEngine.Rendering.Universal.UniversalRenderPipeline");
                    if (type != null)
                    {
                        var prop = type.GetProperty("asset");
                        if (prop != null)
                        {
                            m_asset = new UniversalRenderPipelineAssetKun(prop.GetValue(null) as UnityEngine.Rendering.RenderPipelineAsset);
                        }

                        prop = type.GetProperty("maxVisibleAdditionalLights");
                        if (prop != null)
                        {
                            m_maxVisibleAdditionalLights = (int)prop.GetValue(null);
                        }
                    }                                                                                
                }
            }


            public override void Deserialize(BinaryReader binaryReader)
            {                
                base.Deserialize(binaryReader);
                m_maxVisibleAdditionalLights = binaryReader.ReadInt32();
                m_asset = SerializerKun.DesirializeObject<UniversalRenderPipelineAssetKun>(binaryReader);
                mInstance = this;
            }

            public override void Serialize(BinaryWriter binaryWriter)
            {
                base.Serialize(binaryWriter);
                binaryWriter.Write(m_maxVisibleAdditionalLights);
                SerializerKun.Serialize<UniversalRenderPipelineAssetKun>(binaryWriter, m_asset);
            }


            public static void OnMessageEvent(BinaryReader binaryReader)
            {
                instance.Deserialize(binaryReader);
            }
        }



    }

}