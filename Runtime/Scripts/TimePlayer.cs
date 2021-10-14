using System.IO;


namespace Utj.UnityChoseKun
{
    /// <summary>
    /// 
    /// </summary>
    public class TimePlayer : BasePlayer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public  void OnMessageEventPull(BinaryReader binaryReader)
        {
            var timeKun = new TimeKun(true);
            UnityChoseKunPlayer.SendMessage<TimeKun>(UnityChoseKun.MessageID.TimePull,timeKun);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEventPush(BinaryReader binaryReader)
        {
            var timeKun = new TimeKun();
            timeKun.Deserialize(binaryReader);
            timeKun.WriteBack();
        }        
    }
}
