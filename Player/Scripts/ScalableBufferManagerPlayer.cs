using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utj.UnityChoseKun {
    public static class ScalableBufferManagerPlayer
    {
        public static void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<ScalableBufferManagerKun>(UnityChoseKun.MessageID.ScalableBufferManagerPull, new ScalableBufferManagerKun(true));
        }


        public static void OnMessageEventPush(BinaryReader binaryReader)
        {            
            var obj = new ScalableBufferManagerKun();
            obj.Deserialize(binaryReader);
            obj.WriteBack();
            UnityChoseKunPlayer.SendMessage<ScalableBufferManagerKun>(UnityChoseKun.MessageID.ScalableBufferManagerPush, new ScalableBufferManagerKun(true));
        }
    }
}