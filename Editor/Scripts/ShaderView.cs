using System.IO;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {
        /// <summary>
        /// 
        /// </summary>
        public class ShadersView
        {
            // Member変数の定義
            [SerializeField] static ShaderKun[] m_shaderKuns;
            [SerializeField] static string[] m_shaderNames;
            [SerializeField] Vector2 m_scrollPos;


            public static ShaderKun[] shaderKuns
            {
                get { return m_shaderKuns; }
                private set { m_shaderKuns = value; }
            }

            public static string[] shaderNames
            {
                get { return m_shaderNames; }
                private set { m_shaderNames = value; }
            }

            Vector2 scrollPos
            {
                get { return m_scrollPos; }
                set { m_scrollPos = value; }
            }


            /// <summary>
            /// 
            /// </summary>
            public void OnGUI()
            {
                int cnt = 0;
                if (shaderKuns != null)
                {
                    cnt = shaderKuns.Length;
                    EditorGUILayout.LabelField("Shader List(" + cnt + ")");
                }
                else
                {
                    EditorGUILayout.HelpBox("You can display a list of Shaders. Please Push Pull Button.", MessageType.Info);
                }

                using (new EditorGUI.IndentLevelScope())
                {
                    if (shaderKuns != null)
                    {
                        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                        for (var i = 0; i < shaderKuns.Length; i++)
                        {
                            EditorGUILayout.LabelField(shaderKuns[i].name);
                        }
                        EditorGUILayout.EndScrollView();
                    }
                }
                if (GUILayout.Button("Pull"))
                {
                    UnityChoseKunEditor.SendMessage<ShaderKunPacket>(UnityChoseKun.MessageID.ShaderPull, null);
                }
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="binaryReader"></param>
            public void OnMessageEvent(BinaryReader binaryReader)
            {
                var shaderKunPacket = new ShaderKunPacket();
                shaderKunPacket.Deserialize(binaryReader);

                shaderKuns = shaderKunPacket.shaderKuns;
                shaderNames = new string[shaderKuns.Length];
                for (var i = 0; i < shaderKuns.Length; i++)
                {
                    shaderNames[i] = shaderKuns[i].name;
                }
            }

        }
    }
}