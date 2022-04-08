using System.IO;


namespace Utj.UnityChoseKun.Engine
{

    /// <summary>
    /// 
    /// </summary>
    public class SystemInfoPlayer : BasePlayer
    {
        public void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<SystemInfoKun>(UnityChoseKun.MessageID.SystemInfoPull, new SystemInfoKun(true));
        }
    }
}