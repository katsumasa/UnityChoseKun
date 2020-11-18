using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class PhysicMaterialKun : ObjectKun
    {
        [SerializeField] public PhysicMaterialCombine bounceCombine;
        [SerializeField] public float bounciness;
        [SerializeField] public float dynamicFriction;
        [SerializeField] public PhysicMaterialCombine frictionCombine;
        [SerializeField] public float staticFriction;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PhysicMaterialKun() : this(null) { }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="physicMaterial">PhysicMaterialオブジェクト</param>
        public PhysicMaterialKun(PhysicMaterial physicMaterial):base(physicMaterial)
        {
            if (physicMaterial != null)
            {
                bounceCombine = physicMaterial.bounceCombine;
                bounciness = physicMaterial.bounciness;
                dynamicFriction = physicMaterial.dynamicFriction;
                frictionCombine = physicMaterial.frictionCombine;
                staticFriction = physicMaterial.staticFriction;
            }
        }


        /// <summary>
        /// 内容を書き戻す
        /// </summary>
        /// <param name="obj">PhysicMaterialオブジェクト</param>
        /// <returns></returns>
        public override bool WriteBack(Object obj)
        {
            if (WriteBack(obj))
            {
                var physicMaterial = obj as PhysicMaterial;
                if (physicMaterial)
                {
                    physicMaterial.bounceCombine = bounceCombine;
                    physicMaterial.bounciness = bounciness;
                    physicMaterial.dynamicFriction = dynamicFriction;
                    physicMaterial.frictionCombine = frictionCombine;
                    physicMaterial.staticFriction = staticFriction;
                    return true;
                }                
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write((int)bounceCombine);
            binaryWriter.Write(bounciness);
            binaryWriter.Write(dynamicFriction);
            binaryWriter.Write((int)frictionCombine);
            binaryWriter.Write(staticFriction);
        }


        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            bounceCombine = (PhysicMaterialCombine)binaryReader.ReadInt32();
            bounciness = binaryReader.ReadSingle();
            dynamicFriction = binaryReader.ReadSingle();
            frictionCombine = (PhysicMaterialCombine)binaryReader.ReadInt32();
            staticFriction = binaryReader.ReadSingle();
        }


        public override bool Equals(object obj)
        {
            var other = obj as PhysicMaterialKun;
            if(other == null)
            {
                return false;
            }
            if (!bounceCombine.Equals(other.bounceCombine))
            {
                return false;
            }
            if (!bounciness.Equals(other.bounciness))
            {
                return false;
            }
            if (!dynamicFriction.Equals(other.dynamicFriction))
            {
                return false;
            }
            if (!frictionCombine.Equals(other.frictionCombine))
            {
                return false;
            }
            if (!staticFriction.Equals(other.staticFriction))
            {
                return false;
            }
            return base.Equals(obj);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}