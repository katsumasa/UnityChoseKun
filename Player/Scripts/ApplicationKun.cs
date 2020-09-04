using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class ApplicationKun
    {
        // メンバー変数の定義
        [SerializeField]string m_absoluteURL ;
        public  string absoluteURL {
            get {return m_absoluteURL ;}
            private set {m_absoluteURL = value;}
        }
        
        [SerializeField] string m_buildGUID ;
        public string buildGUID {
            get{return m_buildGUID ;}
            private set {m_buildGUID = value;}
        }
        
        [SerializeField] string m_cloudProjectId ;
        public string cloudProjectId {
            get{return m_cloudProjectId;}
            private set{m_cloudProjectId = value;}
        }

        [SerializeField] string m_companyName;
        public string companyName{
            get{return m_companyName;}
            private set {m_companyName = value;}
        }

        [SerializeField]string m_consoleLogPath ;
        public string consoleLogPath {
            get{return m_consoleLogPath ;}
            private set {m_consoleLogPath  = value;}
        }

        [SerializeField] string m_dataPath ;
        public string dataPath {
            get{return m_dataPath;}
            private set{m_dataPath = value;}
        }

        [SerializeField] bool m_genuine ;
        public bool genuine {
            get{return m_genuine;}
            private set {m_genuine = value;}
        }

        [SerializeField] bool m_genuineCheckAvailable;
        public bool genuineCheckAvailable{
            get {return m_genuineCheckAvailable;}
            private set {m_genuineCheckAvailable = value;}
        }

        [SerializeField]string m_identifier ;
        public string identifier {
            get{return m_identifier;}
            private set{m_identifier = value;}
        }

        [SerializeField]string m_installerName ;
        public  string installerName {
            get{return m_installerName ;}
            private set{m_installerName  = value;}
        }
        
        [SerializeField] ApplicationInstallMode m_installMode ;
        public  ApplicationInstallMode installMode {
            get{return m_installMode;}
            private set{m_installMode = value;}
        }

        [SerializeField] NetworkReachability m_internetReachability ;
        public NetworkReachability internetReachability {
            get {return m_internetReachability;}
            private set {m_internetReachability = value;}
        }

        [SerializeField] bool m_isBatchMode ;
        public bool isBatchMode {
            get {return m_isBatchMode;}
            private set {m_isBatchMode = value;}
        }

        [SerializeField] bool m_isConsolePlatform ;
        public bool isConsolePlatform{
            get{return m_isConsolePlatform;}
            private set{m_isConsolePlatform = value;}
        }
        
        [SerializeField] bool m_isEditor ;
        public bool isEditor {
            get{return m_isEditor;}
            private set{m_isEditor = value;}
        }

        [SerializeField] bool m_isFocused ;
        public bool isFocused {
            get{return m_isFocused;}
            private set {m_isFocused = value;}
        }

        [SerializeField] bool m_isMobilePlatform ;
        public bool isMobilePlatform {
            get{return m_isMobilePlatform ;}
            private set{m_isMobilePlatform  = value;}
        }

        [SerializeField] bool m_isPlaying ;
        public bool isPlaying {
            get{return m_isPlaying;}
            private set{m_isPlaying = value;}
        }

        [SerializeField] string m_persistentDataPath ;
        public string persistentDataPath {
            get{return m_persistentDataPath;}
            private set{m_persistentDataPath = value;}
        }

        [SerializeField] RuntimePlatform m_platform ;
        public RuntimePlatform platform {
            get{return m_platform;}
            private set{m_platform = value;}
        }

        [SerializeField] string m_productName ;
        public string productName {
            get{return m_productName;}
            private set{m_productName = value;}
        }        

        [SerializeField] ApplicationSandboxType m_sandboxType ;
        public ApplicationSandboxType sandboxType{
            get{return m_sandboxType;}
            private set{m_sandboxType = value;}
        }
        
        [SerializeField] string m_streamingAssetsPath ;
        public string streamingAssetsPath {
            get {return m_streamingAssetsPath;}
            private set{m_streamingAssetsPath = value;}
        }

        [SerializeField] SystemLanguage m_systemLanguage;
        public SystemLanguage systemLanguage{
            get{return m_systemLanguage;}
            private set {m_systemLanguage = value;}
        }        

        [SerializeField] string m_temporaryCachePath ;
        public string temporaryCachePath {
            get{return m_temporaryCachePath;}
            private set{m_temporaryCachePath = value;}
        }

        [SerializeField] string m_unityVersion ;
        public string unityVersion {
            get{return m_unityVersion;}
            private set{m_unityVersion = value;}
        }

        [SerializeField] public string m_version ;
        public string version {
            get{return m_version;}
            private set{m_version = value;}
        }


        [SerializeField]ThreadPriority m_backgroundLoadingPriority ;
        public ThreadPriority backgroundLoadingPriority {
            get{return m_backgroundLoadingPriority;}
            set{m_backgroundLoadingPriority = value;}
        }

        [SerializeField] int m_targetFrameRate ;
        public int targetFrameRate {
            get{return m_targetFrameRate;}
            set{m_targetFrameRate = value;}
        }
        [SerializeField] bool m_runInBackground ;
        public bool runInBackground {
            get{return m_runInBackground;}
            set{m_runInBackground = value;}
        }

        [SerializeField] bool m_isDirty;
        public bool isDirty
        {
            get { return m_isDirty; }
            set { m_isDirty = value; }
        }


        public ApplicationKun():this(false){}

        public ApplicationKun(bool isSet):base(){
            if(isSet){
                absoluteURL = Application.absoluteURL;
                buildGUID = Application.buildGUID;
                cloudProjectId = Application.cloudProjectId;
                companyName = Application.companyName;
                consoleLogPath = Application.consoleLogPath;
                dataPath = Application.dataPath;
                genuine = Application.genuine;
                genuineCheckAvailable = Application.genuineCheckAvailable;
                identifier = Application.identifier;
                installerName = Application.installerName;
                installMode = Application.installMode;
                internetReachability = Application.internetReachability;
                isBatchMode = Application.isBatchMode;
                isConsolePlatform = Application.isConsolePlatform;
                isEditor = Application.isEditor;
                isFocused = Application.isFocused;
                isMobilePlatform = Application.isMobilePlatform;
                isPlaying = Application.isPlaying;
                persistentDataPath = Application.persistentDataPath;
                platform = Application.platform;
                productName = Application.productName;
                sandboxType = Application.sandboxType;
                streamingAssetsPath = Application.streamingAssetsPath;
                systemLanguage = Application.systemLanguage;
                temporaryCachePath = Application.temporaryCachePath;
                unityVersion = Application.unityVersion;
                version = Application.version;
                backgroundLoadingPriority = Application.backgroundLoadingPriority;
                targetFrameRate = Application.targetFrameRate;
                runInBackground = Application.runInBackground;
            }
        }

        public void WriteBack()
        {
            if (isDirty)
            {
                Application.backgroundLoadingPriority = backgroundLoadingPriority;
                Application.targetFrameRate = targetFrameRate;
                Application.runInBackground = runInBackground;
            }
        }
    }
}
