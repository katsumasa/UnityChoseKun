using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun{
    // <summary>
    // Androidデバイス固有の設定
    // </summary>
    [System.Serializable]
    public class AndroidKun : ISerializerKun
    {
        [SerializeField] bool m_isSustainedPerformanceMode;
        [SerializeField] bool m_requestSustainedPerformanceMode;
        [SerializeField] bool[] m_hasUserAuthorizedPermissions;
        [SerializeField] bool[] m_requestUserPermissions;
        [SerializeField] string[] m_permissions;


        public bool isSustainedPerformanceMode{
            get {return m_isSustainedPerformanceMode;}
            set {m_isSustainedPerformanceMode = value;}
        }
        
        public bool requestSustainedPerformanceMode {
            get {return m_requestSustainedPerformanceMode;}
            set {m_requestSustainedPerformanceMode = value;}
        }

        
        public string[] permissions {
            get{return m_permissions;}
            set{m_permissions = value;}
        }

        
        public bool[] hasUserAuthorizedPermission{
            get {return m_hasUserAuthorizedPermissions; }
            private set { m_hasUserAuthorizedPermissions = value;}
        }

        
        public bool[] requestUserPermissions{
            get{return m_requestUserPermissions;}
            set{m_requestUserPermissions = value;}
        }


        // <summary>
        // コンストラクター
        // </summary>
        public AndroidKun(){
            isSustainedPerformanceMode = false;

#if UNITY_ANDROID
            permissions = new string[6];
            permissions[0] = UnityEngine.Android.Permission.Camera;
            permissions[1] = UnityEngine.Android.Permission.CoarseLocation;
            permissions[2] = UnityEngine.Android.Permission.ExternalStorageRead;
            permissions[3] = UnityEngine.Android.Permission.ExternalStorageWrite;
            permissions[4] = UnityEngine.Android.Permission.FineLocation;
            permissions[5] = UnityEngine.Android.Permission.Microphone;
            hasUserAuthorizedPermission = new bool[permissions.Length];
#if !UNITY_EDITOR
            for(var i = 0; i < permissions.Length; i++){
                hasUserAuthorizedPermission[i] = UnityEngine.Android.Permission.HasUserAuthorizedPermission(permissions[i]);
            }
#endif
            requestUserPermissions = new bool[permissions.Length];
#endif
        }

        // <summary>
        // デストラクター
        // </summary>
        ~AndroidKun()
        {
#if UNITY_ANDROID
            permissions = null;
            hasUserAuthorizedPermission = null;
            requestUserPermissions = null;
#endif
        }


        /// <summary>
        /// 
        /// </summary>
        public void WriteBack()
        {
#if UNITY_ANDROID
#if UNITY_2019_1_OR_NEWER
            if (requestSustainedPerformanceMode) {
                UnityEngine.Android.AndroidDevice.SetSustainedPerformanceMode(isSustainedPerformanceMode);
                requestSustainedPerformanceMode = false;
            }
#endif
            for (var i = 0; i < permissions.Length; i++){
                if(requestUserPermissions[i]){
                    UnityEngine.Android.Permission.RequestUserPermission(permissions[i]);
                    hasUserAuthorizedPermission[i] = UnityEngine.Android.Permission.HasUserAuthorizedPermission(permissions[i]);
                    requestUserPermissions[i] = false;
                }
            }

#endif
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_isSustainedPerformanceMode);
            binaryWriter.Write(m_requestSustainedPerformanceMode);
            SerializerKun.Serialize(binaryWriter, m_hasUserAuthorizedPermissions);
            SerializerKun.Serialize(binaryWriter, m_requestUserPermissions);
            SerializerKun.Serialize(binaryWriter, m_permissions);                        
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_isSustainedPerformanceMode = binaryReader.ReadBoolean();
            m_requestSustainedPerformanceMode = binaryReader.ReadBoolean();
            m_hasUserAuthorizedPermissions = SerializerKun.DesirializeBooleans(binaryReader);
            m_requestUserPermissions = SerializerKun.DesirializeBooleans(binaryReader);
            m_permissions = SerializerKun.DesirializeStrings(binaryReader);                        
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as AndroidKun;
            if(other == null)
            {
                return false;
            }
            if (!m_isSustainedPerformanceMode.Equals(other.m_isSustainedPerformanceMode))
            {
                return false;
            }
            if (!m_requestSustainedPerformanceMode.Equals(other.m_requestSustainedPerformanceMode))
            {
                return false;
            }

            if(!SerializerKun.Equals<bool>(m_hasUserAuthorizedPermissions, other.m_hasUserAuthorizedPermissions))
            {
                return false;
            }

            if (!SerializerKun.Equals<bool>(m_requestUserPermissions, other.m_requestUserPermissions))
            {
                return false;
            }

            if (!SerializerKun.Equals<string>(m_permissions, other.m_permissions))
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
