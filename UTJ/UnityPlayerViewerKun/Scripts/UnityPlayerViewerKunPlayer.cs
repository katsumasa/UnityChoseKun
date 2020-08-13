namespace Utj.UnityPlayerViewerKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Networking.PlayerConnection;
    using UnityEngine.Rendering;
    using Unity.Collections;

    
    public class UnityPlayerViewerKunPlayer : MonoBehaviour
    {

        enum CaptureMode
        {
            Normal,
            AsyncGPUReadBack,
        }


        CaptureMode captureMode;
        int frameCount;
        int frameCountMax;
        RenderTexture renderTexture;
        
        

        bool isPlay;
        bool isPause;
        bool isFinish;
        bool isAsyncGPUReadBackFinish;
        
       
        UnityPlayerViewerKun.EditorSendData editorSendData;



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
            StartCoroutine(VideoRecoder());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
            PlayerConnection.instance.Register(UnityPlayerViewerKun.kMsgSendEditorToPlayer, OnMessageEvent);
        }


        //
        private void OnDisable()
        {
            Debug.Log("OnDisable");
            PlayerConnection.instance.Unregister(UnityPlayerViewerKun.kMsgSendEditorToPlayer, OnMessageEvent);
        }


        private void OnDestroy()
        {
            // 処理が残っているAsyncGPUReadbackがs全て完了する迄待つ
            AsyncGPUReadback.WaitAllRequests();

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
            editorSendData = JsonUtility.FromJson<UnityPlayerViewerKun.EditorSendData>(json);
            switch (editorSendData.command)
            {
                case UnityPlayerViewerKun.Command.Play:
                    {
                        frameCountMax = editorSendData.frameCount;
                        frameCount = editorSendData.frameCount;
                        
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
                    }
                    break;

                case UnityPlayerViewerKun.Command.Pause:
                    {
                        isPause = !isPause;
                    }
                    break;

                case UnityPlayerViewerKun.Command.Stop:
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
                    case CaptureMode.AsyncGPUReadBack:
                        {                        
                            if (
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
                            }                            
                            frameCount = frameCountMax;
                            isAsyncGPUReadBackFinish = false;
                            
                            ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);
                            AsyncGPUReadback.Request(renderTexture, 0, TextureFormat.RGBA32, ReadbackCompleted);
                        }
                        break;
                    case CaptureMode.Normal:
                        {
                            var texture2D = ScreenCapture.CaptureScreenshotAsTexture();
                            var len = texture2D.GetRawTextureData().Length + 20;
                            var bytes = new byte[len];
                            var idx = 0;
                            idx += SetInt32ToBytes(texture2D.width, bytes, idx);
                            idx += SetInt32ToBytes(texture2D.height, bytes, idx);
                            idx += SetInt32ToBytes((int)TextureFormat.RGBA32, bytes, idx);
                            idx += SetInt32ToBytes(0, bytes, idx);
                            idx += SetInt32ToBytes(0, bytes, idx);
                            System.Array.Copy(texture2D.GetRawTextureData(), 0, bytes, idx, texture2D.GetRawTextureData().Length);                                                     
                            PlayerConnection.instance.Send(
                                UnityPlayerViewerKun.kMsgSendPlayerToEditor,
                                bytes
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
                int len = request.GetData<byte>().Length + 20;
                var bytes = new byte[len];
                var idx = 0;                
                idx +=SetInt32ToBytes(request.width, bytes, idx);
                idx += SetInt32ToBytes(request.height, bytes, idx);
                idx += SetInt32ToBytes((int)TextureFormat.RGBA32, bytes, idx);
                idx += SetInt32ToBytes(0, bytes, idx);
                idx += SetInt32ToBytes(1, bytes, idx);
                NativeArray<byte>.Copy(request.GetData<byte>(), 0,bytes, idx, request.GetData<byte>().Length);
                PlayerConnection.instance.Send(
                    UnityPlayerViewerKun.kMsgSendPlayerToEditor,
                    bytes
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