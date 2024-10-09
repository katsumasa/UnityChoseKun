using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class PhysicMaterialKun : ObjectKun
    {
#if UNITY_6000_0_OR_NEWER
        [SerializeField] public PhysicsMaterialCombine bounceCombine;
#else
        [SerializeField] public PhysicMaterialCombine bounceCombine;
#endif
        [SerializeField] public float bounciness;
        [SerializeField] public float dynamicFriction;
#if UNITY_6000_0_OR_NEWER
        [SerializeField] public PhysicsMaterialCombine frictionCombine;
#else
        [SerializeField] public PhysicMaterialCombine frictionCombine;
#endif
        [SerializeField] public float staticFriction;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PhysicMaterialKun() : this(null) { }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="physicMaterial">PhysicMaterialオブジェクト</param>
#if UNITY_6000_0_OR_NEWER
        public PhysicMaterialKun(PhysicsMaterial physicMaterial):base(physicMaterial)
#else
        public PhysicMaterialKun(PhysicMaterial physicMaterial):base(physicMaterial)
#endif
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
#if UNITY_6000_0_OR_NEWER
                var physicMaterial = obj as PhysicsMaterial;
#else
                var physicMaterial = obj as PhysicMaterial;
#endif
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
#if UNITY_6000_0_OR_NEWER
            bounceCombine = (PhysicsMaterialCombine)binaryReader.ReadInt32();
#else
            bounceCombine = (PhysicMaterialCombine)binaryReader.ReadInt32();
#endif
            bounciness = binaryReader.ReadSingle();
            dynamicFriction = binaryReader.ReadSingle();
#if UNITY_6000_0_OR_NEWER
            frictionCombine = (PhysicsMaterialCombine)binaryReader.ReadInt32();
#else
            frictionCombine = (PhysicMaterialCombine)binaryReader.ReadInt32();
#endif
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