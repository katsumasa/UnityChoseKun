using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Utj.UnityChoseKun.Engine.Rendering;

namespace Utj.UnityChoseKun.Editor.Rendering
{








    public class VolumeComponentView : BehaviourView
    {
        public static class Styles
        {
            static readonly GUIContent[] m_bloomTempalte =
            {
                new GUIContent("Threshold"),
                new GUIContent("Intensity"),
                new GUIContent("Scatter"),
                new GUIContent("Clamp"),
                new GUIContent("Tint"),
                new GUIContent("HighQualityFiltering"),
                new GUIContent("SkipIterations"),
                new GUIContent("DirtTexture"),
                new GUIContent("DirtIntensity"),
            };
            static readonly GUIContent[] m_channelMixerTemplate =
            {
                new GUIContent("RedOutRedIn"),
                new GUIContent("RedOutGreenIn"),
                new GUIContent("RedOutBlueIn"),
                new GUIContent("GreenOutRedIn"),
                new GUIContent("GreenOutGreenIn"),
                new GUIContent("GreenOutBlueIn"),
                new GUIContent("BlueOutRedIn"),
                new GUIContent("BlueOutGreenIn"),
                new GUIContent("BlueOutBlueIn"),
            };
            static readonly GUIContent[] m_colorAdjustmentsTemplate =
            {
                new GUIContent("Post Exposure"),
                new GUIContent("Contrast"),
                new GUIContent("Color Filter"),
                new GUIContent("Hue Shift"),
                new GUIContent("Saturation"),
            };
            static readonly GUIContent[] m_chromaticAberrationTemplate =
            {
                new GUIContent("Intensity"),
            };
            static readonly GUIContent[] m_depthOfFieldTemplate =
            {
                new GUIContent("Mode"),
                new GUIContent("GaussianStart"),
                new GUIContent("GaussianEnd"),
                new GUIContent("GaussianMaxRadius"),
                new GUIContent("HighQualitySampling"),

                new GUIContent("FocusDistance"),
                new GUIContent("FocalLength"),
                new GUIContent("Aperture"),
                new GUIContent("BladeCount"),
                new GUIContent("BladeCurvature"),
                new GUIContent("BladeRotation"),
            };
            static readonly GUIContent[] m_filmGrainTemplate =
            {
                new GUIContent("Type"),
                new GUIContent("Intensity"),
                new GUIContent("Response"),
                new GUIContent("Texture"),
            };
            static readonly GUIContent[] m_lensDistortionTemplate =
            {
                new GUIContent("Intensity"),
                new GUIContent("X Multiplier"),
                new GUIContent("Y Multiplier"),
                new GUIContent("Center"),
                new GUIContent("Scale"),
            };
            static readonly GUIContent[] m_liftGammaGainTemplate =
            {
                new GUIContent("Lift"),
                new GUIContent("Gamma"),
                new GUIContent("Gain"),
            };
            static readonly GUIContent[] m_motionBlurTemplate =
            {
                new GUIContent("Mode"),
                new GUIContent("Quality"),
                new GUIContent("Intensity"),
                new GUIContent("Clamp"),
            };
            static readonly GUIContent[] m_paniniProjectionTemplate =
            {
                new GUIContent("Distance"),
                new GUIContent("Crop To Fit"),
            };
            static readonly GUIContent[] m_shadowsMidtonesHighlightsTemplate =
            {
                new GUIContent("Shadows"),
                new GUIContent("Midtones"),
                new GUIContent("Highlights"),
                new GUIContent("ShadowsStart"),
                new GUIContent("ShadowsEnd"),
                new GUIContent("HighlightsStart"),
                new GUIContent("HighlightsEnd"),
            };
            static readonly GUIContent[] m_splitToningTemplate =
            {
                new GUIContent("Shadows"),
                new GUIContent("Highlights"),
                new GUIContent("Blance"),
            };

            static readonly GUIContent[] m_tonemappingTemplate =
            {
                new GUIContent("Mode"),
            };

            static readonly GUIContent[] m_vignetteTemplate =
            {
                new GUIContent("Color"),
                new GUIContent("Center"),
                new GUIContent("Intensity"),
                new GUIContent("Smoothness"),
                new GUIContent("Rounded"),
            };

            static readonly GUIContent[] m_whiteBalanceTemplate =
            {
                new GUIContent("Temperature"),
                new GUIContent("Tint"),
            };

