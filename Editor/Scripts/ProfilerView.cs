using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;
    using System.IO;
    using System.Linq;
    using UnityEngine.Profiling;

    namespace Editor
    {

        public class ProfilerView
        {
            static ProfilerView m_instance;

            long m_totalRuntimeMemorySizeLong;
            Dictionary<string, long> m_runtimeMemorys;
            bool m_foldout;

            public static ProfilerView instance
            {
                get
                {
                    if(m_instance == null)
                    {
                        m_instance = new ProfilerView();
                    }
                    return m_instance;
                }
            }

            public ProfilerView()
            {
                m_runtimeMemorys = new Dictionary<string, long>();                
            }


            public void OnGUI()
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Allocated Mono heap size: ");
                EditorGUILayout.LabelField(Byte2String(ProfilerKun.instance.monoHeapSizeLong));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Mono used size: ");
                EditorGUILayout.LabelField(Byte2String(ProfilerKun.instance.monoUsedSizeLong));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Total Reserved memory by Unity: ");
                EditorGUILayout.LabelField(Byte2String(ProfilerKun.instance.totalReservedMemoryLong));
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("- Allocated memory by Unity: ");
                EditorGUILayout.LabelField(Byte2String(ProfilerKun.instance.totalAllocatedMemoryLong));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("- Reserved but not allocated: ");
                EditorGUILayout.LabelField(Byte2String(ProfilerKun.instance.totalUnusedReservedMemoryLong));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Total Number of Object: ");
                EditorGUILayout.LabelField(ProfilerKun.instance.objectRuntimeMemorys.Length.ToString("N0"));
                EditorGUILayout.EndHorizontal();
              

                m_foldout = EditorGUILayout.Foldout(m_foldout, "Total Object RuntimeMemorySize: "+ Byte2String(m_totalRuntimeMemorySizeLong));
                if (m_foldout)
                {
                    EditorGUI.indentLevel++;
                    foreach (var r in m_runtimeMemorys)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("- " + r.Key + ": ");
                        EditorGUILayout.LabelField(Byte2String(r.Value));
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Pull"))
                {
                    UnityChoseKunEditor.SendMessage<ProfilerKun>(UnityChoseKun.MessageID.ProfilerPlayerPull, null);
                }
                if (GUILayout.Button("Export Objects RuntimeMemory"))
                {
                    var path = EditorUtility.SaveFilePanel("Export Object RuntimeMemory as csv", "", "runtimememory.csv", "csv");
                    if (!string.IsNullOrEmpty(path))
                    {
                        var sw = new System.IO.StreamWriter(path, false);
                        for (var i = 0; i < ProfilerKun.instance.objectRuntimeMemorys.LongLength; i++)
                        {
                            var o = ProfilerKun.instance.objectRuntimeMemorys[i];  
                            
                            sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"HideFlags.{2}\",{3}", o.name,o.className,o.hideFlags,o.runtimeMemorySizeLong));
                        }
                        sw.Close();
                    }
                }
                EditorGUILayout.EndHorizontal();
            }


            public void OnMessageEvent(BinaryReader binaryReader)
            {
                ProfilerKun.instance.Deserialize(binaryReader);
                m_totalRuntimeMemorySizeLong = 0;
                m_runtimeMemorys.Clear();


                for (var i = 0; i < ProfilerKun.instance.objectRuntimeMemorys.LongLength; i++)
                {
                    var o = ProfilerKun.instance.objectRuntimeMemorys[i];

                    m_totalRuntimeMemorySizeLong += o.runtimeMemorySizeLong;

                    if (!m_runtimeMemorys.ContainsKey(o.className))
                    {
                        m_runtimeMemorys.Add(o.className, o.runtimeMemorySizeLong);
                    }
                    else
                    {
                        m_runtimeMemorys[o.className] += o.runtimeMemorySizeLong;
                    }
                }
                m_runtimeMemorys = m_runtimeMemorys.OrderByDescending(pair => pair.Value).ToDictionary(pair =>pair.Key, pair => pair.Value);

            }


            string Byte2String(long value)
            {
                if (value >= 1024 * 1024 * 1024)
                {
                    float f = (float)value / (1024f * 1024f * 1024f);
                    return f.ToString("F") + " GB";
                }
                else if (value >= 1024 * 1024)
                {
                    float f = (float)value / (1024f * 1024f);
                    return f.ToString("F") + " MB";
                }
                else if (value >= 1024)
                {
                    float f = (float)value / (1024f);
                    return f.ToString("F") + " KB";
                }
                return value.ToString() + " B";
            }
        }


        
    }
}