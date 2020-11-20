using System.IO;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class OnDemandRenderingView
    {
        // メンバー変数の定義

        [SerializeField] OnDemandRenderingKun mOnDemandRenderingKun;


        // プロパティの定義

        OnDemandRenderingKun onDemandRenderingKun
        {
            get {
                if(mOnDemandRenderingKun == null)
                {
                    mOnDemandRenderingKun = new OnDemandRenderingKun();
                }
                return mOnDemandRenderingKun;
            }

            set
            {
                mOnDemandRenderingKun = value;
            }
        }


        // メンバー関数の定義

        /// <summary>
        /// 描画
        /// </summary>
        public void OnGUI()
        {
#if UNITY_2019_3_OR_NEWER
            EditorGUILayout.IntField("effectiveRenderFrameRate",onDemandRenderingKun.effectiveRenderFrameRate);
            EditorGUI.BeginChangeCheck();
            onDemandRenderingKun.renderFrameInterval = EditorGUILayout.IntSlider("renderFrameInterval", onDemandRenderingKun.renderFrameInterval, 0, 100);
            if (EditorGUI.EndChangeCheck())
            {
                onDemandRenderingKun.isDirty = true;
            }
            EditorGUILayout.Toggle("willCurrentFrameRender", onDemandRenderingKun.willCurrentFrameRender);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Pull"))
            {
                UnityChoseKunEditor.SendMessage<OnDemandRenderingKun>(UnityChoseKun.MessageID.OnDemandRenderingPull, null);
            }
            if (GUILayout.Button("Push"))
            {
                UnityChoseKunEditor.SendMessage<OnDemandRenderingKun>(UnityChoseKun.MessageID.OnDemandRenderingPush, onDemandRenderingKun);
            }
            EditorGUILayout.EndHorizontal();
#else
            EditorGUILayout.LabelField("Not Support OnDemandRendering");
#endif
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEvent(BinaryReader binaryReader)
        {
            onDemandRenderingKun.Deserialize(binaryReader);
        }
    }
}
