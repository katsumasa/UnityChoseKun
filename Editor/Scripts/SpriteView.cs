using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class SpriteView
    {
        [SerializeField] SpriteKun m_spriteKun;
        [SerializeField] bool m_spriteFoldout = false;

        public SpriteView(SpriteKun spriteKun)
        {
            m_spriteKun = spriteKun;
        }


        public void OnGUI()
        {
            if (string.IsNullOrEmpty(m_spriteKun.name))
            {
                m_spriteFoldout = EditorGUILayout.Foldout(m_spriteFoldout,"UnKnown");
            } 
            else
            {
                m_spriteFoldout = EditorGUILayout.Foldout(m_spriteFoldout,m_spriteKun.name);
            }
            if (m_spriteFoldout)
            {
                using(new EditorGUI.IndentLevelScope())
                {
                    if (m_spriteKun.pivot != null)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Pivot");
                        var sb = new StringBuilder();
                        sb.AppendFormat("X:{0},Y:{1}", m_spriteKun.pivot.x, m_spriteKun.pivot.y);
                        var st = sb.ToString();
                        EditorGUILayout.LabelField(st);
                        EditorGUILayout.EndHorizontal();
                    }

                    if (m_spriteKun.border != null)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Border");
                        var sb = new StringBuilder();
                        sb.AppendFormat("L:{0},B:{1},R{2},T:{3}", m_spriteKun.border.x, m_spriteKun.border.y, m_spriteKun.border.z, m_spriteKun.border.w);
                        var st = sb.ToString();
                        EditorGUILayout.LabelField(st);
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }
    }

    public class SpritesView
    {
        [SerializeField] static SpriteKun[] m_spriteKuns;
        [SerializeField] SpriteView[] m_spriteViews;
        [SerializeField] Vector2 m_scrollPos;
        static string[] m_spriteNames;

        public static SpriteKun[] spriteKuns
        {
            get
            {
                return m_spriteKuns;
            }
        }

        public static string[] spriteNames
        {
            get
            {
                return m_spriteNames;
            }
        }


        static SpritesView mInstance;
        public static SpritesView instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new SpritesView();
                }
                return mInstance;
            }
        }

        public void OnGUI()
        {
            int cnt = 0;
            if(m_spriteViews != null)
            {
                cnt = m_spriteViews.Length;
                EditorGUILayout.LabelField("Sprite List("+cnt+")");
            } 
            else
            {
                EditorGUILayout.HelpBox("Please Push Pull Button.", MessageType.Info);
            }
            
            if(cnt != 0)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos);
                    for(var i = 0; i < cnt; i++)
                    {
                        m_spriteViews[i].OnGUI();
                    }
                    EditorGUILayout.EndScrollView();
                }
            }

            if(GUILayout.Button("Pull"))
            {
                var packet = new AssetPacket<SpriteKun>();
                packet.isResources = true;
                packet.isScene = true;
                UnityChoseKunEditor.SendMessage<AssetPacket<SpriteKun>>(UnityChoseKun.MessageID.SpritePull,packet);
            }
        }

        public void OnMessageEvent(BinaryReader binaryReader)
        {
            var packet = new AssetPacket<SpriteKun>();
            packet.Deserialize(binaryReader);
            m_spriteKuns = packet.assetKuns;
            m_spriteViews = new SpriteView[m_spriteKuns.Length];
            m_spriteNames = new string[m_spriteKuns.Length];
            for(var i = 0; i < m_spriteKuns.Length; i++)
            {
                m_spriteViews[i] = new SpriteView(m_spriteKuns[i]);
                m_spriteNames[i] = m_spriteKuns[i].name;
            }
        }
    }
}
