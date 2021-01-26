
namespace Utj.UnityChoseKun
{
    using System.IO;

    public class SystemInfoPlayer : BasePlayer
    {
        public void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<SystemInfoKun>(UnityChoseKun.MessageID.SystemInfoPull, new SystemInfoKun(true));
        }
    }
}