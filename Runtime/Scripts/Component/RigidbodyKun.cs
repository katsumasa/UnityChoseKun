﻿using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun.Engine
{    
    [System.Serializable]
    public class RigidbodyKun : ComponentKun
    {        
        [SerializeField] public float mass;
#if UNITY_6000_0_OR_NEWER
        [SerializeField] public float linearDamping;
        [SerializeField] public float angularDamping;

#else
        [SerializeField] public float drag;
        [SerializeField] public float angularDrag;
#endif
        [SerializeField] public bool useGravity;
        [SerializeField] public bool isKinematic;
        [SerializeField] public RigidbodyInterpolation interpolation;
        [SerializeField] public CollisionDetectionMode collisionDetectionMode;
        [SerializeField] public RigidbodyConstraints constraints;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RigidbodyKun() : this(null) { }
        
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="component">Rigidbodyオブジェクト</param>
        public RigidbodyKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.Rigidbody;
            var rigidbody = component as Rigidbody;
            if (rigidbody)
            {
                mass = rigidbody.mass;
#if UNITY_6000_0_OR_NEWER
                linearDamping = rigidbody.linearDamping;
                angularDamping = rigidbody.angularDamping;
#else
                drag = rigidbody.drag;
                angularDrag = rigidbody.angularDrag;
#endif
                useGravity = rigidbody.useGravity;
                isKinematic = rigidbody.isKinematic;
                interpolation = rigidbody.interpolation;
                collisionDetectionMode = rigidbody.collisionDetectionMode;
                constraints = rigidbody.constraints;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool WriteBack(Component component)
        {
            if (base.WriteBack(component))
            {
                Rigidbody rigidbody = component as Rigidbody;
                rigidbody.mass = mass;
#if UNITY_6000_0_OR_NEWER
                rigidbody.linearDamping = linearDamping;
                rigidbody.angularDamping = angularDamping;
#else
                rigidbody.drag = drag;
                rigidbody.angularDrag = angularDrag;
#endif
                rigidbody.useGravity = useGravity;
                rigidbody.isKinematic = isKinematic;
                rigidbody.interpolation = interpolation;
                rigidbody.collisionDetectionMode = this.collisionDetectionMode;
                rigidbody.constraints = this.constraints;
                return true;
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
            binaryWriter.Write(mass);
#if UNITY_6000_0_OR_NEWER
            binaryWriter.Write(linearDamping);
            binaryWriter.Write(angularDamping);

#else
            binaryWriter.Write(drag);
            binaryWriter.Write(angularDrag);
#endif
            binaryWriter.Write(useGravity);
            binaryWriter.Write(isKinematic);
            binaryWriter.Write((int)interpolation);
            binaryWriter.Write((int)collisionDetectionMode);
            binaryWriter.Write((int)constraints);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);

            mass = binaryReader.ReadSingle();
#if UNITY_6000_0_OR_NEWER
            linearDamping = binaryReader.ReadSingle();
            angularDamping = binaryReader.ReadSingle();
#else
            drag = binaryReader.ReadSingle();
            angularDrag = binaryReader.ReadSingle();
#endif
            useGravity = binaryReader.ReadBoolean();
            isKinematic = binaryReader.ReadBoolean();
            interpolation = (RigidbodyInterpolation)binaryReader.ReadInt32();
            collisionDetectionMode = (CollisionDetectionMode)binaryReader.ReadInt32();
            constraints = (RigidbodyConstraints)binaryReader.ReadInt32();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as RigidbodyKun;
            if(other == null)
            {
                return false;
            }
            if (mass.Equals(other.mass) == false)
            {
                return false;
            }
#if UNITY_6000_0_OR_NEWER
            if (linearDamping.Equals(other.linearDamping) == false)
            {
                return false;
            }
            if (angularDamping.Equals(other.angularDamping) == false)
            {
                return false;
            }
#else
            if (drag.Equals(other.drag) == false)
            {
                return false;
            }
            if (angularDrag.Equals(other.angularDrag) == false)
            {
                return false;
            }
#endif
            if (useGravity.Equals(other.useGravity) == false)
            {
                return false;
            }
            if (isKinematic.Equals(other.isKinematic) == false)
            {
                return false;
            }
            if (interpolation.Equals(other.interpolation) == false)
            {
                return false;
            }
            if (constraints.Equals(other.constraints) == false)
            {
                return false;
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
    }
}