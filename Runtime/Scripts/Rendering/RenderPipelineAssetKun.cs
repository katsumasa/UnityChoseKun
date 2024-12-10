using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
//
// Katsumasa.Kimura
//
namespace Utj.UnityChoseKun
{
    namespace Engine
    {
        namespace Rendering
        {
            public enum RenderPipelineAssetType
            {
                NONE,
                RP,
                URP,
                HDRP,
            }


            /// <summary>
            /// Class <c>RenderPipelineAssetKun</c>
            /// </summary>
            public class RenderPipelineAssetKun : ScriptableObjectKun
            {
#if !UNITY_6000_0_OR_NEWER
                [SerializeField] public string[] renderingLayerMaskNames;
#endif
                [SerializeField] public MaterialKun defaultMaterial;

                [SerializeField] public ShaderKun autodeskInteractiveShader;

                [SerializeField] public ShaderKun autodeskInteractiveTransparentShader;

                [SerializeField] public ShaderKun autodeskInteractiveMaskedShader;

                [SerializeField] public ShaderKun terrainDetailLitShader;

                [SerializeField] public ShaderKun terrainDetailGrassShader;

                [SerializeField] public ShaderKun terrainDetailGrassBillboardShader;

                [SerializeField] public MaterialKun defaultParticleMaterial;

                [SerializeField] public MaterialKun defaultLineMaterial;

                [SerializeField] public MaterialKun defaultTerrainMaterial;

                [SerializeField] public MaterialKun defaultUIMaterial;

                [SerializeField] public MaterialKun defaultUIOverdrawMaterial;

                [SerializeField] public MaterialKun defaultUIETC1SupportedMaterial;

                [SerializeField] public MaterialKun default2DMaterial;

                [SerializeField] public ShaderKun defaultShader;

                [SerializeField] public ShaderKun defaultSpeedTree7Shader;

                [SerializeField] public ShaderKun defaultSpeedTree8Shader;


                public RenderPipelineAssetKun() : this(null)
                {
                }


                public RenderPipelineAssetKun(RenderPipelineAsset renderPipelineAsset) : base(renderPipelineAsset)
                {
                    if (renderPipelineAsset)
                    {
#if !UNITY_6000_0_OR_NEWER
                        renderingLayerMaskNames = renderPipelineAsset.renderingLayerMaskNames;
#endif
                        defaultMaterial = new MaterialKun(renderPipelineAsset.defaultMaterial);
                        autodeskInteractiveShader = new ShaderKun(renderPipelineAsset.autodeskInteractiveShader);
                        autodeskInteractiveTransparentShader = new ShaderKun(renderPipelineAsset.autodeskInteractiveTransparentShader);
                        autodeskInteractiveMaskedShader = new ShaderKun(renderPipelineAsset.autodeskInteractiveMaskedShader);
                        terrainDetailLitShader = new ShaderKun(renderPipelineAsset.terrainDetailLitShader);
                        terrainDetailGrassShader = new ShaderKun(renderPipelineAsset.terrainDetailGrassShader);
                        terrainDetailGrassBillboardShader = new ShaderKun(renderPipelineAsset.terrainDetailGrassBillboardShader);
                        defaultParticleMaterial = new MaterialKun(renderPipelineAsset.defaultParticleMaterial);
                        defaultLineMaterial = new MaterialKun(renderPipelineAsset.defaultLineMaterial);
                        defaultTerrainMaterial = new MaterialKun(renderPipelineAsset.defaultTerrainMaterial);
                        defaultUIMaterial = new MaterialKun(renderPipelineAsset.defaultUIMaterial);
                        defaultUIOverdrawMaterial = new MaterialKun(renderPipelineAsset.defaultUIOverdrawMaterial);
                        defaultUIETC1SupportedMaterial = new MaterialKun(renderPipelineAsset.defaultUIETC1SupportedMaterial);
                        default2DMaterial = new MaterialKun(renderPipelineAsset.default2DMaterial);
                        defaultShader = new ShaderKun(renderPipelineAsset.defaultShader);
                        defaultSpeedTree7Shader = new ShaderKun(renderPipelineAsset.defaultSpeedTree7Shader);
                        defaultSpeedTree8Shader = new ShaderKun(renderPipelineAsset.defaultSpeedTree8Shader);
                    }
                }

