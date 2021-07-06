using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utj.UnityChoseKun
{

    [System.Serializable]
    public class SortingLayerView
    {
        public void OnGUI()
        {
            if(SortingLayerKun.layers != null)
            {
                foreach(var layer in SortingLayerKun.layers)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Layer  " + layer.id);
                    EditorGUILayout.TextField(layer.name);
                    EditorGUILayout.EndHorizontal();
                }
            }
            if (GUILayout.Button("Pull"))
            {
                var packet = new SortingLayerPacket();
                UnityChoseKunEditor.SendMessage<SortingLayerPacket>(UnityChoseKun.MessageID.SortingLayerPull, packet);
            }
        }

        public void OnMessageEvent(BinaryReader binaryReader)
        {
            var packet = new SortingLayerPacket();
            packet.Deserialize(binaryReader);
            SortingLayerKun.layers = packet.layers;
        }
    }
}