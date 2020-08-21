using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    // <summary> UnityEngine.ObjectをRuntime/Editor間でシリアライズ/デシリアライズする為のClass </summary>
    [System.Serializable]
    public class ObjectKun 
    {
        // <summary>
        // メンバー変数の定義
        // </summary>

        // <summary> Object名 </summary>
        [SerializeField] protected string m_name;
        // <summary> instanceID </summary>
        [SerializeField]protected int m_instanceID;

        public  string name {
            get{return m_name;}
            set{m_name = value;}
        }

        public int instanceID {
            get {return m_instanceID;}
            protected set {m_instanceID = value;}
        }

        // <summary>
        // 関数の定義
        // </summary>

        // <summary>
        // コンストラクタ
        // </sumamry>
        public ObjectKun():this(null)
        {
            // TODO:int型のInstanceIDの求め方確認
            byte[] gb = System.Guid.NewGuid().ToByteArray();
            instanceID = System.BitConverter.ToInt32(gb,0);
        }

        public ObjectKun(UnityEngine.Object obj)
        {
            if(obj!=null){
                //name = obj.name;
                name = obj.ToString();
                instanceID = obj.GetInstanceID();    
            }
        }
        
        public virtual void WriteBack(UnityEngine.Object obj)
        {
            //...
        }

        public int GetInstanceID()
        {
            return instanceID;
        }
    }
}
