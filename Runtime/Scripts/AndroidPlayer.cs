using System.IO;


namespace Utj.UnityChoseKun.Engine 
{
    /// <summary>
    /// 
    /// </summary>
    public class AndroidPlayer : BasePlayer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<AndroidKun>(UnityChoseKun.MessageID.AndroidPull, new AndroidKun());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEventPush(BinaryReader binaryReader)
        {         
            var androidKun = new AndroidKun();
            androidKun.Deserialize(binaryReader);
            androidKun.WriteBack();
            UnityChoseKunPlayer.SendMessage<AndroidKun>(UnityChoseKun.MessageID.AndroidPush, androidKun);
        }
    }
}
