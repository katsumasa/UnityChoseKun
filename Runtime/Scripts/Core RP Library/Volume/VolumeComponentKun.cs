using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utj.UnityChoseKun.Engine.Rendering
{

    public class VolumeComponentKun : ScriptableObjectKun
    {
        private bool m_Active;
        public bool active
        {
            get { return m_Active; }
            set { m_Active = value; }            
        }

        private string m_DisplayName;
        public string displayName
        {
            get => m_DisplayName;
            set { dirty = true; m_DisplayName = value; }
        }

        private VolumeParameterKun.VolumeParameterKunType[] m_ParameterTypes;
        public VolumeParameterKun.VolumeParameterKunType[] parameterTypes
        { 
            get { return m_ParameterTypes; }
        }


        private VolumeParameterKun[] m_Parameters;
        public VolumeParameterKun[] parameterKuns
        {
            get { return m_Parameters; }
        }

        public VolumeComponentKun():this(null)
        {

        }


        public VolumeComponentKun(ScriptableObject scriptableObject) : base(scriptableObject)
        {
            if(scriptableObject == null)
            {
                return;
            }
            var t = scriptableObject.GetType();
            FieldInfo fi;
            fi = t.GetField("active", BindingFlags.Instance | BindingFlags.Public);
            m_Active = (bool)fi.GetValue(scriptableObject);

            PropertyInfo pi;
            pi = t.GetProperty("displayName", BindingFlags.Instance | BindingFlags.Public);
            m_DisplayName = (string)pi.GetValue(scriptableObject);

            // parametersはSystem.Collections.ObjectModel.ReadOnlyCollection<VolumeParameter>型である為、
            // VolumeParameter型が使えない場合は、IEnumerableでキャストするしか方法がない
            pi = t.GetProperty("parameters", BindingFlags.Instance | BindingFlags.Public);
            var parameters = (IEnumerable)pi.GetValue(scriptableObject);
            var len = 0;
            foreach(var parameter in parameters)
            {
                len++;
            }
            m_Parameters = new VolumeParameterKun[len];
            m_ParameterTypes = new VolumeParameterKun.VolumeParameterKunType[len];
            var i = 0;
            foreach (var parameter in parameters)
            {
                m_Parameters[i] = VolumeParameterKun.Allocater(parameter);
                m_ParameterTypes[i] = m_Parameters[i].volumeParameterKunType;
                i++;
            }
        }


        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_Active);
            binaryWriter.Write(m_DisplayName);
                       
            binaryWriter.Write(m_ParameterTypes.Length);            
            if (m_Parameters.Length > 0)
            {
                for (var i = 0; i < m_Parameters.Length; i++)
                {
                    binaryWriter.Write((int)m_ParameterTypes[i]);
                    m_Parameters[i].Serialize(binaryWriter);
                }
            }                        
        }


        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_Active = binaryReader.ReadBoolean();
            m_DisplayName = binaryReader.ReadString();
            var len = binaryReader.ReadInt32();
            m_ParameterTypes = new VolumeParameterKun.VolumeParameterKunType[len];
            m_Parameters = new VolumeParameterKun[m_ParameterTypes.Length];
            if (len > 0)
            {
                for(var i = 0; i < m_ParameterTypes.Length; i++)
                {
                    m_ParameterTypes[i] = (VolumeParameterKun.VolumeParameterKunType)binaryReader.ReadInt32();
                    m_Parameters[i] = VolumeParameterKun.Allocater(m_ParameterTypes[i]);
                    m_Parameters[i].Deserialize(binaryReader);
                }
            }
        }

        public virtual void WriteBack(ScriptableObject scriptableObject)
        {
            var t = scriptableObject.GetType();
            FieldInfo fi;
            fi = t.GetField("active", BindingFlags.Instance | BindingFlags.Public);
            fi.SetValue(scriptableObject,m_Active);

            PropertyInfo pi;
            pi = t.GetProperty("displayName", BindingFlags.Instance | BindingFlags.Public);
            pi.SetValue(scriptableObject, m_DisplayName);

            pi = t.GetProperty("parameters", BindingFlags.Instance | BindingFlags.Public);
            var parameters = (IEnumerable)pi.GetValue(scriptableObject);
            
            var i = 0;
            foreach(var parameter in parameters)
            {
                m_Parameters[i++].WriteBack(parameter);
            }
        }
    }

}