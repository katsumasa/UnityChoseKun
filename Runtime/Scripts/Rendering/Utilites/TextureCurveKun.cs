using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using Utj.UnityChoseKun.Engine;

namespace Utj.UnityChoseKun.Engine.Rendering
{
    public class TextureCurveKun : ObjectKun
    {
        int m_length;        
        bool m_Loop;
        float m_ZeroValue;
        float m_Range;
        AnimationCurveKun m_Curve;
        AnimationCurveKun m_LoopingCurve;
        Texture2DKun m_Texture;
        bool m_IsCurveDirty;
        bool m_IsTextureDirty;



        public int length { get { return m_length; } set { m_length = value; } }


        public KeyframeKun this[int index] => m_Curve.keys[index];


        public TextureCurveKun() { }

        public TextureCurveKun(object obj)
        {
            var t = obj.GetType();
            var pi = t.GetProperty("length",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.Public);
            m_length = (int)pi.GetValue(obj);

            var fi = t.GetField("m_Loop", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            m_Loop = (bool)fi.GetValue(obj);

            fi = t.GetField("m_ZeroValue", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            m_ZeroValue = (float)fi.GetValue(obj);

            fi = t.GetField("m_Range", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            m_Range = (float)fi.GetValue(obj);

            fi = t.GetField("m_Curve", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var animationCurve = (AnimationCurve)fi.GetValue(obj);
            if (animationCurve != null)
            {
                m_Curve = new AnimationCurveKun(animationCurve);
            }

            fi = t.GetField("m_LoopingCurve", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            animationCurve = (AnimationCurve)fi.GetValue(obj);
            if (animationCurve != null)
            {
                m_LoopingCurve = new AnimationCurveKun(animationCurve);
            }

            fi = t.GetField("m_Texture", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var texture2D = (Texture2D)fi.GetValue(obj);
            if (texture2D != null)
            {
                m_Texture = new Texture2DKun(texture2D);
            }

            fi = t.GetField("m_IsCurveDirty", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            m_IsCurveDirty = (bool)fi.GetValue(obj);

            fi = t.GetField("m_IsTextureDirty", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            m_IsTextureDirty = (bool) fi.GetValue(obj);
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);

            binaryWriter.Write(m_length);
            binaryWriter.Write(m_Loop);
            binaryWriter.Write(m_ZeroValue);
            binaryWriter.Write(m_Range);
            SerializerKun.Serialize<AnimationCurveKun>(binaryWriter, m_Curve);
            SerializerKun.Serialize<AnimationCurveKun>(binaryWriter,m_LoopingCurve);
            SerializerKun.Serialize<Texture2DKun>(binaryWriter, m_Texture);
            binaryWriter.Write(m_IsCurveDirty);
            binaryWriter.Write(m_IsTextureDirty);
        }


        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_length = binaryReader.ReadInt32();
            m_Loop = binaryReader.ReadBoolean();
            m_ZeroValue = binaryReader.ReadSingle();
            m_Range = binaryReader.ReadSingle();
            m_Curve = SerializerKun.DesirializeObject<AnimationCurveKun>(binaryReader);
            m_LoopingCurve = SerializerKun.DesirializeObject<AnimationCurveKun>(binaryReader);
            m_Texture = SerializerKun.DesirializeObject<Texture2DKun>(binaryReader);
            m_IsCurveDirty = binaryReader.ReadBoolean();
            m_IsTextureDirty = binaryReader.ReadBoolean();
        }
    }
}
