using System.IO;


namespace Utj.UnityChoseKun.Engine 
{
    /// <summary>
    /// 
    /// </summary>
    public static class AndroidPlayer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public static void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<AndroidKun>(UnityChoseKun.MessageID.AndroidPull, new AndroidKun());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public static void OnMessageEventPush(BinaryReader binaryReader)
        {         
            var androidKun = new AndroidKun();
            androidKun.Deserialize(binaryReader);
            androidKun.WriteBack();
            UnityChoseKunPlayer.SendMessage<AndroidKun>(UnityChoseKun.MessageID.AndroidPush, androidKun);
        }
    }
}
