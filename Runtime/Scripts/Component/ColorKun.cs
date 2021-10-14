using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class ColorKun : ISerializerKun
    {
        [SerializeField] public float a;
        [SerializeField] public float b;
        [SerializeField] public float g;
        [SerializeField] public float r;

        public ColorKun() { a = 1.0f; }

        public ColorKun(float r,float g,float b,float a)
        {
            this.a = a;
            this.b = b;
            this.g = g;
            this.r = r;
        }


        public ColorKun(Color color)
        {
            this.a = color.a;
            this.b = color.b;
            this.g = color.g;
            this.r = color.r;
        }

        public Color GetColor()
        {
            return new Color(this.r,this.g,this.b,this.a);
        }


        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(a);
            binaryWriter.Write(b);
            binaryWriter.Write(g);
            binaryWriter.Write(r);
        }

        public virtual void Deserialize(BinaryReader binaryReader)
        {
            a = binaryReader.ReadSingle();
            b = binaryReader.ReadSingle();
            g = binaryReader.ReadSingle();
            r = binaryReader.ReadSingle();
        }


        public override bool Equals(object obj)
        {
            ColorKun otherKun = obj as ColorKun;
            if (a.Equals(otherKun.a) == false)
            {
                return false;
            }
            if(b.Equals(otherKun.b) == false)
            {
                return false;
            }
            if (g.Equals(otherKun.g) == false)
            {
                return false;
            }
            if(r.Equals(otherKun.r) == false)
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
