using System.IO;

namespace Utj.UnityChoseKun
{           
    /// <summary>
    /// 
    /// </summary>
    public class ScreenPlayer : BasePlayer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEventPull(BinaryReader binaryReader)
        {
            var screenKun = new ScreenKun(true);
            UnityChoseKunPlayer.SendMessage<ScreenKun>(UnityChoseKun.MessageID.ScreenPull,screenKun);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEventPush(BinaryReader binaryReader)
        {            
            var screenKun = new ScreenKun();
            screenKun.Deserialize(binaryReader);
            screenKun.WriteBack();
        }                
    }
}