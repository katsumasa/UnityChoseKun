namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    

    public class TimePlayer : BasePlayer
    {
        public  void OnMessageEventPull(string json)
        {
            var timeKun = new TimeKun(true);
            SendMessage<TimeKun>(UnityChoseKun.MessageID.TimePull,timeKun);
        }

        public void OnMessageEventPush(string json)
        {
            var timeKun = JsonUtility.FromJson<TimeKun>(json);
            timeKun.WriteBack();
        }        
    }
}
