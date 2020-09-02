namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class ScreenKun
    {   
        [SerializeField]bool m_autorotateToLandscapeLeft; 
        public bool autorotateToLandscapeLeft{
            get{return m_autorotateToLandscapeLeft;}
            set{m_autorotateToLandscapeLeft = value;}
        }
        [SerializeField] bool m_autorotateToLandscapeRight;
        public bool autorotateToLandscapeRight{
            get{return m_autorotateToLandscapeRight;}
            set{m_autorotateToLandscapeRight = value;}
        }
        [SerializeField] bool m_autorotateToPortrait;
        public bool autorotateToPortrait{
            get{return m_autorotateToPortrait;}
            set {m_autorotateToPortrait = value;}
        }
        [SerializeField] bool m_autorotateToPortraitUpsideDown;
        public bool autorotateToPortraitUpsideDown{
            get{return m_autorotateToPortraitUpsideDown;}
            set{m_autorotateToPortraitUpsideDown = value;}
        }
        [SerializeField] float m_brightness;
        public float brightness{
            get{return m_brightness;}
            set{m_brightness = value;}
        }
        [SerializeField] int m_currentResolutionWidth;
        public int currentResolutionWidth{
            get{return m_currentResolutionWidth;}
            set{m_currentResolutionWidth = value;}
        }
        [SerializeField] int m_currentResolutionHeight;
        public int currentResolutionHeight{
            get{return m_currentResolutionHeight;}
            set{m_currentResolutionHeight = value;}
        }
        [SerializeField] int m_currentResolutionRefreshRate;
        public int currentResolutionRefreshRate{
            get{return m_currentResolutionRefreshRate;}
            set{m_currentResolutionRefreshRate = value;}
        }
        [SerializeField] Rect[] m_cutouts;
        public Rect[] cutouts{
            get{return m_cutouts;}
            set{m_cutouts = value;}
        }
        [SerializeField] float m_dpi;
        public float dpi{
            get{return m_dpi;}
            set{m_dpi = value;}
        }
        [SerializeField] bool m_fullScreen;
        public bool fullScreen{
            get{return m_fullScreen;}
            set{m_fullScreen = value;}
        }
        [SerializeField] FullScreenMode m_fullScreenMode;
        public FullScreenMode fullScreenMode{
            get{return m_fullScreenMode;}
            set{m_fullScreenMode = value;}
        }

        [SerializeField] int m_height;
        public int height{
            get{return m_height;}
            set{m_height = value;}
        }
        [SerializeField] ScreenOrientation m_orientation;
        public ScreenOrientation orientation{
            get{return m_orientation;}
            set{m_orientation = value;}
        }
        [SerializeField] Resolution[] m_resolutions;
        public Resolution[] resolutions{
            get{return m_resolutions;}
            set{m_resolutions = value;}
        }
        [SerializeField] Rect m_safeArea;
        public Rect safeArea{
            get{return m_safeArea;}
            set{m_safeArea = value;}
        }
        [SerializeField] int m_sleepTimeout;
        public int sleepTimeout{
            get{return m_sleepTimeout;}
            set{m_sleepTimeout = value;}
        }
        [SerializeField] int m_width;
        public int width{
            get{return m_width;}
            set{m_width = value;}
        }
        [SerializeField] int m_preferredRefreshRate;
        public int preferredRefreshRate{
            get{return m_preferredRefreshRate;}
            set{m_preferredRefreshRate = value;}
        }

        public ScreenKun():this(false){}

        public ScreenKun(bool isSet)
        {            
            if(isSet){
                #if UNITY_2019_1_OR_NEWER
                brightness = Screen.brightness;
                cutouts = new Rect[Screen.cutouts.Length];
                for (var i = 0; i < Screen.cutouts.Length; i++)
                {
                    cutouts[i] = Screen.cutouts[i];
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
                currentResolutionRefreshRate = Screen.currentResolution.refreshRate;

                
                height = Screen.height;
                orientation = Screen.orientation;
                resolutions = new Resolution[Screen.resolutions.Length];
                for (var i = 0; i < Screen.resolutions.Length; i++)
                {
                    resolutions[i] = Screen.resolutions[i];
                }
                safeArea = Screen.safeArea;
                sleepTimeout = Screen.sleepTimeout;
                width = Screen.width;         
            }
        }

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
            Screen.SetResolution(width, height, fullScreenMode, preferredRefreshRate);                    
        }
    }

}