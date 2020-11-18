using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;


namespace Utj.UnityChoseKun{
    /// <summary>
    /// UnityEngine.ObjectをRuntime/Editor間でシリアライズ/デシリアライズする為のClass
    /// </summary>    
    [System.Serializable]    
    public class ObjectKun : ISerializerKun
    {        
        [SerializeField] protected string m_name;
        [SerializeField] protected int m_instanceID;
        [SerializeField] bool m_dirty;


        public string name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        
        public int instanceID {
            get {return m_instanceID;}
            protected set {m_instanceID = value;}
        }


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
            name = "";
            dirty = false;
            if (obj != null){
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetInstanceID()
        {
            return instanceID;
        }


        /// <summary>
        /// ObjectをSerializeする
        /// </summary>
        /// <param name="binaryWriter"></param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_name);
            binaryWriter.Write(m_instanceID);
            binaryWriter.Write(m_dirty);
        }


        /// <summary>
        /// ObjectをDeserializeする
        /// </summary>
        /// <param name="binaryReader"></param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {                        
            m_name = binaryReader.ReadString();
            m_instanceID = binaryReader.ReadInt32();
            m_dirty = binaryReader.ReadBoolean();
        }


        /// <summary>
        /// ObjectKunの比較
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var objectKun = obj as ObjectKun;
            if(objectKun == null)
            {
                return false;
            }
            if(string.Compare(name, objectKun.name) != 0)
            {
                return false;
            }            
            if (instanceID != objectKun.instanceID)
            {
                return false;
            }
            if(dirty != objectKun.dirty)
            {
                return false;
            }            
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return instanceID;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObjectKun Allocater()
        {
            return new ObjectKun();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static ObjectKun[] Allocater(int len)
        {
            return new ObjectKun[len];
        }
    }
}
