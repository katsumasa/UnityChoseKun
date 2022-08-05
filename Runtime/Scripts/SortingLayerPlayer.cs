using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine
{
    [System.Serializable]
    public class SortingLayerPacket : ISerializerKun
    {
        [SerializeField] public SortingLayerKun[] layers;


        public SortingLayerPacket():base()
        {

        }

        public SortingLayerPacket(SortingLayer[] layers):base()
        {
            this.layers = new SortingLayerKun[layers.Length];
            for(var i = 0; i < layers.Length; i++)
            {
                this.layers[i] = new SortingLayerKun(layers[i]);
            }
        }

        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            SerializerKun.Serialize<SortingLayerKun>(binaryWriter, layers);
        }

        public virtual void Deserialize(BinaryReader binaryReader)
        {
            layers = SerializerKun.DesirializeObjects<SortingLayerKun>(binaryReader);
        }
    }


    public class SortingLayerPlayer : BasePlayer
    {
        public void OnMessageEventPull(BinaryReader binaryReader)
        {
            var packet = new SortingLayerPacket(SortingLayer.layers);                   
            UnityChoseKunPlayer.SendMessage<SortingLayerPacket>(UnityChoseKun.MessageID.SortingLayerPull,packet);
        }
    }
}