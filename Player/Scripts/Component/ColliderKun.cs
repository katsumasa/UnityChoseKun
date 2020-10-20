using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    /// <summary>
    /// Colliderオブジェクトをシリアライズ/デシリアライズする為のクラス
    /// Programed by Katsumasa.Kimura
    /// </summary>
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


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ColliderKun() : this(null) { }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collider">元となるColliderオブジェクト</param>
        public ColliderKun(Collider collider) : base(collider)
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

        /// <summary>
        /// ColliderKunの内容を書き戻す
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool WriteBack(Component component)
        {
            if (base.WriteBack(component))
            {            
                var collider = component as Collider;
                collider.contactOffset = contactOffset;
                collider.enabled = enabled;
                collider.isTrigger = isTrigger;                
                return true;
            }
            return false;
        }
    }


    /// <summary>
    /// CapsuleColliderをシリアライズ・デシリアライズする為のClass
    /// Programed by Katsumasa.Kimura
    /// </summary>
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


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CapsuleColliderKun() : this(null) { }                


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collider">元となるCapuselCollider</param>
        public CapsuleColliderKun(Collider collider) : base(collider)
        {
            componentKunType = ComponentKunType.CapsuleCollider;
            var capsuleCollider = collider as CapsuleCollider;
            if (capsuleCollider != null)
            {
                
                direction = capsuleCollider.direction;
                height = capsuleCollider.height;
                radius = capsuleCollider.radius;
            }
        }


        /// <summary>
        /// CapuselColliderKunの内容をCapuselColliderへ書き戻す
        /// </summary>
        /// <param name="component">CapuselColliderオブジェクト</param>
        /// <returns></returns>
        public override bool WriteBack(Component component)
        {
            if (base.WriteBack(component))
            {
                var capsuleCollider = component as CapsuleCollider;
                if (capsuleCollider)
                {
                    capsuleCollider.center = center;
                    capsuleCollider.direction = direction;
                    capsuleCollider.height = height;
                    capsuleCollider.radius = radius;
                    return true;
                }
            }
            return false;
        }
    }



    /// <summary>
    /// MeshColliderをシリアライズ・デシリアライズする為のClass
    /// </summary>
    [System.Serializable]
    public class MeshColliderKun : ColliderKun
    {
        [SerializeField] public bool convex;
        [SerializeField] public MeshColliderCookingOptions cookingOptions;
        [SerializeField] public string sharedMesh;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MeshColliderKun() : this(null) { }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collider">MeshColliderオブジェクト</param>
        public MeshColliderKun(Collider collider) : base(collider)
        {
            componentKunType = ComponentKunType.MeshCollider;
            MeshCollider meshCollider = collider as MeshCollider;
            if (meshCollider)
            {
                convex = meshCollider.convex;
                cookingOptions = meshCollider.cookingOptions;
                sharedMesh = meshCollider.sharedMesh.name;
            }
        }


        /// <summary>
        /// MeshColliderKunの内容をMeshColliderへ書き戻す
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool WriteBack(Component component)
        {
            if (base.WriteBack(component))
            {
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