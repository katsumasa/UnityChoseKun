using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    // <summary>
    // Androidデバイス固有の設定
    // </summary>
    [System.Serializable]
    public class AndroidKun 
    {
        [SerializeField] bool m_isSustainedPerformanceMode;
        public bool isSustainedPerformanceMode{
            get {return m_isSustainedPerformanceMode;}
            set {m_isSustainedPerformanceMode = value;}
        }
        [SerializeField] bool m_requestSustainedPerformanceMode;
        public bool requestSustainedPerformanceMode {
            get {return m_requestSustainedPerformanceMode;}
            set {m_requestSustainedPerformanceMode = value;}
        }

        [SerializeField] string [] m_permissions;
        public string[] permissions {
            get{return m_permissions;}
            set{m_permissions = value;}
        }

        [SerializeField] bool[] m_hasUserAuthorizedPermissions;
        public bool[] hasUserAuthorizedPermission{
            get {return m_hasUserAuthorizedPermissions; }
            private set { m_hasUserAuthorizedPermissions = value;}
        }

        [SerializeField] bool[] m_requestUserPermissions;
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
    }
}
