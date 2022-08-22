using System.IO;


namespace Utj.UnityChoseKun.Engine
{

    /// <summary>
    /// 
    /// </summary>
    public static class SystemInfoPlayer
    {
        public static void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<SystemInfoKun>(UnityChoseKun.MessageID.SystemInfoPull, new SystemInfoKun(true));
        }
    }
}