namespace Utj.UnityChoseKun
{    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    

   [System.Serializable]
    public class TransformKun : ComponentKun{
        
        [SerializeField] protected Vector3 m_localPosition;
        public Vector3 localPosition {
            get{return m_localPosition;}
            set{m_localPosition = value;}
        }
        [SerializeField] protected Vector3 m_localScale;
        public Vector3 localScale{
            get{return m_localScale;}
            set{m_localScale = value;}
        }

        [SerializeField] protected Vector3 m_localRotation;
        public Vector3 localRotation{
            get{return m_localRotation;}
            set{m_localScale = value;}
        }

        
        [SerializeField] protected  int m_parentInstanceID;
        public int parentInstanceID{
            get {return m_parentInstanceID;}
            protected set {m_parentInstanceID = value;}
        }
        [SerializeField] int m_childCount;
        private int childCount {
            get{return m_childCount;}
            set{m_childCount = value;}
        }

        public TransformKun():this(null){}
        public TransformKun(Component component):base(component){
            var transform = component as Transform;
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