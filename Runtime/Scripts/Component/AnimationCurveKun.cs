using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    public class AnimationCurveKun : ISerializerKun
    {
        KeyframeKun[] m_keys;       
        WrapMode m_postWrapMode;
        WrapMode m_preWrapMode;


        public KeyframeKun[] keys
        {
            get { return m_keys; }
        }

        public int length
        {
            get
            {
                if(m_keys == null) { return 0; }
                return m_keys.Length;
            }
        }

        public WrapMode postWrapMode
        {
            get { return m_postWrapMode; }
        }

        public WrapMode preWrapMode
        {
            get { return m_preWrapMode; }
        }

        public AnimationCurveKun()
        { }

        public AnimationCurveKun(AnimationCurve animationCurve)
        {
            if(animationCurve.length != 0)
            {
                m_keys = new KeyframeKun[animationCurve.length];
                for(var i = 0; i < m_keys .Length; i++)
                {
                    m_keys[i] = new KeyframeKun(animationCurve.keys[i]);
                }
            }
            m_postWrapMode = animationCurve.postWrapMode;
            m_preWrapMode = animationCurve.preWrapMode;
        }

        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            SerializerKun.Serialize<KeyframeKun>(binaryWriter, m_keys);
            binaryWriter.Write((int)m_postWrapMode);
            binaryWriter.Write((int)m_preWrapMode);
        }

        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_keys =  SerializerKun.DesirializeObjects<KeyframeKun>(binaryReader);
            m_postWrapMode = (WrapMode)binaryReader.ReadInt32();
            m_preWrapMode = (WrapMode)binaryReader.ReadInt32();
        }
    }
}