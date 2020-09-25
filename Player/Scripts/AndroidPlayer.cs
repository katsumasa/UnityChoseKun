using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun {
    public class AndroidPlayer : BasePlayer
    {
        public void OnMessageEventPull(string json)
        {            
            SendMessage<ScreenKun>(UnityChoseKun.MessageID.AndroidPull, new AndroidKun());
        }


        public void OnMessageEventPush(string json)
        {         
            var androidKun = JsonUtility.FromJson<AndroidKun>(json);
            androidKun.WriteBack();
            SendMessage<ScreenKun>(UnityChoseKun.MessageID.AndroidPush, androidKun);
        }
    }
}
