namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    

    public class TimePlayer : BasePlayer
    {
        public  void OnMessageEventPull(byte[] bytes)
        {
            var timeKun = new TimeKun(true);
            SendMessage<TimeKun>(UnityChoseKun.MessageID.TimePull,timeKun);
        }

        public void OnMessageEventPush(byte[] bytes)
        {
            var timeKun = UnityChoseKun.GetObject<TimeKun>(bytes);
            timeKun.WriteBack();
        }        
    }
}
