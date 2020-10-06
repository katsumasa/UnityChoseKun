using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public struct Matrix4x4Kun
    {
        [SerializeField] Vector4Kun[] columns;

        public Matrix4x4Kun(Vector4Kun column0, Vector4Kun column1, Vector4Kun column2, Vector4Kun column3)
        {
            columns = new Vector4Kun[4];
            columns[0] = column0;
            columns[1] = column1;
            columns[2] = column2;
            columns[3] = column3;
        }

        public Matrix4x4Kun(Matrix4x4 matrix4X4):this(new Vector4Kun(matrix4X4.GetRow(0)), new Vector4Kun(matrix4X4.GetRow(1)), new Vector4Kun(matrix4X4.GetRow(2)), new Vector4Kun(matrix4X4.GetRow(3))) { }
        
        public Matrix4x4 GetMatrix4X4()
        {
            return new Matrix4x4(
                columns[0].GetVector4(),
                columns[1].GetVector4(),
                columns[2].GetVector4(),
                columns[3].GetVector4());

        }
    }
}
