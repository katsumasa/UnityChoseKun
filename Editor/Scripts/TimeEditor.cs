namespace Utj.UnityChoseKun
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using System.Linq;

    public class TimeEditor : UnityChoseKunEditor
    {
        TimeKun m_timeKun;
        TimeKun timeKun {
            get {
                if(m_timeKun == null){
                    m_timeKun = new TimeKun();
                }
                return m_timeKun;                
            }
            set {m_timeKun = value;}
        }


        public void OnGUI()
        {
            EditorGUILayout.LabelField("Time");
            using (new EditorGUI.IndentLevelScope()){
                EditorGUILayout.FloatField("deltaTime", timeKun.deltaTime);
                EditorGUILayout.FloatField("fixedUnscaledDeltaTime", timeKun.fixedUnscaledDeltaTime);
                EditorGUILayout.FloatField("fixedUnscaledTime", timeKun.fixedUnscaledTime);
                EditorGUILayout.IntField("frameCount", timeKun.frameCount);
                EditorGUILayout.FloatField("frameCount", timeKun.frameCount);
                EditorGUILayout.FloatField("realtimeSinceStartup", timeKun.realtimeSinceStartup);
                EditorGUILayout.FloatField("smoothDeltaTime", timeKun.smoothDeltaTime);
                EditorGUILayout.FloatField("time", timeKun.time);
                EditorGUILayout.FloatField("timeSinceLevelLoad", timeKun.timeSinceLevelLoad);
                EditorGUILayout.FloatField("unscaledDeltaTime", timeKun.unscaledDeltaTime);
                EditorGUILayout.FloatField("unscaledTime", timeKun.unscaledTime);
                EditorGUILayout.Toggle("inFixedTimeStep", timeKun.inFixedTimeStep);

                EditorGUILayout.Space();

                timeKun.captureFramerate = EditorGUILayout.IntField("captureFramerate", timeKun.captureFramerate);
                timeKun.fixedDeltaTime = EditorGUILayout.FloatField("fixedDeltaTime", timeKun.fixedDeltaTime);
                timeKun.maximumDeltaTime = EditorGUILayout.FloatField("maximumDeltaTime", timeKun.maximumDeltaTime);
                timeKun.timeScale = EditorGUILayout.FloatField("timeScale", timeKun.timeScale);

                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
            }
            if (GUILayout.Button("Pull"))
            {                
                SendMessage<TimeKun>(UnityChoseKun.MessageID.TimePull,timeKun);
            }
            if (GUILayout.Button("Push"))
            {                
                SendMessage<TimeKun>(UnityChoseKun.MessageID.TimePush,timeKun);
            }
            EditorGUILayout.EndHorizontal();
        }


        public void OnMessageEvent(string json)
        {
            timeKun = JsonUtility.FromJson<TimeKun>(json);
        }


    }
}
