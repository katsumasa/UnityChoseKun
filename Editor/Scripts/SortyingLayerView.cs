using System.IO;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    using Engine;


    namespace Editor
    {
        [System.Serializable]
        public class SortingLayerView
        {

            static SortingLayerView mInstance;
            public static SortingLayerView instance
            {
                get
                {
                    if(mInstance == null)
                    {
                        mInstance = new SortingLayerView();
                    }
                    return mInstance;
                }
            }


            public void OnGUI()
            {
                if (SortingLayerKun.layers != null)
                {
                    foreach (var layer in SortingLayerKun.layers)
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
}