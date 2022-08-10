using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine
{    
    /// <summary>
    /// QualitySettingsPlayerのClass
    /// </summary>
    public static class QualitySettingsPlayer
    {
        public static void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<QualitySettingsKun>(UnityChoseKun.MessageID.QualitySettingsPull, new QualitySettingsKun(true));
        }


        public static void OnMessageEventPush(BinaryReader binaryReader)
        {
            var qualitySettingsKun = new QualitySettingsKun();
            qualitySettingsKun.Deserialize(binaryReader);
            qualitySettingsKun.WriteBack();
        }
    }
}