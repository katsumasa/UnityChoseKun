using System.IO;


namespace Utj.UnityChoseKun.Engine
{
    public static class OnDemandRenderingPlayer
    {
        public static void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<OnDemandRenderingKun>(UnityChoseKun.MessageID.OnDemandRenderingPull, new OnDemandRenderingKun(true));
        }


        public static void OnMessageEventPush(BinaryReader binaryReader)
        {
            var obj = new OnDemandRenderingKun();
            obj.Deserialize(binaryReader);
            obj.WriteBack();
            UnityChoseKunPlayer.SendMessage<OnDemandRenderingKun>(UnityChoseKun.MessageID.OnDemandRenderingPush, new OnDemandRenderingKun(true));
        }
    }
}