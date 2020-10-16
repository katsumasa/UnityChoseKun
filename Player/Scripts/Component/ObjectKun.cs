using System.Collections;
using System.Collections.Generic;


using UnityEngine;
namespace Utj.UnityChoseKun{
    /// <summary>
    /// UnityEngine.ObjectをRuntime/Editor間でシリアライズ/デシリアライズする為のClass
    /// </summary>
    [System.Serializable]
    public class ObjectKun 
    {
        
        [SerializeField] protected string m_name;
        public string name
        {
            get { return m_name; }
            set { m_name = value; }
        }


        [SerializeField]protected int m_instanceID;
        public int instanceID {
            get {return m_instanceID;}
            protected set {m_instanceID = value;}
        }


        [SerializeField] bool m_dirty;
        public bool dirty {
            get {return m_dirty;}
            set {m_dirty = value;}
        }
        

        public virtual bool dirtyInHierarchy {
            get {return dirty;}
        }


        /// <summary>
        /// ObjectKunのコンストラクタ
        /// </summary>
        public ObjectKun():this(null)
        {
            // TODO:int型のInstanceIDの求め方確認
            byte[] gb = System.Guid.NewGuid().ToByteArray();
            instanceID = System.BitConverter.ToInt32(gb,0);
        }


        /// <summary>
        /// ObjectKunのコンストラクタ
        /// </summary>
        /// <param name="obj">Object型のオブジェクト</param>
        public ObjectKun(UnityEngine.Object obj)
        {
            if(obj!=null){
                name = obj.GetType().Name;
                instanceID = obj.GetInstanceID();    
            }
        }
        

        /// <summary>
        /// Objectに内容を書き戻す
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>結果 true:書き戻しが発生 false:書き戻しの必要がなかった</returns>
        public virtual bool WriteBack(UnityEngine.Object obj)
        {
            return dirty ;
        }

        public int GetInstanceID()
        {
            return instanceID;
        }
    }
}
