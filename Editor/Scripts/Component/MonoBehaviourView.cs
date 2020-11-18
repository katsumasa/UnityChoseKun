using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class MonoBehaviourView : BehaviourView
    {


        static class Styles
        {
            public static readonly Texture2D Icon = (Texture2D)EditorGUIUtility.Load("d_cs Script Icon");
        }
        
        
        public MonoBehaviourView():base()
        {
            componentIcon = Styles.Icon;
        }


        public override ComponentKun GetComponentKun()
        {
            return behaviourKun;
        }


        
    }
}