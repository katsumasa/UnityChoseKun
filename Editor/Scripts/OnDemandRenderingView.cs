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

        Vector2 scrollPos
        {
            get;
            set;
        }


        static OnDemandRenderingView mInstance;
        public static OnDemandRenderingView instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new OnDemandRenderingView();
                }
                return mInstance;
            }
        }


        // メンバー関数の定義

        /// <summary>
        /// 描画
        /// </summary>
        public void OnGUI()
        {
#if UNITY_2019_3_OR_NEWER
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("effectiveRenderFrameRate", "現在の設定から想定される描画[FPS]"));
            EditorGUILayout.LabelField(onDemandRenderingKun.effectiveRenderFrameRate.ToString() + "[FPS]");
            //EditorGUILayout.Toggle(new GUIContent("willCurrentFrameRender","現在のフレームが描画が発生するフレームであるか"), onDemandRenderingKun.willCurrentFrameRender);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            onDemandRenderingKun.renderFrameInterval = EditorGUILayout.IntSlider(new GUIContent("renderFrameInterval","描画を行うフレーム間隔"), onDemandRenderingKun.renderFrameInterval, 1, 100);
            if (EditorGUI.EndChangeCheck())
            {
                onDemandRenderingKun.isDirty = true;
                UnityChoseKunEditor.SendMessage<OnDemandRenderingKun>(UnityChoseKun.MessageID.OnDemandRenderingPush, onDemandRenderingKun);
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();
            //if (GUILayout.Button("Pull"))
            {
                UnityChoseKunEditor.SendMessage<OnDemandRenderingKun>(UnityChoseKun.MessageID.OnDemandRenderingPull, null);
            }
            //if (GUILayout.Button("Push"))
            {
               // UnityChoseKunEditor.SendMessage<OnDemandRenderingKun>(UnityChoseKun.MessageID.OnDemandRenderingPush, onDemandRenderingKun);
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
