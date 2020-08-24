namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    

    public class ScreenPlayer : BasePlayer
    {
        public void OnMessageEventPull(string json)
        {
            Debug.Log("OnMessageEventPull");
            var screenKun = new ScreenKun();
            SendMessage<ScreenKun>(UnityChoseKun.MessageID.ScreenPull,screenKun);
        }


        public void OnMessageEventPush(string json)
        {
            Debug.Log("OnMessageEventPush");
            var screenKun = JsonUtility.FromJson<ScreenKun>(json);
            screenKun.WriteBack();
        }                
    }
}