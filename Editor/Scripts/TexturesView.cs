using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {

        [System.Serializable]
        public class TextureView
        {
            public static readonly Texture2D TextureIcon = (Texture2D)EditorGUIUtility.Load("d_Texture Icon");

            [SerializeField] TextureKun m_textureKun;
            [SerializeField] bool m_textureFoldout = false;


            TextureKun textureKun
            {
                get { return m_textureKun; }
                set { m_textureKun = value; }
            }

            bool textureFoldout
            {
                get { return m_textureFoldout; }
                set { m_textureFoldout = value; }
            }



            /// <summary>
            /// 
            /// </summary>
            /// <param name="textureKun"></param>
            public TextureView(TextureKun textureKun)
            {
                this.textureKun = textureKun;
            }


            /// <summary>
            /// 
            /// </summary>
            void DrawTexture()
            {
                EditorGUI.BeginChangeCheck();

                EditorGUILayout.EnumPopup("Texture Shape", textureKun.dimension);
                EditorGUILayout.LabelField("Advanced");
                using (new EditorGUI.IndentLevelScope())
                {
                    textureKun.isReadable = EditorGUILayout.Toggle("Read/Write Enable", textureKun.isReadable);
                    EditorGUILayout.LabelField("Mip");
                    using (new EditorGUI.IndentLevelScope())
                    {
                        EditorGUILayout.FloatField("Mip Bias", textureKun.mipMapBias);
#if UNITY_2019_1_OR_NEWER
                        EditorGUILayout.IntField("Mip Level", textureKun.mipmapCount);
#endif
                    }
                }
                textureKun.wrapMode = (TextureWrapMode)EditorGUILayout.EnumPopup("WrapMode Mode", textureKun.wrapMode);
                textureKun.filterMode = (FilterMode)EditorGUILayout.EnumPopup("Filter Mode", textureKun.filterMode);
                textureKun.anisoLevel = EditorGUILayout.IntSlider("Aniso Level", textureKun.anisoLevel, 1, 9);

                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                EditorGUILayout.IntField("Width", textureKun.width);
                EditorGUILayout.IntField("Height", textureKun.height);
#if UNITY_2019_1_OR_NEWER
                textureKun.graphicsFormat = (UnityEngine.Experimental.Rendering.GraphicsFormat)EditorGUILayout.EnumPopup("Format", textureKun.graphicsFormat);
#endif
                EditorGUILayout.IntField("Update Count", (int)textureKun.updateCount);
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
                if (EditorGUI.EndChangeCheck())
                {
                    textureKun.dirty = true;
                }
            }

            public void OnGUI()
            {
                var content = new GUIContent(TextureIcon);
                if (string.IsNullOrEmpty(textureKun.name))
                {
                    content.text = "UnKnown";
                }
                else
                {
                    content.text = textureKun.name;
                }
                textureFoldout = EditorGUILayout.Foldout(textureFoldout, content);
                if (textureFoldout)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        DrawTexture();
                    }
                }
            }
        }




        [System.Serializable]
        public class TexturesView
        {
            static TextureKun[] m_textureKuns;
            static string[] m_textureNames;

            TextureView[] m_textureViews;
            Vector2 m_scrollPos;


            static TexturesView mInstance;
            public static TexturesView instance
            {
                get
                {
                    if(mInstance == null)
                    {
                        mInstance = new TexturesView();
                    }
                    return mInstance;
                }
            }


            public static TextureKun[] textureKuns
            {
                get { return m_textureKuns; }
                set { m_textureKuns = value; }
            }


            public static string[] textureNames
            {
                get { return m_textureNames; }
                private set { m_textureNames = value; }
            }


            TextureView[] textureViews
            {
                get { return m_textureViews; }
                set { m_textureViews = value; }
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
                if (textureViews != null)
                {
                    cnt = textureViews.Length;
                    EditorGUILayout.LabelField("Texture List(" + cnt + ")");
                }
                else
                {
                    EditorGUILayout.HelpBox("You can display list of Texture.Please Push Pull Button.", MessageType.Info);
                }

                if (cnt != 0)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);                        
                        for (var i = 1; i < cnt; i++)
                        {
                            textureViews[i].OnGUI();
                        }
                        EditorGUILayout.EndScrollView();
                    }
                }

                if (GUILayout.Button("Pull"))
                {
                    var packet = new TexturePlayer.TextureKunPacket();
                    packet.isResources = true;
                    packet.isScene = true;
                    UnityChoseKunEditor.SendMessage<TexturePlayer.TextureKunPacket>(UnityChoseKun.MessageID.TexturePull, packet);
                }

            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="binaryReader"></param>
            public void OnMessageEvent(BinaryReader binaryReader)
            {
                var textureKunPacket = new TexturePlayer.TextureKunPacket();
                textureKunPacket.Deserialize(binaryReader);

                m_textureKuns = new TextureKun[textureKunPacket.textureKuns.Length + 1];
                m_textureKuns[0] = new TextureKun();
                m_textureKuns[0].name = "None(Texture)";
                for (var i = 1; i < m_textureKuns.Length; i++)
                {
                    m_textureKuns[i] = textureKunPacket.textureKuns[i - 1];
                }

                m_textureViews = new TextureView[m_textureKuns.Length];
                m_textureNames = new string[m_textureKuns.Length];                
                for (var i = 0; i < m_textureKuns.Length; i++)
                {                    
                    m_textureViews[i] = new TextureView(m_textureKuns[i]);
                    m_textureNames[i] = m_textureKuns[i].name;
                }
            }
        }
    }
}