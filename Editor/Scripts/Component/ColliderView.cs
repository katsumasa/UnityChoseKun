using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun
{
    public class ColliderView : ComponentView
    {
        private static class Styles
        {
            public static Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_BoxCollider Icon");            
        }


        protected ColliderKun colliderKun
        {
            get { return componentKun as ColliderKun; }
            set { componentKun = value; }
        }
        
        public ColliderView()
        {
            componentIcon = Styles.ComponentIcon;
            foldout = true;
        }        


        protected virtual bool DrawHeader()
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            EditorGUILayout.BeginHorizontal();
            var iconContent = new GUIContent(mComponentIcon);
            foldout = EditorGUILayout.Foldout(foldout, iconContent);                          // Foldout & Icon

            EditorGUI.BeginChangeCheck();
            var content = new GUIContent(colliderKun.name);

            var rect = EditorGUILayout.GetControlRect();
            colliderKun.enabled = EditorGUI.ToggleLeft(new Rect(rect.x - 24, rect.y, rect.width, rect.height), content, colliderKun.enabled);
            if (EditorGUI.EndChangeCheck())
            {
                colliderKun.dirty = true;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));

            return foldout;
        }


        public override bool OnGUI()
        {
            if (DrawHeader())
            {
                EditorGUI.BeginChangeCheck();
                using (new EditorGUI.IndentLevelScope())
                {
                    colliderKun.isTrigger = EditorGUILayout.Toggle("Is Trigger", colliderKun.isTrigger);                    
                    EditorGUILayout.LabelField("Material", colliderKun.material);

                    colliderKun.contactOffset = EditorGUILayout.FloatField("Contact Offset", colliderKun.contactOffset);
                    
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Center");
                    colliderKun.boundsKun.center = EditorGUILayout.Vector3Field("", colliderKun.boundsKun.center);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Size");
                    colliderKun.boundsKun.size = EditorGUILayout.Vector3Field("", colliderKun.boundsKun.size);
                    EditorGUILayout.EndHorizontal();
                }
                if (EditorGUI.EndChangeCheck())
                {
                    colliderKun.dirty = true;
                }
                return true;
            }
            

            return false;
        }
    }
}
