using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun {
    public class AndroidPlayer : BasePlayer
    {
        public void OnMessageEventPull(byte[] bytes)
        {            
            SendMessage<AndroidKun>(UnityChoseKun.MessageID.AndroidPull, new AndroidKun());
        }


        public void OnMessageEventPush(byte[] bytes)
        {         
            var androidKun = UnityChoseKun.GetObject<AndroidKun>(bytes);
            androidKun.WriteBack();
            SendMessage<AndroidKun>(UnityChoseKun.MessageID.AndroidPush, androidKun);
        }
    }
}
