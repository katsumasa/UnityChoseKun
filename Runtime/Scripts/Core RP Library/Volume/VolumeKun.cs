using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine.Rendering
{
    public class VolumeKun : MonoBehaviourKun, IVolumeKun
    {
        private bool m_IsGlobal = true;
        public bool isGlobal
        {
            get => m_IsGlobal;
            set => m_IsGlobal = value;
        }

        private float m_Priority = 0f;
        public float priority
        {
            get => m_Priority;
            set { dirty = true; m_Priority = value; }
        }


        private float m_BlendDistance = 0f;
        public float blendDistance
        {
            get { return m_BlendDistance; }
            set { dirty = true; m_BlendDistance = value; }
        }

        private float m_Weight = 1f;
        public float weight
        {
            get => m_Weight;
            set { m_Weight = value; dirty = true; }
        }

        private VolumeProfileKun m_SharedProfile;
        public VolumeProfileKun sharedProfile
        {
            get { return m_SharedProfile; }
            set { m_SharedProfile = value; dirty = true; }
        }

        private VolumeProfileKun m_Profile;
        public VolumeProfileKun profile
        {
            get { return m_Profile; }
            set { m_Profile = value; dirty = true; }
        }

        private ColliderKun[] m_Colliders;
        public List<ColliderKun> colliders
        {
            get
            {
                return m_Colliders.ToList();
            }
        }

        private VolumeProfileKun m_ProfileRef;
        public VolumeProfileKun profileRef
        {
            get { return m_ProfileRef; }
        }

        public VolumeKun() : this(null) { }

        public VolumeKun(Component component) : base(component)
        {
            componentKunType = ComponentKunType.Volume;
            if(component == null)
            {
                return;
            }

            var type = component.GetType();
            if(type.Name != "Volume")
            {
                throw new System.Exception("type is not Volume");
            }

            var pi = type.GetProperty("isGlobal", BindingFlags.Instance | BindingFlags.Public);
            m_IsGlobal = (bool)pi.GetValue(component);

            var fi = type.GetField("priority", BindingFlags.Instance | BindingFlags.Public);
            m_Priority = (float)fi.GetValue(component);

            fi = type.GetField("blendDistance", BindingFlags.Instance | BindingFlags.Public);
            m_BlendDistance = (float)fi.GetValue(component);

            fi = type.GetField("weight", BindingFlags.Instance | BindingFlags.Public);
            m_Weight = (float)fi.GetValue(component);

            fi = type.GetField("sharedProfile", BindingFlags.Instance | BindingFlags.Public);
            m_SharedProfile = new VolumeProfileKun((ScriptableObject)fi.GetValue(component));

            pi = type.GetProperty("profile", BindingFlags.Instance | BindingFlags.Public);
            m_Profile = new VolumeProfileKun((ScriptableObject)pi.GetValue(component));

            pi = type.GetProperty("colliders", BindingFlags.Instance | BindingFlags.Public);
            var cs = (List<Collider>)pi.GetValue(component);
            m_Colliders = new ColliderKun[cs.Count];
            for(var i = 0; i < cs.Count; i++)
            {
                m_Colliders[i] = new ColliderKun(cs[i]);
            }

            pi = type.GetProperty("profileRef", BindingFlags.Instance | BindingFlags.NonPublic);
            var volumeProfile = pi.GetValue(component);
            m_ProfileRef = new VolumeProfileKun((ScriptableObject)volumeProfile);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool WriteBack(Component component)
        {
            var type = component.GetType();
            if (base.WriteBack(component))
            {
                
                var pi = type.GetProperty("isGlobal", BindingFlags.Instance | BindingFlags.Public);
                pi.SetValue(component, m_IsGlobal);

                var fi = type.GetField("priority", BindingFlags.Instance | BindingFlags.Public);
                fi.SetValue(component, m_Priority);

                fi = type.GetField("blendDistance", BindingFlags.Instance | BindingFlags.Public);
                fi.SetValue(component, m_BlendDistance);

                fi = type.GetField("weight", BindingFlags.Instance | BindingFlags.Public);
                fi.SetValue(component, m_Weight);
            }
            //if (m_SharedProfile != null)
            //{
            //    var fi = type.GetField("sharedProfile", BindingFlags.Instance | BindingFlags.Public);
            //    m_SharedProfile.WriteBack((ScriptableObject)fi.GetValue(component));
            //}

            if (m_Profile != null)
            {
                var pi = type.GetProperty("profile", BindingFlags.Instance | BindingFlags.Public);
                m_Profile.WriteBack((ScriptableObject)pi.GetValue(component));
            }

            if (m_Colliders != null)
            {
                var pi = type.GetProperty("colliders", BindingFlags.Instance | BindingFlags.Public);
                for (var i = 0; i < m_Colliders.Length; i++)
                {
                    m_Colliders[i].WriteBack((Collider)pi.GetValue(component));
                }
            }

            //if (m_ProfileRef != null)
            //{
            //    var pi = type.GetProperty("profileRef", BindingFlags.Instance | BindingFlags.NonPublic);
            //    m_ProfileRef.WriteBack((ScriptableObject)pi.GetValue(component));
            //}

            return true;
            
            
        }


        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_IsGlobal);
            binaryWriter.Write(m_Priority);
            binaryWriter.Write(m_BlendDistance);
            binaryWriter.Write(m_Weight);
            SerializerKun.Serialize<VolumeProfileKun>(binaryWriter, m_SharedProfile);
            SerializerKun.Serialize<VolumeProfileKun>(binaryWriter, m_Profile);
            SerializerKun.Serialize<ColliderKun>(binaryWriter,m_Colliders);
            SerializerKun.Serialize<VolumeProfileKun>(binaryWriter, m_ProfileRef);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_IsGlobal = binaryReader.ReadBoolean();
            m_Priority = binaryReader.ReadSingle();
            m_BlendDistance = binaryReader.ReadSingle();
            m_Weight = binaryReader.ReadSingle();
            m_SharedProfile = SerializerKun.DesirializeObject<VolumeProfileKun>(binaryReader);
            m_Profile = SerializerKun.DesirializeObject<VolumeProfileKun>(binaryReader);
            m_Colliders = SerializerKun.DesirializeObjects<ColliderKun>(binaryReader);
            m_ProfileRef = SerializerKun.DesirializeObject<VolumeProfileKun>(binaryReader);
        }

        public override bool Equals(object obj)
        {
            if(base.Equals(obj) == false)
            {
                return false;
            }
            var other = obj as VolumeKun;
            if(other == null)
            {
                return false;
            }
            if(m_IsGlobal.Equals(other.m_IsGlobal) == false)
            {
                return false;
            }
            if(m_Priority.Equals(other.m_Priority) == false)
            {
                return false;
            }
            if(m_BlendDistance.Equals(other.m_BlendDistance) == false)
            {
                return false;
            }
            if(m_Weight.Equals(other.m_Weight) == false)
            {
                return false;
            }
            if((m_SharedProfile != null) && (m_SharedProfile.Equals(other.m_SharedProfile) == false))
            {
                
                return false;                
            }
            if((m_Profile != null) && (m_Profile.Equals(other.m_Profile) == false))
            {
                return false;
            }
            if(m_Colliders != null)
            {
                if(other.m_Colliders == null)
                {
                    return false;
                }
                if(m_Colliders.Length != other.m_Colliders.Length)
                {
                    return false;
                }
                for(var i = 0; i < m_Colliders.Length; i++)
                {
                    if(m_Colliders[i].Equals(other.m_Colliders[i]) == false)
                    {
                        return false;
                    }
                }
            }
            if((m_ProfileRef != null) && (m_ProfileRef.Equals(other.m_ProfileRef) == false))
            {
                return false;
            }

            return true;
        }
    }
}
