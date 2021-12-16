using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;


namespace Utj.UnityChoseKun
{
    public class RenderPipelineAssetKun : ISerializerKun
    {

        [SerializeField] public string[] renderingLayerMaskNames;

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


        public RenderPipelineAssetKun(RenderPipelineAsset renderPipelineAsset)
        {
            if (renderPipelineAsset)
            {
                renderingLayerMaskNames         = renderPipelineAsset.renderingLayerMaskNames;
                defaultMaterial                 = new MaterialKun(renderPipelineAsset.defaultMaterial);
                autodeskInteractiveShader       = new ShaderKun(renderPipelineAsset.autodeskInteractiveShader);
                autodeskInteractiveTransparentShader = new ShaderKun(renderPipelineAsset.autodeskInteractiveTransparentShader);
                autodeskInteractiveMaskedShader = new ShaderKun(renderPipelineAsset.autodeskInteractiveMaskedShader);
                terrainDetailLitShader          = new ShaderKun(renderPipelineAsset.terrainDetailLitShader);
                terrainDetailGrassShader        = new ShaderKun(renderPipelineAsset.terrainDetailGrassShader);
                terrainDetailGrassBillboardShader = new ShaderKun(renderPipelineAsset.terrainDetailGrassBillboardShader);
                defaultParticleMaterial         = new MaterialKun(renderPipelineAsset.defaultParticleMaterial);
                defaultLineMaterial             = new MaterialKun(renderPipelineAsset.defaultLineMaterial);
                defaultTerrainMaterial          = new MaterialKun(renderPipelineAsset.defaultTerrainMaterial);
                defaultUIMaterial               = new MaterialKun(renderPipelineAsset.defaultUIMaterial);
                defaultUIOverdrawMaterial       = new MaterialKun(renderPipelineAsset.defaultUIOverdrawMaterial);
                defaultUIETC1SupportedMaterial  = new MaterialKun(renderPipelineAsset.defaultUIETC1SupportedMaterial);
                default2DMaterial               = new MaterialKun(renderPipelineAsset.default2DMaterial);
                defaultShader                   = new ShaderKun(renderPipelineAsset.defaultShader);
                defaultSpeedTree7Shader         = new ShaderKun(renderPipelineAsset.defaultSpeedTree7Shader);
                defaultSpeedTree8Shader         = new ShaderKun(renderPipelineAsset.defaultSpeedTree8Shader);
            }
        }

        public void WriteBack()
        {            
        }


        public void Deserialize(BinaryReader binaryReader)
        {
            renderingLayerMaskNames = SerializerKun.DesirializeStrings(binaryReader);
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
    

        public void Serialize(BinaryWriter binaryWriter)
        {
            SerializerKun.Serialize(binaryWriter, renderingLayerMaskNames);
            SerializerKun.Serialize<MaterialKun>(binaryWriter,defaultMaterial);
            SerializerKun.Serialize<ShaderKun>(binaryWriter,autodeskInteractiveShader);
            SerializerKun.Serialize<ShaderKun>(binaryWriter,autodeskInteractiveTransparentShader);
            SerializerKun.Serialize<ShaderKun>(binaryWriter,autodeskInteractiveMaskedShader);
            SerializerKun.Serialize<ShaderKun>(binaryWriter,terrainDetailLitShader);
            SerializerKun.Serialize<ShaderKun>(binaryWriter,terrainDetailGrassShader);
            SerializerKun.Serialize<ShaderKun>(binaryWriter,terrainDetailGrassBillboardShader);
            SerializerKun.Serialize<MaterialKun>(binaryWriter,defaultParticleMaterial);
            SerializerKun.Serialize<MaterialKun>(binaryWriter,defaultLineMaterial);
            SerializerKun.Serialize<MaterialKun>(binaryWriter,defaultTerrainMaterial);
            SerializerKun.Serialize<MaterialKun>(binaryWriter,defaultUIMaterial);
            SerializerKun.Serialize<MaterialKun>(binaryWriter,defaultUIOverdrawMaterial);
            SerializerKun.Serialize<MaterialKun>(binaryWriter,defaultUIETC1SupportedMaterial);
            SerializerKun.Serialize<MaterialKun>(binaryWriter,default2DMaterial);
            SerializerKun.Serialize<ShaderKun>(binaryWriter,defaultShader);
            SerializerKun.Serialize<ShaderKun>(binaryWriter, defaultSpeedTree7Shader);
            SerializerKun.Serialize<ShaderKun>(binaryWriter,defaultSpeedTree8Shader);
        }
    }
}