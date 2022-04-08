using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utj.UnityChoseKun.Engine
{
    namespace Rendering
    {
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
    }
}