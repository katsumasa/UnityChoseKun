using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class ScalableBufferManagerView
    {
        [SerializeField] ScalableBufferManagerKun m_scalableBufferManagerKun;
        bool isPull = false;

        ScalableBufferManagerKun scalableBufferManagerKun
        {
            get
            {
                if (m_scalableBufferManagerKun == null)
                {
                    m_scalableBufferManagerKun = new ScalableBufferManagerKun();
                }
                return m_scalableBufferManagerKun;
            }

            set
            {
                m_scalableBufferManagerKun = value;
            }
        }


        Vector2 scrollPos
        {
            get;
            set;
        }


        public void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            if (isPull == false)
            {
                EditorGUILayout.HelpBox("Please Pull Request.", MessageType.Info);
            }
            
            EditorGUI.BeginChangeCheck();
            scalableBufferManagerKun.widthScaleFactor = EditorGUILayout.Slider("widthScale", scalableBufferManagerKun.widthScaleFactor, 0.01f, 10.0f);
            scalableBufferManagerKun.heightScaleFactor = EditorGUILayout.Slider("heightScale", scalableBufferManagerKun.heightScaleFactor, 0.01f, 10.0f);
            if (EditorGUI.EndChangeCheck())
            {
                if (isPull)
                {
                    scalableBufferManagerKun.isDirty = true;
                    UnityChoseKunEditor.SendMessage<ScalableBufferManagerKun>(UnityChoseKun.MessageID.ScalableBufferManagerPush, scalableBufferManagerKun);
                    scalableBufferManagerKun.isDirty = false;
                }
            }
            EditorGUILayout.EndScrollView();


            if (GUILayout.Button("Pull"))
            {
                UnityChoseKunEditor.SendMessage<ScalableBufferManagerKun>(UnityChoseKun.MessageID.ScalableBufferManagerPull, null);
            }

            
        }


        public void OnMessageEvent(BinaryReader binaryReader)
        {
            scalableBufferManagerKun.Deserialize(binaryReader);
            isPull = true;
        }
    }
}