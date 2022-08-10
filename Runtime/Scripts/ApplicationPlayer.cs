using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine
{

    public static class ApplicationPlayer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public static void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<ApplicationKun>(UnityChoseKun.MessageID.ApplicationPull, new ApplicationKun(true));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public static void OnMessageEventPush(BinaryReader binaryReader)
        {
            var applicationKun = new ApplicationKun();
            applicationKun.Deserialize(binaryReader);
            applicationKun.WriteBack();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public static void OnMessageEventQuit(BinaryReader binaryReader)
        {
            Application.Quit();
        }
    }
}
