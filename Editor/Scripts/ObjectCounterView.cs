namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;


    [System.Serializable]
    public class ObjectCounterView
    {
        int[] mComponentCounts;

        int[] componentCounts
        {
            get
            {
                if(mComponentCounts == null || mComponentCounts.Length != (int)ComponentKun.ComponentKunType.Max)
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
                int count = 0;
                if(componentCounts != null)
                {
                    count = componentCounts[i];
                }


                EditorGUILayout.IntField(new GUIContent(t.ToString()), count);
            }

            if (GUILayout.Button("Analayze"))
            {
                Countup();
            }
        }



        private void Countup()
        {
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