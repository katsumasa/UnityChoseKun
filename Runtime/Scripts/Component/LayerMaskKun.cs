using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Utj.UnityChoseKun
{
    public partial class LayerMaskKun : ISerializerKun
    {
        private int m_Mask;
        private static string[] m_layerNames;
        private static int[] m_masks;
        

        public static implicit operator int(LayerMaskKun mask)
        {
            return mask.m_Mask;
        }

        public static implicit operator LayerMaskKun(int intVal)
        {
            LayerMaskKun mask = new LayerMaskKun();
            mask.m_Mask = intVal;
            return mask;
        }

        public int value
        {
            get { return m_Mask; }
            set { m_Mask = value; }
        }

        public LayerMaskKun()
        {
            if (m_layerNames == null)
            {
                m_layerNames = new string[32];
                m_masks = new int[32];                
                for (var i = 0; i < 32; i++)
                {
                    m_layerNames [i] = LayerMask.LayerToName(i);
                    m_masks[i] = LayerMask.GetMask(m_layerNames[i]);
                }
            }
        }

        public static int GetMask(params string[] layerNames)
        {
            if (layerNames == null) throw new ArgumentNullException("layerNames");

            int mask = 0;
            foreach (string name in layerNames)
            {
                int layer = NameToLayer(name);

                if (layer != -1)
                    mask |= 1 << layer;
            }
            return mask;
        }


        public static string LayerToName(int layer)
        {
            return m_layerNames[layer];
        }


        public static int NameToLayer(string layerName)
        {            
            for(var i = 0; i < m_layerNames.Length; i++)
            {
                if (m_layerNames.Equals(layerName))
                {
                    return i;
                }
            }            
            return -1;
        }

        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(m_Mask);
            for(var i = 0; i < 32; i++)
            {
                binaryWriter.Write(m_layerNames[i]);
                binaryWriter.Write(m_masks[i]);
            }
            
        }

        public virtual void Deserialize(BinaryReader binaryReader)
        {
            m_Mask =  binaryReader.ReadInt32();
            for(var i = 0; i < 32; i++)
            {
                m_layerNames[i] = binaryReader.ReadString();
                m_masks[i] = binaryReader.ReadInt32();
            }
        }

#if UNITY_EDITOR
        public static LayerMaskKun LayerMaskField(GUIContent label, LayerMaskKun layerMask)
        {
            List<string> layers = new List<string>();
            List<int> layerNumbers = new List<int>();

            for (var i = 0; i < 32; ++i)
            {
                string layerName = LayerMaskKun.LayerToName(i);
                if (!string.IsNullOrEmpty(layerName))
                {
                    layers.Add(layerName);
                    layerNumbers.Add(i);
                }
            }
            int maskWithoutEmpty = 0;
            for (var i = 0; i < layerNumbers.Count; ++i)
            {
                if (0 < ((1 << layerNumbers[i]) & layerMask.value))
                    maskWithoutEmpty |= 1 << i;
            }
            maskWithoutEmpty = EditorGUILayout.MaskField(label, maskWithoutEmpty, layers.ToArray());
            int mask = 0;
            for (var i = 0; i < layerNumbers.Count; ++i)
            {
                if (0 < (maskWithoutEmpty & (1 << i)))
                    mask |= 1 << layerNumbers[i];
            }
            layerMask.value = mask;
            return layerMask;
        }
#endif
    }
}
