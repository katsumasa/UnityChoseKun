namespace Utj.UnityChoseKun
{    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


   [System.Serializable]
    public class TransformKun {
        public Vector3 localPosition;
        public Vector3 localScale;
        public Vector3 localRotation;
        public int instanceID;
        public int parentInstanceID;
        private int childCount;
        public TransformKun():this(null){}
        public TransformKun(Transform transform){
            if(transform != null){
                localPosition = transform.localPosition;
                localScale = transform.localScale;
                localRotation = transform.localRotation.eulerAngles;
                instanceID = transform.GetInstanceID();
                if(transform.parent != null){
                    parentInstanceID = transform.parent.GetInstanceID();
                }
            }
        }

        public  void WriteBack(Transform transform)
        {
            if(transform){
                transform.localPosition = localPosition;
                transform.localScale = localScale;
                transform.localRotation =  Quaternion.Euler(localRotation);
            }
        }
    } 

}