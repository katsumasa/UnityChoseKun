namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.Networking.PlayerConnection;
    using UnityEngine.Experimental.Networking.PlayerConnection;
    using PlayerConnectionUtility = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUIUtility;
    using PlayerConnectionGUILayout = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUILayout;
    

    // UnityPlayerViewerKunのEditorEditor側の処理
    // Katsumasa Kimura
    public class PlayerViewKunEditorWindow : EditorWindow
    {

        /// <summary>
        /// 変数の定義 
        /// </summary>
        
        static PlayerViewKunEditorWindow window;        
        IConnectionState attachProfilerState;                
        PlayerView.EditorSendData editorSendData;
        Texture2D playerViewTexture;
        

        /// <summary>
        /// 関数の定義 
        /// </summary>
        [MenuItem("Window/UnityChoseKun/PlayerView")]
        static void Create()
        {
            if (window == null)
            {
                window = (PlayerViewKunEditorWindow)EditorWindow.GetWindow(typeof(PlayerViewKunEditorWindow));
            }
            window.titleContent = new GUIContent("Player");
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
            playerViewTexture = new Texture2D(2960, 1140, TextureFormat.RGBA32, false);
            editorSendData.frameCount = 1;
        }

        //
        private void OnDestroy()
        {            
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Unregister(PlayerView.kMsgSendPlayerToEditor, OnMessageEvent);
            UnityEditor.Networking.PlayerConnection.EditorConnection.instance.DisconnectAll();
            if (playerViewTexture == null)
                DestroyImmediate(playerViewTexture);
            window = null;
        }


        //
        private void OnEnable()
        {
            if (attachProfilerState == null)
            {
                attachProfilerState = PlayerConnectionUtility.GetAttachToPlayerState(this);
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
            GUILayoutConnect();
            EditorGUILayout.Separator();
            GUILayoutPlayView();
        }



        //=========================================================================================
        // ユニーク関数の定義
        //=========================================================================================
        


        // ----------------------------------------------------------------------------------------
        // <summary> メッセージ受信時のCB </summary>
        // ----------------------------------------------------------------------------------------
        private void OnMessageEvent(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
        {
            //Debug.Log("OnMessageEvent");           
            var width           = System.BitConverter.ToInt32(args.data, 0);
            var height          = System.BitConverter.ToInt32(args.data, 4);
            var textureFormat   = (TextureFormat)System.BitConverter.ToInt32(args.data, 8);
            var mipChain        = System.BitConverter.ToBoolean(args.data, 12);
            var flip            = System.BitConverter.ToBoolean(args.data, 16);


            // 設定の変更があった場合、Texture2Dを作り直す必要がある
            if ((playerViewTexture == null) ||
                (playerViewTexture.width != width) ||
                (playerViewTexture.height != height) ||
                (playerViewTexture.format != textureFormat)
                )
            {
                if(playerViewTexture != null)
                {
                    DestroyImmediate(playerViewTexture);
                }
                playerViewTexture = new Texture2D(width, height, textureFormat, mipChain);
            }



            // 画像を整形する            
            var raw = new byte[args.data.Length - 20];
            // TextureFormatに1Pixel辺りのバイト数は記載が無いのか・・・
            int slide = width * 4;
            // 画像データが上下反転しているケースがある
            if (flip)
            {
                for (var y = 0; y < height; y++)
                {
                    var i1 = (height - (y + 1)) * slide + 20;
                    var i2 = y * slide;
                    System.Array.Copy(args.data, i1, raw, i2, slide);
                }
            }
            else
            {
                for (var y = 0; y < height; y++)
                {
                    var i1 = y * slide + 20;
                    var i2 = y * slide;
                    System.Array.Copy(args.data, i1, raw, i2, slide);

                }
            }
            playerViewTexture.LoadRawTextureData(raw);
            playerViewTexture.Apply();
            // EditorWidowを再描画
            window.Repaint();
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
            EditorGUILayout.LabelField("Connect To");                        
            PlayerConnectionGUILayout.AttachToPlayerDropdown(attachProfilerState, EditorStyles.toolbarDropDown);            
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
            EditorGUILayout.BeginHorizontal();
            editorSendData.isUseAsyncGPUReadback = EditorGUILayout.Toggle("Enable Async GPU Readback", editorSendData.isUseAsyncGPUReadback);
            editorSendData.frameCount = EditorGUILayout.IntField("Skip frame ", editorSendData.frameCount);
            EditorGUILayout.EndHorizontal();

            

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Play"))
            {             
                editorSendData.command = PlayerView.Command.Play;
                SendMessage(editorSendData);
            }

            if (GUILayout.Button("Stop"))
            {
                editorSendData.command = PlayerView.Command.Stop;
                SendMessage(editorSendData);
            }
            EditorGUILayout.EndHorizontal();


            if (playerViewTexture != null)
            {
                var r1 = EditorGUILayout.GetControlRect();
                var r2 = new Rect(r1.x, r1.y, r1.width, position.height - (r1.y + r1.height));
                EditorGUI.DrawPreviewTexture(
                    r2,
                    playerViewTexture,
                    null,
                    ScaleMode.ScaleToFit
                    );
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