using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    public class SpriteRendererKun : RendererKun
    {
        [SerializeField] public SpriteKun m_Sprite;
        [SerializeField] public ColorKun m_Color;
        [SerializeField] public bool m_FlipX;
        [SerializeField] public bool m_FlipY;
        [SerializeField] public SpriteDrawMode m_DrawMode;
        [SerializeField] public SpriteMaskInteraction m_MaskInteraction;
        [SerializeField] public SpriteSortPoint m_SpriteSortPoint;



        public SpriteKun sprite
        {
            get { return m_Sprite; }
            set { m_Sprite = value; }
        }

        public ColorKun color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }
        public bool flipX
        {
            get { return m_FlipX; }
            set { m_FlipX = value; }
        }

        public bool flipY
        {
            get { return m_FlipY; }
            set { m_FlipY = value; }
        }

        public SpriteDrawMode drawMode
        {
            get { return m_DrawMode; }
            set { m_DrawMode = value; }
        }

        public SpriteMaskInteraction maskInteraction
        {
            get { return m_MaskInteraction; }
            set { m_MaskInteraction = value; }
        }

        public SpriteSortPoint spriteSortPoint
        {
            get { return m_SpriteSortPoint; }
            set { m_SpriteSortPoint = value; }
        }


        public SpriteRendererKun():this(null)
        {

        }


        public SpriteRendererKun(SpriteRenderer spriteRenderer) : base(spriteRenderer)
        {
            componentKunType = ComponentKunType.SpriteRenderer;
            if (spriteRenderer != null)
            {
                sprite = new SpriteKun(spriteRenderer.sprite);
                color = new ColorKun(spriteRenderer.color);
                flipX = spriteRenderer.flipX;
                flipY = spriteRenderer.flipY;
                drawMode = spriteRenderer.drawMode;
                maskInteraction = spriteRenderer.maskInteraction;
                spriteSortPoint = spriteRenderer.spriteSortPoint;
            }
        }


        public override bool WriteBack(Component component)
        {
            Debug.Log("WriteBack");
            base.WriteBack(component);

            var spriteRenderer = component as SpriteRenderer;
            if (spriteRenderer)
            {
                if (spriteRenderer.sprite.GetInstanceID() != sprite.GetInstanceID())
                {
                    var tmp = SpriteKun.GetSprite(sprite.instanceID);
                    if (tmp != null)
                    {
                        spriteRenderer.sprite = tmp;
                    }
                }
                spriteRenderer.color = color.GetColor();
                spriteRenderer.flipX = flipX;
                spriteRenderer.flipY = flipY;
                spriteRenderer.drawMode = drawMode;
                spriteRenderer.maskInteraction = maskInteraction;
                spriteRenderer.spriteSortPoint = spriteSortPoint;


                Debug.Log("WriteBack");

                return true;
            }

            return false;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<SpriteKun>(binaryWriter, m_Sprite);
            SerializerKun.Serialize<ColorKun>(binaryWriter, m_Color);
            binaryWriter.Write(m_FlipX);
            binaryWriter.Write(m_FlipY);
            binaryWriter.Write((int)m_DrawMode);
            binaryWriter.Write((int)m_MaskInteraction);
            binaryWriter.Write((int)m_SpriteSortPoint);

        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Sprite = SerializerKun.DesirializeObject<SpriteKun>(binaryReader);
            m_Color = SerializerKun.DesirializeObject<ColorKun>(binaryReader);
            m_FlipX = binaryReader.ReadBoolean();
            m_FlipY = binaryReader.ReadBoolean();
            m_DrawMode = (SpriteDrawMode)binaryReader.ReadInt32();
            m_MaskInteraction = (SpriteMaskInteraction)binaryReader.ReadInt32();
            m_SpriteSortPoint = (SpriteSortPoint)binaryReader.ReadInt32();
        }
    }
}