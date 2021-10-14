using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    /// <summary>
    /// QualitySettingsPlayerのClass
    /// </summary>
    public class QualitySettingsPlayer : BasePlayer
    {
        public void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<QualitySettingsKun>(UnityChoseKun.MessageID.QualitySettingsPull, new QualitySettingsKun(true));
        }


        public void OnMessageEventPush(BinaryReader binaryReader)
        {
            var qualitySettingsKun = new QualitySettingsKun();
            qualitySettingsKun.Deserialize(binaryReader);
            qualitySettingsKun.WriteBack();
        }
    }
}