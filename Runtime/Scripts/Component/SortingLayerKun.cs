using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class SortingLayerKun : ISerializerKun
    {
        [SerializeField] public static SortingLayerKun[] layers;
        [SerializeField] public int id;
        [SerializeField] public string name;
        [SerializeField] public int value;


        public SortingLayerKun():base()
        {
        }

        public SortingLayerKun(SortingLayer sortingLayer):base()
        {            
            id = sortingLayer.id;
            name = sortingLayer.name;
            value = sortingLayer.value;
        }


        public virtual bool WriteBack(UnityEngine.Object obj)
        {
            return true;
        }

        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(id);
            binaryWriter.Write(name);
            binaryWriter.Write(value);
        }

        public virtual void Deserialize(BinaryReader binaryReader)
        {
            id = binaryReader.ReadInt32();
            name = binaryReader.ReadString();
            value = binaryReader.ReadInt32();
        }
    }
}