namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;


    [System.Serializable]
    public class ObjectCounterView
    {
        [SerializeField] int[] mComponentCounts;

        int[] componentCounts
        {
            get
            {
                if(mComponentCounts == null)
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
            for(var i = 0; i < (int)ComponentKun.ComponentKunType.Max; i++)
            {
                var t = (ComponentKun.ComponentKunType)i;
                EditorGUILayout.IntField(new GUIContent(t.ToString()), componentCounts[i]);
            }

            if (GUILayout.Button("Analayze"))
            {
                Countup();
            }
        }



        private void Countup()
        {
            componentCounts = new int[(int)ComponentKun.ComponentKunType.Max];
            var window = (PlayerHierarchyWindow)EditorWindow.GetWindow(typeof(PlayerHierarchyWindow));            
            foreach (var gameObjectKun in window.sceneKun.gameObjectKuns)
            {                
                foreach(var componentKunType in gameObjectKun.componentKunTypes)
                {
                    componentCounts[(int)componentKunType]++;
                }
            }
        }
    }
}