using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Utj.UnityChoseKun
{           
    /// <summary>
    /// Transformをシリアライズ・デシリアライズする為のClass
    /// </summary>
    [System.Serializable]
    public class TransformKun : ComponentKun{
        
        [SerializeField] protected Vector3Kun m_localPosition;
        [SerializeField] protected Vector3Kun m_localScale;
        [SerializeField] protected Vector3Kun m_localRotation;
        [SerializeField] protected int m_parentInstanceID;
        [SerializeField] int m_childCount;


        public Vector3 localPosition {
            get{return m_localPosition.GetVector3();}
            set{m_localPosition = new Vector3Kun(value);}
        }


        public Vector3 localScale{
            get{return m_localScale.GetVector3();}
            set{m_localScale = new Vector3Kun(value);}
        }

        
        public Vector3 localRotation{
            get{return m_localRotation.GetVector3();}
            set{ m_localRotation = new Vector3Kun(value);}
        }
        

        public int parentInstanceID{
            get {return m_parentInstanceID;}
            set {m_parentInstanceID = value;}
        }


        private int childCount {
            get{return m_childCount;}
            set{m_childCount = value;}
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TransformKun():this(null){}


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="component"></param>
        public TransformKun(Component component):base(component){
            componentKunType = ComponentKunType.Transform;
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


        /// <summary>
        /// 書き戻し
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public  override bool WriteBack(Component component)
        {
            if(base.WriteBack(component)){
                var transform = component as Transform;
                if(transform!=null){
                    //Debug.Log("Transform WriteBack" + localPosition + localRotation + localScale);                    


                    transform.parent = GetTransform(parentInstanceID);
                    transform.localPosition = localPosition;
                    transform.localScale = localScale;
                    transform.localRotation = Quaternion.Euler(new Vector3(localRotation.x, localRotation.y, localRotation.z));
                }
                return true;
            }
            return false;
        }


        public Transform GetTransform(int instanceID)
        {
            var transform = Transform.FindObjectsOfType<Transform>().FirstOrDefault(q => q.GetInstanceID() == instanceID);


            return transform;
        }
    } 

}