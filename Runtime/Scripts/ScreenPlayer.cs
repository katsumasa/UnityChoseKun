using System.IO;

namespace Utj.UnityChoseKun.Engine
{           
    /// <summary>
    /// 
    /// </summary>
    public static class ScreenPlayer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public static void OnMessageEventPull(BinaryReader binaryReader)
        {
            var screenKun = new ScreenKun(true);
            UnityChoseKunPlayer.SendMessage<ScreenKun>(UnityChoseKun.MessageID.ScreenPull,screenKun);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public static void OnMessageEventPush(BinaryReader binaryReader)
        {            
            var screenKun = new ScreenKun();
            screenKun.Deserialize(binaryReader);
            screenKun.WriteBack();
        }                
    }
}