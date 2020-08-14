namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    [System.Serializable]
    public class TimeKun
    {        
        public float deltaTime; // RO        
        public float fixedUnscaledDeltaTime;// RO
        public float fixedUnscaledTime; // RO
        public int frameCount; // RO               
        public float realtimeSinceStartup; // RO
        public float smoothDeltaTime; // RO
        public float time; // RO        
        public float timeSinceLevelLoad; // RO
        public float unscaledDeltaTime; // RO
        public float unscaledTime; // RO
        public bool inFixedTimeStep;//RO

        public int captureFramerate;
        public float fixedDeltaTime;
        public float maximumDeltaTime;
        public float timeScale;


        public TimeKun()
        {
            deltaTime = Time.deltaTime;
            fixedUnscaledDeltaTime = Time.fixedUnscaledDeltaTime;
            fixedUnscaledTime = Time.fixedUnscaledTime;
            frameCount = Time.frameCount;
            realtimeSinceStartup = Time.realtimeSinceStartup;
            smoothDeltaTime = Time.smoothDeltaTime;
            time = Time.time;
            timeSinceLevelLoad = Time.timeSinceLevelLoad;
            unscaledDeltaTime = Time.unscaledDeltaTime;
            unscaledTime = Time.unscaledTime;

            captureFramerate = Time.captureFramerate;
            fixedDeltaTime = Time.fixedDeltaTime;
            inFixedTimeStep = Time.inFixedTimeStep;
            maximumDeltaTime = Time.maximumDeltaTime;
            timeScale = Time.timeScale;
        }


        public void WriteBack()
        {
            Time.captureFramerate = captureFramerate;
            Time.fixedDeltaTime = fixedDeltaTime;
            Time.maximumDeltaTime = maximumDeltaTime;
            Time.timeScale = timeScale;   
        }


    }

}