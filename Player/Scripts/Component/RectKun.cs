using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class RectKun : ISerializerKun
    {
        [SerializeField] public float x;
        [SerializeField] public float y;
        [SerializeField] public float width;
        [SerializeField] public float height;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public RectKun(float x, float y, float w, float h)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        public RectKun(Rect rect)
        {
            x = rect.x;
            y = rect.y;
            width = rect.width;
            height = rect.height;
        }


        /// <summary>
        /// 
        /// </summary>
        public RectKun() : this(0, 0, 1, 1)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Rect GetRect()
        {
            return new Rect(x, y, width, height);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(x);
            binaryWriter.Write(y);
            binaryWriter.Write(width);
            binaryWriter.Write(height);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            x = binaryReader.ReadSingle();
            y = binaryReader.ReadSingle();
            width = binaryReader.ReadSingle();
            height = binaryReader.ReadSingle();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as RectKun;
            if(other == null)
            {
                return false;
            }
            if (x.Equals(other.x) == false)
            {
                return false;
            }
            if (y.Equals(other.y) == false)
            {
                return false;
            }
            if (width.Equals(other.width) == false)
            {
                return false;
            }
            if (height.Equals(other.height) == false)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
