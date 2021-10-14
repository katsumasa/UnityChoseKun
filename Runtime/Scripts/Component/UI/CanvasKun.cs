using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class CanvasKun : BehaviourKun
    {
        [SerializeField] public RenderMode renderMode;
        [SerializeField] public bool pixelPerfect;
        [SerializeField] public int sortingOrder;
        [SerializeField] public int targetDisplay;
        [SerializeField] public AdditionalCanvasShaderChannels additionalShaderChannels;
        [SerializeField] public CameraKun worldCamera;
        [SerializeField] public int sortingLayerID;
        [SerializeField] public string sortingLayerName;

        public CanvasKun() : this(null) { }

        public CanvasKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.Canvas;
            Canvas canvas = component as Canvas;            
            if (canvas != null)
            {
                renderMode = canvas.renderMode;
                pixelPerfect = canvas.pixelPerfect;
                sortingOrder = canvas.sortingOrder;
                targetDisplay = canvas.targetDisplay;
                additionalShaderChannels = canvas.additionalShaderChannels;
                worldCamera = new CameraKun(canvas.worldCamera);
                sortingLayerID = canvas.sortingLayerID;
                sortingLayerName = canvas.sortingLayerName;
            }
        }


        public override bool WriteBack(Component component)
        {
            var result = base.WriteBack(component);
            var canvas = component as Canvas;
            if (canvas)
            {
                canvas.renderMode = renderMode;
                canvas.pixelPerfect = pixelPerfect;
                canvas.sortingOrder = sortingOrder;
                canvas.targetDisplay = targetDisplay;
                canvas.additionalShaderChannels = additionalShaderChannels;
                
                if(worldCamera != null && worldCamera.instanceID != 0)
                {
                    for(var i = 0; i < Camera.allCameras.Length; i++)
                    {
                        if(Camera.allCameras[i].GetInstanceID() == worldCamera.GetInstanceID())
                        {
                            canvas.worldCamera = Camera.allCameras[i];
                            break;
                        }
                    }                    
                }                
                canvas.sortingLayerID = sortingLayerID;
                return true;
            }

            return result;
        }


        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);

            binaryWriter.Write((int)renderMode);
            binaryWriter.Write(pixelPerfect);
            binaryWriter.Write(sortingOrder);
            binaryWriter.Write(targetDisplay);
            binaryWriter.Write((int)additionalShaderChannels);
            SerializerKun.Serialize<CameraKun>(binaryWriter, worldCamera);
            binaryWriter.Write(sortingLayerID);
            binaryWriter.Write(sortingLayerName);
        }


        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);

            renderMode = (RenderMode)binaryReader.ReadInt32();
            pixelPerfect = binaryReader.ReadBoolean();
            sortingOrder = binaryReader.ReadInt32();
            targetDisplay = binaryReader.ReadInt32();
            additionalShaderChannels = (AdditionalCanvasShaderChannels)binaryReader.ReadInt32();
            worldCamera = SerializerKun.DesirializeObject<CameraKun>(binaryReader);
            sortingLayerID = binaryReader.ReadInt32();
            sortingLayerName = binaryReader.ReadString();
        }

    }
}