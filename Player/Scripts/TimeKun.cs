namespace Utj.UnityChoseKun
{    
    using System.IO;
    using UnityEngine;


    [System.Serializable]
    public class TimeKun : ISerializerKun
    {
        [SerializeField] int m_frameCount; // RO               
        [SerializeField] int m_captureFramerate;
        [SerializeField] bool m_inFixedTimeStep;//RO
        [SerializeField] float m_deltaTime;
        [SerializeField] float m_fixedUnscaledDeltaTime;// RO
        [SerializeField] float m_fixedUnscaledTime; // RO        
        [SerializeField] float m_realtimeSinceStartup; // RO
        [SerializeField] float m_smoothDeltaTime; // RO
        [SerializeField] float m_time; // RO        
        [SerializeField] float m_timeSinceLevelLoad; // RO
        [SerializeField] float m_unscaledDeltaTime; // RO
        [SerializeField] float m_unscaledTime; // RO                
        [SerializeField] float m_fixedDeltaTime;
        [SerializeField] float m_maximumDeltaTime;
        [SerializeField] float m_timeScale;

        

        public float deltaTime{
            get{return m_deltaTime;}
            private set{m_deltaTime = value;}
        }
        
        public float fixedUnscaledDeltaTime{
            get{return m_fixedUnscaledDeltaTime;}
            private set{m_fixedUnscaledDeltaTime = value;}
        }
        
        public float fixedUnscaledTime{
            get{return m_fixedUnscaledTime;}
            set{m_fixedUnscaledTime = value;}
        }
        
        public int frameCount{
            get{return m_frameCount;}
            private set{m_frameCount = value;}
        }

        public float realtimeSinceStartup{
            get{return m_realtimeSinceStartup;}
            set{m_realtimeSinceStartup = value;}
        }

        public float smoothDeltaTime{
            get{return m_smoothDeltaTime;}
            private set{m_smoothDeltaTime = value;}
        }

        public float time{
            get{return m_time;}
            private set{m_time = value;}
        }
        
        public float timeSinceLevelLoad{
            get{return m_timeSinceLevelLoad;}
            private set{m_timeSinceLevelLoad = value;}
        }

        public float unscaledDeltaTime{
            get{return m_unscaledDeltaTime;}
            private set {m_unscaledDeltaTime = value;}
        }
        
        public float unscaledTime{
            get{return m_unscaledTime;}
            private set{ m_unscaledTime = value;}
        }
        
        public bool inFixedTimeStep{
            get{return m_inFixedTimeStep;}
            private set {m_inFixedTimeStep = value;}
        }
        
        public int captureFramerate{
            get{return m_captureFramerate;}
            set{m_captureFramerate = value;}
        }
        
        public float fixedDeltaTime{
            get{return m_fixedDeltaTime;}
            set{m_fixedDeltaTime = value;}
        }
        
        public float maximumDeltaTime{
            get{return m_maximumDeltaTime;}
            set{m_maximumDeltaTime = value;}
        }
        
        public float timeScale{
            get{return m_timeScale;}
            set{m_timeScale = value;}
        }


        /// <summary>
        /// 
        /// </summary>
        public TimeKun():this(false){}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSet"></param>
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


        /// <summary>
        /// 
        /// </summary>
        public void WriteBack()
        {
            Time.captureFramerate = captureFramerate;
            Time.fixedDeltaTime = fixedDeltaTime;
            Time.maximumDeltaTime = maximumDeltaTime;
            Time.timeScale = timeScale;   
        }


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_frameCount);
            binaryWriter.Write(m_captureFramerate);
            binaryWriter.Write(m_inFixedTimeStep);
            binaryWriter.Write(m_deltaTime);
            binaryWriter.Write(m_fixedUnscaledDeltaTime);
            binaryWriter.Write(m_fixedUnscaledTime);
            binaryWriter.Write(m_realtimeSinceStartup);
            binaryWriter.Write(m_smoothDeltaTime);
            binaryWriter.Write(m_time);
            binaryWriter.Write(m_timeSinceLevelLoad);
            binaryWriter.Write(m_unscaledDeltaTime);
            binaryWriter.Write(m_unscaledTime);
            binaryWriter.Write(m_fixedDeltaTime);
            binaryWriter.Write(m_maximumDeltaTime);
            binaryWriter.Write(m_timeScale);
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_frameCount = binaryReader.ReadInt32();
            m_captureFramerate = binaryReader.ReadInt32();
            m_inFixedTimeStep = binaryReader.ReadBoolean();
            m_deltaTime = binaryReader.ReadSingle();
            m_fixedUnscaledDeltaTime = binaryReader.ReadSingle();
            m_fixedUnscaledTime = binaryReader.ReadSingle();
            m_realtimeSinceStartup = binaryReader.ReadSingle();
            m_smoothDeltaTime = binaryReader.ReadSingle();
            m_time = binaryReader.ReadSingle();
            m_timeSinceLevelLoad = binaryReader.ReadSingle();
            m_unscaledDeltaTime = binaryReader.ReadSingle();
            m_unscaledTime = binaryReader.ReadSingle();
            m_fixedDeltaTime = binaryReader.ReadSingle();
            m_maximumDeltaTime = binaryReader.ReadSingle();
            m_timeScale = binaryReader.ReadSingle();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as TimeKun;
            if(other == null)
            {
                return false;
            }
            if (!m_frameCount.Equals(other.m_frameCount))
            {
                return false;
            }
            if (!m_captureFramerate.Equals(other.m_captureFramerate))
            {
                return false;
            }
            if (!m_inFixedTimeStep.Equals(other.m_inFixedTimeStep))
            {
                return false;
            }
            if (!m_deltaTime.Equals(other.m_deltaTime))
            {
                return false;
            }
            if (!m_fixedUnscaledDeltaTime.Equals(other.m_fixedUnscaledDeltaTime))
            {
                return false;
            }
            if (!m_fixedUnscaledTime.Equals(other.m_fixedUnscaledTime)){
                return false;
            }
            if (!m_realtimeSinceStartup.Equals(other.m_realtimeSinceStartup))
            {
                return false;
            }
            if (!m_smoothDeltaTime.Equals(other.m_smoothDeltaTime))
            {
                return false;
            }
            if (!m_time.Equals(other.m_time)){
                return false;
            }
            if (!m_timeSinceLevelLoad.Equals(other.m_timeSinceLevelLoad))
            {
                return false;
            }
            if (!m_unscaledDeltaTime.Equals(other.m_unscaledDeltaTime))
            {
                return false;
            }
            if (!m_unscaledTime.Equals(other.m_unscaledDeltaTime))
            {
                return false;
            }
            if (!m_fixedDeltaTime.Equals(other.m_fixedDeltaTime))
            {
                return false;
            }
            if (!m_maximumDeltaTime.Equals(other.m_maximumDeltaTime))
            {
                return false;
            }
            if (!m_timeScale.Equals(other.m_timeScale))
            {
                return false;
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