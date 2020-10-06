namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    

    public class ScreenPlayer : BasePlayer
    {
        public void OnMessageEventPull(byte[] bytes)
        {
            //Debug.Log("ScreenPlayer:OnMessageEventPull");
            var screenKun = new ScreenKun(true);
            SendMessage<ScreenKun>(UnityChoseKun.MessageID.ScreenPull,screenKun);
        }


        public void OnMessageEventPush(byte[] bytes)
        {
            //Debug.Log("ScreenPlayer:OnMessageEventPush");
            var screenKun = UnityChoseKun.GetObject<ScreenKun>(bytes);
            screenKun.WriteBack();
        }                
    }
}