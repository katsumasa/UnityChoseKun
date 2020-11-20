#if DEBUG
#define UNITY_CHOSEKUN_DEBUG
#else
#undef UNITY_CHOSEKUN_DEBUG
#endif

/// <summary>
/// いずれかのSERIALIZATIONを選択する
/// </summary>
#define SERIALIZATION_BINARFORMATTER
//#define SERIALIZATION_JSON


namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;
    using TMPro;
    using System;

    /// <summary>
    /// UnityChoseKunの共通パケット
    /// </summary>
    [System.Serializable]    
    public class UnityChoseKunMessageData
    {
        [SerializeField] public UnityChoseKun.MessageID id;        
        [SerializeField] public byte[] bytes;
    }

    /// <summary>
    /// UnityChoseKun Class
    /// </summary>
    public class UnityChoseKun
    {
        /// <summary>
        /// MessageID
        /// </summary>
        public enum MessageID
        {
            ScreenPull,
            ScreenPush,
            TimePull,
            TimePush,
            GameObjectPull,
            GameObjectPush,
            ShaderPull,
            TexturePull,
            ApplicationPull,
            ApplicationPush,
            ApplicationQuit,
            AndroidPull,
            AndroidPush,
            HierarchyPush,
            QualitySettingsPull,
            QualitySettingsPush,
            OnDemandRenderingPull,
            OnDemandRenderingPush,

        }


        /// <summary>
        /// From Editor to Playerで使用するPlayerConnec用のGUID
        /// </summary>        
        public static readonly System.Guid kMsgSendEditorToPlayer = new System.Guid("a819fa0823134ed9bfc6cf17eac8a232");

        /// <summary>
        /// From Player to Editorで使用するPlayerConnect用のGUID
        /// </summary>
        public static readonly System.Guid kMsgSendPlayerToEditor = new System.Guid("5b9b9d37e331433cbd31c6cf8093d8da");



        /// <summary>
        /// ClassIDからSystem.Typeの文字列を取得
        /// https://docs.unity3d.com/ja/2019.4/Manual/ClassIDReference.html 
        /// </summary>
        public static Dictionary<int, string> mClassID2TypeDict = new Dictionary<int, string>(){
          {0   ,"Object"},
          {1   ,"GameObject"},
          {2   ,"Component"},
          {3   ,"LevelGameManager"},
          {4   ,"Transform"},
          {5   ,"TimeManager"},
          {6   ,"GlobalGameManager"},
          {8   ,"Behaviour"},
          {9   ,"GameManager"},
          {11  ,"AudioManager"},
          {13  ,"InputManager"},
          {18  ,"EditorExtension"},
          {19  ,"Physics2DSettings"},
          {20  ,"Camera"},
          {21  ,"Material"},
          {23  ,"MeshRenderer"},
          {25  ,"Renderer"},
          {27  ,"Texture"},
          {28  ,"Texture2D"},
          {29  ,"OcclusionCullingSettings"},
          {30  ,"GraphicsSettings"},
          {33  ,"MeshFilter"},
          {41  ,"OcclusionPortal"},
          {43  ,"Mesh"},
          {45  ,"Skybox"},
          {47  ,"QualitySettings"},
          {48  ,"Shader"},
          {49  ,"TextAsset"},
          {50  ,"Rigidbody2D"},
          {53  ,"Collider2D"},
          {54  ,"Rigidbody"},
          {55  ,"PhysicsManager"},
          {56  ,"Collider"},
          {57  ,"Joint"},
          {58  ,"CircleCollider2D"},
          {59  ,"HingeJoint"},
          {60  ,"PolygonCollider2D"},
          {61  ,"BoxCollider2D"},
          {62  ,"PhysicsMaterial2D"},
          {64  ,"MeshCollider"},
          {65  ,"BoxCollider"},
          {66  ,"CompositeCollider2D"},
          {68  ,"EdgeCollider2D"},
          {70  ,"CapsuleCollider2D"},
          {72  ,"ComputeShader"},
          {74  ,"AnimationClip"},
          {75  ,"ConstantForce"},
          {78  ,"TagManager"},
          {81  ,"Audio Listener"},
          {82  ,"AudioSource"},
          {83  ,"AudioClip"},
          {84  ,"RenderTexture"},
          {86  ,"CustomRenderTexture"},
          {89  ,"Cubemap"},
          {90  ,"Avatar"},
          {91  ,"AnimatorController"},
          {93  ,"RuntimeAnimatorController"},
          {94  ,"ScriptMapper"},
          {95  ,"Animator"},
          {96  ,"TrailRenderer"},
          {98  ,"DelayedCallManager"},
          {102 ,"TextMesh"},
          {104 ,"RenderSettings"},
          {108 ,"Light"},
          {109 ,"CGProgram"},
          {110 ,"BaseAnimationTrack"},
          {111 ,"Animation"},
          {114 ,"MonoBehaviour"},
          {115 ,"MonoScript"},
          {116 ,"MonoManager"},
          {117 ,"Texture3D"},
          {118 ,"NewAnimationTrack"},
          {119 ,"Projector"},
          {120 ,"LineRenderer"},
          {121 ,"Flare"},
          {122 ,"Halo"},
          {123 ,"LensFlare"},
          {124 ,"FlareLayer"},
          {125 ,"HaloLayer"},
          {126 ,"NavMeshProjectSettings"},
          {128 ,"Font"},
          {129 ,"PlayerSettings"},
          {130 ,"NamedObject"},
          {134 ,"PhysicMaterial"},
          {135 ,"SphereCollider"},
          {136 ,"CapsuleCollider"},
          {137 ,"SkinnedMeshRenderer"},
          {138 ,"FixedJoint"},
          {141 ,"BuildSettings"},
          {142 ,"AssetBundle"},
          {143 ,"CharacterController"},
          {144 ,"CharacterJoint"},
          {145 ,"SpringJoint"},
          {146 ,"WheelCollider"},
          {147 ,"ResourceManager"},
          {150 ,"PreloadData"},
          {153 ,"ConfigurableJoint"},
          {154 ,"TerrainCollider"},
          {156 ,"TerrainData"},
          {157 ,"LightmapSettings"},
          {158 ,"WebCamTexture"},
          {159 ,"EditorSettings"},
          {162 ,"EditorUserSettings"},
          {164 ,"AudioReverbFilter"},
          {165 ,"AudioHighPassFilter"},
          {166 ,"AudioChorusFilter"},
          {167 ,"AudioReverbZone"},
          {168 ,"AudioEchoFilter"},
          {169 ,"AudioLowPassFilter"},
          {170 ,"AudioDistortionFilter"},
          {171 ,"SparseTexture"},
          {180 ,"AudioBehaviour"},
          {181 ,"AudioFilter"},
          {182 ,"WindZone"},
          {183 ,"Cloth"},
          {184 ,"SubstanceArchive"},
          {185 ,"ProceduralMaterial"},
          {186 ,"ProceduralTexture"},
          {187 ,"Texture2DArray"},
          {188 ,"CubemapArray"},
          {191 ,"OffMeshLink"},
          {192 ,"OcclusionArea"},
          {193 ,"Tree"},
          {195 ,"NavMeshAgent"},
          {196 ,"NavMeshSettings"},
          {198 ,"ParticleSystem"},
          {199 ,"ParticleSystemRenderer"},
          {200 ,"ShaderVariantCollection"},
          {205 ,"LODGroup"},
          {206 ,"BlendTree"},
          {207 ,"Motion"},
          {208 ,"NavMeshObstacle"},
          {210 ,"SortingGroup"},
          {212 ,"SpriteRenderer"},
          {213 ,"Sprite"},
          {214 ,"CachedSpriteAtlas"},
          {215 ,"ReflectionProbe"},
          {218 ,"Terrain"},
          {220 ,"LightProbeGroup"},
          {221 ,"AnimatorOverrideController"},
          {222 ,"CanvasRenderer"},
          {223 ,"Canvas"},
          {224 ,"RectTransform"},
          {225 ,"CanvasGroup"},
          {226 ,"BillboardAsset"},
          {227 ,"BillboardRenderer"},
          {228 ,"SpeedTreeWindAsset"},
          {229 ,"AnchoredJoint2D"},
          {230 ,"Joint2D"},
          {231 ,"SpringJoint2D"},
          {232 ,"DistanceJoint2D"},
          {233 ,"HingeJoint2D"},
          {234 ,"SliderJoint2D"},
          {235 ,"WheelJoint2D"},
          {236 ,"ClusterInputManager"},
          {237 ,"BaseVideoTexture"},
          {238 ,"NavMeshData"},
          {240 ,"AudioMixer"},
          {241 ,"AudioMixerController"},
          {243 ,"AudioMixerGroupController"},
          {244 ,"AudioMixerEffectController"},
          {245 ,"AudioMixerSnapshotController"},
          {246 ,"PhysicsUpdateBehaviour2D"},
          {247 ,"ConstantForce2D"},
          {248 ,"Effector2D"},
          {249 ,"AreaEffector2D"},
          {250 ,"PointEffector2D"},
          {251 ,"PlatformEffector2D"},
          {252 ,"SurfaceEffector2D"},
          {253 ,"BuoyancyEffector2D"},
          {254 ,"RelativeJoint2D"},
          {255 ,"FixedJoint2D"},
          {256 ,"FrictionJoint2D"},
          {257 ,"TargetJoint2D"},
          {258 ,"LightProbes"},
          {259 ,"LightProbeProxyVolume"},
          {271 ,"SampleClip"},
          {272 ,"AudioMixerSnapshot"},
          {273 ,"AudioMixerGroup"},
          {290 ,"AssetBundleManifest"},
          {300 ,"RuntimeInitializeOnLoadManager"},
          {310 ,"UnityConnectSettings"},
          {319 ,"AvatarMask"},
          {320 ,"PlayableDirector"},
          {328 ,"VideoPlayer"},
          {329 ,"VideoClip"},
          {330 ,"ParticleSystemForceField"},
          {331 ,"SpriteMask"},
          {362 ,"WorldAnchor"},
          {363 ,"OcclusionCullingData"},
          {1001,"PrefabInstance"},
          {1002,"EditorExtensionImpl"},
          {1003,"AssetImporter"},
          {1004,"AssetDatabaseV1"},
          {1005,"Mesh3DSImporter"},
          {1006,"TextureImporter"},
          {1007,"ShaderImporter"},
          {1008,"ComputeShaderImporter"},
          {1020,"AudioImporter"},
          {1026,"HierarchyState"},
          {1028,"AssetMetaData"},
          {1029,"DefaultAsset"},
          {1030,"DefaultImporter"},
          {1031,"TextScriptImporter"},
          {1032,"SceneAsset"},
          {1034,"NativeFormatImporter"},
          {1035,"MonoImporter"},
          {1038,"LibraryAssetImporter"},
          {1040,"ModelImporter"},
          {1041,"FBXImporter"},
          {1042,"TrueTypeFontImporter"},
          {1045,"EditorBuildSettings"},
          {1048,"InspectorExpandedState"},
          {1049,"AnnotationManager"},
          {1050,"PluginImporter"},
          {1051,"EditorUserBuildSettings"},
          {1055,"IHVImageFormatImporter"},
          {1101,"AnimatorStateTransition"},
          {1102,"AnimatorState"},
          {1105,"HumanTemplate"},
          {1107,"AnimatorStateMachine"},
          {1108,"PreviewAnimationClip"},
          {1109,"AnimatorTransition"},
          {1110,"SpeedTreeImporter"},
          {1111,"AnimatorTransitionBase"},
          {1112,"SubstanceImporter"},
          {1113,"LightmapParameters"},
          {1120,"LightingDataAsset"},
          {1124,"SketchUpImporter"},
          {1125,"BuildReport"},
          {1126,"PackedAssets"},
          {1127,"VideoClipImporter"},
          {100000      ,"int"},
          {100001      ,"bool"},
          {100002      ,"float"},
          {100003      ,"MonoObject"},
          {100004      ,"Collision"},
          {100005      ,"Vector3f"},
          {100006      ,"RootMotionData"},
          {100007      ,"Collision2D"},
          {100008      ,"AudioMixerLiveUpdateFloat"},
          {100009      ,"AudioMixerLiveUpdateBool"},
          {100010      ,"Polygon2D"},
          {100011      ,"void"},
          {19719996    ,"TilemapCollider2D"},
          {41386430    ,"AssetImporterLog"},
          {73398921    ,"VFXRenderer"},
          {76251197    ,"SerializableManagedRefTestClass"},
          {156049354   ,"Grid"},
          {181963792   ,"Preset"},
          {277625683   ,"EmptyObject"},
          {285090594   ,"IConstraint"},
          {293259124   ,"TestObjectWithSpecialLayoutOne"},
          {294290339   ,"AssemblyDefinitionReferenceImporter"},
          {334799969   ,"SiblingDerived"},
          {342846651   ,"TestObjectWithSerializedMapStringNonAlignedStruct"},
          {367388927   ,"SubDerived"},
          {369655926   ,"AssetImportInProgressProxy"},
          {382020655   ,"PluginBuildInfo"},
          {426301858   ,"EditorProjectAccess"},
          {468431735   ,"PrefabImporter"},
          {478637458   ,"TestObjectWithSerializedArray"},
          {478637459   ,"TestObjectWithSerializedAnimationCurve"},
          {483693784   ,"TilemapRenderer"},
          {638013454   ,"SpriteAtlasDatabase"},
          {641289076   ,"AudioBuildInfo"},
          {644342135   ,"CachedSpriteAtlasRuntimeData"},
          {646504946   ,"RendererFake"},
          {662584278   ,"AssemblyDefinitionReferenceAsset"},
          {668709126   ,"BuiltAssetBundleInfoSet"},
          {687078895   ,"SpriteAtlas"},
          {747330370   ,"RayTracingShaderImporter"},
          {825902497   ,"RayTracingShader"},
          {877146078   ,"PlatformModuleSetup"},
          {895512359   ,"AimConstraint"},
          {937362698   ,"VFXManager"},
          {994735392   ,"VisualEffectSubgraph"},
          {994735403   ,"VisualEffectSubgraphOperator"},
          {994735404   ,"VisualEffectSubgraphBlock"},
          {1001480554  ,"Prefab"},
          {1027052791  ,"LocalizationImporter"},
          {1091556383  ,"Derived"},
          {1111377672  ,"PropertyModificationsTargetTestObject"},
          {1114811875  ,"ReferencesArtifactGenerator"},
          {1152215463  ,"AssemblyDefinitionAsset"},
          {1154873562  ,"SceneVisibilityState"},
          {1183024399  ,"LookAtConstraint"},
          {1223240404  ,"MultiArtifactTestImporter"},
          {1268269756  ,"GameObjectRecorder"},
          {1325145578  ,"LightingDataAssetParent"},
          {1386491679  ,"PresetManager"},
          {1392443030  ,"TestObjectWithSpecialLayoutTwo"},
          {1403656975  ,"StreamingManager"},
          {1480428607  ,"LowerResBlitTexture"},
          {1542919678  ,"StreamingController"},
          {1628831178  ,"TestObjectVectorPairStringBool"},
          {1742807556  ,"GridLayout"},
          {1766753193  ,"AssemblyDefinitionImporter"},
          {1773428102  ,"ParentConstraint"},
          {1803986026  ,"FakeComponent"},
          {1818360608  ,"PositionConstraint"},
          {1818360609  ,"RotationConstraint"},
          {1818360610  ,"ScaleConstraint"},
          {1839735485  ,"Tilemap"},
          {1896753125  ,"PackageManifest"},
          {1896753126  ,"PackageManifestImporter"},
          {1953259897  ,"TerrainLayer"},
          {1971053207  ,"SpriteShapeRenderer"},
          {1977754360  ,"NativeObjectType"},
          {1981279845  ,"TestObjectWithSerializedMapStringBool"},
          {1995898324  ,"SerializableManagedHost"},
          {2058629509  ,"VisualEffectAsset"},
          {2058629510  ,"VisualEffectImporter"},
          {2058629511  ,"VisualEffectResource"},
          {2059678085  ,"VisualEffectObject"},
          {2083052967  ,"VisualEffect"},
          {2083778819  ,"LocalizationAsset"},
          {2089858483  ,"ScriptedImporter"},
        };



        /// <summary>
        /// System TypeからClassIDを取得する
        /// </summary>
        /// <param name="systemType">System Type</param>
        /// <returns>Class ID</returns>
        public static int SystemType2ClassID(string systemType)
        {
            foreach (KeyValuePair<int, string> item in mClassID2TypeDict)
            {

                if (string.Compare(systemType, item.Value)==0)
                {
                    return item.Key;
                }
                
            }
            return 0;
        }


        /// <summary>
        /// Class IDからSystem Typeを取得する
        /// </summary>
        /// <param name="classID">Class ID</param>
        /// <returns>System Type</returns>
        public static string ClassID2SystemType(int classID)
        {
            return mClassID2TypeDict[classID];
        }



        /// <summary>
        /// Objectデータをbyteの配列へ変換する
        /// </summary>
        /// <typeparam name="T">変換するObjectの型</typeparam>
        /// <param name="src">変換するObject</param>
        /// <param name="dst">byte型の配列</param>
        public static void ObjectToBytes<T>(T src, out byte[] dst)
#if SERIALIZATION_BINARFORMATTER
        {
            UnityChoseKun.Log("Start Serialize");
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            try
            {
                bf.Serialize(ms, src);
                dst = ms.ToArray();
            }
            finally
            {
                ms.Close();
                UnityChoseKun.Log("End Serialize");
            }
        }
#else
        {
            UnityChoseKun.Log("Start Serialize");
            var json = JsonUtility.ToJson(src);
            dst = System.Text.Encoding.ASCII.GetBytes(json);
            UnityChoseKun.Log("End Serialize");
        }
#endif


        public static void ObjectToBytes(object src, out byte[] dst)
#if SERIALIZATION_BINARFORMATTER
        {
            UnityChoseKun.Log("Start Serialize");
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            try
            {
                bf.Serialize(ms, src);
                dst = ms.ToArray();
            }
            finally
            {
                ms.Close();
                UnityChoseKun.Log("End Serialize");
            }
        }
#else
        {
            UnityChoseKun.Log("Start Serialize");
            var json = JsonUtility.ToJson(src);
            dst = System.Text.Encoding.ASCII.GetBytes(json);
            UnityChoseKun.Log("End Serialize");
        }
#endif




        /// <summary>
        /// byte配列からObjectへ変換する
        /// </summary>
        /// <typeparam name="T">変換後のオブジェクトの型</typeparam>
        /// <param name="src">byte配列</param>
        /// <param name="dst">変換されたオブジェクト</param>
        public static void BytesToObject<T>(byte[] src, out T dst)
#if SERIALIZATION_BINARFORMATTER
        {
            UnityChoseKun.Log("Start Deserialize");
            if (src != null)
            {
                var bf = new BinaryFormatter();
                var ms = new MemoryStream(src);
                try
                {
                    dst = (T)bf.Deserialize(ms);
                }
                finally
                {
                    ms.Close();
                    UnityChoseKun.Log("End Deserialize");
                }
            }
            else
            {
                dst = default(T);
            }
        }
#else
        {
            UnityChoseKun.Log("Start Deserialize");
            var json = System.Text.Encoding.ASCII.GetString(src);
            dst = JsonUtility.FromJson<T>(json);
            UnityChoseKun.Log("End Deserialize");
        }
#endif



        /// <summary>
        /// byte配列をオブジェクトに変換する
        /// </summary>
        /// <typeparam name="T">変換後のオブジェクトの型</typeparam>
        /// <param name="src">byte配列</param>
        /// <returns>変換されたオブジェクト</returns>
        public static T GetObject<T>(byte[] src)
#if SERIALIZATION_BINARFORMATTER
        {            
            if (src == null)
            {
                return default(T);
            }
            UnityChoseKun.Log("Start Deserialize");
            var bf = new BinaryFormatter();
            var ms = new MemoryStream(src);
            try
            {

                var t = (T)bf.Deserialize(ms);
                return t;
            }
            finally
            {
                ms.Close();
                UnityChoseKun.Log("End Deserialize");
            }                        
        }
#else
        {
            UnityChoseKun.Log("Start Deserialize");
            var json = System.Text.Encoding.ASCII.GetString(src);
            var t = JsonUtility.FromJson<T>(json);
            UnityChoseKun.Log("End Deserialize");            
            return t;
            
        }
#endif


        public static object GetObject(byte[] src, System.Type type)
#if SERIALIZATION_BINARFORMATTER
        {
            UnityChoseKun.Log("Start Deserialize");
            var bf = new BinaryFormatter();
            var ms = new MemoryStream(src);
            try
            {
                return bf.Deserialize(ms);
            }
            finally
            {
                ms.Close();
                UnityChoseKun.Log("End Deserialize");
            }
        }
#else
        {
            UnityChoseKun.Log("Start Deserialize");
            var json = System.Text.Encoding.ASCII.GetString(src);
            var t = JsonUtility.FromJson(json,type);
            UnityChoseKun.Log("End Deserialize");
            return t;
        }
#endif

        public static void Log(object obj)
        {
#if UNITY_CHOSEKUN_DEBUG
            var time = Time.realtimeSinceStartup;
            Debug.Log(time + "[sec] " + obj);
#endif
        }


        public static void LogError(object obj)
        {
#if UNITY_CHOSEKUN_DEBUG
            Debug.LogError(obj);
#endif
        }


        public static void LogWarning(object obj)
        {
#if UNITY_CHOSEKUN_DEBUG
            Debug.LogWarning(obj);
#endif
        }


        public static void LogAssert(bool condition)
        {
#if UNITY_CHOSEKUN_DEBUG
            Debug.Assert(condition);
#endif
        }
    }

}