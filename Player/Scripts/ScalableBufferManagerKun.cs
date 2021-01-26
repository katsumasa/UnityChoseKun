using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    public class ScalableBufferManagerKun : ISerializerKun
    {
        public float heightScaleFactor;
        public float widthScaleFactor;
        public bool isDirty;


        public ScalableBufferManagerKun() : this(false) { }


        public ScalableBufferManagerKun(bool isSet) : base()
        {
            if (isSet)
            {
                widthScaleFactor = ScalableBufferManager.widthScaleFactor;
                heightScaleFactor = ScalableBufferManager.heightScaleFactor;                
            }
            isDirty = false;
        }


        public void WriteBack()
        {
            if (isDirty)
            {
                ScalableBufferManager.ResizeBuffers(widthScaleFactor, heightScaleFactor);                
            }
        }


        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(heightScaleFactor);
            binaryWriter.Write(widthScaleFactor);
            binaryWriter.Write(isDirty);
        }


        public virtual void Deserialize(BinaryReader binaryReader)
        {
            heightScaleFactor = binaryReader.ReadSingle();
            widthScaleFactor = binaryReader.ReadSingle();
            isDirty = binaryReader.ReadBoolean();
        }


        public override bool Equals(object obj)
        {
            var other = obj as ScalableBufferManagerKun;
            if (other == null)
            {
                return false;
            }
            if (!float.Equals(heightScaleFactor, other.heightScaleFactor))
            {
                return false;
            }
            if (!float.Equals(widthScaleFactor, other.widthScaleFactor))
            {
                return false;
            }
            return true;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


 
}