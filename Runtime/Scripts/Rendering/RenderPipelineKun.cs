using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utj.UnityChoseKun
{
    namespace Engine.Rendering
    {
        /// <summary>
        /// See "Runtime/Export/RenderPipeline/RenderPipeline.cs "
        /// </summary>
        public class RenderPipelineKun : ISerializerKun
        {
 
            
            public RenderPipelineKun()
            {
                var t = typeof(RenderPipeline);
                var porps = t.GetProperties();
                foreach(var prop in porps) {
                    Debug.Log(prop.Name);
                }

                var fields = t.GetFields(System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic);
                foreach(var field in fields)
                {
                    Debug.Log(field.Name);
                }
            }


            public virtual void Deserialize(BinaryReader binaryReader)
            {
                
            }

            public virtual void Serialize(BinaryWriter binaryWriter)
            {
                
            }
        }
    }
}