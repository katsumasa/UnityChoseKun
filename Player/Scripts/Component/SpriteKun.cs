using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class SpriteKun : ObjectKun
    {
        static Sprite[] m_Sprites;

        public static Sprite GetSprite(int instanceID)
        {
            if (m_Sprites != null)
            {
                foreach (var sprite in m_Sprites)
                {
                    if (sprite.GetInstanceID() == instanceID)
                    {
                        return sprite;
                    }
                }
            }
            return null;
        }


        public SpriteKun(Sprite sprite) : base(sprite)
        {
            if (sprite != null)
            {
                name = sprite.name;
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
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
        }
    }
}