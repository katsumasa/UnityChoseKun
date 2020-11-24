namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.Networking.PlayerConnection;
#if UNITY_2020_1_OR_NEWER
    using UnityEngine.Networking.PlayerConnection;
    using PlayerConnectionUtility = UnityEditor.Networking.PlayerConnection.PlayerConnectionGUIUtility;
    using PlayerConnectionGUILayout = UnityEditor.Networking.PlayerConnection.PlayerConnectionGUILayout;
#else
    using UnityEngine.Experimental.Networking.PlayerConnection;
    using PlayerConnectionUtility = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUIUtility;
    using PlayerConnectionGUILayout = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUILayout;
#endif
    using System.Security.AccessControl;
    using TMPro;
    using System;


    // UnityPlayerViewerKunのEditorEditor側の処理
    // Katsumasa Kimura

    public class PlayerViewKunEditorWindow : EditorWindow
    {
        [System.Serializable]
        public class Style
        {
            public static Texture2D GAME_VIEW_ICON = (Texture2D)EditorGUIUtility.Load("d_UnityEditor.GameView");
            public static Texture2D MERTO_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.Merto");
            public static Texture2D LUMIN_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.Lumin");
            public static Texture2D SWITCH_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.Switch");
            public static Texture2D PS4_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.PS4");
            public static Texture2D WEBGL_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.WebGL");
            public static Texture2D TVOS_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.tvOS");
            public static Texture2D IPHONE_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.iPhone");
            public static Texture2D ANDROID_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.Android");
            public static Texture2D STANDALONE_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.Standalone");
            public static Texture2D XBOXONE_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.XboxOne");
            public static Texture2D STADIA_ICON = (Texture2D)EditorGUIUtility.Load("d_BuildSettings.Stadia");

            public static readonly GUIContent PlayContents = new GUIContent((Texture2D)EditorGUIUtility.Load("d_PlayButton On@2x"),"Play / Stop");
            public static readonly GUIContent RecOnContents = new GUIContent((Texture2D)EditorGUIUtility.Load("d_Record On@2x"),"Stop Record");
            public static readonly GUIContent RecOffContents = new GUIContent((Texture2D)EditorGUIUtility.Load("d_Record Off@2x"),"Start Record");
            public static readonly GUIContent FolderContents = new GUIContent((Texture2D)EditorGUIUtility.Load("d_OpenedFolder Icon"),"Set Save Location");
            public static readonly GUIContent CaptureContents = new GUIContent((Texture2D)EditorGUIUtility.Load("d_SceneViewCamera@2x"),"Capture Screen");
        }


        /// <summary>
        /// 変数の定義 
        /// </summary>

        
        static PlayerViewKunEditorWindow window;        
        IConnectionState attachProfilerState;                
        PlayerView.EditorSendData editorSendData;
        Texture2D playerViewTexture;
        [SerializeField] string recordPath;
        [SerializeField] bool isRecord;
        [SerializeField] int recordMaxFrame;
        [SerializeField] int recordCount;
        [SerializeField] bool isPlay;
        [SerializeField] PlayerViewPlayer.TextureHeader textureHeader;

        /// <summary>
        /// 関数の定義 
        /// </summary>
        [MenuItem("Window/UnityChoseKun/Player View")]
        static void Create()
        {
            if (window == null)
            {
                window = (PlayerViewKunEditorWindow)EditorWindow.GetWindow(typeof(PlayerViewKunEditorWindow));
            }
            
            window.titleContent = new GUIContent(new GUIContent("Player View", Style.GAME_VIEW_ICON));
            window.wantsMouseMove = true;
            window.autoRepaintOnSceneChange = true;
            window.Show();
        }


        //
        // 基底クラスの関数のオーバーライド
        //
        
        private void Awake()
        {            
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Initialize();
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Register(PlayerView.kMsgSendPlayerToEditor, OnMessageEvent);
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Register(PlayerView.kMsgSendPlayerToEditorHeader, OnMessageEventHeader);
            playerViewTexture = new Texture2D(2960, 1140, TextureFormat.RGBA32, false);
            editorSendData.frameCount = 1;
            recordMaxFrame = 200;
             
        }

        //
        private void OnDestroy()
        {            
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Unregister(PlayerView.kMsgSendPlayerToEditor, OnMessageEvent);
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Unregister(PlayerView.kMsgSendPlayerToEditorHeader, OnMessageEventHeader);
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.DisconnectAll();
            if (playerViewTexture == null)
                DestroyImmediate(playerViewTexture);
            window = null;
        }


        //
        private void OnEnable()
        {
            if (window == null)
            {
                window = (PlayerViewKunEditorWindow)EditorWindow.GetWindow(typeof(PlayerViewKunEditorWindow));
            }
            if (attachProfilerState == null)
            {
#if UNITY_2020_1_OR_NEWER
                attachProfilerState = PlayerConnectionUtility.GetConnectionState(this);
#else
                attachProfilerState = PlayerConnectionUtility.GetAttachToPlayerState(this);
#endif
            }
        }


        //
        private void OnDisable()
        {
            attachProfilerState.Dispose();
            attachProfilerState = null;
        }


        //
        private void OnGUI()
        {
            ChangeTitleContent();
            GUILayoutConnect();
            EditorGUILayout.Separator();
            GUILayoutPlayView();
        }



        //=========================================================================================
        // ユニーク関数の定義
        //=========================================================================================

        // ----------------------------------------------------------------------------------------
        // <summary>接続先のデバイスアイコンに切り替える </summary>
        // ----------------------------------------------------------------------------------------
        private void ChangeTitleContent()
        {
            RuntimePlatform platform = RuntimePlatform.WindowsEditor;
            Texture2D texture = null;

            if (EditorWindow.HasOpenInstances<UnityChoseKunEditorWindow>()){
                var chosekunEdotorWindow = (UnityChoseKunEditorWindow)EditorWindow.GetWindow(typeof(UnityChoseKunEditorWindow));
                if (chosekunEdotorWindow != null)
                {
                    platform = chosekunEdotorWindow.GetRuntimePlatform();
                }
            }


            
            switch (platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    {
                        texture = Style.IPHONE_ICON;
                    }
                    break;

                case RuntimePlatform.Android:
                    {
                        texture = Style.ANDROID_ICON;
                    }
                    break;

                case RuntimePlatform.Lumin:
                    {
                        texture = Style.LUMIN_ICON;
                    }
                    break;

                case RuntimePlatform.Switch:
                    {
                        texture = Style.SWITCH_ICON;
                    }
                    break;

                case RuntimePlatform.WSAPlayerARM:
                case RuntimePlatform.WSAPlayerX64:
                case RuntimePlatform.WSAPlayerX86:
                    {
                        texture = Style.MERTO_ICON;
                    }
                    break;

                case RuntimePlatform.PS4:
                    {
                        texture = Style.PS4_ICON;
                    }
                    break;

                case RuntimePlatform.WebGLPlayer:
                    {
                        texture = Style.WEBGL_ICON;
                    }
                    break;

                case RuntimePlatform.tvOS:
                    {
                        texture = Style.TVOS_ICON;
                    }
                    break;
#if UNITY_2019_1_OR_NEWER
                case RuntimePlatform.Stadia:
                    {
                        texture = Style.STADIA_ICON;
                    }
                    break;
#endif
                case RuntimePlatform.XboxOne:
                    {
                        texture = Style.XBOXONE_ICON;
                    }
                    break;

                default:
                    {
                        texture = Style.GAME_VIEW_ICON;
                    }
                    break;
            }
            if(texture == null)
            {
                texture = Style.GAME_VIEW_ICON;
            }
            window.titleContent = new GUIContent(new GUIContent("Player View", texture));
        }


        private void OnMessageEventHeader(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
        {
            var bf = new BinaryFormatter();
            var ms = new MemoryStream(args.data);
            textureHeader = (PlayerViewPlayer.TextureHeader)bf.Deserialize(ms);

        }


        // ----------------------------------------------------------------------------------------
        // <summary> メッセージ受信時のCB </summary>
        // ----------------------------------------------------------------------------------------
        private void OnMessageEvent(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
        {
            if (textureHeader != null)
            {
                if ((playerViewTexture == null) ||
                    (playerViewTexture.width != textureHeader.width) ||
                    (playerViewTexture.height != textureHeader.height) ||
                    (playerViewTexture.format != textureHeader.textureFormat)
                )
                {
                    if (playerViewTexture != null)
                    {
                        DestroyImmediate(playerViewTexture);
                    }
                    playerViewTexture = new Texture2D(
                        textureHeader.width,
                        textureHeader.height,
                        textureHeader.textureFormat,
                        textureHeader.mipChain);
                }

                byte[] raw;
                int slide = textureHeader.width * 4;
                // 画像データが上下反転しているケースがある
                if (textureHeader.flip)
                {
                    raw = new byte[args.data.Length];
                    for (var y = 0; y < textureHeader.height; y++)
                    {
                        var i1 = (textureHeader.height - (y + 1)) * slide;
                        var i2 = y * slide;
                        System.Array.Copy(args.data, i1, raw, i2, slide);
                    }
                }
                else
                {
                    raw = args.data;
                }
                playerViewTexture.LoadRawTextureData(raw);
                playerViewTexture.Apply();
                // EditorWidowを再描画
                if (window != null)
                {
                    window.Repaint();
                }
            }
        }


        // ----------------------------------------------------------------------------------------
        // <summary> UnityPlayerViewerKunPlayerへデータを送信する </summary>
        // ----------------------------------------------------------------------------------------
        private void SendMessage(object obj)
        {
            Debug.Log("SendMessage");
            var json = JsonUtility.ToJson(obj);
            var bytes = System.Text.Encoding.ASCII.GetBytes(json);
            EditorConnection.instance.Send(PlayerView.kMsgSendEditorToPlayer, bytes);
        }


        // ----------------------------------------------------------------------------------------
        // <summary> 接続先選択GUIの描画
        // ----------------------------------------------------------------------------------------
        private void GUILayoutConnect()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Connect To", GUILayout.ExpandWidth(false));
#if UNITY_2020_1_OR_NEWER
            PlayerConnectionGUILayout.ConnectionTargetSelectionDropdown(attachProfilerState, EditorStyles.toolbarDropDown);
#else
            PlayerConnectionGUILayout.AttachToPlayerDropdown(attachProfilerState, EditorStyles.toolbarDropDown);
#endif
            
            // Play
            {
                var contentSize = EditorStyles.label.CalcSize(Style.PlayContents);
                EditorGUI.BeginChangeCheck();
                isPlay = GUILayout.Toggle(isPlay, Style.PlayContents, EditorStyles.toolbarButton, GUILayout.MaxWidth(contentSize.x + 10));
                if (EditorGUI.EndChangeCheck())
                {
                    if (isPlay)
                    {
                        editorSendData.command = PlayerView.Command.Play;                        
                    } 
                    else
                    {
                        editorSendData.command = PlayerView.Command.Stop;
                    }
                    SendMessage(editorSendData);
                }
            }

            
            // Rec
            {
                EditorGUI.BeginDisabledGroup(recordPath == null || recordPath.Length == 0);
                var contentSize = EditorStyles.label.CalcSize(Style.RecOnContents);
                EditorGUI.BeginChangeCheck();
                isRecord = GUILayout.Toggle(isRecord, isRecord ? Style.RecOnContents: Style.RecOffContents, EditorStyles.toolbarButton, GUILayout.MaxWidth(contentSize.x + 10));
                if (EditorGUI.EndChangeCheck())
                {
                    if (isRecord)
                    {
                        recordCount = 0;
                    }
                }
                EditorGUI.EndDisabledGroup();
            }

            // Capture
            {                
                var contentSize = EditorStyles.label.CalcSize(Style.RecOnContents);
                if (GUILayout.Button(Style.CaptureContents, EditorStyles.toolbarButton, GUILayout.MaxWidth(contentSize.x + 10)))
                {
                    System.DateTime dt = System.DateTime.Now;
                    string result = dt.ToString("yyyyMMddHHmmss");
                    var path = EditorUtility.SaveFilePanel(
                        "Save texture as PNG",
                        "",
                        result + ".png",
                        "png");
                    if (path.Length != 0)
                    {
                        var pngData = playerViewTexture.EncodeToPNG();
                        if (pngData != null)
                            System.IO.File.WriteAllBytes(path, pngData);
                    }
                }             
            }

            // Save Folder
            {
                if (GUILayout.Button(Style.FolderContents, EditorStyles.toolbarButton, GUILayout.ExpandWidth(false)))
                {
                    if (isRecord == false)
                    {
                        recordPath = EditorUtility.SaveFolderPanel("Save textures to folder", "", "");
                    }
                }
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
            


            var playerCount = EditorConnection.instance.ConnectedPlayers.Count;
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.AppendLine(string.Format("{0} players connected.", playerCount));
            int i = 0;
            foreach (var p in EditorConnection.instance.ConnectedPlayers)
            {
                builder.AppendLine(string.Format("[{0}] - {1} {2}", i++, p.name, p.playerId));
            }
            EditorGUILayout.HelpBox(builder.ToString(), MessageType.Info);
        }


        // ----------------------------------------------------------------------------------------
        // <summary> PlayView GUIの描画 </summary>
        // ----------------------------------------------------------------------------------------
        private void GUILayoutPlayView()
        {
            if (recordPath == null || recordPath.Length == 0)
            {
                EditorGUILayout.HelpBox("Please Set Record Folder.", MessageType.Info);
            }

#if UNITY_2019_1_OR_NEWER
            EditorGUI.BeginDisabledGroup(isPlay);
            editorSendData.isUseAsyncGPUReadback = EditorGUILayout.ToggleLeft("Enable Async GPU Readback", editorSendData.isUseAsyncGPUReadback);
            EditorGUI.EndDisabledGroup();
#endif
            EditorGUI.BeginDisabledGroup(isPlay);
            editorSendData.frameCount = EditorGUILayout.IntField("Reflesh Interval", editorSendData.frameCount, GUILayout.ExpandWidth(false));
            editorSendData.frameCount = Math.Max(0, editorSendData.frameCount);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.LabelField(new GUIContent("Record Folder","Record and Capture Folder"),new GUIContent(recordPath));                        
            
            // ここでキャプチャ処理
            if (playerViewTexture != null && recordPath != null && recordPath.Length != 0 && recordCount < recordMaxFrame　&& isRecord){
                var pngData = playerViewTexture.EncodeToPNG();
                if (pngData != null){
                    var path = recordPath + "/" + recordCount.ToString("D4") + ".png";
                    System.IO.File.WriteAllBytes(path, pngData);
                    recordCount++;
                }
                if(recordCount >= recordMaxFrame){
                    isRecord = false;
                }
            }

            EditorGUI.BeginDisabledGroup(isRecord);
            var tmp = EditorGUILayout.IntField("Record Count Max", recordMaxFrame, GUILayout.ExpandWidth(false));            
            {
                recordMaxFrame = Math.Max(0, tmp);
                recordMaxFrame = Math.Min(99999, recordMaxFrame);
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(isRecord);
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            var tmpFrame = EditorGUILayout.IntSlider("Record Count", recordCount, 1, recordMaxFrame);
            if (EditorGUI.EndChangeCheck())
            {
                if (isRecord == false && playerViewTexture != null)
                {
                    var fpath = recordPath + "/" + tmpFrame.ToString("D4") + ".png";
                    if (System.IO.File.Exists(fpath))
                    {
                        recordCount = tmpFrame;
                        var bytes = System.IO.File.ReadAllBytes(fpath);
                        playerViewTexture.LoadImage(bytes);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();

            // 描画
            if (playerViewTexture != null)
            {
                var r1 = EditorGUILayout.GetControlRect();               
                var r2 = new Rect(r1.x, r1.y, r1.width, position.height - (r1.y + r1.height) - 30.0f);
                
                EditorGUI.DrawPreviewTexture(
                    r2,
                    playerViewTexture,
                    null,
                    ScaleMode.ScaleToFit
                    );
               // GUILayoutUtility.GetRect(r2.width, r2.height);
            }


            



        }


        // ----------------------------------------------------------------------------------------
        // <summary> Int32をbyte arrayの途中に挿入する </summary>
        // ----------------------------------------------------------------------------------------
        int SetInt32ToBytes(int value, in byte[] dsts, int idx)
        {
            var bytes = System.BitConverter.GetBytes(value);
            for (var i = 0; i < bytes.Length; i++)
            {
                dsts[idx + i] = bytes[i];
            }
            return bytes.Length;
        }


        
    }
}