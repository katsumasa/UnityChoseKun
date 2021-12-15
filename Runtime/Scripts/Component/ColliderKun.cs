using System.IO;
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
        [SerializeField] public float contactOffset;
        [SerializeField] public bool enabled;
        [SerializeField] public bool isTrigger;
        [SerializeField] public string material;


        public BoundsKun boundsKun
        {
            get { return m_bounds; }
            private set { m_bounds = value; }
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ColliderKun() : this(null) { }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collider">元となるColliderオブジェクト</param>
        public ColliderKun(Component component) : base(component)
        {
            componentKunType = ComponentKunType.Collider;
            var collider = component as Collider;
            if (collider != null)
            {
                boundsKun = new BoundsKun();
                material = "None";
                if (collider != null)
                {
                    boundsKun = new BoundsKun(collider.bounds);
                    contactOffset = collider.contactOffset;
                    enabled = collider.enabled;
                    isTrigger = collider.isTrigger;
                    material = collider.material.name;
                }
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


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<BoundsKun>(binaryWriter, m_bounds);           
            binaryWriter.Write(contactOffset);
            binaryWriter.Write(enabled);
            binaryWriter.Write(isTrigger);
            binaryWriter.Write(material);
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_bounds = SerializerKun.DesirializeObject<BoundsKun>(binaryReader);
            contactOffset =  binaryReader.ReadSingle();
            enabled     = binaryReader.ReadBoolean();
            isTrigger   = binaryReader.ReadBoolean();
            material    = binaryReader.ReadString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var colliderKun = other as ColliderKun;
            if(colliderKun == null)
            {
                return false;
            }
            if(boundsKun.Equals(colliderKun.boundsKun) == false)
            {
                return false;
            }
            if(contactOffset.Equals(colliderKun.contactOffset) == false)
            {
                return false;
            }
            if(enabled != colliderKun.enabled)
            {
                return false;
            }
            if(isTrigger != colliderKun.isTrigger)
            {
                return false;
            }
            if(material.Equals(colliderKun.material) == false)
            {
                return false;
            }
            return true;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
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
        [SerializeField] public int direction;
        [SerializeField] public float height;
        [SerializeField] public float radius;


        public Vector3 center
        {
            get { return m_center.GetVector3(); }
            set { m_center = new Vector3Kun(value); }
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CapsuleColliderKun() : this(null) { }                


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collider">元となるCapuselCollider</param>
        public CapsuleColliderKun(Component component) : base(component)
        {
            componentKunType = ComponentKunType.CapsuleCollider;
            var collider = component as Collider;
            if (collider != null)
            {

                m_center = new Vector3Kun();
                var capsuleCollider = collider as CapsuleCollider;
                if (capsuleCollider != null)
                {
                    center = capsuleCollider.center;
                    direction = capsuleCollider.direction;
                    height = capsuleCollider.height;
                    radius = capsuleCollider.radius;
                }
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<Vector3Kun>(binaryWriter, m_center);            
            binaryWriter.Write(direction);
            binaryWriter.Write(height);
            binaryWriter.Write(radius);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_center = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader);            
            direction = binaryReader.ReadInt32();
            height = binaryReader.ReadSingle();
            radius = binaryReader.ReadSingle();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            var colliderKun = other as CapsuleColliderKun;
            if(colliderKun == null)
            {
                return false;
            }
            if(center.Equals(colliderKun.center) == false)
            {
                return false;
            }
            if(direction != colliderKun.direction)
            {
                return false;
            }
            if(height != colliderKun.height)
            {
                return false;
            }
            if(radius != colliderKun.radius)
            {
                return false;
            }
            return base.Equals(other);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
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
        public MeshColliderKun(Component component) : base(component)
        {
            componentKunType = ComponentKunType.MeshCollider;            
            sharedMesh = "None";
            
            var meshCollider = component as MeshCollider;
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


        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(convex);
            binaryWriter.Write((int)cookingOptions);
            binaryWriter.Write(sharedMesh);
        }


        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            convex = binaryReader.ReadBoolean();
            cookingOptions = (MeshColliderCookingOptions)binaryReader.ReadInt32();
            sharedMesh = binaryReader.ReadString();
        }


        public override bool Equals(object other)
        {
            var otherKun = other as MeshColliderKun;
            if(otherKun == null)
            {
                return false;
            }
            if(convex != otherKun.convex)
            {
                return false;
            }
            if(cookingOptions != otherKun.cookingOptions)
            {
                return false;
            }
            if(sharedMesh != otherKun.sharedMesh)
            {
                return false;
            }            
            return base.Equals(other);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}