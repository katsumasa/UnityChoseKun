﻿namespace Utj.UnityChoseKun
{
    using System.IO;
    using System.Runtime.InteropServices;
    using UnityEngine;


    [System.Serializable]
    public class ScreenKun : ISerializerKun
    {
        [SerializeField] int m_currentResolutionWidth;
        [SerializeField] int m_currentResolutionHeight;
#if UNITY_2022_1_OR_NEWER
        [SerializeField] RefreshRate m_currentRefreshRateRatio;
#else
        [SerializeField] int m_currentResolutionRefreshRate;
#endif
        [SerializeField] int m_width;
        [SerializeField] int m_height;
        [SerializeField] int m_sleepTimeout;
#if UNITY_2022_1_OR_NEWER
        [SerializeField] RefreshRate m_preferredRefreshRateRatio;
#else
        [SerializeField] int m_preferredRefreshRate;
#endif
        [SerializeField] bool m_autorotateToLandscapeLeft;
        [SerializeField] bool m_autorotateToLandscapeRight;
        [SerializeField] bool m_autorotateToPortrait;
        [SerializeField] bool m_autorotateToPortraitUpsideDown;
        [SerializeField] bool m_fullScreen;
#if UNITY_2019_1_OR_NEWER
        [SerializeField] float m_brightness;
#endif
        [SerializeField] float m_dpi;
        [SerializeField] FullScreenMode m_fullScreenMode;
        [SerializeField] ScreenOrientation m_orientation;
        [SerializeField] RectKun m_safeArea;
#if UNITY_2019_1_OR_NEWER
        [SerializeField] RectKun[] m_cutouts;
#endif
        [SerializeField] ResolutionKun[] m_resolutions;

        public bool autorotateToLandscapeLeft
        {
            get { return m_autorotateToLandscapeLeft; }
            set { m_autorotateToLandscapeLeft = value; }
        }

        public bool autorotateToLandscapeRight
        {
            get { return m_autorotateToLandscapeRight; }
            set { m_autorotateToLandscapeRight = value; }
        }

        public bool autorotateToPortrait
        {
            get { return m_autorotateToPortrait; }
            set { m_autorotateToPortrait = value; }
        }

        public bool autorotateToPortraitUpsideDown
        {
            get { return m_autorotateToPortraitUpsideDown; }
            set { m_autorotateToPortraitUpsideDown = value; }
        }
#if UNITY_2019_1_OR_NEWER
        public float brightness
        {
            get { return m_brightness; }
            set { m_brightness = value; }
        }
#endif
        public int currentResolutionWidth
        {
            get { return m_currentResolutionWidth; }
            set { m_currentResolutionWidth = value; }
        }

        public int currentResolutionHeight
        {
            get { return m_currentResolutionHeight; }
            set { m_currentResolutionHeight = value; }
        }

#if UNITY_2022_1_OR_NEWER
        public RefreshRate currentRefreshRateRatio
        {
            get
            {
                return m_currentRefreshRateRatio;
            }
            set
            {
                m_currentRefreshRateRatio = value;
            }
        }
#else
        public int currentResolutionRefreshRate{
            get{return m_currentResolutionRefreshRate;}
            set{m_currentResolutionRefreshRate = value;}
        }
#endif

#if UNITY_2019_1_OR_NEWER
        public Rect[] cutouts
        {
            get
            {
                var rects = new Rect[m_cutouts.Length];
                for (var i = 0; i < rects.Length; i++)
                {
                    rects[i] = m_cutouts[i].GetRect();
                }
                return rects;
            }

            set
            {
                m_cutouts = new RectKun[value.Length];
                for (var i = 0; i < m_cutouts.Length; i++)
                {
                    m_cutouts[i] = new RectKun(value[i]);
                }
            }
        }
#endif
        public float dpi
        {
            get { return m_dpi; }
            private set { m_dpi = value; }
        }

        public bool fullScreen
        {
            get { return m_fullScreen; }
            set { m_fullScreen = value; }
        }

        public FullScreenMode fullScreenMode
        {
            get { return m_fullScreenMode; }
            set { m_fullScreenMode = value; }
        }


        public int height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        public ScreenOrientation orientation
        {
            get { return m_orientation; }
            set { m_orientation = value; }
        }

        public Resolution[] resolutions
        {
            get
            {
                var resolutions = new Resolution[m_resolutions.Length];
                for (var i = 0; i < resolutions.Length; i++)
                {
                    resolutions[i] = m_resolutions[i].GetResolution();
                }
                return resolutions;
            }
            set
            {
                m_resolutions = new ResolutionKun[value.Length];
                for (var i = 0; i < m_resolutions.Length; i++)
                {
                    m_resolutions[i] = new ResolutionKun(value[i]);
                }
            }
        }


        public Rect safeArea
        {
            get { return m_safeArea.GetRect(); }
            set { m_safeArea = new RectKun(value); }
        }


        public int sleepTimeout
        {
            get { return m_sleepTimeout; }
            set { m_sleepTimeout = value; }
        }

        public int width
        {
            get { return m_width; }
            set { m_width = value; }
        }
#if UNITY_2022_1_OR_NEWER
        public RefreshRate preferredRefreshRateRatio
        {
            get { return m_preferredRefreshRateRatio; }
            set { m_preferredRefreshRateRatio = value; }
        }
#else
        public int preferredRefreshRate{
            get{return m_preferredRefreshRate;}
            set{m_preferredRefreshRate = value;}
        }
#endif

        /// <summary>
        /// 
        /// </summary>
        public ScreenKun() : this(false) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSet"></param>
        public ScreenKun(bool isSet)
        {
            if (isSet)
            {
#if UNITY_2019_1_OR_NEWER
                brightness = Screen.brightness;
                m_cutouts = new RectKun[Screen.cutouts.Length];
                for (var i = 0; i < Screen.cutouts.Length; i++)
                {
                    m_cutouts[i] = new RectKun(Screen.cutouts[i]);
                }
#endif

                autorotateToLandscapeLeft = Screen.autorotateToLandscapeLeft;
                autorotateToLandscapeRight = Screen.autorotateToLandscapeRight;
                autorotateToPortrait = Screen.autorotateToPortrait;
                autorotateToPortraitUpsideDown = Screen.autorotateToPortraitUpsideDown;


                fullScreen = Screen.fullScreen;
                fullScreenMode = Screen.fullScreenMode;
                orientation = Screen.orientation;
                sleepTimeout = Screen.sleepTimeout;

                currentResolutionWidth = Screen.currentResolution.width;
                currentResolutionHeight = Screen.currentResolution.height;
#if UNITY_2022_1_OR_NEWER
                currentRefreshRateRatio = Screen.currentResolution.refreshRateRatio;
                preferredRefreshRateRatio = currentRefreshRateRatio;
#else
                currentResolutionRefreshRate = Screen.currentResolution.refreshRate;
                m_preferredRefreshRate = currentResolutionRefreshRate;
#endif
                dpi = Screen.dpi;

                height = Screen.height;
                orientation = Screen.orientation;
                m_resolutions = new ResolutionKun[Screen.resolutions.Length];
                for (var i = 0; i < Screen.resolutions.Length; i++)
                {
                    m_resolutions[i] = new ResolutionKun(Screen.resolutions[i]);
                }
                safeArea = Screen.safeArea;
                sleepTimeout = Screen.sleepTimeout;
                width = Screen.width;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void WriteBack()
        {
#if UNITY_2019_1_OR_NEWER
            Screen.brightness = brightness;
#endif
            Screen.autorotateToLandscapeLeft = autorotateToLandscapeLeft;
            Screen.autorotateToLandscapeRight = autorotateToLandscapeRight;
            Screen.autorotateToPortrait = autorotateToPortrait;
            Screen.autorotateToPortraitUpsideDown = autorotateToPortraitUpsideDown;
            Screen.fullScreen = fullScreen;
            Screen.fullScreenMode = fullScreenMode;
            Screen.orientation = orientation;
            Screen.sleepTimeout = sleepTimeout;
#if UNITY_2022_1_OR_NEWER
            Screen.SetResolution(Screen.width, Screen.height, fullScreenMode, m_preferredRefreshRateRatio);
#else
            Screen.SetResolution(width, height, fullScreenMode, preferredRefreshRate);                    
#endif
        }


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_currentResolutionWidth);
            binaryWriter.Write(m_currentResolutionHeight);
#if UNITY_2022_1_OR_NEWER
            binaryWriter.Write(m_currentRefreshRateRatio.numerator);
            binaryWriter.Write(m_currentRefreshRateRatio.denominator);
#else
            binaryWriter.Write(m_currentResolutionRefreshRate);
#endif

            binaryWriter.Write(m_width);
            binaryWriter.Write(m_height);
            binaryWriter.Write(m_sleepTimeout);
#if UNITY_2022_1_OR_NEWER
            binaryWriter.Write(m_preferredRefreshRateRatio.numerator);
            binaryWriter.Write(m_preferredRefreshRateRatio.denominator);
#else
            binaryWriter.Write(m_preferredRefreshRate);
#endif
            binaryWriter.Write(m_autorotateToLandscapeLeft);
            binaryWriter.Write(m_autorotateToLandscapeRight);
            binaryWriter.Write(m_autorotateToPortrait);
            binaryWriter.Write(m_autorotateToPortraitUpsideDown);
            binaryWriter.Write(m_fullScreen);

#if UNITY_2019_1_OR_NEWER
            binaryWriter.Write(m_brightness);
#endif

            binaryWriter.Write(m_dpi);
            binaryWriter.Write((int)m_fullScreenMode);
            binaryWriter.Write((int)m_orientation);

            SerializerKun.Serialize<RectKun>(binaryWriter, m_safeArea);
#if UNITY_2019_1_OR_NEWER
            SerializerKun.Serialize<RectKun>(binaryWriter, m_cutouts);
#endif
            SerializerKun.Serialize<ResolutionKun>(binaryWriter, m_resolutions);
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_currentResolutionWidth = binaryReader.ReadInt32();
            m_currentResolutionHeight = binaryReader.ReadInt32();
#if UNITY_2022_1_OR_NEWER
            m_currentRefreshRateRatio = new RefreshRate();
            m_currentRefreshRateRatio.numerator = binaryReader.ReadUInt32();
            m_currentRefreshRateRatio.denominator = binaryReader.ReadUInt32();
#else
            m_currentResolutionRefreshRate = binaryReader.ReadInt32();
#endif
            m_width = binaryReader.ReadInt32();
            m_height = binaryReader.ReadInt32();
            m_sleepTimeout = binaryReader.ReadInt32();
#if UNITY_2022_1_OR_NEWER
            m_preferredRefreshRateRatio = new RefreshRate();
            m_preferredRefreshRateRatio.numerator = binaryReader.ReadUInt32();
            m_preferredRefreshRateRatio.denominator = binaryReader.ReadUInt32();
#else
            m_preferredRefreshRate = binaryReader.ReadInt32();
#endif
            m_autorotateToLandscapeLeft = binaryReader.ReadBoolean();
            m_autorotateToLandscapeRight = binaryReader.ReadBoolean();
            m_autorotateToPortrait = binaryReader.ReadBoolean();
            m_autorotateToPortraitUpsideDown = binaryReader.ReadBoolean();
            m_fullScreen = binaryReader.ReadBoolean();
#if UNITY_2019_1_OR_NEWER
            m_brightness = binaryReader.ReadSingle();
#endif
            m_dpi = binaryReader.ReadSingle();
            m_fullScreenMode = (FullScreenMode)binaryReader.ReadInt32();
            m_orientation = (ScreenOrientation)binaryReader.ReadInt32();
            m_safeArea = SerializerKun.DesirializeObject<RectKun>(binaryReader);
#if UNITY_2019_1_OR_NEWER
            m_cutouts = SerializerKun.DesirializeObjects<RectKun>(binaryReader);
#endif
            m_resolutions = SerializerKun.DesirializeObjects<ResolutionKun>(binaryReader);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as ScreenKun;
            if (other == null)
            {
                return false;
            }
            if (!m_currentResolutionWidth.Equals(other.m_currentResolutionWidth))
            {
                return false;
            }
            if (!m_currentResolutionHeight.Equals(other.m_currentResolutionHeight))
            {
                return false;
            }
#if UNITY_2022_1_OR_NEWER
            if (!m_currentRefreshRateRatio.Equals(other.m_currentRefreshRateRatio))
            {
                return false;
            }
#else
            if (!m_currentResolutionRefreshRate.Equals(other.m_currentResolutionRefreshRate))
            {
                return false;
            }
#endif
            if (!m_width.Equals(other.m_width))
            {
                return false;
            }
            if (!m_height.Equals(other.m_height))
            {
                return false;
            }
            if (!m_sleepTimeout.Equals(other.m_sleepTimeout))
            {
                return false;
            }
#if UNITY_2022_1_OR_NEWER
            if (!m_preferredRefreshRateRatio.Equals(other.m_preferredRefreshRateRatio))
            {
                return false;
            }
#else
            if (!m_preferredRefreshRate.Equals(other.m_preferredRefreshRate))
            {
                return false;
            }
#endif
            if (!m_autorotateToLandscapeLeft.Equals(other.m_autorotateToLandscapeLeft))
            {
                return false;
            }
            if (!m_autorotateToLandscapeRight.Equals(other.m_autorotateToLandscapeRight))
            {
                return false;
            }
            if (!m_autorotateToPortrait.Equals(other.m_autorotateToPortrait))
            {
                return false;
            }
            if (!m_autorotateToPortraitUpsideDown.Equals(other.m_autorotateToPortraitUpsideDown))
            {
                return false;
            }
            if (!m_fullScreen.Equals(other.m_fullScreen))
            {
                return false;
            }
#if UNITY_2019_1_OR_NEWER
            if (!m_brightness.Equals(other.m_brightness))
            {
                return false;
            }
#endif
            if (!m_dpi.Equals(other.m_dpi))
            {
                return false;
            }
            if (!m_fullScreenMode.Equals(other.m_fullScreenMode))
            {
                return false;
            }
            if (!m_orientation.Equals(other.m_orientation))
            {
                return false;
            }
            if (!RectKun.Equals(m_safeArea, other.m_safeArea))
            {
                return false;
            }
#if UNITY_2019_1_OR_NEWER
            if (m_cutouts != null)
            {
                if (other.m_cutouts == null)
                {
                    return false;
                }
                if (m_cutouts.Length != other.m_cutouts.Length)
                {
                    return false;
                }
                for (var i = 0; i < m_cutouts.Length; i++)
                {
                    if (!RectKun.Equals(m_cutouts[i], other.m_cutouts[i]))
                    {
                        return false;
                    }
                }
            }
#endif
            if (m_resolutions != null)
            {
                if (other.m_resolutions == null)
                {
                    return false;
                }
                if (m_resolutions.Length != other.m_resolutions.Length)
                {
                    return false;
                }
                for (var i = 0; i < m_resolutions.Length; i++)
                {
                    if (!ResolutionKun.Equals(m_resolutions[i], other.m_resolutions[i]))
                    {
                        return false;
                    }
                }
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