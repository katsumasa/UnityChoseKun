namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class ScreenKun
    {        
        public bool autorotateToLandscapeLeft;
        public bool autorotateToLandscapeRight;
        public bool autorotateToPortrait;
        public bool autorotateToPortraitUpsideDown;
        public float brightness;
        public int currentResolutionWidth;
        public int currentResolutionHeight;
        public int currentResolutionRefreshRate;
        public Rect[] cutouts;
        public float dpi;
        public bool fullScreen;
        public FullScreenMode fullScreenMode;
        public int height;
        public ScreenOrientation orientation;
        public Resolution[] resolutions;
        public Rect safeArea;
        public int sleepTimeout;
        public int width;
        public int preferredRefreshRate;

        

        public ScreenKun()
        {
            autorotateToLandscapeLeft = Screen.autorotateToLandscapeLeft;
            autorotateToLandscapeRight = Screen.autorotateToLandscapeRight;
            autorotateToPortrait = Screen.autorotateToPortrait;
            autorotateToPortraitUpsideDown = Screen.autorotateToPortraitUpsideDown;
            brightness = Screen.brightness;
            fullScreen = Screen.fullScreen;
            fullScreenMode = Screen.fullScreenMode;
            orientation = Screen.orientation;
            sleepTimeout = Screen.sleepTimeout;

            currentResolutionWidth = Screen.currentResolution.width;
            currentResolutionHeight = Screen.currentResolution.height;
            currentResolutionRefreshRate = Screen.currentResolution.refreshRate;

            cutouts = new Rect[Screen.cutouts.Length];
            for (var i = 0; i < Screen.cutouts.Length; i++)
            {
                cutouts[i] = Screen.cutouts[i];
            }
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

        public void WriteBack()
        {
            Screen.autorotateToLandscapeLeft = autorotateToLandscapeLeft;
            Screen.autorotateToLandscapeRight = autorotateToLandscapeRight;
            Screen.autorotateToPortrait = autorotateToPortrait;
            Screen.autorotateToPortraitUpsideDown = autorotateToPortraitUpsideDown;
            Screen.brightness = brightness;
            Screen.fullScreen = fullScreen;
            Screen.fullScreenMode = fullScreenMode;
            Screen.orientation = orientation;
            Screen.sleepTimeout = sleepTimeout;
            Screen.SetResolution(width, height, fullScreenMode, preferredRefreshRate);                    
        }
    }

}