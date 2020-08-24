namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    

    public class ScreenPlayer : BasePlayer
    {
        public void OnMessageEventPull(string json)
        {
            //Debug.Log("ScreenPlayer:OnMessageEventPull");
            var screenKun = new ScreenKun(true);
            SendMessage<ScreenKun>(UnityChoseKun.MessageID.ScreenPull,screenKun);
        }


        public void OnMessageEventPush(string json)
        {
            //Debug.Log("ScreenPlayer:OnMessageEventPush");
            var screenKun = JsonUtility.FromJson<ScreenKun>(json);
            screenKun.WriteBack();
        }                
    }
}