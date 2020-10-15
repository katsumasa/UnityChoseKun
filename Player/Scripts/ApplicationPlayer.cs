using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{

    public class ApplicationPlayer : BasePlayer
    {
        public void OnMessageEventPull(byte[] bytes)
        {
            SendMessage<ApplicationKun>(UnityChoseKun.MessageID.ApplicationPull, new ApplicationKun(true));
        }


        public void OnMessageEventPush(byte[] bytes)
        {
            var applicationKun = UnityChoseKun.GetObject<ApplicationKun>(bytes);
            applicationKun.WriteBack();
        }

        public void OnMessageEventQuit(byte[] bytes)
        {
            Application.Quit();
        }
    }
}
