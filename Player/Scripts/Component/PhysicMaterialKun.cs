using System.Collections;
using System.Collections.Generic;
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
            bounceCombine = physicMaterial.bounceCombine;
            bounciness = physicMaterial.bounciness;
            dynamicFriction = physicMaterial.dynamicFriction;
            frictionCombine = physicMaterial.frictionCombine;
            staticFriction = physicMaterial.staticFriction;
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
    }
}