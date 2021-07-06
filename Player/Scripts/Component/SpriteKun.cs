using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class SpriteKun : ObjectKun
    {        
        [SerializeField] public Vector2Kun pivot;
        [SerializeField] public Vector4Kun border;

        


        public SpriteKun(Sprite sprite) : base(sprite)
        {
            if (sprite != null)
            {
                name = sprite.name;
                pivot = new Vector2Kun(sprite.pivot);
                border = new Vector4Kun(sprite.border);
            } 
            else
            {
                pivot = new Vector2Kun();
                border = new Vector4Kun();
            }
        }

        public SpriteKun() : base(null) { }

        public override bool WriteBack(UnityEngine.Object obj)
        {
            return base.WriteBack(obj);                        
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<Vector2Kun>(binaryWriter, pivot);
            SerializerKun.Serialize<Vector4Kun>(binaryWriter, border);            
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            pivot = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
            border = SerializerKun.DesirializeObject<Vector4Kun>(binaryReader);
        }
    }
}