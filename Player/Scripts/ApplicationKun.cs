using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class ApplicationKun : ISerializerKun
    {
        // メンバー変数の定義
        [SerializeField] string m_absoluteURL ;
        [SerializeField] string m_buildGUID;
        [SerializeField] string m_cloudProjectId;
        [SerializeField] string m_companyName;
        [SerializeField] string m_consoleLogPath;
        [SerializeField] string m_dataPath;
        [SerializeField] bool m_genuine;
        [SerializeField] bool m_genuineCheckAvailable;
        [SerializeField] string m_identifier;
        [SerializeField] string m_installerName;
        [SerializeField] ApplicationInstallMode m_installMode;
        [SerializeField] NetworkReachability m_internetReachability;
        [SerializeField] bool m_isBatchMode;
        [SerializeField] bool m_isConsolePlatform;
        [SerializeField] bool m_isEditor;
        [SerializeField] bool m_isFocused;
        [SerializeField] bool m_isMobilePlatform;
        [SerializeField] bool m_isPlaying;
        [SerializeField] string m_persistentDataPath;
        [SerializeField] RuntimePlatform m_platform;
        [SerializeField] string m_productName;
        [SerializeField] ApplicationSandboxType m_sandboxType;
        [SerializeField] string m_streamingAssetsPath;
        [SerializeField] SystemLanguage m_systemLanguage;
        [SerializeField] string m_temporaryCachePath;
        [SerializeField] string m_unityVersion;
        [SerializeField] public string m_version;
        [SerializeField] ThreadPriority m_backgroundLoadingPriority;
        [SerializeField] int m_targetFrameRate;
        [SerializeField] bool m_runInBackground;
        [SerializeField] bool m_isDirty;



        public  string absoluteURL {
            get {return m_absoluteURL ;}
            private set {m_absoluteURL = value;}
        }
        
        
        public string buildGUID {
            get{return m_buildGUID ;}
            private set {m_buildGUID = value;}
        }
        

        public string cloudProjectId {
            get{return m_cloudProjectId;}
            private set{m_cloudProjectId = value;}
        }

        
        public string companyName{
            get{return m_companyName;}
            private set {m_companyName = value;}
        }


        public string consoleLogPath {
            get{return m_consoleLogPath ;}
            private set {m_consoleLogPath  = value;}
        }


        public string dataPath {
            get{return m_dataPath;}
            private set{m_dataPath = value;}
        }

        public bool genuine {
            get{return m_genuine;}
            private set {m_genuine = value;}
        }
        
        public bool genuineCheckAvailable{
            get {return m_genuineCheckAvailable;}
            private set {m_genuineCheckAvailable = value;}
        }

        public string identifier {
            get{return m_identifier;}
            private set{m_identifier = value;}
        }

        
        public  string installerName {
            get{return m_installerName ;}
            private set{m_installerName  = value;}
        }
        
        
        public  ApplicationInstallMode installMode {
            get{return m_installMode;}
            private set{m_installMode = value;}
        }

        
        public NetworkReachability internetReachability {
            get {return m_internetReachability;}
            private set {m_internetReachability = value;}
        }

        
        public bool isBatchMode {
            get {return m_isBatchMode;}
            private set {m_isBatchMode = value;}
        }

        
        public bool isConsolePlatform{
            get{return m_isConsolePlatform;}
            private set{m_isConsolePlatform = value;}
        }
        
        
        public bool isEditor {
            get{return m_isEditor;}
            private set{m_isEditor = value;}
        }


        public bool isFocused {
            get{return m_isFocused;}
            private set {m_isFocused = value;}
        }

        
        public bool isMobilePlatform {
            get{return m_isMobilePlatform ;}
            private set{m_isMobilePlatform  = value;}
        }

        
        public bool isPlaying {
            get{return m_isPlaying;}
            private set{m_isPlaying = value;}
        }

        
        public string persistentDataPath {
            get{return m_persistentDataPath;}
            private set{m_persistentDataPath = value;}
        }

        
        public RuntimePlatform platform {
            get{return m_platform;}
            private set{m_platform = value;}
        }

        
        public string productName {
            get{return m_productName;}
            private set{m_productName = value;}
        }        

        
        public ApplicationSandboxType sandboxType{
            get{return m_sandboxType;}
            private set{m_sandboxType = value;}
        }
        
        
        public string streamingAssetsPath {
            get {return m_streamingAssetsPath;}
            private set{m_streamingAssetsPath = value;}
        }

        
        public SystemLanguage systemLanguage{
            get{return m_systemLanguage;}
            private set {m_systemLanguage = value;}
        }        

        
        public string temporaryCachePath {
            get{return m_temporaryCachePath;}
            private set{m_temporaryCachePath = value;}
        }

        
        public string unityVersion {
            get{return m_unityVersion;}
            private set{m_unityVersion = value;}
        }

        
        public string version {
            get{return m_version;}
            private set{m_version = value;}
        }


        
        public ThreadPriority backgroundLoadingPriority {
            get{return m_backgroundLoadingPriority;}
            set{m_backgroundLoadingPriority = value;}
        }

        
        public int targetFrameRate {
            get{return m_targetFrameRate;}
            set{m_targetFrameRate = value;}
        }
        
        public bool runInBackground {
            get{return m_runInBackground;}
            set{m_runInBackground = value;}
        }

        
        public bool isDirty
        {
            get { return m_isDirty; }
            set { m_isDirty = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public ApplicationKun():this(false){}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSet"></param>
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


        /// <summary>
        /// 
        /// </summary>
        public void WriteBack()
        {
            if (isDirty)
            {
                Application.backgroundLoadingPriority = backgroundLoadingPriority;
                Application.targetFrameRate = targetFrameRate;
                Application.runInBackground = runInBackground;
            }
        }


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_absoluteURL);
            binaryWriter.Write(m_buildGUID);
            binaryWriter.Write(m_cloudProjectId);
            binaryWriter.Write(m_companyName);
            binaryWriter.Write(m_consoleLogPath);
            binaryWriter.Write(m_dataPath);
            binaryWriter.Write(m_genuine);
            binaryWriter.Write(m_genuineCheckAvailable);
            binaryWriter.Write(m_identifier);
            binaryWriter.Write(m_installerName);
            binaryWriter.Write((int)m_installMode);
            binaryWriter.Write((int)m_internetReachability);
            binaryWriter.Write(m_isBatchMode);
            binaryWriter.Write(m_isConsolePlatform);
            binaryWriter.Write(m_isEditor);
            binaryWriter.Write(m_isFocused);
            binaryWriter.Write(m_isMobilePlatform);
            binaryWriter.Write(m_isPlaying);
            binaryWriter.Write(m_persistentDataPath);
            binaryWriter.Write((int)m_platform);
            binaryWriter.Write(m_productName);
            binaryWriter.Write((int)m_sandboxType);
            binaryWriter.Write(m_streamingAssetsPath);
            binaryWriter.Write((int)m_systemLanguage);
            binaryWriter.Write(m_temporaryCachePath);
            binaryWriter.Write(m_unityVersion);
            binaryWriter.Write(m_version);
            binaryWriter.Write((int)m_backgroundLoadingPriority);
            binaryWriter.Write(m_targetFrameRate);
            binaryWriter.Write(m_runInBackground);
            binaryWriter.Write(m_isDirty);
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_absoluteURL = binaryReader.ReadString();
            m_buildGUID = binaryReader.ReadString();
            m_cloudProjectId = binaryReader.ReadString();
            m_companyName = binaryReader.ReadString();
            m_consoleLogPath = binaryReader.ReadString();
            m_dataPath = binaryReader.ReadString();
            m_genuine = binaryReader.ReadBoolean();
            m_genuineCheckAvailable = binaryReader.ReadBoolean();
            m_identifier = binaryReader.ReadString();
            m_installerName = binaryReader.ReadString();
            m_installMode = (ApplicationInstallMode)binaryReader.ReadInt32();
            m_internetReachability = (NetworkReachability)binaryReader.ReadInt32();
            m_isBatchMode = binaryReader.ReadBoolean();
            m_isConsolePlatform = binaryReader.ReadBoolean();
            m_isEditor = binaryReader.ReadBoolean();
            m_isFocused = binaryReader.ReadBoolean();
            m_isMobilePlatform = binaryReader.ReadBoolean();
            m_isPlaying = binaryReader.ReadBoolean();
            m_persistentDataPath = binaryReader.ReadString();
            m_platform = (RuntimePlatform)binaryReader.ReadInt32();
            m_productName = binaryReader.ReadString();
            m_sandboxType = (ApplicationSandboxType)binaryReader.ReadInt32();
            m_streamingAssetsPath = binaryReader.ReadString();
            m_systemLanguage = (SystemLanguage)binaryReader.ReadInt32();
            m_temporaryCachePath = binaryReader.ReadString();
            m_unityVersion = binaryReader.ReadString();
            m_version = binaryReader.ReadString();
            m_backgroundLoadingPriority = (ThreadPriority)binaryReader.ReadInt32();
            m_targetFrameRate = binaryReader.ReadInt32();
            m_runInBackground = binaryReader.ReadBoolean();
            m_isDirty = binaryReader.ReadBoolean();
        }

        public override bool Equals(object obj)
        {
            var other = obj as ApplicationKun;
            if(other == null)
            {
                return false;
            }
            if (!string.Equals(m_absoluteURL, other.m_absoluteURL))
            {
                return false;
            }
            if (!string.Equals(m_buildGUID, other.m_buildGUID))
            {
                return false;
            }
            if (!string.Equals(m_cloudProjectId, other.m_cloudProjectId))
            {
                return false;
            }
            if (!string.Equals(m_companyName, other.m_companyName))
            {
                return false;
            }
            if (!string.Equals(m_consoleLogPath, other.m_consoleLogPath))
            {
                return false;
            }
            if (!string.Equals(m_dataPath, other.m_dataPath))
            {
                return false;
            }
            if (!bool.Equals(m_genuine, other.m_genuine))
            {
                return false;
            }
            if (!bool.Equals(m_genuineCheckAvailable, other.m_genuineCheckAvailable))
            {
                return false;
            }
            if (!string.Equals(m_identifier, other.m_identifier))
            {
                return false;
            }
            if (!string.Equals(m_installerName, other.m_installerName))
            {
                return false;
            }
            if (!m_installMode.Equals(other.m_installMode))
            {
                return false;
            }
            if (!m_internetReachability.Equals(other.m_internetReachability))
            {
                return false;
            }
            if (!bool.Equals(m_isBatchMode, other.m_isBatchMode))
            {
                return false;
            }
            if (!bool.Equals(m_isConsolePlatform, other.m_isConsolePlatform))
            {
                return false;
            }
            if (!bool.Equals(m_isEditor, other.m_isEditor))
            {
                return false;
            }
            if (!bool.Equals(m_isFocused, other.m_isFocused))
            {
                return false;
            }
            if (!bool.Equals(m_isMobilePlatform, other.m_isMobilePlatform))
            {
                return false;
            }
            if (!bool.Equals(m_isPlaying, other.m_isPlaying))
            {
                return false;
            }
            if (!string.Equals(m_persistentDataPath, other.m_persistentDataPath))
            {
                return false;
            }
            if (!m_platform.Equals(other.m_platform))
            {
                return false;
            }
            if (!string.Equals(m_productName, other.m_productName))
            {
                return false;
            }
            if (!m_sandboxType.Equals(other.m_sandboxType))
            {
                return false;
            }
            if (!string.Equals(m_streamingAssetsPath, other.m_streamingAssetsPath))
            {
                return false;
            }
            if (!m_systemLanguage.Equals(other.m_systemLanguage))
            {
                return false;
            }
            if (!string.Equals(m_temporaryCachePath, other.m_temporaryCachePath))
            {
                return false;
            }
            if (!string.Equals(m_unityVersion, other.m_unityVersion))
            {
                return false;
            }
            if (!string.Equals(m_version, other.m_version))
            {
                return false;
            }
            if (!m_backgroundLoadingPriority.Equals(other.m_backgroundLoadingPriority))
            {
                return false;
            }
            if (!m_targetFrameRate.Equals(other.m_targetFrameRate))
            {
                return false;
            }
            if (!bool.Equals(m_runInBackground, other.m_runInBackground))
            {
                return false;
            }
            if (!bool.Equals(m_isDirty, other.m_isDirty))
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
