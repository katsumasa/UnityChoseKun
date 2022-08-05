using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utj.UnityChoseKun.Engine
{
    namespace Rendering
    {
#if UNITY_2021_2_OR_NEWER
        public class RenderPipelineGlobalSettingsKun : ObjectKun
        {
            public RenderPipelineGlobalSettingsKun(RenderPipelineGlobalSettings renderPipelineGlobalSettings):base(renderPipelineGlobalSettings)
            {
                if(renderPipelineGlobalSettings == null)
                {
                    return;
                }
            }            
        }
#endif
    }
}