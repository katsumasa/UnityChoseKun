using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Utj.UnityChoseKun.Engine
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
        [SerializeField] int m_sceneHn;

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

        public int sceneHn
        {
            get { return m_sceneHn; }
            set { m_sceneHn = value; }
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
            m_localPosition = new Vector3Kun(0, 0, 0);
            m_localScale = new Vector3Kun(1, 1, 1);
            m_localRotation = new Vector3Kun();
            var transform = component as Transform;
            if(transform != null){
                localPosition = transform.localPosition;
                localScale = transform.localScale;
                localRotation = transform.localRotation.eulerAngles;
                instanceID = transform.GetInstanceID();
                if(transform.parent != null){
                    parentInstanceID = transform.parent.GetInstanceID();
                }
                sceneHn = transform.gameObject.scene.handle;
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
                    //Debug.Log("parentInstanceID " + parentInstanceID);
                    if (parentInstanceID == 0)
                    {
                        transform.parent = null;
                        if (transform.gameObject.scene.handle != sceneHn)
                        {
                            for (var i = 0; i < SceneManager.sceneCount; i++)
                            {
                                var scene = SceneManager.GetSceneAt(i);
                                if (scene.handle == sceneHn)
                                {
                                    SceneManager.MoveGameObjectToScene(transform.gameObject, scene);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        transform.parent = GetTransform(parentInstanceID);
                    }
                    transform.localPosition = localPosition;
                    transform.localScale = localScale;
                    transform.localRotation = Quaternion.Euler(new Vector3(localRotation.x, localRotation.y, localRotation.z));
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceID"></param>
        /// <returns></returns>
        public Transform GetTransform(int instanceID)
        {            
            var transform = Transform.FindObjectsOfType<Transform>().FirstOrDefault(q => q.GetInstanceID() == instanceID);
            return transform;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);

            SerializerKun.Serialize<Vector3Kun>(binaryWriter, m_localPosition);
            SerializerKun.Serialize<Vector3Kun>(binaryWriter, m_localScale);
            SerializerKun.Serialize<Vector3Kun>(binaryWriter, m_localRotation);                        
            binaryWriter.Write(m_parentInstanceID);
            binaryWriter.Write(m_childCount);
            binaryWriter.Write(m_sceneHn);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);

            m_localPosition = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader);
            m_localScale = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader);
            m_localRotation = SerializerKun.DesirializeObject<Vector3Kun>(binaryReader);                        
            m_parentInstanceID = binaryReader.ReadInt32();
            m_childCount = binaryReader.ReadInt32();
            m_sceneHn = binaryReader.ReadInt32();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as TransformKun;
            if(other == null)
            {
                return false;
            }
            if (m_localPosition.Equals(other.m_localPosition) == false)
            {
                return false;
            }
            if (m_localRotation.Equals(other.m_localRotation) == false)
            {
                return false;
            }
            if (m_localScale.Equals(other.m_localScale) == false)
            {
                return false;
            }
            if(m_parentInstanceID.Equals(other.parentInstanceID)== false)
            {
                return false;
            }
            if (childCount.Equals(other.childCount) == false)
            {
                return false;
            }
            return base.Equals(obj);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    } 

}