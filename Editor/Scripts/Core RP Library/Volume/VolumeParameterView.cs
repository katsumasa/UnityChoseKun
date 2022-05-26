using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utj.UnityChoseKun.Engine.Rendering;


namespace Utj.UnityChoseKun.Editor.Rendering
{
   /// <summary>
   /// VolumeParameterをInspectorへ表示する為のクラス
   /// </summary>
    public abstract class VolumeParameterView
    {
        static readonly Dictionary<VolumeParameterKun.VolumeParameterKunType, GUIContent> m_volumeParameterTypeToContentl = new Dictionary<VolumeParameterKun.VolumeParameterKunType, GUIContent>()
        {
            {VolumeParameterKun.VolumeParameterKunType.BoolParameterKun, new GUIContent("bool")},
            {VolumeParameterKun.VolumeParameterKunType.LayerMaskParametetKun,new GUIContent("LayerMask") },
            {VolumeParameterKun.VolumeParameterKunType.IntParameterKun, new GUIContent("int")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpIntParameterKun, new GUIContent("int")},
            {VolumeParameterKun.VolumeParameterKunType.MinIntParameterKun,new GUIContent("int")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpMinIntParameterKun, new GUIContent("int")},
            {VolumeParameterKun.VolumeParameterKunType.MaxIntParameterKun,new GUIContent("int")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpMaxIntParameterKun,new GUIContent("int")},
            {VolumeParameterKun.VolumeParameterKunType.ClampedIntParameterKun,new GUIContent("int")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpClampedIntParameterKun,new GUIContent("int")},
            {VolumeParameterKun.VolumeParameterKunType.FloatParameterKun,new GUIContent("float") },
            {VolumeParameterKun.VolumeParameterKunType.NoInterpFloatParameterKun, new GUIContent("float")},
            {VolumeParameterKun.VolumeParameterKunType.MinFloatParameterKun,new GUIContent("float")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpMinFloatParameterKun, new GUIContent("float")},
            {VolumeParameterKun.VolumeParameterKunType.MaxFloatParameterKun,new GUIContent("float")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpMaxFloatParameterKun, new GUIContent("float")},            
            {VolumeParameterKun.VolumeParameterKunType.ClampedFloatParameterKun, new GUIContent("float")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpClampedFloatParameterKun,new GUIContent("float")},
            {VolumeParameterKun.VolumeParameterKunType.FloatRangeParameterKun, new GUIContent("foat")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpFloatRangeParameterKun,new GUIContent("float")},
            {VolumeParameterKun.VolumeParameterKunType.ColorParameterKun,new GUIContent ("Color")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpColorParameterKun, new GUIContent("Color")},
            {VolumeParameterKun.VolumeParameterKunType.Vector2ParameterKun,new GUIContent("Vector2")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpVector2ParameterKun,new GUIContent("Vector2")},
            {VolumeParameterKun.VolumeParameterKunType.Vector3ParameterKun,new GUIContent("Vector3")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpVector3ParameterKun, new GUIContent("Vector3")},
            {VolumeParameterKun.VolumeParameterKunType.Vector4ParameterKun, new GUIContent("Vector4")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpVector4ParameterKun,new GUIContent("Vector4")},
            {VolumeParameterKun.VolumeParameterKunType.TextureParameterKun, new GUIContent("Texture")},
            {VolumeParameterKun.VolumeParameterKunType.Texture2DParameterKun, new GUIContent("Texture2D")},
            {VolumeParameterKun.VolumeParameterKunType.Texture3DParameterKun, new GUIContent("Texture3D")},
            {VolumeParameterKun.VolumeParameterKunType.RenderTextureParameterKun, new GUIContent("RenderTexture")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpRenderTextureParameterKun, new GUIContent("RenderTexture")},
            {VolumeParameterKun.VolumeParameterKunType.CubemapParameterKun,new GUIContent("Cubemap")},
            {VolumeParameterKun.VolumeParameterKunType.NoInterpCubemapParameterKun, new GUIContent("Cubemap")},
            {VolumeParameterKun.VolumeParameterKunType.AnimationCurveParameterKun, new GUIContent("AnimationCurve")},
            {VolumeParameterKun.VolumeParameterKunType.TextureCurveParameterKun,new GUIContent("TextureCurve") },
            {VolumeParameterKun.VolumeParameterKunType.TonemappingModeParameterKun,new GUIContent("Mode")},
            {VolumeParameterKun.VolumeParameterKunType.DepthOfFieldModeParameterKun, new GUIContent("Mode")},
            {VolumeParameterKun.VolumeParameterKunType.FilmGrainLookupParameterKun, new GUIContent("Lookup")},
            {VolumeParameterKun.VolumeParameterKunType.MotionBlurModeParameterKun, new GUIContent("Mode")},
            {VolumeParameterKun.VolumeParameterKunType.MotionBlurQualityParameterKun, new GUIContent("Mode")},
        };
        




