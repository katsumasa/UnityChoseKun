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

        public PhysicMaterialKun() : base()
        {            
        }

        public PhysicMaterialKun(PhysicMaterial physicMaterial):this()
        {
            bounceCombine = physicMaterial.bounceCombine;
            bounciness = physicMaterial.bounciness;
            dynamicFriction = physicMaterial.dynamicFriction;
            frictionCombine = physicMaterial.frictionCombine;
            staticFriction = physicMaterial.staticFriction;
        }

        public override bool WriteBack(Object obj)
        {
            if (dirty)
            {
                WriteBack(obj);
                var physicMaterial = obj as PhysicMaterial;
                if (physicMaterial)
                {
                    physicMaterial.bounceCombine = bounceCombine;
                    physicMaterial.bounciness = bounciness;
                    physicMaterial.dynamicFriction = dynamicFriction;
                    physicMaterial.frictionCombine = frictionCombine;
                    physicMaterial.staticFriction = staticFriction;
                }

                return true;
            }
            return false;
        }
    }
}