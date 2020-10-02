namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Networking.PlayerConnection;
    using UnityEngine.Rendering;
    using Unity.Collections;
    using System;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;

    public class PlayerViewPlayer : MonoBehaviour
    {

        [Serializable]
        public class TextureHeader
        {
            [SerializeField] public Int32 width;
            [SerializeField] public Int32 height;
            [SerializeField] public TextureFormat textureFormat;
            [SerializeField] public bool mipChain;
            [SerializeField] public bool flip;

            public TextureHeader()
            {
            }

            public TextureHeader(Int32 width,Int32 height,TextureFormat textureFormat,bool mipChain,bool flip)
            {
                this.width = width;
                this.height = height;
                this.textureFormat = textureFormat;
                this.mipChain = mipChain;
                this.flip = flip;
            }
        }


        enum CaptureMode
        {
            Normal,
            AsyncGPUReadBack,
        }


        CaptureMode captureMode;
        int frameCount;
        int frameCountMax;
        RenderTexture renderTexture;
        TextureHeader textureHeader;
        BinaryFormatter binaryFormatter;
        MemoryStream memoryStream;

        bool isPlay;
        bool isPause;
        bool isFinish;
        bool isAsyncGPUReadBackFinish;
        PlayerView.EditorSendData editorSendData;



        /// <summary>
        ///
        /// </summary>

        // Start is called before the first frame update
        void Start()
        {
            isPause = false;
            isPlay = false;
            isFinish = false;
            isAsyncGPUReadBackFinish = false;            
            renderTexture = null;
            textureHeader = null;
            StartCoroutine(VideoRecoder());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
            PlayerConnection.instance.Register(PlayerView.kMsgSendEditorToPlayer, OnMessageEvent);
        }


        //
        private void OnDisable()
        {
            Debug.Log("OnDisable");
            PlayerConnection.instance.Unregister(PlayerView.kMsgSendEditorToPlayer, OnMessageEvent);
        }


        private void OnDestroy()
        {
            #if UNITY_2019_1_OR_NEWER
            // 処理が残っているAsyncGPUReadbackが全て完了する迄待つ
            AsyncGPUReadback.WaitAllRequests();
            #endif

            if (renderTexture != null)
            {
                Destroy(renderTexture);
                renderTexture = null;
            }
            
        }

        private void OnMessageEvent(MessageEventArgs args)
        {
            Debug.Log("OnMessageEvent");
            var json = System.Text.Encoding.ASCII.GetString(args.data);
            editorSendData = JsonUtility.FromJson<PlayerView.EditorSendData>(json);
            switch (editorSendData.command)
            {
                case PlayerView.Command.Play:
                    {
                        frameCountMax = editorSendData.frameCount;
                        if (editorSendData.isUseAsyncGPUReadback && SystemInfo.supportsAsyncGPUReadback)
                        {
                            captureMode = CaptureMode.AsyncGPUReadBack;
                        } else
                        {
                            captureMode = CaptureMode.Normal;
                        }
                        frameCount = 0;
                        isPlay = true;                        
                        isAsyncGPUReadBackFinish = true;
                        textureHeader = null;
                    }
                    break;

                case PlayerView.Command.Pause:
                    {
                        isPause = !isPause;
                    }
                    break;

                case PlayerView.Command.Stop:
                    {
                        isPlay = false;
                    }
                    break;
            }
        }


        private IEnumerator VideoRecoder()
        {
            while (isFinish == false)
            {
                yield return new WaitForEndOfFrame();
                if(isPlay == false)
                {
                    continue;
                }
                if (isPause == false)
                {
                    frameCount--;
                }
                if(frameCount >= 0  || isAsyncGPUReadBackFinish == false)
                {
                    continue;
                }
                if(isFinish == true)
                {
                    break;
                }
                switch (captureMode)
                {
                    #if UNITY_2019_1_OR_NEWER
                    case CaptureMode.AsyncGPUReadBack:
                        {                        
                            if (
                                (textureHeader == null) ||
                                (renderTexture == null) ||
                                (renderTexture.width != Screen.currentResolution.width) ||
                                (renderTexture.height != Screen.currentResolution.height)
                                )
                            {                                
                                if(renderTexture != null)
                                {
                                    renderTexture.Release();
                                    Destroy(renderTexture);
                                }                                                               
                                renderTexture = new RenderTexture(
                                    Screen.currentResolution.width,
                                    Screen.currentResolution.height,
                                    0
                                    );
                                var header = new TextureHeader(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false, true);
                                var bf = new BinaryFormatter();
                                var ms = new MemoryStream();
                                try
                                {
                                    bf.Serialize(ms, header);
                                    PlayerConnection.instance.Send(PlayerView.kMsgSendPlayerToEditorHeader, ms.ToArray());
                                }
                                finally
                                {
                                    ms.Close();
                                }
                                textureHeader = header;
                            }                            
                            frameCount = frameCountMax;
                            isAsyncGPUReadBackFinish = false;
                            
                            ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);
                            AsyncGPUReadback.Request(renderTexture, 0, TextureFormat.RGBA32, ReadbackCompleted);
                        }
                        break;
                    case CaptureMode.Normal:
                    #else
                    default:
                    #endif
                        {
                            var texture2D = ScreenCapture.CaptureScreenshotAsTexture();
                            var header = new TextureHeader(texture2D.width, texture2D.height, TextureFormat.RGBA32,false,false);
                            if(textureHeader == null || textureHeader != header)
                            {
                                var bf = new BinaryFormatter();
                                var ms = new MemoryStream();
                                try
                                {
                                    bf.Serialize(ms, header);                                    
                                    PlayerConnection.instance.Send(PlayerView.kMsgSendPlayerToEditorHeader, ms.ToArray());
                                }
                                finally
                                {
                                    ms.Close();
                                }
                                textureHeader = header;
                            }
                            PlayerConnection.instance.Send(
                                PlayerView.kMsgSendPlayerToEditor,
                                texture2D.GetRawTextureData()
                            );
                            Destroy(texture2D);
                            frameCount = frameCountMax;
                        }
                        break;
                }
            }        
        }


        // AsyncGPUReadback.Requestの完了CB
        void ReadbackCompleted(AsyncGPUReadbackRequest request)
        {
            if (request.hasError == true)
            {                
                Debug.Log("AsyncGPUReadbackRequest has error.");
            }
            else
            {        
                PlayerConnection.instance.Send(
                    PlayerView.kMsgSendPlayerToEditor,
                    request.GetData<byte>().ToArray()
                );
            }
            isAsyncGPUReadBackFinish = true;
        }



        int SetInt32ToBytes(int value,in byte[] dsts,int idx)
        {
            var bytes = System.BitConverter.GetBytes(value);
            for(var i = 0; i < bytes.Length; i++)
            {
                dsts[idx + i] = bytes[i];
            }
            return bytes.Length;
        }

    }
}