        public T GetValue<T>() 
        {
            return ((VolumeParameterView <T>) this).volumeParameterKun;
        }
        

        public virtual void Header<T>() where T : VolumeParameterKun
        {
            // Dirtyフラグをここでクリアする
            GetValue<T>().dirty = false;

            EditorGUI.BeginChangeCheck();
            var value = GetValue<T>().overrideState;
            value = EditorGUILayout.ToggleLeft(((VolumeParameterView<T>)this).label, value);
            if(EditorGUI.EndChangeCheck())            
            {
                GetValue<T>().overrideState = value;
            }
        }


        public virtual void OnGUI()
        {
        }


        public static VolumeParameterView Allocater(VolumeParameterKun volumeParameterKun,GUIContent content = null)
        {

            if(content == null)
            {
                content = m_volumeParameterTypeToContentl[volumeParameterKun.volumeParameterKunType];
            }

            switch (volumeParameterKun.volumeParameterKunType)
            {
                case VolumeParameterKun.VolumeParameterKunType.BoolParameterKun:                    
                    return new BoolParameterView(content, (BoolParameterKun)volumeParameterKun);
                
                case VolumeParameterKun.VolumeParameterKunType.LayerMaskParametetKun:                    
                    return new LayerMaskParameterView(content, (LayerMaskParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.IntParameterKun:                    
                    return new IntParameterView(content, (IntParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpIntParameterKun:                    
                    return new NoInterpIntParameterView(content,(NoInterpIntParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.MinIntParameterKun:
                    return new MinIntParameterView(content,(MinIntParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpMinIntParameterKun:
                    return new NoInterpMinIntParameterView(content,(NoInterpMinIntParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.MaxIntParameterKun:
                    return new MaxIntParameterView(content,(MaxIntParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpMaxIntParameterKun:
                    return new NoInterpMaxIntParameterView(content,(NoInterpMaxIntParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.ClampedIntParameterKun:
                    return new ClampedIntParameterView(content,(ClampedIntParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpClampedIntParameterKun:
                    return new NoInterpClampedIntParameterView(content,(NoInterpClampedIntParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.FloatParameterKun:
                    return new FloatParameterView(content,(FloatParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpFloatParameterKun:
                    return new NoInterpFloatParameterView(content,(NoInterpFloatParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.MinFloatParameterKun:
                    return new MinFloatParameterView(content,(MinFloatParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpMinFloatParameterKun:
                    return new NoInterpMinFloatParameterView(content,(NoInterpMinFloatParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.MaxFloatParameterKun:
                    return new MaxFloatParameterView(content,(MaxFloatParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpMaxFloatParameterKun:
                    return new NoInterpMaxFloatParameterView(content, (NoInterpMaxFloatParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.ClampedFloatParameterKun:
                    return new ClampedFloatParameterView(content,(ClampedFloatParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpClampedFloatParameterKun:
                    return new NoInterpClampedFloatParameterView(content,(NoInterpClampedFloatParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.FloatRangeParameterKun:
                    return new FloatRangeParameterView(content, (FloatRangeParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpFloatRangeParameterKun:
                    return new NoInterpFloatRangeParameterView(content,(NoInterpFloatRangeParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.ColorParameterKun:
                    return new ColorParameterView(content,(ColorParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpColorParameterKun:
                    return new NoInterpColorParameterView(content,(NoInterpColorParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.Vector2ParameterKun:
                    return new Vector2ParameterView(content,(Vector2ParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpVector2ParameterKun:
                    return new NoInterpVector2ParameterView(content,(NoInterpVector2ParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.Vector3ParameterKun:
                    return new Vector3ParameterView(content,(Vector3ParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpVector3ParameterKun:
                    return new NoInterpVector3ParameterView(content,(NoInterpVector3ParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.Vector4ParameterKun:
                    return new Vector4ParameterView(content,(Vector4ParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpVector4ParameterKun:
                    return new NoInterpVector4ParameterView(content,(NoInterpVector4ParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.TextureParameterKun:
                    return new TextureParameterView(content, (TextureParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpTextureParameterKun:
                    return new NoInterpTextureParameterView(content, (NoInterpTextureParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.Texture2DParameterKun:
                    return new Texture2DParameterView(content, (Texture2DParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.Texture3DParameterKun:
                    return new Texture3DParameterView(content, (Texture3DParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.RenderTextureParameterKun:
                    return new RenderTextureParameterView(content, (RenderTextureParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpRenderTextureParameterKun:
                    return new NoInterpRenderTextureParameterView(content, (NoInterpRenderTextureParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.CubemapParameterKun:
                    return new CubemapParameterView(content,(CubemapParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.NoInterpCubemapParameterKun:
                    return new NoInterpCubemapParameterView(content, (NoInterpCubemapParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.AnimationCurveParameterKun:
                    return new AnimationCurveParameterView(content,(AnimationCurveParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.TextureCurveParameterKun:
                    return new UnknownParameterView();

                // URP Override
                case VolumeParameterKun.VolumeParameterKunType.TonemappingModeParameterKun:
                    return new TonemappingModeParameterView(content, (TonemappingModeParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.DepthOfFieldModeParameterKun:
                    return new DepthOfFieldModeParameterView(content,(DepthOfFieldModeParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.FilmGrainLookupParameterKun:
                    return new FilmGrainLookupParameterView(content,(FilmGrainLookupParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.MotionBlurModeParameterKun:
                    return new MotionBlurModeParameterView(content,(MotionBlurModeParameterKun)volumeParameterKun);

                case VolumeParameterKun.VolumeParameterKunType.MotionBlurQualityParameterKun:
                    return new MotionBlurQualityParameterView (content,(MotionBlurQualityParameterKun)volumeParameterKun);                    
            }
            return new UnknownParameterView();
        }
    }

    
    public class VolumeParameterView<T> : VolumeParameterView
    //public class VolumeParameterView<T> where T : VolumeParameterKun
    {
        protected T m_volumeParameterKun;
        protected GUIContent m_label;


        public virtual T volumeParameterKun
        {
            get { return m_volumeParameterKun; }
        }
        
        public virtual GUIContent label
        {
            get { return m_label; }
        }

        public VolumeParameterView(GUIContent label,T volumeParameterKun)
        {
            m_label= label;
            m_volumeParameterKun = volumeParameterKun;
        }

        public override void OnGUI() { }        
    }


    /// <summary>
    /// UnityChoseKunがサポートしていないパラメーター用
    /// </summary>
    public class UnknownParameterView : VolumeParameterView
    {
        public override void OnGUI()
        {
            EditorGUILayout.LabelField("Unkown Parameter");
        }
    }


    /// <summary>
    /// BoolParameterを表示する為のクラス
    /// </summary>
    public class BoolParameterView : VolumeParameterView<BoolParameterKun>
    {        
        public BoolParameterView(GUIContent label,BoolParameterKun volumeParameterKun) : base(label,volumeParameterKun) { }
             
        public BoolParameterView(GUIContent content,VolumeParameterKun volumeParameterKun) : this(content,volumeParameterKun as BoolParameterKun) { }


        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<BoolParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.ToggleLeft("",value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.SetValue(value);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }

    public class LayerMaskParameterView : VolumeParameterView<LayerMaskParameterKun>
    {
        public LayerMaskParameterView(GUIContent label,LayerMaskParameterKun layerMaskParameter) : base(label,layerMaskParameter) { }

        public LayerMaskParameterView(GUIContent label,VolumeParameterKun volumeParameterKun) : this(label,volumeParameterKun as LayerMaskParameterKun) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            {
                base.Header<LayerMaskParameterKun>();
                EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
                {
                    var value = m_volumeParameterKun.value;
                    EditorGUI.BeginChangeCheck();
                    value = LayerMaskKun.LayerMaskField(new GUIContent(), value);
                    if (EditorGUI.EndChangeCheck())
                    {
                        m_volumeParameterKun.value = value;
                    }
                }
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    public class IntParameterView : VolumeParameterView<IntParameterKun>
    {
        public IntParameterView(GUIContent label, IntParameterKun intParameterKun) : base(label, intParameterKun) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<IntParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.IntField("", m_volumeParameterKun.value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpIntParameterView : VolumeParameterView<NoInterpIntParameterKun>
    {
        public NoInterpIntParameterView(GUIContent content, NoInterpIntParameterKun parameter) : base(content, parameter) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpIntParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.IntField("", m_volumeParameterKun.value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class MinIntParameterView : VolumeParameterView<MinIntParameterKun>
    {
        public MinIntParameterView(GUIContent content, MinIntParameterKun parameter) : base(content, parameter) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<MinIntParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.IntField("", m_volumeParameterKun.value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpMinIntParameterView : VolumeParameterView<NoInterpMinIntParameterKun>
    {

        public NoInterpMinIntParameterView(GUIContent content, NoInterpMinIntParameterKun parameter) : base(content, parameter) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpMinIntParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.IntField("", m_volumeParameterKun.value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }

    public class MaxIntParameterView : VolumeParameterView<MaxIntParameterKun>
    {
        public MaxIntParameterView(GUIContent content, MaxIntParameterKun parameter) : base(content, parameter) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<MaxIntParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.IntField("", m_volumeParameterKun.value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }

    public class NoInterpMaxIntParameterView : VolumeParameterView<NoInterpMaxIntParameterKun>
    {
        public NoInterpMaxIntParameterView(GUIContent content, NoInterpMaxIntParameterKun parameter): base(content, parameter) { }
        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpMaxIntParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.IntField("", m_volumeParameterKun.value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }

    public class ClampedIntParameterView : VolumeParameterView<ClampedIntParameterKun>
    {
        public ClampedIntParameterView(GUIContent content, ClampedIntParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<ClampedIntParameterKun>();
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.IntSlider("", m_volumeParameterKun.value, m_volumeParameterKun.min, m_volumeParameterKun.max);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpClampedIntParameterView : VolumeParameterView<NoInterpClampedIntParameterKun>
    {
        public NoInterpClampedIntParameterView(GUIContent content, NoInterpClampedIntParameterKun param) : base(content, param) { }
        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpClampedIntParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.IntSlider("", m_volumeParameterKun.value, m_volumeParameterKun.min, m_volumeParameterKun.max);            
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class FloatParameterView : VolumeParameterView<FloatParameterKun>
    {
        public FloatParameterView(GUIContent content, FloatParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<FloatParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.FloatField("", value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpFloatParameterView : VolumeParameterView<NoInterpFloatParameterKun>
    {
        public NoInterpFloatParameterView(GUIContent content, NoInterpFloatParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpFloatParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.FloatField("", value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class MinFloatParameterView : VolumeParameterView<MinFloatParameterKun>
    {
        public MinFloatParameterView(GUIContent content, MinFloatParameterKun param) : base(content, param) { }
        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<MinFloatParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.FloatField("", value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpMinFloatParameterView : VolumeParameterView<NoInterpMinFloatParameterKun>
    {
        public NoInterpMinFloatParameterView(GUIContent content, NoInterpMinFloatParameterKun param) : base(content, param) { }
        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpMinFloatParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.FloatField("", value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class MaxFloatParameterView : VolumeParameterView<MaxFloatParameterKun>
    {
        public MaxFloatParameterView(GUIContent content, MaxFloatParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<MaxFloatParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.FloatField("", value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpMaxFloatParameterView : VolumeParameterView<NoInterpMaxFloatParameterKun>
    {
        public NoInterpMaxFloatParameterView(GUIContent content, NoInterpMaxFloatParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpMaxFloatParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.FloatField("", value);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class ClampedFloatParameterView : VolumeParameterView<ClampedFloatParameterKun>
    {
        public ClampedFloatParameterView(GUIContent content, ClampedFloatParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<ClampedFloatParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();            
            value = EditorGUILayout.Slider("", value,m_volumeParameterKun.min,m_volumeParameterKun.max);            
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpClampedFloatParameterView : VolumeParameterView<NoInterpClampedFloatParameterKun>
    {
        public NoInterpClampedFloatParameterView(GUIContent content, NoInterpClampedFloatParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpClampedFloatParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            value = EditorGUILayout.Slider("", value, m_volumeParameterKun.min, m_volumeParameterKun.max);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class FloatRangeParameterView : VolumeParameterView<FloatRangeParameterKun>
    {
        public FloatRangeParameterView(GUIContent content, FloatRangeParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<FloatRangeParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = EditorGUILayout.Vector2Field("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpFloatRangeParameterView : VolumeParameterView<NoInterpFloatRangeParameterKun>
    {
        public NoInterpFloatRangeParameterView(GUIContent content, NoInterpFloatRangeParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpFloatRangeParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = EditorGUILayout.Vector2Field("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class ColorParameterView : VolumeParameterView<ColorParameterKun>
    {
        public ColorParameterView(GUIContent content, ColorParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<ColorParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = EditorGUILayout.ColorField(new GUIContent(),value1, m_volumeParameterKun.showEyeDropper, m_volumeParameterKun.showAlpha, m_volumeParameterKun.hdr);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpColorParameterView : VolumeParameterView<NoInterpColorParameterKun>
    {
        public NoInterpColorParameterView(GUIContent content, NoInterpColorParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpColorParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = EditorGUILayout.ColorField(new GUIContent(), value1, m_volumeParameterKun.showEyeDropper, m_volumeParameterKun.showAlpha, m_volumeParameterKun.hdr);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }
    

    public class Vector2ParameterView : VolumeParameterView<Vector2ParameterKun>
    {
        public Vector2ParameterView(GUIContent content, Vector2ParameterKun param) :base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<Vector2ParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value; ;
            EditorGUI.BeginChangeCheck();
            var value2 = EditorGUILayout.Vector2Field("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpVector2ParameterView : VolumeParameterView<NoInterpVector2ParameterKun>
    {
        public NoInterpVector2ParameterView(GUIContent content, NoInterpVector2ParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpVector2ParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = EditorGUILayout.Vector2Field("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class Vector3ParameterView : VolumeParameterView<Vector3ParameterKun>
    {
        public Vector3ParameterView(GUIContent content, Vector3ParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<Vector3ParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = EditorGUILayout.Vector3Field("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpVector3ParameterView : VolumeParameterView<NoInterpVector3ParameterKun>
    {
        public NoInterpVector3ParameterView(GUIContent content, NoInterpVector3ParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpVector3ParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = EditorGUILayout.Vector3Field("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class Vector4ParameterView : VolumeParameterView<Vector4ParameterKun>
    {
        public Vector4ParameterView(GUIContent content, Vector4ParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<Vector4ParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = EditorGUILayout.Vector4Field("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpVector4ParameterView : VolumeParameterView<NoInterpVector4ParameterKun>
    {
        public NoInterpVector4ParameterView(GUIContent content, NoInterpVector4ParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpVector4ParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = EditorGUILayout.Vector4Field("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class TextureParameterView : VolumeParameterView<TextureParameterKun>
    {
        public TextureParameterView(GUIContent content, TextureParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<TextureParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            if (m_volumeParameterKun.value == null)
            {
                EditorGUILayout.LabelField("", "None");
            }
            else
            {
                EditorGUILayout.LabelField("", m_volumeParameterKun.value.name);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpTextureParameterView : VolumeParameterView<NoInterpTextureParameterKun>
    {
        public NoInterpTextureParameterView(GUIContent content, NoInterpTextureParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpTextureParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            if (m_volumeParameterKun.value == null)
            {
                EditorGUILayout.LabelField("", "None");
            }
            else
            {
                EditorGUILayout.LabelField("", m_volumeParameterKun.value.name);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class Texture2DParameterView : VolumeParameterView<Texture2DParameterKun>
    {
        public Texture2DParameterView(GUIContent content, Texture2DParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<Texture2DParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            if (m_volumeParameterKun.value == null)
            {
                EditorGUILayout.LabelField("", "None");
            }
            else
            {
                EditorGUILayout.LabelField("", m_volumeParameterKun.value.name);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class Texture3DParameterView : VolumeParameterView<Texture3DParameterKun>
    {
        public Texture3DParameterView(GUIContent content, Texture3DParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<Texture3DParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            if (m_volumeParameterKun.value == null)
            {
                EditorGUILayout.LabelField("", "None");
            }
            else
            {
                EditorGUILayout.LabelField("", m_volumeParameterKun.value.name);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class RenderTextureParameterView : VolumeParameterView<RenderTextureParameterKun>
    {
        public RenderTextureParameterView(GUIContent content, RenderTextureParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<RenderTextureParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            if (m_volumeParameterKun.value == null)
            {
                EditorGUILayout.LabelField("", "None");
            }
            else
            {
                EditorGUILayout.LabelField("", m_volumeParameterKun.value.name);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpRenderTextureParameterView : VolumeParameterView<NoInterpRenderTextureParameterKun>
    {
        public NoInterpRenderTextureParameterView(GUIContent content, NoInterpRenderTextureParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpRenderTextureParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            if (m_volumeParameterKun.value == null)
            {
                EditorGUILayout.LabelField("", "None");
            }
            else
            {
                EditorGUILayout.LabelField("", m_volumeParameterKun.value.name);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class CubemapParameterView : VolumeParameterView<CubemapParameterKun>
    {
        public CubemapParameterView(GUIContent content, CubemapParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<CubemapParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            if (m_volumeParameterKun.value == null)
            {
                EditorGUILayout.LabelField("", "None");
            }
            else
            {
                EditorGUILayout.LabelField("", m_volumeParameterKun.value.name);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class NoInterpCubemapParameterView : VolumeParameterView<NoInterpCubemapParameterKun>
    {
        public NoInterpCubemapParameterView(GUIContent content, NoInterpCubemapParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<NoInterpCubemapParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            if (m_volumeParameterKun.value == null)
            {
                EditorGUILayout.LabelField("", "None");
            }
            else
            {
                EditorGUILayout.LabelField("", m_volumeParameterKun.value.name);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class AnimationCurveParameterView : VolumeParameterView<AnimationCurveParameterKun>
    {
        public AnimationCurveParameterView(GUIContent content, AnimationCurveParameterKun parameter) : base(content, parameter) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<AnimationCurveParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = m_volumeParameterKun.value;
            var value2 = EditorGUILayout.CurveField("", value1);
            if(value1 != value2)
            {
                m_volumeParameterKun.value = value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    /// <summary>
    /// ここからURP用
    /// </summary>

    public class TonemappingModeParameterView : VolumeParameterView<TonemappingModeParameterKun>
    {
        enum TonemappingModeKun
        {
            None,
            Neutral, // Neutral tonemapper
            ACES,    // ACES Filmic reference tonemapper (custom approximation)
        }

        public TonemappingModeParameterView(GUIContent content, TonemappingModeParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<TonemappingModeParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = (TonemappingModeKun)m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = (TonemappingModeKun)EditorGUILayout.EnumPopup("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = (int)value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class DepthOfFieldModeParameterView : VolumeParameterView<DepthOfFieldModeParameterKun>
    {
        enum DepthOfFieldModeKun
        {
            Off,
            Gaussian, // Non physical, fast, small radius, far blur only
            Bokeh
        }

        public DepthOfFieldModeParameterView(GUIContent content, DepthOfFieldModeParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<DepthOfFieldModeParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = (DepthOfFieldModeKun)m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = (DepthOfFieldModeKun)EditorGUILayout.EnumPopup("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = (int)value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class FilmGrainLookupParameterView : VolumeParameterView<FilmGrainLookupParameterKun>
    {
        enum FilmGrainLookupKun
        {
            Thin1,
            Thin2,
            Medium1,
            Medium2,
            Medium3,
            Medium4,
            Medium5,
            Medium6,
            Large01,
            Large02,
            Custom
        }

        public FilmGrainLookupParameterView(GUIContent content, FilmGrainLookupParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<FilmGrainLookupParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = (FilmGrainLookupKun)m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = (FilmGrainLookupKun)EditorGUILayout.EnumPopup("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = (int)value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class MotionBlurModeParameterView : VolumeParameterView<MotionBlurModeParameterKun>
    {
        enum MotionBlurModeKun
        {
            CameraOnly,
            CameraAndObjects
        }

        public MotionBlurModeParameterView(GUIContent content, MotionBlurModeParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<MotionBlurModeParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = (MotionBlurModeKun)m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = (MotionBlurModeKun)EditorGUILayout.EnumPopup("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = (int)value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class MotionBlurQualityParameterView : VolumeParameterView<MotionBlurQualityParameterKun>
    {
        enum MotionBlurQualityKun
        {
            Low,
            Medium,
            High
        }

        public MotionBlurQualityParameterView(GUIContent content, MotionBlurQualityParameterKun param) : base(content, param) { }

        public override void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            base.Header<MotionBlurQualityParameterKun>();
            EditorGUI.BeginDisabledGroup(!m_volumeParameterKun.overrideState);
            var value1 = (MotionBlurQualityKun)m_volumeParameterKun.value;
            EditorGUI.BeginChangeCheck();
            var value2 = (MotionBlurQualityKun)EditorGUILayout.EnumPopup("", value1);
            if (EditorGUI.EndChangeCheck())
            {
                m_volumeParameterKun.value = (int)value2;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }
}
