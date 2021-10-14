using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    // <summary>
    // SerializeObject for Vector2
    // </summary>
    [System.Serializable]
    public class Vector2Kun : ISerializerKun
    {
        [SerializeField]public float x;
        [SerializeField]public float y;


        /// <summary>
        /// 
        /// </summary>
        public Vector2Kun():this(0,0) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2Kun(float x,float y)
        {
            this.x = x;
            this.y = y;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="v2"></param>
        public Vector2Kun(Vector2 v2) : this(v2.x, v2.y) { }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector2 GetVector2()
        {
            return new Vector2(x,y);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(x);
            binaryWriter.Write(y);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            x = binaryReader.ReadSingle();
            y = binaryReader.ReadSingle();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            Vector2Kun v2 = other as Vector2Kun;
            if(other == null)
            {
                return false;
            }
            if(x != v2.x || y != v2.y)
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


        /// <summary>
        /// Vector2Kunのアロケーター
        /// </summary>
        /// <returns></returns>
        public static Vector2Kun Allocater()
        {
            return new Vector2Kun();
        }


        /// <summary>
        /// Vector2Kunの配列を確保する
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static Vector2Kun[] Allocater(int len)
        {
            return new Vector2Kun[len];
        }
    }
}
