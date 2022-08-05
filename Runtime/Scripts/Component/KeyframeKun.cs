using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    public class KeyframeKun : ISerializerKun
    {
        float m_inTangent;
        float m_inWeight;
        float m_outTangent;
        float m_outWeight;
        float m_time;
        float m_value;
        WeightedMode m_weightedMode;


        public float inTangent
        {
            get { return m_inTangent; }
        }
        public float inWeight
        {
            get { return m_inWeight; }
        }
        public float outTangent
        {
            get { return m_outTangent; }
        }
        public float outWeight
        {
            get { return m_outWeight; }
        }
        public float time
        {
            get { return m_time; }
        }
        public float value
        {
            get { return m_value; }
        }
        public WeightedMode weightedMode
        {
            get { return m_weightedMode; }
        }


        public KeyframeKun() { }

        public KeyframeKun(Keyframe keyframe)
        {
            m_inTangent = keyframe.inTangent;
            m_inWeight = keyframe.inWeight;
            m_outTangent = keyframe.outTangent;
            m_outWeight = keyframe.outWeight;
            m_time = keyframe.time;
            m_value = keyframe.value;
            m_weightedMode = keyframe.weightedMode;
        }


        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_inTangent);
            binaryWriter.Write(m_inWeight);
            binaryWriter.Write(m_outTangent);
            binaryWriter.Write(m_outWeight);
            binaryWriter.Write(m_time);
            binaryWriter.Write(m_value);
            binaryWriter.Write((int)m_weightedMode);
        }

        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_inTangent = binaryReader.ReadSingle();
            m_inWeight = binaryReader.ReadSingle();
            m_outTangent = binaryReader.ReadSingle();
            m_outWeight = binaryReader.ReadSingle();
            m_time = binaryReader.ReadSingle();
            m_value = binaryReader.ReadSingle();
            m_weightedMode = (WeightedMode)binaryReader.ReadInt32();
        }

        public Keyframe GetKeyframe()
        {
            var keyFrame = new Keyframe(m_time,m_value);
            keyFrame.inTangent = m_inTangent;
            keyFrame.inWeight = m_inWeight;
            keyFrame.outTangent = m_outTangent;
            keyFrame.outWeight = m_outWeight;
            keyFrame.weightedMode = m_weightedMode;
            return keyFrame;
        }
    }

}