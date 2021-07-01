using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class SpriteRendererView : RendererView
    {
        private static class Styles
        {
            public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_SpriteRenderer Icon");
            public static readonly GUIContent Sprite = new GUIContent("Sprite");
            public static readonly GUIContent Color = new GUIContent("Color");
            public static readonly GUIContent Flip = new GUIContent("Flip");
            public static readonly GUIContent DeawMode = new GUIContent("Draw Mode");
            public static readonly GUIContent MaskInteraction = new GUIContent("Mask Interaction");
            public static readonly GUIContent SpriteSpotPoint = new GUIContent("Sprite Spot Point");
            public static readonly GUIContent AdditionalSettings = new GUIContent("Additional Settings");
            public static readonly GUIContent SortingLayer = new GUIContent("Sorting Layer");
            public static readonly GUIContent OrderInLayer = new GUIContent("Order in Layer");
        }


        SpriteRendererKun spriteRendererKun
        {
            get { return rendererKun as SpriteRendererKun; }
            set { rendererKun = value; }
        }

        bool m_IsAdditionalSettings;

        public SpriteRendererView():base()
        {
            componentIcon = Styles.ComponentIcon;
            foldout = true;
        }

        public override bool OnGUI()
        {
            if (DrawHeader())
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUI.BeginChangeCheck();
                    //EditorGUILayout.LabelField(Styles.Sprite, spriteRendererKun.sprite.name);
                    EditorGUILayout.TextField(Styles.Sprite,spriteRendererKun.sprite.name);
                    spriteRendererKun.color = new ColorKun(EditorGUILayout.ColorField(Styles.Color, spriteRendererKun.color.GetColor()));

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(Styles.Flip);
                    spriteRendererKun.flipX = EditorGUILayout.ToggleLeft("X", spriteRendererKun.flipX);
                    spriteRendererKun.flipY = EditorGUILayout.ToggleLeft("Y", spriteRendererKun.flipY);
                    EditorGUILayout.EndHorizontal();

                    spriteRendererKun.drawMode = (SpriteDrawMode)EditorGUILayout.EnumPopup(Styles.DeawMode, spriteRendererKun.drawMode);
                    spriteRendererKun.maskInteraction = (SpriteMaskInteraction)EditorGUILayout.EnumPopup(Styles.MaskInteraction, spriteRendererKun.maskInteraction);
                    spriteRendererKun.spriteSortPoint = (SpriteSortPoint)EditorGUILayout.EnumPopup(Styles.SpriteSpotPoint, spriteRendererKun.spriteSortPoint);

                    materialView.OnGUI();

                    m_IsAdditionalSettings = EditorGUILayout.Foldout(m_IsAdditionalSettings,Styles.AdditionalSettings);
                    if (m_IsAdditionalSettings)
                    {
                        using (new EditorGUI.IndentLevelScope())
                        {
                            spriteRendererKun.sortingLayerID = EditorGUILayout.LayerField(Styles.SortingLayer, spriteRendererKun.sortingLayerID);
                            spriteRendererKun.sortingOrder = EditorGUILayout.IntField(Styles.OrderInLayer, spriteRendererKun.sortingOrder);
                        }
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        spriteRendererKun.dirty = true;
                    }
                }
            }
            return true;
        }
    }
}