using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine.Rendering
{
    public class VolumeProfileKun : ScriptableObjectKun
    {

        VolumeComponentKun[] m_components;
        public VolumeComponentKun[] components
        {
            get
            {
                return m_components;  
            }
        }

        public VolumeProfileKun() : base(null) { }


        public VolumeProfileKun(ScriptableObject volumeProfiler) : base(volumeProfiler)
        {
            if(volumeProfiler == null)
            {
                return;
            }

            var type = volumeProfiler.GetType();
            var fi = type.GetField("components", BindingFlags.Instance | BindingFlags.Public);
            var commponents = (IEnumerable)fi.GetValue(volumeProfiler);
            var len = 0;
            foreach(var component in commponents)
            {
                len++;
            }

            m_components = new VolumeComponentKun[len];
            len = 0;
            foreach (var component in commponents)
            {
                m_components[len] = new VolumeComponentKun((ScriptableObject)component);
                len++;
            }
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);            
            SerializerKun.Serialize<VolumeComponentKun>(binaryWriter, m_components);           
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_components = SerializerKun.DesirializeObjects<VolumeComponentKun>(binaryReader);            
        }

        public virtual void WriteBack(ScriptableObject volumeProfiler)
        {
            var type = volumeProfiler.GetType();
            var fi = type.GetField("components", BindingFlags.Instance | BindingFlags.Public);
            var commponents = (IEnumerable)fi.GetValue(volumeProfiler);

            var i = 0;
            foreach(var component in commponents)
            {
                m_components[i++].WriteBack(component as ScriptableObject);
            }
        }

    }
}