            public static readonly Dictionary<string, GUIContent[]> m_componentTemplates = new Dictionary<string, GUIContent[]>()
            {
                {"Bloom",m_bloomTempalte},
                {"ChannelMixer",m_channelMixerTemplate},
                {"ColorAdjustments" ,m_colorAdjustmentsTemplate},
                {"ChromaticAberration",m_chromaticAberrationTemplate },
                //{"Color Curves", m_colorCurvesTemplate},
                {"DepthOfField",m_depthOfFieldTemplate },
                {"FilmGrain",m_filmGrainTemplate },
                {"LensDistortion",m_lensDistortionTemplate },
                {"LiftGammaGain",m_liftGammaGainTemplate },
                {"MotionBlur",m_motionBlurTemplate },
                {"PaniniProjection",m_paniniProjectionTemplate},
                {"ShadowsMidtonesHighlights",m_shadowsMidtonesHighlightsTemplate},
                {"SplitToning",m_splitToningTemplate },
                {"Tonemapping", m_tonemappingTemplate },
                {"Vignette",m_vignetteTemplate },
                {"WhiteBalance",m_whiteBalanceTemplate},
            };

            public static GUIContent[] GetGUIContents(string name)
            {
                foreach(var key in m_componentTemplates.Keys)
                {
                    if(name.Contains(key))                    
                    {
                        return m_componentTemplates[key];
                    }
                }
                return null;                    
            }
        }



        VolumeComponentKun m_volumeComponentKun;
        bool m_foldOut;

        VolumeParameterView[] m_volumeParameterViews;

        public VolumeComponentView(VolumeComponentKun component)
        {
            m_volumeComponentKun = component;
            m_foldOut = true;

            var contens = Styles.GetGUIContents(m_volumeComponentKun.name);
            if (contens != null)
            {
                if(contens.Length != m_volumeComponentKun.parameterKuns.Length)
                {
                    throw new Exception($"{m_volumeComponentKun.name} contens.Length({contens.Length}) != m_volumeComponentKun.parameterKuns.Length({m_volumeComponentKun.parameterKuns.Length})");
                }

                m_volumeParameterViews = new VolumeParameterView[m_volumeComponentKun.parameterKuns.Length];
                for (var i = 0; i < m_volumeComponentKun.parameterKuns.Length; i++)
                {
                    m_volumeParameterViews[i] = VolumeParameterView.Allocater(m_volumeComponentKun.parameterKuns[i],contens[i]);
                }
            }
        }


        public override bool OnGUI()
        {
            var rect = EditorGUILayout.GetControlRect();
            EditorGUI.DrawRect(rect,new Color32(50,50,50,255));
            m_foldOut = EditorGUI.Foldout(rect,m_foldOut,new GUIContent());         
            var active = m_volumeComponentKun.active;            
            active = EditorGUI.ToggleLeft(new Rect(rect.x+24, rect.y, rect.width, rect.height), m_volumeComponentKun.name, active);                       
            if(active != m_volumeComponentKun.active)
            {
                m_volumeComponentKun.active = active;
            }

            

            if (!m_foldOut)
            {
                return m_foldOut;
            }

            if (m_volumeParameterViews == null)
            {
                EditorGUILayout.LabelField("Not Support");
                return m_foldOut;
            }



            bool isALL = true;
            bool isNone = true;
            for(var i = 0; i < m_volumeComponentKun.parameterKuns.Length; i++)
            {
                if (m_volumeComponentKun.parameterKuns[i].overrideState)
                {
                    isNone = false;
                } 
                else
                {
                    isALL = false;
                }
            }


            rect = EditorGUILayout.GetControlRect();
            EditorGUI.BeginChangeCheck();
            isALL = EditorGUI.ToggleLeft(new Rect(rect.x,rect.y,64,rect.height), new GUIContent("ALL"), isALL);
            if (EditorGUI.EndChangeCheck())
            {
                if (isALL)
                {
                    foreach(var param in m_volumeComponentKun.parameterKuns)
                    {
                        param.overrideState = true;
                    }
                }
            }

            EditorGUI.BeginChangeCheck();
            isNone = EditorGUI.ToggleLeft(new Rect(rect.x + 64,rect.y,rect.width,rect.height), new GUIContent("NONE"), isNone);
            if (EditorGUI.EndChangeCheck())
            {
                if (isNone)
                {
                    foreach (var param in m_volumeComponentKun.parameterKuns)
                    {
                        param.overrideState = false;
                    }
                }
            }

            EditorGUI.indentLevel++;
            for (var i = 0; i < m_volumeParameterViews.Length; i++)
            {
                m_volumeParameterViews[i].OnGUI();
            }
            EditorGUI.indentLevel--;

            return m_foldOut;
        }




    }
}