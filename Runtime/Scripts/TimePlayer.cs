using System.IO;


namespace Utj.UnityChoseKun.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class TimePlayer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public static void OnMessageEventPull(BinaryReader binaryReader)
        {
            var timeKun = new TimeKun(true);
            UnityChoseKunPlayer.SendMessage<TimeKun>(UnityChoseKun.MessageID.TimePull,timeKun);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public static void OnMessageEventPush(BinaryReader binaryReader)
        {
            var timeKun = new TimeKun();
            timeKun.Deserialize(binaryReader);
            timeKun.WriteBack();
        }        
    }
}
