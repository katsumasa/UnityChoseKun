using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{

    public class ApplicationPlayer : BasePlayer
    {
        public void OnMessageEventPull(string json)
        {
            SendMessage<ApplicationKun>(UnityChoseKun.MessageID.ApplicationPull, new ApplicationKun(true));
        }


        public void OnMessageEventPush(string json)
        {
            var applicationKun = JsonUtility.FromJson<ApplicationKun>(json);
            applicationKun.WriteBack();
        }

        public void OnMessageEventQuit(string json)
        {
            Application.Quit();
        }
    }
}