                public virtual void WriteBack(RenderPipelineAsset renderPipelineAsset)
                {
                    base.WriteBack(renderPipelineAsset);
                }


                public override void Deserialize(BinaryReader binaryReader)
                {
                    base.Deserialize(binaryReader);
#if !UNITY_6000_0_OR_NEWER
                    renderingLayerMaskNames = SerializerKun.DesirializeStrings(binaryReader);
#endif
                    defaultMaterial = SerializerKun.DesirializeObject<MaterialKun>(binaryReader);
                    autodeskInteractiveShader = SerializerKun.DesirializeObject<ShaderKun>(binaryReader);
                    autodeskInteractiveTransparentShader = SerializerKun.DesirializeObject<ShaderKun>(binaryReader);
                    autodeskInteractiveMaskedShader = SerializerKun.DesirializeObject<ShaderKun>(binaryReader);
                    terrainDetailLitShader = SerializerKun.DesirializeObject<ShaderKun>(binaryReader);
                    terrainDetailGrassShader = SerializerKun.DesirializeObject<ShaderKun>(binaryReader);
                    terrainDetailGrassBillboardShader = SerializerKun.DesirializeObject<ShaderKun>(binaryReader);
                    defaultParticleMaterial = SerializerKun.DesirializeObject<MaterialKun>(binaryReader);
                    defaultLineMaterial = SerializerKun.DesirializeObject<MaterialKun>(binaryReader);
                    defaultTerrainMaterial = SerializerKun.DesirializeObject<MaterialKun>(binaryReader);
                    defaultUIMaterial = SerializerKun.DesirializeObject<MaterialKun>(binaryReader);
                    defaultUIOverdrawMaterial = SerializerKun.DesirializeObject<MaterialKun>(binaryReader);
                    defaultUIETC1SupportedMaterial = SerializerKun.DesirializeObject<MaterialKun>(binaryReader);
                    default2DMaterial = SerializerKun.DesirializeObject<MaterialKun>(binaryReader);
                    defaultShader = SerializerKun.DesirializeObject<ShaderKun>(binaryReader);
                    defaultSpeedTree7Shader = SerializerKun.DesirializeObject<ShaderKun>(binaryReader);
                    defaultSpeedTree8Shader = SerializerKun.DesirializeObject<ShaderKun>(binaryReader);
                }


                public override void Serialize(BinaryWriter binaryWriter)
                {
                    base.Serialize(binaryWriter);
#if !UNITY_6000_0_OR_NEWER
                    SerializerKun.Serialize(binaryWriter, renderingLayerMaskNames);
#endif
                    SerializerKun.Serialize<MaterialKun>(binaryWriter, defaultMaterial);
                    SerializerKun.Serialize<ShaderKun>(binaryWriter, autodeskInteractiveShader);
                    SerializerKun.Serialize<ShaderKun>(binaryWriter, autodeskInteractiveTransparentShader);
                    SerializerKun.Serialize<ShaderKun>(binaryWriter, autodeskInteractiveMaskedShader);
                    SerializerKun.Serialize<ShaderKun>(binaryWriter, terrainDetailLitShader);
                    SerializerKun.Serialize<ShaderKun>(binaryWriter, terrainDetailGrassShader);
                    SerializerKun.Serialize<ShaderKun>(binaryWriter, terrainDetailGrassBillboardShader);
                    SerializerKun.Serialize<MaterialKun>(binaryWriter, defaultParticleMaterial);
                    SerializerKun.Serialize<MaterialKun>(binaryWriter, defaultLineMaterial);
                    SerializerKun.Serialize<MaterialKun>(binaryWriter, defaultTerrainMaterial);
                    SerializerKun.Serialize<MaterialKun>(binaryWriter, defaultUIMaterial);
                    SerializerKun.Serialize<MaterialKun>(binaryWriter, defaultUIOverdrawMaterial);
                    SerializerKun.Serialize<MaterialKun>(binaryWriter, defaultUIETC1SupportedMaterial);
                    SerializerKun.Serialize<MaterialKun>(binaryWriter, default2DMaterial);
                    SerializerKun.Serialize<ShaderKun>(binaryWriter, defaultShader);
                    SerializerKun.Serialize<ShaderKun>(binaryWriter, defaultSpeedTree7Shader);
                    SerializerKun.Serialize<ShaderKun>(binaryWriter, defaultSpeedTree8Shader);
                }

