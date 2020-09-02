namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    [System.Serializable]
    public class TimeKun
    {        
        [SerializeField] float m_deltaTime;
        public float deltaTime{
            get{return m_deltaTime;}
            private set{m_deltaTime = value;}
        }
        [SerializeField] float m_fixedUnscaledDeltaTime;// RO
        public float fixedUnscaledDeltaTime{
            get{return m_fixedUnscaledDeltaTime;}
            private set{m_fixedUnscaledDeltaTime = value;}
        }
        [SerializeField] float m_fixedUnscaledTime; // RO
        public float fixedUnscaledTime{
            get{return m_fixedUnscaledTime;}
            set{m_fixedUnscaledTime = value;}
        }
        [SerializeField] int m_frameCount; // RO               
        public int frameCount{
            get{return m_frameCount;}
            private set{m_frameCount = value;}
        }
        [SerializeField] float m_realtimeSinceStartup; // RO
        public float realtimeSinceStartup{
            get{return m_realtimeSinceStartup;}
            set{m_realtimeSinceStartup = value;}
        }
        [SerializeField] float m_smoothDeltaTime; // RO
        public float smoothDeltaTime{
            get{return m_smoothDeltaTime;}
            private set{m_smoothDeltaTime = value;}
        }
        [SerializeField] float m_time; // RO        
        public float time{
            get{return m_time;}
            private set{m_time = value;}
        }
        [SerializeField] float m_timeSinceLevelLoad; // RO
        public float timeSinceLevelLoad{
            get{return m_timeSinceLevelLoad;}
            private set{m_timeSinceLevelLoad = value;}
        }
        [SerializeField] float m_unscaledDeltaTime; // RO
        public float unscaledDeltaTime{
            get{return m_unscaledDeltaTime;}
            private set {m_unscaledDeltaTime = value;}
        }
        

        [SerializeField] float m_unscaledTime; // RO
        public float unscaledTime{
            get{return m_unscaledTime;}
            private set{ m_unscaledTime = value;}
        }
        [SerializeField] bool m_inFixedTimeStep;//RO
        public bool inFixedTimeStep{
            get{return m_inFixedTimeStep;}
            private set {m_inFixedTimeStep = value;}
        }

        [SerializeField] int m_captureFramerate;
        public int captureFramerate{
            get{return m_captureFramerate;}
            set{m_captureFramerate = value;}
        }
        [SerializeField] float m_fixedDeltaTime;
        public float fixedDeltaTime{
            get{return m_fixedDeltaTime;}
            set{m_fixedDeltaTime = value;}
        }
        [SerializeField] float m_maximumDeltaTime;
        public float maximumDeltaTime{
            get{return m_maximumDeltaTime;}
            set{m_maximumDeltaTime = value;}
        }
        [SerializeField] float m_timeScale;
        public float timeScale{
            get{return m_timeScale;}
            set{m_timeScale = value;}
        }

        public TimeKun():this(false){}

        public TimeKun(bool isSet)
        {
            if(isSet){            
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