using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    namespace Engine
    {
        using Rendering;


        public class GraphicsSettingsPlayer
        {
            public static void OnMessageEventPull(BinaryReader binaryReader)
            {               
                UnityChoseKunPlayer.SendMessage<GraphicsSettingsKun>(UnityChoseKun.MessageID.GraphicsSettingsPull, new GraphicsSettingsKun(true));
            }


            public static void OnMessageEventPush(BinaryReader binaryReader)
            {
                var obj = new GraphicsSettingsKun();
                obj.Deserialize(binaryReader);
                obj.WriteBack();
                UnityChoseKunPlayer.SendMessage<GraphicsSettingsKun>(UnityChoseKun.MessageID.GraphicsSettingsPush, new GraphicsSettingsKun(true));
            }

        }
    }
}
