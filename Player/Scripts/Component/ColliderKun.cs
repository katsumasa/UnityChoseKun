using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class ColliderKun : ComponentKun
    {        
        [SerializeField] BoundsKun m_bounds;
        public BoundsKun boundsKun
        {
            get { return m_bounds; }
            private set { m_bounds = value; }
        }


        [SerializeField] public float contactOffset;
        [SerializeField] public bool enabled;
        [SerializeField] public bool isTrigger;
        [SerializeField] public string material;


        public ColliderKun() : this(null) { }


        public ColliderKun(Collider collider) : base()
        {
            componentKunType = ComponentKunType.Collider;
            if (collider != null)
            {
                boundsKun = new BoundsKun(collider.bounds);
                contactOffset = collider.contactOffset;
                enabled = collider.enabled;
                isTrigger = collider.isTrigger;
                material = collider.material.name;
            }
        }

        public override bool WriteBack(Component component)
        {
            if (dirty)
            {
                base.WriteBack(component);

                var collider = component as Collider;
                collider.contactOffset = contactOffset;
                collider.enabled = enabled;
                collider.isTrigger = isTrigger;                
                return true;
            }
            return false;
        }
    }


    [System.Serializable]
    public class CapsuleColliderKun : ColliderKun
    {
        [SerializeField] Vector3Kun m_center;
        public Vector3 center
        {
            get { return m_center.GetVector3(); }
            set { m_center = new Vector3Kun(value); }
        }
        [SerializeField] public int direction;
        [SerializeField] public float height;
        [SerializeField] public float radius;


        public CapsuleColliderKun() : this(null) { }                


        public CapsuleColliderKun(Collider collider) : base(collider)
        {

            componentKunType = ComponentKunType.CapsuleCollider;
            if (collider != null)
            {
                CapsuleCollider capsuleCollider = collider as CapsuleCollider;
                direction = capsuleCollider.direction;
                height = capsuleCollider.height;
                radius = capsuleCollider.radius;
            }
        }

        public override bool WriteBack(Component component)
        {
            if (dirty)
            {
                base.WriteBack(component);
                var capsuleCollider = component as CapsuleCollider;
                if (capsuleCollider)
                {
                    capsuleCollider.center = center;
                    capsuleCollider.direction = direction;
                    capsuleCollider.height = height;
                    capsuleCollider.radius = radius;
                }
                return true;
            }
            return false;
        }
    }


    [System.Serializable]
    public class MeshColliderKun : ColliderKun
    {
        [SerializeField] public bool convex;
        [SerializeField] public MeshColliderCookingOptions cookingOptions;
        [SerializeField] public string sharedMesh;

        public MeshColliderKun() : this(null) { }
        
        

        public MeshColliderKun(Collider collider) : base(collider)
        {
            componentKunType = ComponentKunType.MeshCollider;
            if (collider)
            {
                MeshCollider meshCollider = collider as MeshCollider;
                convex = meshCollider.convex;
                cookingOptions = meshCollider.cookingOptions;
                sharedMesh = meshCollider.sharedMesh.name;
            }
        }

        public override bool WriteBack(Component component)
        {
            if (dirty)
            {
                base.WriteBack(component);
                var meshCollider = component as MeshCollider;
                if (meshCollider)
                {
                    meshCollider.convex = convex;
                    meshCollider.cookingOptions = cookingOptions;
                }
                return true;
            }
            return false;
        }

    }
}