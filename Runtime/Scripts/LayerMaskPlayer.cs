using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine
{
    public static class LayerMaskPlayer
    {       
        public static void OnMessageEventPull(BinaryReader binaryReader)
        {
            UnityChoseKunPlayer.SendMessage<LayerMaskKun>(UnityChoseKun.MessageID.LayerMaskPull, new LayerMaskKun());
        }
    }
}