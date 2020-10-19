using System.Collections;
using System.Collections.Generic;


using UnityEngine;
namespace Utj.UnityChoseKun
{    
    [System.Serializable]
    public class RigidbodyKun : ComponentKun
    {
        
        [SerializeField] public float mass;
        [SerializeField] public float drag;
        [SerializeField] public float angularDrag;
        [SerializeField] public bool useGravity;
        [SerializeField] public bool isKinematic;
        [SerializeField] public RigidbodyInterpolation interpolation;
        [SerializeField] public CollisionDetectionMode collisionDetectionMode;
        [SerializeField] public RigidbodyConstraints constraints;

        public RigidbodyKun() : base() {
            componentKunType = ComponentKunType.Rigidbody;
        }


        public RigidbodyKun(Rigidbody rigidbody):this()
        {
            
            mass = rigidbody.mass;
            drag = rigidbody.drag;
            angularDrag = rigidbody.angularDrag;
            useGravity = rigidbody.useGravity;
            isKinematic = rigidbody.isKinematic;
            interpolation = rigidbody.interpolation;
            collisionDetectionMode = rigidbody.collisionDetectionMode;
            constraints = rigidbody.constraints;
        }


        public override bool WriteBack(Component component)
        {
            if (base.WriteBack(component))
            {
                Rigidbody rigidbody = component as Rigidbody;
                rigidbody.mass = mass;
                rigidbody.drag = drag;
                rigidbody.angularDrag = angularDrag;
                rigidbody.useGravity = useGravity;
                rigidbody.isKinematic = isKinematic;
                rigidbody.interpolation = interpolation;
                rigidbody.collisionDetectionMode = this.collisionDetectionMode;
                rigidbody.constraints = this.constraints;
                return true;
            }
            return false;
        }
    }
}