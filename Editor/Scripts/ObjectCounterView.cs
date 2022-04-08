using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {
        [System.Serializable]
        public class ObjectCounterView
        {
            [SerializeField] Vector2 mScrollPos;
            int[] mComponentCounts;


            int[] componentCounts
            {
                get
                {
                    if (mComponentCounts == null || mComponentCounts.Length != (int)ComponentKun.ComponentKunType.Max)
                    {
                        mComponentCounts = new int[(int)ComponentKun.ComponentKunType.Max];
                    }
                    return mComponentCounts;
                }

                set
                {
                    mComponentCounts = value;
                }
            }


            public void OnGUI()
            {
                EditorGUILayout.LabelField("Number of Components in a Scene");

                mScrollPos = EditorGUILayout.BeginScrollView(mScrollPos);
                for (var i = 0; i < (int)ComponentKun.ComponentKunType.Max; i++)
                {
                    var t = (ComponentKun.ComponentKunType)i;
                    int count = 0;
                    if (componentCounts != null)
                    {
                        count = componentCounts[i];
                    }
                    EditorGUILayout.IntField(new GUIContent(t.ToString()), count);
                }
                EditorGUILayout.EndScrollView();


                if (GUILayout.Button("Analayze"))
                {
                    Countup();
                }
            }



            private void Countup()
            {
                var window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));

                componentCounts = null;

                foreach (var sceneKun in window.sceneManagerKun.sceneKuns)
                {
                    foreach (var gameObjectKun in sceneKun.gameObjectKuns)
                    {
                        foreach (var componentKunType in gameObjectKun.componentKunTypes)
                        {
                            componentCounts[(int)componentKunType]++;
                        }
                    }
                }
            }
        }
    }
}