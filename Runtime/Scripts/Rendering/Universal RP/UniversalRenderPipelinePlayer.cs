using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    namespace Engine
    {
        namespace Rendering.Universal
        {
            public class UniversalRenderPipelinePlayer
            {
                public static void OnMessageEventPull(BinaryReader binaryReader)
                {
                    if (GraphicsSettingsKun.renderPipelineAssetType == RenderPipelineAssetType.URP)
                    {
                        UnityChoseKunPlayer.SendMessage<UniversalRenderPipelineKun>(UnityChoseKun.MessageID.UniversalRenderPipelinePull, new UniversalRenderPipelineKun(true));
                    }
                }

                public static void OnMessageEventPush(BinaryReader binaryReader)
                {
                    // 
                }
            }
        }
    }
}