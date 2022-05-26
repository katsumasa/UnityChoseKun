using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine
{
    namespace Rendering
    {
        namespace Universal
        {
            /// <summary>Light Layers.</summary>
            [Flags]
            public enum LightLayerEnum
            {
                /// <summary>The light will no affect any object.</summary>
                Nothing = 0,   // Custom name for "Nothing" option
                /// <summary>Light Layer 0.</summary>
                LightLayerDefault = 1 << 0,
                /// <summary>Light Layer 1.</summary>
                LightLayer1 = 1 << 1,
                /// <summary>Light Layer 2.</summary>
                LightLayer2 = 1 << 2,
                /// <summary>Light Layer 3.</summary>
                LightLayer3 = 1 << 3,
                /// <summary>Light Layer 4.</summary>
                LightLayer4 = 1 << 4,
                /// <summary>Light Layer 5.</summary>
                LightLayer5 = 1 << 5,
                /// <summary>Light Layer 6.</summary>
                LightLayer6 = 1 << 6,
                /// <summary>Light Layer 7.</summary>
                LightLayer7 = 1 << 7,
                /// <summary>Everything.</summary>
                Everything = 0xFF, // Custom name for "Everything" option
            }

            public class UniversalAdditionalLightDataKun : BehaviourKun
            {
                [SerializeField] int m_Version = 1;
                internal int version
                {
                    get => m_Version;
                    private set { m_Version = value; }
                }

                [Tooltip("Controls if light Shadow Bias parameters use pipeline settings.")]
                [SerializeField] bool m_UsePipelineSettings = true;

                public bool usePipelineSettings
                {
                    get { return m_UsePipelineSettings; }
                    set { m_UsePipelineSettings = value; }
                }

                public static readonly int AdditionalLightsShadowResolutionTierCustom = -1;
                public static readonly int AdditionalLightsShadowResolutionTierLow = 0;
                public static readonly int AdditionalLightsShadowResolutionTierMedium = 1;
                public static readonly int AdditionalLightsShadowResolutionTierHigh = 2;
                public static readonly int AdditionalLightsShadowDefaultResolutionTier = AdditionalLightsShadowResolutionTierHigh;
                public static readonly int AdditionalLightsShadowDefaultCustomResolution = 128;
                public static readonly int AdditionalLightsShadowMinimumResolution = 128;

                [Tooltip("Controls if light shadow resolution uses pipeline settings.")]
                [SerializeField] int m_AdditionalLightsShadowResolutionTier = AdditionalLightsShadowDefaultResolutionTier;

                public int additionalLightsShadowResolutionTier
                {
                    get { return m_AdditionalLightsShadowResolutionTier; }
                    private set { m_AdditionalLightsShadowResolutionTier = value; }
                }

                // The layer(s) this light belongs too.
                [SerializeField] LightLayerEnum m_LightLayerMask = LightLayerEnum.LightLayerDefault;

                public LightLayerEnum lightLayerMask
                {
                    get { return m_LightLayerMask; }
                    set { m_LightLayerMask = value; }
                }

                [SerializeField] bool m_CustomShadowLayers = false;

                // if enabled, shadowLayerMask use the same settings as lightLayerMask.
                public bool customShadowLayers
                {
                    get { return m_CustomShadowLayers; }
                    set { m_CustomShadowLayers = value; }
                }

                // The layer(s) used for shadow casting.
                [SerializeField] LightLayerEnum m_ShadowLayerMask = LightLayerEnum.LightLayerDefault;

                public LightLayerEnum shadowLayerMask
                {
                    get { return m_ShadowLayerMask; }
                    set { m_ShadowLayerMask = value; }
                }

                [Tooltip("Controls the size of the cookie mask currently assigned to the light.")]
                [SerializeField] Vector2Kun m_LightCookieSize = new Vector2Kun(Vector2.one);
                public Vector2Kun lightCookieSize
                {
                    get => m_LightCookieSize;
                    set => m_LightCookieSize = value;
                }

                [Tooltip("Controls the offset of the cookie mask currently assigned to the light.")]
                [SerializeField] Vector2Kun m_LightCookieOffset = new Vector2Kun(Vector2.zero);
                public Vector2Kun lightCookieOffset
                {
                    get => m_LightCookieOffset;
                    set => m_LightCookieOffset = value;
                }
               


                public UniversalAdditionalLightDataKun() : this(null) { }


                public UniversalAdditionalLightDataKun(Component component) : base(component)
                {
                    componentKunType = ComponentKunType.UniversalAdditionalLightData;

                    if (component == null)
                    {
                        return;
                    }
                    var t = component.GetType();
                    PropertyInfo propertyInfo;

                    propertyInfo = t.GetProperty("version");
                    if (propertyInfo != null)
                    {
                        version = (int)propertyInfo.GetValue(component);
                    }

                    propertyInfo = t.GetProperty("usePipelineSettings");
                    if (propertyInfo != null)
                    {
                        usePipelineSettings = (bool)propertyInfo.GetValue(component);
                    }

                    propertyInfo = t.GetProperty("additionalLightsShadowResolutionTier");
                    if (propertyInfo != null)
                    {
                        additionalLightsShadowResolutionTier = (int)propertyInfo.GetValue(component);
                    }

                    propertyInfo = t.GetProperty("lightLayerMask");
                    if (propertyInfo != null)
                    {
                        lightLayerMask = (LightLayerEnum)propertyInfo.GetValue(component);
                    }

                    propertyInfo = t.GetProperty("customShadowLayers");
                    if (propertyInfo != null)
                    {
                        customShadowLayers = (bool)propertyInfo.GetValue(component);
                    }

                    propertyInfo = t.GetProperty("shadowLayerMask");
                    if (propertyInfo != null)
                    {
                        shadowLayerMask = (LightLayerEnum)propertyInfo.GetValue(component);
                    }

                    propertyInfo = t.GetProperty("lightCookieSize");
                    if (propertyInfo != null)
                    {
                        lightCookieSize = new Vector2Kun((Vector2)propertyInfo.GetValue(component));
                    }

                    propertyInfo = t.GetProperty("lightCookieOffset");
                    if (propertyInfo != null)
                    {
                        lightCookieOffset = new Vector2Kun((Vector2)propertyInfo.GetValue(component));
                    }
                }

                public override bool WriteBack(Component component)
                {
                    base.WriteBack(component);
                    if (dirty)
                    {
                        dirty = false;
                        var t = component.GetType();

                        PropertyInfo propertyInfo;

                        propertyInfo = t.GetProperty("usePipelineSettings");
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(component, usePipelineSettings);
                        }

                        propertyInfo = t.GetProperty("additionalLightsShadowResolutionTier");
                        if (propertyInfo != null)
                        {
                            //propertyInfo.SetValue(component, additionalLightsShadowResolutionTier);
                        }

                        propertyInfo = t.GetProperty("lightLayerMask");
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(component, (int)lightLayerMask);
                        }

                        propertyInfo = t.GetProperty("customShadowLayers");
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(component, customShadowLayers);
                        }

                        propertyInfo = t.GetProperty("shadowLayerMask");
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(component, (int)shadowLayerMask);
                        }

                        propertyInfo = t.GetProperty("lightCookieSize");
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(component, lightCookieSize.GetVector2());
                        }

                        propertyInfo = t.GetProperty("lightCookieOffset");
                        if (propertyInfo != null)
                        {
                            propertyInfo.SetValue(component, lightCookieOffset.GetVector2());
                        }

                        return true;
                    }
                    return false;
                }


                public override void Serialize(BinaryWriter binaryWriter)
                {
                    base.Serialize(binaryWriter);
                    binaryWriter.Write((int)m_Version);
                    binaryWriter.Write((bool)m_UsePipelineSettings);
                    binaryWriter.Write((int)m_AdditionalLightsShadowResolutionTier);
                    binaryWriter.Write((int)m_LightLayerMask);
                    binaryWriter.Write((bool)m_CustomShadowLayers);
                    binaryWriter.Write((int)m_ShadowLayerMask);
                    SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_LightCookieSize);
                    SerializerKun.Serialize<Vector2Kun>(binaryWriter, m_LightCookieOffset);
                }


                public override void Deserialize(BinaryReader binaryReader)
                {
                    base.Deserialize(binaryReader);
                    m_Version = binaryReader.ReadInt32();
                    m_UsePipelineSettings = binaryReader.ReadBoolean();
                    m_AdditionalLightsShadowResolutionTier = binaryReader.ReadInt32();
                    m_LightLayerMask = (LightLayerEnum)binaryReader.ReadInt32();
                    m_CustomShadowLayers = (bool)binaryReader.ReadBoolean();
                    m_ShadowLayerMask = (LightLayerEnum)binaryReader.ReadInt32();
                    m_LightCookieSize = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
                    m_LightCookieOffset = SerializerKun.DesirializeObject<Vector2Kun>(binaryReader);
                }
            }
        }
    }
}