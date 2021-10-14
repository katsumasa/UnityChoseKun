using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class Vector4Kun : Vector3Kun
    {        
        [SerializeField] public float w;


        /// <summary>
        /// 
        /// </summary>
        public Vector4Kun() : this(0, 0, 0, 0) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4Kun(float x, float y, float z, float w):base(x,y,x)
        {        
            this.w = w;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="v4"></param>
        public Vector4Kun(Vector4 v4) : this(v4.x, v4.y, v4.z, v4.w) { }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector4 GetVector4()
        {
            return new Vector4(x, y, x, w);
        }


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(w);
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            w = binaryReader.ReadSingle();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            Vector4Kun v4 = other as Vector4Kun;
            if (other == null)
            {
                return false;
            }
            if (w != v4.w)
            {
                return false;
            }
            return base.Equals(other);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        /// <summary>
        /// Allocater
        /// </summary>
        /// <returns></returns>
        public static  new Vector4Kun Allocater()
        {
            return new Vector4Kun();
        }


        public static new Vector4Kun[] Allocater(int len)
        {
            return new Vector4Kun[len];
        }
    }

}