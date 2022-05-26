using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine.Rendering
{
    /// <summary>
    /// VolumeParameterをRuntime<->Editor間でシリアライズする為のクラス
    /// </summary>
    public abstract class VolumeParameterKun : ISerializerKun
    {

        /// <summary>
        /// VolumeParameterからVolumeParameterを生成する
        /// </summary>
        /// <param name="volumeParameter">VolumeParameter</param>
        /// <returns>VolumeParameterKun</returns>
        public static VolumeParameterKun Allocater(object volumeParameter)
        {           
            var type = volumeParameter.GetType();
            var pi = type.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
            var overrideState = (bool)pi.GetValue(volumeParameter);

            pi = type.GetProperty("value", BindingFlags.Instance | BindingFlags.Public);
            var value = pi.GetValue(volumeParameter);
            if (type.Name == "BoolParameter")
            {             
                return new BoolParameterKun((bool)value, overrideState);
            }
            else if(type.Name == "LayerMaskParametetr")
            {
                return new LayerMaskParameterKun((LayerMaskKun)value, overrideState);
            }
            else if(type.Name == "IntParameter")
            {
                return new IntParameterKun((int)value, overrideState);
            }
            else if(type.Name == "NoInterpIntParameter")
            {
                return new NoInterpIntParameterKun((int)value,overrideState);
            }
            else if(type.Name == "MinIntParameter")
            {
                var fi = type.GetField("min");
                var min = (int)fi.GetValue(volumeParameter);
                return new MinIntParameterKun((int)value, min,overrideState);
            }
            else if(type.Name == "NoInterpMinIntParameter")
            {
                var fi = type.GetField("min");
                var min = (int)fi.GetValue(volumeParameter);
                return new NoInterpMinIntParameterKun((int)value, min, overrideState);
            }
            else if(type.Name == "MaxIntParameter")
            {
                var fi = type.GetField("max");
                var max = (int)fi.GetValue(volumeParameter);
                return new MaxIntParameterKun((int)value,max,overrideState);
            }
            else if(type.Name == "NoInterpMaxIntParameter")
            {
                var fi = type.GetField("max");
                var max = (int)fi.GetValue(volumeParameter);
                return new NoInterpMaxIntParameterKun((int)value,max,overrideState);
            }
            else if(type.Name == "ClampedIntParameter")
            {
                var fi = type.GetField("min");
                var min = (int)fi.GetValue(volumeParameter);
                fi = type.GetField("max");
                var max = (int)fi.GetValue(volumeParameter);
                return new ClampedIntParameterKun((int)value,min,max,overrideState);
            }
            else if(type.Name == "NoInterpClampedIntParameter")
            {
                var fi = type.GetField("min");
                var min = (int)fi.GetValue(volumeParameter);
                fi = type.GetField("max");
                var max = (int)fi.GetValue(volumeParameter);
                return new NoInterpClampedIntParameterKun((int)value,min,max,overrideState);
            }
            else if(type.Name == "FloatParameter")
            {
                return new FloatParameterKun((float)value, overrideState);
            }
            else if(type.Name == "NoInterpFloatParameter")
            {
                return new NoInterpFloatParameterKun((float)value,overrideState);
            }
            else if(type.Name == "MinFloatParameter")
            {
                var fi = type.GetField("min");
                var min = (float)fi.GetValue(volumeParameter);
                return new MinFloatParameterKun((float)value,min,overrideState);
            }
            else if(type.Name == "NoInterpMinFloatParameter")
            {
                var fi = type.GetField("min");
                var min = (float)fi.GetValue(volumeParameter);
                return new NoInterpMinFloatParameterKun((float)value, min, overrideState);
            }
            else if(type.Name == "MaxFloatParameter")
            {
                var fi = type.GetField("max");
                var max = (float)fi.GetValue(volumeParameter);
                return new MaxFloatParameterKun((float)value,max,overrideState);
            }
            else if(type.Name == "NoInterpMaxFloatParameter")
            {
                var fi = type.GetField("max");
                var max = (float)fi.GetValue(volumeParameter);
                return new NoInterpMaxFloatParameterKun((float)value, max, overrideState);
            }
            else if(type.Name == "ClampedFloatParameter")
            {                
                var fi = type.GetField("min");
                var min = (float)fi.GetValue(volumeParameter);
                fi = type.GetField("max");
                var max = (float)fi.GetValue(volumeParameter);
                return new ClampedFloatParameterKun((float)value,min,max,overrideState);
            }
            else if(type.Name == "NoInterpClampedFloatParameter")
            {
                var fi = type.GetField("min");
                var min = (float)fi.GetValue(volumeParameter);
                fi = type.GetField("max");
                var max = (float)fi.GetValue(volumeParameter);
                return new NoInterpClampedFloatParameterKun((float)value,min,max);
            }
            else if(type.Name == "FloatRangeParameter")
            {
                var fi = type.GetField("min");
                var min = (float)fi.GetValue(volumeParameter);
                fi = type.GetField("max");
                var max = (float)fi.GetValue(volumeParameter);
                return new FloatRangeParameterKun((Vector2)value, min, max,overrideState);
            }
            else if(type.Name == "NoInterpFloatRangeParameter")
            {
                var fi = type.GetField("min");
                var min = (float)fi.GetValue(volumeParameter);
                fi = type.GetField("max");
                var max = (float)fi.GetValue(volumeParameter);
                return new NoInterpFloatRangeParameterKun((Vector2)value, min, max, overrideState);
            }
            else if(type.Name == "ColorParameter")
            {
                var fi = type.GetField("hdr");
                var hdr = (bool)fi.GetValue(volumeParameter);
                fi = type.GetField("showAlpha");
                var showAlpha = (bool)fi.GetValue(volumeParameter);
                fi = type.GetField("showEyeDropper");
                var showEyeDropper = (bool)fi.GetValue(volumeParameter);
                return new ColorParameterKun((Color)value,hdr,showAlpha,showEyeDropper,overrideState);
            }
            else if(type.Name == "NoInterpColorParameter")
            {
                var fi = type.GetField("hdr");
                var hdr = (bool)fi.GetValue(volumeParameter);
                fi = type.GetField("showAlpha");
                var showAlpha = (bool)fi.GetValue(volumeParameter);
                fi = type.GetField("showEyeDropper");
                var showEyeDropper = (bool)fi.GetValue(volumeParameter);
                return new NoInterpColorParameterKun((Color)value, hdr, showAlpha, showEyeDropper, overrideState);
            }
            else if(type.Name == "Vector2Parameter")
            {
                return new Vector2ParameterKun((Vector2)value,overrideState);
            }
            else if(type.Name == "NoInterpVector2Parameter")
            {
                return new NoInterpVector2ParameterKun((Vector2)value, overrideState);
            }
            else if(type.Name == "Vector3Parameter")
            {
                return new Vector3ParameterKun((Vector3)value, overrideState);
            }
            else if(type.Name == "NoInterpVector3Parameter")
            {
                return new NoInterpVector3ParameterKun((Vector3)value, overrideState);
            }
            else if(type.Name == "Vector4Parameter")
            {
                return new Vector4ParameterKun((Vector4)value, overrideState);
            }
            else if(type.Name == "NoInterpVector4Parameter")
            {
                return new NoInterpVector4ParameterKun((Vector4)value, overrideState);
            }
            else if(type.Name == "TextureParameter")
            {
                return new TextureParameterKun((Texture)value,overrideState);
            }
            else if(type.Name == "NoInterpTextureParameter")
            {
                return new NoInterpTextureParameterKun((Texture)value,overrideState);
            }
            else if(type.Name == "Texture2DParameter")
            {
                return new Texture2DParameterKun((Texture)value, overrideState);
            }
            else if(type.Name == "Texture3DParameter")
            {
                return new Texture3DParameterKun((Texture)value, overrideState);
            }
            else if(type.Name == "RenderTextureParameter")
            {
                return new RenderTextureParameterKun((RenderTexture)value,overrideState);
            }                        
            else if(type.Name == "NoInterpRenderTextureParameter")
            {
                return new NoInterpRenderTextureParameterKun((RenderTexture)value, overrideState);
            }
            else if(type.Name == "CubemapParameter")
            {
                return new CubemapParameterKun((Texture)value,overrideState);
            }
            else if(type.Name == "NoInterpCubemapParameter")
            {
                return new NoInterpCubemapParameterKun((Cubemap)value, overrideState);
            }
            else if(type.Name == "AnimationCurveParameter")
            {
                return new AnimationCurveParameterKun((AnimationCurve)value,overrideState);
            }
            else if(type.Name == "TextureCurveParameter")
            {
                return new TextureCurveParameterKun(new TextureCurveKun(value), overrideState);
            }
            else if(type.Name == "TonemappingModeParameter")
            {
                return new TonemappingModeParameterKun((int)value,overrideState);
            }
            else if(type.Name == "DepthOfFieldModeParameter")
            {
                return new DepthOfFieldModeParameterKun((int)value,overrideState);
            }
            else if(type.Name == "FilmGrainLookupParameter")
            {
                return new FilmGrainLookupParameterKun((int)value,overrideState);
            }
            else if(type.Name == "MotionBlurModeParameter")
            {
                return new MotionBlurModeParameterKun((int)value,overrideState);
            }
            else if(type.Name == "MotionBlurQualityParameter")
            {
                return new MotionBlurQualityParameterKun((int)value,overrideState);
            }

            return new UnknownParameterKun();
        }


        /// <summary>
        /// VolumeParameterKunTypeからVolumeParameterKunを生成する
        /// </summary>
        /// <param name="volumeParameterKunType">VolumeParameterKunType</param>
        /// <returns>VolumeParameterKun</returns>
        public static VolumeParameterKun Allocater(VolumeParameterKunType volumeParameterKunType)
        {
            switch (volumeParameterKunType)
            {
                case VolumeParameterKun.VolumeParameterKunType.BoolParameterKun:
                    return new BoolParameterKun();                   

                case VolumeParameterKun.VolumeParameterKunType.LayerMaskParametetKun:
                    return new LayerMaskParameterKun(-1);                    

                case VolumeParameterKun.VolumeParameterKunType.IntParameterKun:
                    return  new IntParameterKun(0);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpIntParameterKun:
                    return  new NoInterpIntParameterKun(0);

                case VolumeParameterKun.VolumeParameterKunType.MinIntParameterKun:
                    return  new MinIntParameterKun(0, 0);
                    
                case VolumeParameterKun.VolumeParameterKunType.NoInterpMinIntParameterKun:
                    return new NoInterpMinIntParameterKun(0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.MaxIntParameterKun:
                    return  new MaxIntParameterKun(0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpMaxIntParameterKun:
                    return new NoInterpMaxIntParameterKun(0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.ClampedIntParameterKun:
                    return new ClampedIntParameterKun(0, 0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpClampedIntParameterKun:
                    return new NoInterpClampedIntParameterKun(0, 0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.FloatParameterKun:
                    return new FloatParameterKun(0);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpFloatParameterKun:
                    return new NoInterpFloatParameterKun(0);                    

                case VolumeParameterKun.VolumeParameterKunType.MinFloatParameterKun:
                    return new MinFloatParameterKun(0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpMinFloatParameterKun:
                    return new NoInterpMinFloatParameterKun(0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.MaxFloatParameterKun:
                    return new MaxFloatParameterKun(0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpMaxFloatParameterKun:
                    return new NoInterpMaxFloatParameterKun(0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.ClampedFloatParameterKun:
                    return new ClampedFloatParameterKun(0, 0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpClampedFloatParameterKun:
                    return new NoInterpClampedFloatParameterKun(0, 0, 0); 

                case VolumeParameterKun.VolumeParameterKunType.FloatRangeParameterKun:
                    return new FloatRangeParameterKun(Vector2.zero, 0, 0);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpFloatRangeParameterKun:
                    return new NoInterpFloatRangeParameterKun(Vector2.zero, 0, 0);
                    
                case VolumeParameterKun.VolumeParameterKunType.ColorParameterKun:
                    return new ColorParameterKun(Color.white);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpColorParameterKun:
                    return new NoInterpColorParameterKun(Color.white);                    

                case VolumeParameterKun.VolumeParameterKunType.Vector2ParameterKun:
                    return new Vector2ParameterKun(Vector2.zero);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpVector2ParameterKun:
                    return new NoInterpVector2ParameterKun(Vector2.zero); 

                case VolumeParameterKun.VolumeParameterKunType.Vector3ParameterKun:
                    return new Vector3ParameterKun(Vector3.zero);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpVector3ParameterKun:
                    return new NoInterpVector3ParameterKun(Vector3.zero);                    

                case VolumeParameterKun.VolumeParameterKunType.Vector4ParameterKun:
                    return new Vector4ParameterKun(Vector4.zero);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpVector4ParameterKun:
                    return new NoInterpVector4ParameterKun(Vector4.zero);                    

                case VolumeParameterKun.VolumeParameterKunType.TextureParameterKun:
                    return new TextureParameterKun(null);

                case VolumeParameterKunType.NoInterpTextureParameterKun:
                    return new NoInterpTextureParameterKun(null);

                case VolumeParameterKun.VolumeParameterKunType.Texture2DParameterKun:
                    return new Texture2DParameterKun(null);                    

                case VolumeParameterKun.VolumeParameterKunType.Texture3DParameterKun:
                    return new Texture3DParameterKun(null);                    

                case VolumeParameterKun.VolumeParameterKunType.RenderTextureParameterKun:
                    return new RenderTextureParameterKun(null);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpRenderTextureParameterKun:
                    return new NoInterpRenderTextureParameterKun(null);                    

                case VolumeParameterKun.VolumeParameterKunType.CubemapParameterKun:
                    return new CubemapParameterKun(null);                    

                case VolumeParameterKun.VolumeParameterKunType.NoInterpCubemapParameterKun:
                    return new NoInterpCubemapParameterKun(null);                    

                case VolumeParameterKun.VolumeParameterKunType.AnimationCurveParameterKun:
                    return new AnimationCurveParameterKun(null);

                case VolumeParameterKunType.TextureCurveParameterKun:
                    return new TextureCurveParameterKun(null);

                case VolumeParameterKun.VolumeParameterKunType.TonemappingModeParameterKun:
                    return new TonemappingModeParameterKun(0);                    

                case VolumeParameterKun.VolumeParameterKunType.DepthOfFieldModeParameterKun:
                    return new DepthOfFieldModeParameterKun(0);                    

                case VolumeParameterKun.VolumeParameterKunType.FilmGrainLookupParameterKun:
                    return new FilmGrainLookupParameterKun(0);                    

                case VolumeParameterKun.VolumeParameterKunType.MotionBlurModeParameterKun:
                    return new MotionBlurModeParameterKun(0);                    

                case VolumeParameterKun.VolumeParameterKunType.MotionBlurQualityParameterKun:
                    return new MotionBlurQualityParameterKun(0);                    
            }
            return new UnknownParameterKun(null) ;
        }


        /// <summary>
        /// 
        /// </summary>
        public enum VolumeParameterKunType
        {
            BoolParameterKun,
            LayerMaskParametetKun,
            IntParameterKun,
            NoInterpIntParameterKun,
            MinIntParameterKun,
            NoInterpMinIntParameterKun,
            MaxIntParameterKun,
            NoInterpMaxIntParameterKun,
            ClampedIntParameterKun,
            NoInterpClampedIntParameterKun,
            FloatParameterKun,
            NoInterpFloatParameterKun,
            MinFloatParameterKun,
            NoInterpMinFloatParameterKun,
            MaxFloatParameterKun,
            NoInterpMaxFloatParameterKun,
            ClampedFloatParameterKun,
            NoInterpClampedFloatParameterKun,
            FloatRangeParameterKun,
            NoInterpFloatRangeParameterKun,
            ColorParameterKun,
            NoInterpColorParameterKun,
            Vector2ParameterKun,
            NoInterpVector2ParameterKun,
            Vector3ParameterKun,
            NoInterpVector3ParameterKun,
            Vector4ParameterKun,
            NoInterpVector4ParameterKun,
            TextureParameterKun,
            NoInterpTextureParameterKun,
            Texture2DParameterKun,
            Texture3DParameterKun,
            RenderTextureParameterKun,
            NoInterpRenderTextureParameterKun,
            CubemapParameterKun,
            NoInterpCubemapParameterKun,
            AnimationCurveParameterKun,

            TextureCurveParameterKun,

            // URP Override
            TonemappingModeParameterKun,
            DepthOfFieldModeParameterKun,
            FilmGrainLookupParameterKun,
            MotionBlurModeParameterKun,
            MotionBlurQualityParameterKun,

            Unknown,
        }

        

        /// <summary>
        /// VolumeParameterKunのタイプ
        /// </summary>
        protected VolumeParameterKunType m_VolumeParameterKunType;

        public VolumeParameterKunType volumeParameterKunType
        {
            get => m_VolumeParameterKunType;            
        }

        /// <summary>
        /// Editor上で編集するか否か
        /// </summary>
        protected bool m_OverrideState;
        public virtual bool overrideState
        {
            get => m_OverrideState;
            set => m_OverrideState = value;
        }

        /// <summary>
        /// 値に変更があったか否か
        /// </summary>
        protected bool m_Dirty;
        public bool dirty
        {
            get => m_Dirty;
            set => m_Dirty = value;
        }

        internal abstract void Interp(VolumeParameterKun from, VolumeParameterKun to, float t);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            return ((VolumeParameterKun<T>)this).value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void SetValue(VolumeParameterKun parameter);


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public void SetValue<T>(T value)
        {
            ((VolumeParameterKun<T>)this).SetValue(value);            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write((int)m_VolumeParameterKunType);
            binaryWriter.Write(m_OverrideState);
            binaryWriter.Write(m_Dirty);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_VolumeParameterKunType = (VolumeParameterKunType)binaryReader.ReadInt32();
            m_OverrideState = binaryReader.ReadBoolean();
            m_Dirty = binaryReader.ReadBoolean();
        }

        public virtual void WriteBack(object parameter)
        {
            var t = parameter.GetType();
            var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);                        
            pi.SetValue(parameter, overrideState);
        }        
    }


    

    /// <summary>
    /// VolumeParameter<typeparamref name="T"/>を取り扱う為のClass
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class VolumeParameterKun<T> : VolumeParameterKun, IEquatable<VolumeParameterKun<T>>
    {
        /// <summary>
        /// 取り扱う値
        /// </summary>
        protected T m_Value;        
        public virtual T value
        {
            get => m_Value;
            set { m_Value = value; m_Dirty = true; }
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VolumeParameterKun()
            : this(default, false)
        {
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">設定する値</param>
        /// <param name="overrideState">オーバーライド可能か否か</param>
        protected VolumeParameterKun(T value, bool overrideState)
        {
            m_Value = value;
            this.overrideState = overrideState;
            m_Dirty = false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="t"></param>
        internal override void Interp(VolumeParameterKun from, VolumeParameterKun to, float t)
        {
            // Note: this is relatively unsafe (assumes that from and to are both holding type T)
            Interp(from.GetValue<T>(), to.GetValue<T>(), t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="t"></param>
        public virtual void Interp(T from, T to, float t)
        {
            // Default interpolation is naive
            m_Value = t > 0f ? to : from;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        public void Override(T x)
        {
            overrideState = true;
            m_Value = x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public override void SetValue(VolumeParameterKun parameter)
        {
            m_Value = parameter.GetValue<T>();           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(T value)
        {
            m_Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{value} ({overrideState})";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(VolumeParameterKun<T> lhs, T rhs) => lhs != null && !ReferenceEquals(lhs.value, null) && lhs.value.Equals(rhs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(VolumeParameterKun<T> lhs, T rhs) => !(lhs == rhs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        public static explicit operator T(VolumeParameterKun<T> prop) => prop.m_Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(VolumeParameterKun<T> other)
        {
            return EqualityComparer<T>.Default.Equals(m_Value, other.m_Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public override void Serialize(BinaryWriter binaryWriter) 
        {
            base.Serialize(binaryWriter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader) 
        {
            base.Deserialize(binaryReader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public override void WriteBack(object parameter)
        {
            base.WriteBack(parameter);
            if (m_Dirty)
            {
                var t = parameter.GetType();
                var fi = t.GetField("m_Value", BindingFlags.Instance | BindingFlags.NonPublic);
                fi.SetValue(parameter, value);                
            }
        }
    }

    /// <summary>
    /// UnityChoseKunが未対応なVolumeParameterを継承したクラス
    /// </summary>
    public class UnknownParameterKun : VolumeParameterKun<object>
    {
        public UnknownParameterKun(object value = null,bool overrideState = false):base(value,overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.Unknown;
        }
    }


    /// <summary>
    /// BoolParameterを取り扱う為のクラス
    /// </summary>
    public class BoolParameterKun : VolumeParameterKun<bool>
    {        
        public BoolParameterKun(bool value=false, bool overrideState = false)
               : base(value, overrideState) 
        {
            m_VolumeParameterKunType = VolumeParameterKunType.BoolParameterKun;
        }
        
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(GetValue<bool>());
        }
        
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadBoolean();
        }
    }


    /// <summary>
    /// LayerMaskParameterを取り扱う為のクラス
    /// </summary>
    public class LayerMaskParameterKun : VolumeParameterKun<int>
    {
        // :::NOTE:::
        //　LayerMaskを直接取り扱うよりもintで扱う方が都合が良い為、ここではintで処理を行い、View側でLayerMaskに変換する
        //

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        /// <param name="overrideState"></param>
        public LayerMaskParameterKun(int value,bool overrideState = false) : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.LayerMaskParametetKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);            
            binaryWriter.Write(value);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadInt32();
        }

        public override void WriteBack(object parameter)
        {
            var t = parameter.GetType();
            var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
            pi.SetValue(parameter, overrideState);
            if (m_Dirty)
            {
                var fi = t.GetField("m_Value", BindingFlags.Instance | BindingFlags.NonPublic);
                var layerMask = (LayerMask)fi.GetValue(parameter);
                layerMask.value = m_Value;
            }
        }
    }

    /// <summary>
    /// intを取り扱う為のクラス
    /// </summary>
    public class IntParameterKun : VolumeParameterKun<int>
    {
        public IntParameterKun(int value,bool overrideState = false):base(value,overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.IntParameterKun;
        }

        public sealed override void Interp(int from, int to, float t)
        {
            // Int snapping interpolation. Don't use this for enums as they don't necessarily have
            // contiguous values. Use the default interpolator instead (same as bool).
            m_Value = (int)(from + (to - from) * t);
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(GetValue<int>());
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadInt32();
        }
    }

    /// <summary>
    /// NoInterpIntParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpIntParameterKun : VolumeParameterKun<int>
    {
        /// <summary>
        /// Creates a new <see cref="NoInterpIntParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpIntParameterKun(int value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpIntParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(GetValue<int>());
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadInt32();
        }
    }


    /// <summary>
    /// MinIntParameterを取り扱う為のクラス
    /// </summary>
    public class MinIntParameterKun : IntParameterKun
    {        
        public int min;


        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override int value
        {
            get => m_Value;
            set { m_Value = Mathf.Max(value, min); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <see cref="MinIntParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="min">The minimum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public MinIntParameterKun(int value, int min, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.MinIntParameterKun;
            this.min = min;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);            
            binaryWriter.Write(min);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);            
            min = binaryReader.ReadInt32();
        }        
    }


    /// <summary>
    /// NoInterpMinIntParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpMinIntParameterKun : VolumeParameterKun<int>
    {
        /// <summary>
        /// The minimum value to clamp this parameter to.
        /// </summary>        
        public int min;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override int value
        {
            get => m_Value;
            set { m_Value = Mathf.Max(value, min); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <see cref="NoInterpMinIntParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="min">The minimum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpMinIntParameterKun(int value, int min, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpMinIntParameterKun;   
            this.min = min;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Value);
            binaryWriter.Write(min);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadInt32();
            min = binaryReader.ReadInt32();
        }
    }


    /// <summary>
    /// MaxIntParameterを取り扱う為のクラス
    /// </summary>
    public class MaxIntParameterKun : IntParameterKun
    {

        public int max;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override int value
        {
            get => m_Value;
            set { m_Value = Mathf.Min(value, max); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <see cref="MaxIntParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="max">The maximum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public MaxIntParameterKun(int value, int max, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.MaxIntParameterKun;
            this.max = max;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(max);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            max = binaryReader.ReadInt32();
        }
    }

    /// <summary>
    /// NoInterpMaxIntParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpMaxIntParameterKun : VolumeParameterKun<int>
    {

        public int max;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override int value
        {
            get => m_Value;
            set { m_Value = Mathf.Min(value, max); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <see cref="NoInterpMaxIntParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="max">The maximum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpMaxIntParameterKun(int value, int max, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpMaxIntParameterKun;
            this.max = max;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Value);
            binaryWriter.Write(max);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadInt32();
            max = binaryReader.ReadInt32();
        }       
    }


    /// <summary>
    /// ClampedIntParameterを取り扱う為のクラス
    /// </summary>
    public class ClampedIntParameterKun : VolumeParameterKun<int>
    {
        public int min;
        public int max;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override int value
        {
            get => m_Value;
            set { m_Value = Mathf.Clamp(value, min, max); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <see cref="ClampedIntParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="min">The minimum value to clamp the parameter to</param>
        /// <param name="max">The maximum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public ClampedIntParameterKun(int value, int min, int max, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.ClampedIntParameterKun;
            this.min = min;
            this.max = max;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Value);
            binaryWriter.Write(min);
            binaryWriter.Write(max);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadInt32();
            min = binaryReader.ReadInt32();
            max = binaryReader.ReadInt32();
        }       
    }


    /// <summary>
    /// NoInterpClampedIntParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpClampedIntParameterKun : VolumeParameterKun<int>
    {
        /// <summary>
        /// The minimum value to clamp this parameter to.
        /// </summary>            
        public int min;

        /// <summary>
        /// The maximum value to clamp this parameter to.
        /// </summary>            
        public int max;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override int value
        {
            get => m_Value;
            set { m_Value = Mathf.Clamp(value, min, max); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <see cref="NoInterpClampedIntParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="min">The minimum value to clamp the parameter to</param>
        /// <param name="max">The maximum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpClampedIntParameterKun(int value, int min, int max, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpClampedIntParameterKun;
            this.min = min;
            this.max = max;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Value);
            binaryWriter.Write(min);
            binaryWriter.Write(max);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadInt32();
            min = binaryReader.ReadInt32();
            max = binaryReader.ReadInt32();
        }
    }


    /// <summary>
    /// FloatParameterを取り扱う為のクラス
    /// </summary>
    public class FloatParameterKun : VolumeParameterKun<float>
    {
        /// <summary>
        /// Creates a new <seealso cref="FloatParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter</param>
        /// <param name="overrideState">The initial override state for the parameter</param>
        public FloatParameterKun(float value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.FloatParameterKun;
        }

        /// <summary>
        /// Interpolates between two <c>float</c> values.
        /// </summary>
        /// <param name="from">The start value</param>
        /// <param name="to">The end value</param>
        /// <param name="t">The interpolation factor in range [0,1]</param>
        public sealed override void Interp(float from, float to, float t)
        {
            m_Value = from + (to - from) * t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadSingle();
        }
    }


    /// <summary>
    /// NoInterpFloatParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpFloatParameterKun : VolumeParameterKun<float>
    {
        /// <summary>
        /// Creates a new <seealso cref="NoInterpFloatParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpFloatParameterKun(float value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpFloatParameterKun;
        }
    }

    public class MinFloatParameterKun : FloatParameterKun
    {
        /// <summary>
        /// The minimum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float min;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override float value
        {
            get => m_Value;
            set { m_Value = Mathf.Max(value, min); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <seealso cref="MinFloatParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="min">The minimum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public MinFloatParameterKun(float value, float min, bool overrideState = false)
            : base(value, overrideState)
        {
            this.min = min;
            m_VolumeParameterKunType = VolumeParameterKunType.MinFloatParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Value);
            binaryWriter.Write(min);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadSingle();
            min = binaryReader.ReadSingle();
        }        
    }


    /// <summary>
    /// NoInterpMinFloatParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpMinFloatParameterKun : VolumeParameterKun<float>
    {
        /// <summary>
        /// The minimum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float min;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override float value
        {
            get => m_Value;
            set { m_Value = Mathf.Max(value, min); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <seealso cref="NoInterpMinFloatParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to storedin the parameter.</param>
        /// <param name="min">The minimum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpMinFloatParameterKun(float value, float min, bool overrideState = false)
            : base(value, overrideState)
        {
            this.min = min;
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpMinFloatParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Value);
            binaryWriter.Write(min);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadSingle();
            min = binaryReader.ReadSingle();
        }        
    }


    /// <summary>
    /// MaxFloatParameterを取り扱う為のクラス
    /// </summary>
    public class MaxFloatParameterKun : FloatParameterKun
    {
        /// <summary>
        /// The maximum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float max;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override float value
        {
            get => m_Value;
            set { m_Value = Mathf.Min(value, max); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <seealso cref="MaxFloatParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="max">The maximum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public MaxFloatParameterKun(float value, float max, bool overrideState = false)
            : base(value, overrideState)
        {
            this.max = max;
            m_VolumeParameterKunType = VolumeParameterKunType.MaxFloatParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Value);
            binaryWriter.Write(max);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadSingle();
            max = binaryReader.ReadSingle();
        }        
    }


    /// <summary>
    /// NoInterpMaxFloatParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpMaxFloatParameterKun : VolumeParameterKun<float>
    {
        /// <summary>
        /// The maximum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float max;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override float value
        {
            get => m_Value;
            set { m_Value = Mathf.Min(value, max); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <seealso cref="NoInterpMaxFloatParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="max">The maximum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpMaxFloatParameterKun(float value, float max, bool overrideState = false)
            : base(value, overrideState)
        {
            this.max = max;
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpMaxFloatParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Value);
            binaryWriter.Write(max);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadSingle();
            max = binaryReader.ReadSingle();
        }        
    }


    /// <summary>
    /// ClampedFloatParameterを取り扱う為のクラス
    /// </summary>
    public class ClampedFloatParameterKun : FloatParameterKun
    {
        /// <summary>
        /// The minimum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float min;

        /// <summary>
        /// The maximum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float max;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override float value
        {
            get => m_Value;
            set { m_Value = Mathf.Clamp(value, min, max); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <seealso cref="ClampedFloatParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="min">The minimum value to clamp the parameter to</param>
        /// <param name="max">The maximum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public ClampedFloatParameterKun(float value, float min, float max, bool overrideState = false)
            : base(value, overrideState)
        {
            this.min = min;
            this.max = max;
            m_VolumeParameterKunType = VolumeParameterKunType.ClampedFloatParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(min);
            binaryWriter.Write(max);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            min = binaryReader.ReadSingle();
            max = binaryReader.ReadSingle();
        }        
    }


    /// <summary>
    /// NoInterpClampedFloatParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpClampedFloatParameterKun : VolumeParameterKun<float>
    {
        /// <summary>
        /// The minimum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float min;

        /// <summary>
        /// The maximum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float max;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override float value
        {
            get => m_Value;
            set { m_Value = Mathf.Clamp(value, min, max); m_Dirty = true; }
        }

        /// <summary>
        /// Creates a new <seealso cref="NoInterpClampedFloatParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="min">The minimum value to clamp the parameter to</param>
        /// <param name="max">The maximum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpClampedFloatParameterKun(float value, float min, float max, bool overrideState = false)
            : base(value, overrideState)
        {
            this.min = min;
            this.max = max;
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpClampedFloatParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Value);
            binaryWriter.Write(min);
            binaryWriter.Write(max);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = binaryReader.ReadSingle();
            min = binaryReader.ReadSingle();
            max = binaryReader.ReadSingle();
        }        
    }

    /// <summary>
    /// FloatRangeParameterを取り扱う為のクラス
    /// </summary>
    public class FloatRangeParameterKun : VolumeParameterKun<Vector2>
    {
        /// <summary>
        /// The minimum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float min;

        /// <summary>
        /// The maximum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float max;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override Vector2 value
        {
            get => m_Value;
            set
            {
                m_Value.x = Mathf.Max(value.x, min);
                m_Value.y = Mathf.Min(value.y, max);
                m_Dirty = true;
            }
        }

        /// <summary>
        /// Creates a new <seealso cref="FloatRangeParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="min">The minimum value to clamp the parameter to</param>
        /// <param name="max">The maximum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public FloatRangeParameterKun(Vector2 value, float min, float max, bool overrideState = false)
            : base(value, overrideState)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Interpolates between two <c>Vector2</c> values.
        /// </summary>
        /// <param name="from">The start value</param>
        /// <param name="to">The end value</param>
        /// <param name="t">The interpolation factor in range [0,1]</param>
        public override void Interp(Vector2 from, Vector2 to, float t)
        {
            m_Value.x = from.x + (to.x - from.x) * t;
            m_Value.y = from.y + (to.y - from.y) * t;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            // Vector2Kunへ変換sする
            var v2 = new Vector2Kun(m_Value);
            SerializerKun.Serialize<Vector2Kun>(binaryWriter, v2);
            binaryWriter.Write(min);
            binaryWriter.Write(max);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            // Vector2Kunで受けっとって変換
            var v2 = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
            m_Value = v2.GetVector2();
            min = binaryReader.ReadSingle();
            max = binaryReader.ReadSingle();
        }
    }


    /// <summary>
    ///  NoInterpFloatRangeParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpFloatRangeParameterKun : VolumeParameterKun<Vector2>
    {
        /// <summary>
        /// The minimum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float min;

        /// <summary>
        /// The maximum value to clamp this parameter to.
        /// </summary>
        [NonSerialized]
        public float max;

        /// <summary>
        /// The value that this parameter stores.
        /// </summary>
        /// <remarks>
        /// You can override this property to define custom behaviors when the value is changed.
        /// </remarks>
        public override Vector2 value
        {
            get => m_Value;
            set
            {
                m_Value.x = Mathf.Max(value.x, min);
                m_Value.y = Mathf.Min(value.y, max);
                m_Dirty = true;
            }
        }

        /// <summary>
        /// Creates a new <seealso cref="NoInterpFloatRangeParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="min">The minimum value to clamp the parameter to</param>
        /// <param name="max">The maximum value to clamp the parameter to.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpFloatRangeParameterKun(Vector2 value, float min, float max, bool overrideState = false)
            : base(value, overrideState)
        {
            this.min = min;
            this.max = max;
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpFloatRangeParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);            
            SerializerKun.Serialize<Vector2Kun>(binaryWriter, new Vector2Kun(m_Value));
            binaryWriter.Write(min);
            binaryWriter.Write(max);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader).GetVector2();
            min = binaryReader.ReadSingle();
            max = binaryReader.ReadSingle();
        }
    }

    /// <summary>
    /// ColorParameterを取り扱う為のクラス
    /// </summary>
    public class ColorParameterKun : VolumeParameterKun<Color>
    {
        /// <summary>
        /// Is this color HDR?
        /// </summary>
        [NonSerialized]
        public bool hdr = false;

        /// <summary>
        /// Should the alpha channel be editable in the editor?
        /// </summary>
        [NonSerialized]
        public bool showAlpha = true;

        /// <summary>
        /// Should the eye dropper be visible in the editor?
        /// </summary>
        [NonSerialized]
        public bool showEyeDropper = true;

        /// <summary>
        /// Creates a new <seealso cref="ColorParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public ColorParameterKun(Color value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.ColorParameterKun;
        }

        /// <summary>
        /// Creates a new <seealso cref="ColorParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="hdr">Specifies whether the color is HDR or not.</param>
        /// <param name="showAlpha">Specifies whether you can edit the alpha channel in the Inspector or not.</param>
        /// <param name="showEyeDropper">Specifies whether the eye dropper is visible in the editor or not.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public ColorParameterKun(Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false)
            : base(value, overrideState)
        {
            this.hdr = hdr;
            this.showAlpha = showAlpha;
            this.showEyeDropper = showEyeDropper;
            this.overrideState = overrideState;
            m_VolumeParameterKunType = VolumeParameterKunType.ColorParameterKun;
        }

        /// <summary>
        /// Interpolates between two <c>Color</c> values.
        /// </summary>
        /// <remarks>
        /// For performance reasons, this function interpolates the RGBA channels directly.
        /// </remarks>
        /// <param name="from">The start value.</param>
        /// <param name="to">The end value.</param>
        /// <param name="t">The interpolation factor in range [0,1].</param>
        public override void Interp(Color from, Color to, float t)
        {
            // Lerping color values is a sensitive subject... We looked into lerping colors using
            // HSV and LCH but they have some downsides that make them not work correctly in all
            // situations, so we stick with RGB lerping for now, at least its behavior is
            // predictable despite looking desaturated when `t ~= 0.5` and it's faster anyway.
            m_Value.r = from.r + (to.r - from.r) * t;
            m_Value.g = from.g + (to.g - from.g) * t;
            m_Value.b = from.b + (to.b - from.b) * t;
            m_Value.a = from.a + (to.a - from.a) * t;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<ColorKun>(binaryWriter, new ColorKun(m_Value));
            binaryWriter.Write(hdr);
            binaryWriter.Write(showAlpha);
            binaryWriter.Write(showEyeDropper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = SerializerKun.DesirializeObject<ColorKun>(binaryReader).GetColor();
            hdr = binaryReader.ReadBoolean();
            showAlpha = binaryReader.ReadBoolean();
            showEyeDropper = binaryReader.ReadBoolean();
        }        
    }

    /// <summary>
    /// NoInterpColorParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpColorParameterKun : VolumeParameterKun<Color>
    {
        /// <summary>
        /// Specifies whether the color is HDR or not.
        /// </summary>
        public bool hdr = false;

        /// <summary>
        /// Specifies whether you can edit the alpha channel in the Inspector or not.
        /// </summary>
        [NonSerialized]
        public bool showAlpha = true;

        /// <summary>
        /// Specifies whether the eye dropper is visible in the editor or not.
        /// </summary>
        [NonSerialized]
        public bool showEyeDropper = true;

        /// <summary>
        /// Creates a new <seealso cref="NoInterpColorParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpColorParameterKun(Color value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpColorParameterKun;
        }

        /// <summary>
        /// Creates a new <seealso cref="NoInterpColorParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="hdr">Specifies whether the color is HDR or not.</param>
        /// <param name="showAlpha">Specifies whether you can edit the alpha channel in the Inspector or not.</param>
        /// <param name="showEyeDropper">Specifies whether the eye dropper is visible in the editor or not.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpColorParameterKun(Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false)
            : base(value, overrideState)
        {
            this.hdr = hdr;
            this.showAlpha = showAlpha;
            this.showEyeDropper = showEyeDropper;
            this.overrideState = overrideState;
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpColorParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<ColorKun>(binaryWriter, new ColorKun(m_Value));
            binaryWriter.Write(hdr);
            binaryWriter.Write(showAlpha);
            binaryWriter.Write(showEyeDropper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = SerializerKun.DesirializeObject<ColorKun>(binaryReader).GetColor();
            hdr = binaryReader.ReadBoolean();
            showAlpha = binaryReader.ReadBoolean();
            showEyeDropper = binaryReader.ReadBoolean();
        }        
    }

    /// <summary>
    /// Vector2Parameterを取り扱う為のクラス
    /// </summary>
    public class Vector2ParameterKun : VolumeParameterKun<Vector2>
    {
        /// <summary>
        /// Creates a new <seealso cref="Vector2Parameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public Vector2ParameterKun(Vector2 value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.Vector2ParameterKun;
        }

        /// <summary>
        /// Interpolates between two <c>Vector2</c> values.
        /// </summary>
        /// <param name="from">The start value.</param>
        /// <param name="to">The end value.</param>
        /// <param name="t">The interpolation factor in range [0,1].</param>
        public override void Interp(Vector2 from, Vector2 to, float t)
        {
            m_Value.x = from.x + (to.x - from.x) * t;
            m_Value.y = from.y + (to.y - from.y) * t;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<Vector2Kun>(binaryWriter, new Vector2Kun(m_Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader).GetVector2();
        }        
    }

    /// <summary>
    /// NoInterpVector2Parameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpVector2ParameterKun : VolumeParameterKun<Vector2>
    {
        /// <summary>
        /// Creates a new <seealso cref="NoInterpVector2Parameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpVector2ParameterKun(Vector2 value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpVector2ParameterKun;
        }


        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<Vector2Kun>(binaryWriter, new Vector2Kun(m_Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader).GetVector2();
        }        
    }

    /// <summary>
    /// Vector3Parameterを取り扱う為のクラス
    /// </summary>
    public class Vector3ParameterKun : VolumeParameterKun<Vector3>
    {
        /// <summary>
        /// Creates a new <seealso cref="Vector3Parameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public Vector3ParameterKun(Vector3 value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.Vector3ParameterKun;
        }

        /// <summary>
        /// Interpolates between two <c>Vector3</c> values.
        /// </summary>
        /// <param name="from">The start value.</param>
        /// <param name="to">The end value.</param>
        /// <param name="t">The interpolation factor in range [0,1].</param>
        public override void Interp(Vector3 from, Vector3 to, float t)
        {
            m_Value.x = from.x + (to.x - from.x) * t;
            m_Value.y = from.y + (to.y - from.y) * t;
            m_Value.z = from.z + (to.z - from.z) * t;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<Vector3Kun>(binaryWriter, new Vector3Kun(m_Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader).GetVector3();
        }        
    }


    /// <summary>
    /// NoInterpVector3Parameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpVector3ParameterKun : VolumeParameterKun<Vector3>
    {
        /// <summary>
        /// Creates a new <seealso cref="Vector3Parameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpVector3ParameterKun(Vector3 value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpVector3ParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<Vector3Kun>(binaryWriter, new Vector3Kun(m_Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader).GetVector3();
        }
    }


    /// <summary>
    /// Vector4Parameterを取り扱う為のクラス
    /// </summary>
    public class Vector4ParameterKun : VolumeParameterKun<Vector4>
    {
        /// <summary>
        /// Creates a new <seealso cref="Vector4Parameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public Vector4ParameterKun(Vector4 value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.Vector4ParameterKun;
        }

        /// <summary>
        /// Interpolates between two <c>Vector4</c> values.
        /// </summary>
        /// <param name="from">The start value.</param>
        /// <param name="to">The end value.</param>
        /// <param name="t">The interpolation factor in range [0,1].</param>
        public override void Interp(Vector4 from, Vector4 to, float t)
        {
            m_Value.x = from.x + (to.x - from.x) * t;
            m_Value.y = from.y + (to.y - from.y) * t;
            m_Value.z = from.z + (to.z - from.z) * t;
            m_Value.w = from.w + (to.w - from.w) * t;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<Vector4Kun>(binaryWriter, new Vector4Kun(m_Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = SerializerKun.DesirializeObject<Vector4Kun>(binaryReader).GetVector4();
        }        
    }

    /// <summary>
    /// NoInterpVector4Parameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpVector4ParameterKun : VolumeParameterKun<Vector4>
    {
        /// <summary>
        /// Creates a new <seealso cref="Vector4Parameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpVector4ParameterKun(Vector4 value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpVector4ParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<Vector4Kun>(binaryWriter, new Vector4Kun(m_Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = SerializerKun.DesirializeObject<Vector4Kun>(binaryReader).GetVector4();
        }        
    }


    /// <summary>
    /// TextureParameterを取り扱う為のクラス
    /// </summary>
    public class TextureParameterKun : VolumeParameterKun<Texture>
    {
        /// <summary>
        /// Creates a new <seealso cref="TextureParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public TextureParameterKun(Texture value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.TextureParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<TextureKun>(binaryWriter, new TextureKun(m_Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            var textureKun = SerializerKun.DesirializeObject<TextureKun>(binaryReader);
            if (textureKun != null)
            {
                textureKun.WriteBack(m_Value);
            }
        }

        public override void WriteBack(object parameter)
        {
            var t = parameter.GetType();
            {
                var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
                pi.SetValue(parameter, overrideState);
            }
            // 現状Textureの書き換えは行わない
        }
    }


    /// <summary>
    /// NoInterpTextureParameterを取り扱う為のクラス
    /// </summary>
    public class NoInterpTextureParameterKun : VolumeParameterKun<Texture>
    {
        /// <summary>
        /// Creates a new <seealso cref="TextureParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpTextureParameterKun(Texture value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpTextureParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<TextureKun>(binaryWriter, new TextureKun(m_Value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            var textureKun= SerializerKun.DesirializeObject<TextureKun>(binaryReader);
            if (textureKun != null)
            {
                textureKun.WriteBack(m_Value);
            }
        }

        public override void WriteBack(object parameter)
        {            
            var t = parameter.GetType();
            {
                var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
                pi.SetValue(parameter, overrideState);
            }
            // 現状Textureの書き換えは行わない
        }
    }


    public class Texture2DParameterKun : VolumeParameterKun<Texture>
    {
        /// <summary>
        /// Creates a new <seealso cref="Texture2DParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public Texture2DParameterKun(Texture value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.Texture2DParameterKun;
        }


        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<TextureKun>(binaryWriter, new TextureKun(m_Value));
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);

            var textureKun = SerializerKun.DesirializeObject<TextureKun>(binaryReader);
            if (textureKun != null)
            {
                textureKun.WriteBack(m_Value);
            }
        }

        public override void WriteBack(object parameter)
        {
            var t = parameter.GetType();
            {
                var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
                pi.SetValue(parameter, overrideState);
            }            
        }
    }

    public class Texture3DParameterKun : VolumeParameterKun<Texture>
    {
        /// <summary>
        /// Creates a new <seealso cref="Texture3DParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public Texture3DParameterKun(Texture value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.Texture3DParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<TextureKun>(binaryWriter, new TextureKun(m_Value));
        }
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            var textureKun = SerializerKun.DesirializeObject<TextureKun>(binaryReader);
            if (textureKun != null)
            {
                textureKun.WriteBack(m_Value);
            }
        }

        public override void WriteBack(object parameter)
        {
            var t = parameter.GetType();
            {
                var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
                pi.SetValue(parameter, overrideState);
            }
        }
    }

    public class RenderTextureParameterKun : VolumeParameterKun<RenderTexture>
    {
        /// <summary>
        /// Creates a new <seealso cref="RenderTextureParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public RenderTextureParameterKun(RenderTexture value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.RenderTextureParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<RenderTextureKun>(binaryWriter, new RenderTextureKun(m_Value));
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            var renderTextureKun = SerializerKun.DesirializeObject<RenderTextureKun>(binaryReader);
            if(renderTextureKun != null)
            {
                renderTextureKun.WriteBack(m_Value);
            }
        }

        public override void WriteBack(object parameter)
        {
            var t = parameter.GetType();
            {
                var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
                pi.SetValue(parameter, overrideState);
            }
        }
    }

    public class NoInterpRenderTextureParameterKun : VolumeParameterKun<RenderTexture>
    {
        /// <summary>
        /// Creates a new <seealso cref="NoInterpRenderTextureParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpRenderTextureParameterKun(RenderTexture value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpRenderTextureParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<RenderTextureKun>(binaryWriter, new RenderTextureKun(m_Value));
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            var renderTextureKun = SerializerKun.DesirializeObject<RenderTextureKun>(binaryReader);
            if(renderTextureKun != null)
            {
                renderTextureKun.WriteBack(m_Value);
            }
        }
        public override void WriteBack(object parameter)
        {
            var t = parameter.GetType();
            {
                var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
                pi.SetValue(parameter, overrideState);
            }            
        }
    }


    public class CubemapParameterKun : VolumeParameterKun<Texture>
    {
        /// <summary>
        /// Creates a new <seealso cref="CubemapParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public CubemapParameterKun(Texture value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.CubemapParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<TextureKun>(binaryWriter, new TextureKun(m_Value));
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            var texture = SerializerKun.DesirializeObject<TextureKun>(binaryReader);
            if(texture != null)
            {
                texture.WriteBack(m_Value);
            }
        }

        public override void WriteBack(object parameter)
        {
            var t = parameter.GetType();
            {
                var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
                pi.SetValue(parameter, overrideState);
            }            
        }
    }

    public class NoInterpCubemapParameterKun : VolumeParameterKun<Cubemap>
    {
        /// <summary>
        /// Creates a new <seealso cref="NoInterpCubemapParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to store in the parameter.</param>
        /// <param name="overrideState">The initial override state for the parameter.</param>
        public NoInterpCubemapParameterKun(Cubemap value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.NoInterpCubemapParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<CubemapKun>(binaryWriter, new CubemapKun(m_Value));
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            var cubemapKun = SerializerKun.DesirializeObject<CubemapKun>(binaryReader);
            if(cubemapKun != null)
            {
                cubemapKun.WriteBack(m_Value);
            }
        }

        public override void WriteBack(object parameter)
        {
            var t = parameter.GetType();
            {
                var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
                pi.SetValue(parameter, overrideState);
            }            
        }
    }

    public class AnimationCurveParameterKun : VolumeParameterKun<AnimationCurve>
    {
        /// <summary>
        /// Creates a new <seealso cref="AnimationCurveParameter"/> instance.
        /// </summary>
        /// <param name="value">The initial value to be stored in the parameter</param>
        /// <param name="overrideState">The initial override state for the parameter</param>
        public AnimationCurveParameterKun(AnimationCurve value, bool overrideState = false)
            : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.AnimationCurveParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<AnimationCurveKun>(binaryWriter, new AnimationCurveKun(m_Value));
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            
            var animationCurveKun = SerializerKun.DesirializeObject<AnimationCurveKun>(binaryReader);
            if(animationCurveKun != null)
            {
                m_Value =  animationCurveKun.GetAnimationCurve();
            }
        }

        public override void WriteBack(object parameter)
        {
            // overrideStateのみライトバック
            var t = parameter.GetType();
            var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
            pi.SetValue(parameter, overrideState);
        }
    }


    public class TextureCurveParameterKun : VolumeParameterKun<TextureCurveKun>
    {
        public TextureCurveParameterKun(TextureCurveKun value,bool overrideState = false) : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.TextureCurveParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<TextureCurveKun>(binaryWriter, m_Value);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Value = SerializerKun.DesirializeObject<TextureCurveKun>(binaryReader);
        }

        public override void WriteBack(object parameter)
        {
            // overrideStateのみライトバック
            var t = parameter.GetType();
            var pi = t.GetProperty("overrideState", BindingFlags.Instance | BindingFlags.Public);
            pi.SetValue(parameter, overrideState);
        }
    }

    // URP用のパラメータ

    public class TonemappingModeParameterKun : VolumeParameterKun<int>
    {
        public TonemappingModeParameterKun(int value, bool overrideState = false) : base(value, overrideState)
        {
            m_VolumeParameterKunType = VolumeParameterKunType.TonemappingModeParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write((int)value);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            value = binaryReader.ReadInt32();
        }
    }


    public sealed class DepthOfFieldModeParameterKun : VolumeParameterKun<int> 
    { 
        public DepthOfFieldModeParameterKun(int value, bool overrideState = false) : base(value, overrideState) 
        {
            m_VolumeParameterKunType = VolumeParameterKunType.DepthOfFieldModeParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write((int)value);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            value = binaryReader.ReadInt32();
        }
    }
    

    public class FilmGrainLookupParameterKun : VolumeParameterKun<int> 
    { 
        public FilmGrainLookupParameterKun(int value, bool overrideState = false) : base(value, overrideState) 
        {
            m_VolumeParameterKunType = VolumeParameterKunType.FilmGrainLookupParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write((int)value);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            value = binaryReader.ReadInt32();
        }        
    }    
    

    public class MotionBlurModeParameterKun : VolumeParameterKun<int> {
        public MotionBlurModeParameterKun(int value, bool overrideState = false) : base(value, overrideState) 
        {
            m_VolumeParameterKunType = VolumeParameterKunType.MotionBlurModeParameterKun;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write((int)value);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            value = binaryReader.ReadInt32();
        }
    }


    public class MotionBlurQualityParameterKun : VolumeParameterKun<int> { 
        public MotionBlurQualityParameterKun(int value, bool overrideState = false) : base(value, overrideState) 
        {
            m_VolumeParameterKunType = VolumeParameterKunType.MotionBlurQualityParameterKun;
        }
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(value);
        }

        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            value = binaryReader.ReadInt32();
        }
    }
}
