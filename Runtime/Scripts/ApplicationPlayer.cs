using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{

    public class ApplicationPlayer : BasePlayer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<ApplicationKun>(UnityChoseKun.MessageID.ApplicationPull, new ApplicationKun(true));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEventPush(BinaryReader binaryReader)
        {
            var applicationKun = new ApplicationKun();
            applicationKun.Deserialize(binaryReader);
            applicationKun.WriteBack();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEventQuit(BinaryReader binaryReader)
        {
            Application.Quit();
        }
    }
}
