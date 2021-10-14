using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class Matrix4x4Kun:ISerializerKun
    {
        [SerializeField] Vector4Kun[] columns;


        /// <summary>
        /// 
        /// </summary>
        public Matrix4x4Kun():this(new Vector4Kun(1,0,0,0),new Vector4Kun(0,1,0,0),new Vector4Kun(0,0,1,0),new Vector4Kun(0,0,0,0))
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="column0"></param>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <param name="column3"></param>
        public Matrix4x4Kun(Vector4Kun column0, Vector4Kun column1, Vector4Kun column2, Vector4Kun column3)
        {
            columns = new Vector4Kun[4];
            columns[0] = column0;
            columns[1] = column1;
            columns[2] = column2;
            columns[3] = column3;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix4X4"></param>
        public Matrix4x4Kun(Matrix4x4 matrix4X4):this(new Vector4Kun(matrix4X4.GetRow(0)), new Vector4Kun(matrix4X4.GetRow(1)), new Vector4Kun(matrix4X4.GetRow(2)), new Vector4Kun(matrix4X4.GetRow(3))) { }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Matrix4x4 GetMatrix4X4()
        {
            return new Matrix4x4(
                columns[0].GetVector4(),
                columns[1].GetVector4(),
                columns[2].GetVector4(),
                columns[3].GetVector4()
                );
        }


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            SerializerKun.Serialize<Vector4Kun>(binaryWriter, columns);
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            columns = (Vector4Kun[])SerializerKun.DesirializeObjects<Vector4Kun>(binaryReader);            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as Matrix4x4Kun;
            if(other == null)
            {
                return false;
            }
            for (var i = 0; i < 4; i++)
            {
                if (columns[i].Equals(other.columns[i]) == false)
                {
                    return false;
                }
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static Matrix4x4Kun Allocater()
        {
            return new Matrix4x4Kun();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static Matrix4x4Kun[] Allocater(int len)
        {
            return new Matrix4x4Kun[len];
        }
    }
}
