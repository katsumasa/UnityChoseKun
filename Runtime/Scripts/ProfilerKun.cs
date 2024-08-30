using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Profiling;


namespace Utj.UnityChoseKun.Engine
{
    public class ObjectRuntimeMemory : ISerializerKun
    {        
        [SerializeField] protected string m_name;
        [SerializeField] protected string m_className;
        [SerializeField] protected long m_runtimeMemorySizeLong;
        [SerializeField] protected HideFlags m_hideFlags;
        
        public string name
        {
            get { return m_name; }
        }

        public string className
        {
            get { return m_className; }
        }

        public long runtimeMemorySizeLong
        {
            get { return m_runtimeMemorySizeLong; }
        }

        public HideFlags hideFlags
        {
            get { return m_hideFlags; }
        }

        public ObjectRuntimeMemory() : this(null) { }
        

        public ObjectRuntimeMemory(UnityEngine.Object o)
        {
            if (o == null)
            {
                m_name = "";
                m_className = "";
                m_runtimeMemorySizeLong = 0;
                m_hideFlags = HideFlags.None;
            }
            else
            {
                m_name = o.name;
                m_className = o.GetType().Name;
                m_runtimeMemorySizeLong = Profiler.GetRuntimeMemorySizeLong(o);
                m_hideFlags = o.hideFlags;
            }
        }

        public void Deserialize(BinaryReader binaryReader)
        {
            m_name = binaryReader.ReadString();
            m_className = binaryReader.ReadString();
            m_runtimeMemorySizeLong = binaryReader.ReadInt64();
            m_hideFlags = (HideFlags)binaryReader.ReadInt32();
        }

        public void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_name);
            binaryWriter.Write(m_className);
            binaryWriter.Write((long)m_runtimeMemorySizeLong);
            binaryWriter.Write((int)m_hideFlags);
        }
    }


    public class ProfilerKun : ISerializerKun
    {
        static ProfilerKun m_instance;

        public static ProfilerKun instance
        {
            get
            {
                if(m_instance == null)
                {
                    m_instance = new ProfilerKun();
                }
                return m_instance;
            }
        }

        [SerializeField] long m_monoHeapSizeLong;
        [SerializeField] long m_monoUsedSizeLong;
        [SerializeField] long m_totalReservedMemoryLong;
        [SerializeField] long m_totalAllocatedMemoryLong;
        [SerializeField] long m_totalUnusedReservedMemoryLong;
        [SerializeField] ObjectRuntimeMemory[] m_objectRuntimeMemorys;


        public long monoHeapSizeLong
        {
            get { return m_monoHeapSizeLong; }
            set { m_monoHeapSizeLong = value; }
        }

        public long monoUsedSizeLong
        {
            get { return m_monoUsedSizeLong; }
            set { m_monoUsedSizeLong = value; }
        }

        public long totalReservedMemoryLong
        {
            get { return m_totalReservedMemoryLong; }
            set { m_totalReservedMemoryLong = value; }
        }

        public long totalAllocatedMemoryLong
        {
            get { return m_totalAllocatedMemoryLong; }
            set { m_totalAllocatedMemoryLong = value; }
        }

        public long totalUnusedReservedMemoryLong
        {
            get { return m_totalUnusedReservedMemoryLong; }
            set { m_totalUnusedReservedMemoryLong = value;}
        }


        public ObjectRuntimeMemory[] objectRuntimeMemorys
        {
            get { return m_objectRuntimeMemorys; }
            set { m_objectRuntimeMemorys = value;}
        }


        public ProfilerKun()
        {
            m_objectRuntimeMemorys = new ObjectRuntimeMemory[0];
        }

        public void Deserialize(BinaryReader binaryReader)
        {
            m_monoHeapSizeLong = binaryReader.ReadInt64();
            m_monoUsedSizeLong = binaryReader.ReadInt64();
            m_totalReservedMemoryLong = binaryReader.ReadInt64();
            m_totalAllocatedMemoryLong= binaryReader.ReadInt64();
            m_totalUnusedReservedMemoryLong= binaryReader.ReadInt64();


            int len = binaryReader.ReadInt32();
            m_objectRuntimeMemorys = new ObjectRuntimeMemory[len];
            for (var i = 0; i < len; i++)
            {
                m_objectRuntimeMemorys[i] = new ObjectRuntimeMemory();
                m_objectRuntimeMemorys[i].Deserialize(binaryReader);
            }
        }

        public void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_monoHeapSizeLong);
            binaryWriter.Write(m_monoUsedSizeLong);
            binaryWriter.Write(m_totalReservedMemoryLong);
            binaryWriter.Write(m_totalAllocatedMemoryLong);
            binaryWriter.Write(m_totalUnusedReservedMemoryLong);

            binaryWriter.Write(m_objectRuntimeMemorys.Length);
            for(var i = 0; i != m_objectRuntimeMemorys.Length; i++)
            {
                m_objectRuntimeMemorys[i].Serialize(binaryWriter);
            }
        }
    }
}