                public override bool Equals(object obj)
                {
                    if (obj == null)
                    {
                        return false;
                    }
                    if (base.Equals(obj) == false)
                    {
                        return false;
                    }
                    var clone = obj as RenderPipelineAssetKun;
                    if (clone == null)
                    {
                        return false;
                    }
#if !UNITY_6000_0_OR_NEWER
                    if (renderingLayerMaskNames.Length != clone.renderingLayerMaskNames.Length)
                    {
                        return false;
                    }

                    for (var i = 0; i < renderingLayerMaskNames.Length; i++)
                    {
                        if (renderingLayerMaskNames[i] != clone.renderingLayerMaskNames[i])
                        {
                            return false;
                        }
                    }
#endif

                    if (defaultMaterial.Equals(clone.defaultMaterial) == false)
                    {
                        return false;
                    }
                    if (autodeskInteractiveShader.Equals(clone.autodeskInteractiveShader) == false)
                    {
                        return false;
                    }
                    if (autodeskInteractiveTransparentShader.Equals(clone.autodeskInteractiveTransparentShader) == false)
                    {
                        return false;
                    }
                    if (autodeskInteractiveMaskedShader.Equals(clone.autodeskInteractiveMaskedShader) == false)
                    {
                        return false;
                    }
                    if (terrainDetailLitShader.Equals(clone.terrainDetailLitShader) == false)
                    {
                        return false;
                    }
                    if (terrainDetailGrassShader.Equals(clone.terrainDetailGrassShader) == false)
                    {
                        return false;
                    }
                    if (terrainDetailGrassBillboardShader.Equals(clone.terrainDetailGrassBillboardShader) == false)
                    {
                        return false;
                    }
                    if (defaultParticleMaterial.Equals(clone.defaultParticleMaterial) == false)
                    {
                        return false;
                    }
                    if (defaultLineMaterial.Equals(clone.defaultLineMaterial) == false)
                    {
                        return false;
                    }
                    if (defaultTerrainMaterial.Equals(clone.defaultTerrainMaterial) == false)
                    {
                        return false;
                    }
                    if (defaultUIMaterial.Equals(clone.defaultUIMaterial) == false)
                    {
                        return false;
                    }
                    if (defaultUIOverdrawMaterial.Equals(clone.defaultUIOverdrawMaterial) == false)
                    {
                        return false;
                    }
                    if (defaultUIETC1SupportedMaterial.Equals(clone.defaultUIETC1SupportedMaterial) == false)
                    {
                        return false;
                    }
                    if (default2DMaterial.Equals(clone.default2DMaterial) == false)
                    {
                        return false;
                    }
                    if (defaultShader.Equals(clone.defaultShader) == false)
                    {
                        return false;
                    }
                    if (defaultSpeedTree7Shader.Equals(clone.defaultSpeedTree7Shader) == false)
                    {
                        return false;
                    }
                    if (defaultSpeedTree8Shader.Equals(clone.defaultSpeedTree8Shader) == false)
                    {
                        return false;
                    }
                    return true;
                }


                public override int GetHashCode()
                {
                    return base.GetHashCode();
                }
            }
        }
    }
}