using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class Vector3Kun : Vector2Kun
    {        
        [SerializeField] public float z;


        /// <summary>
        /// 
        /// </summary>
        public Vector3Kun() : this(0, 0, 0) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3Kun(float x, float y, float z) : base(x,y)
        {            
            this.z = z;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="v3"></param>
        public Vector3Kun(Vector3 v3) : this(v3.x, v3.y, v3.z) { }                


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector3 GetVector3()
        {
            return new Vector3(x, y, z);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(z);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);            
            z = binaryReader.ReadSingle();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            Vector3Kun v3 = other as Vector3Kun;
            if (other == null)
            {
                return false;
            }
            if (z != v3.z)
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static new Vector3Kun Allocater()
        {
            return new Vector3Kun();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static new Vector3Kun[] Allocater(int len)
        {
            return new Vector3Kun[len];
        }
    }
}