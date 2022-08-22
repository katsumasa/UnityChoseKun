using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine
{

    public static class AssetPlayer<A, B>
        where A : UnityEngine.Object
        where B : ObjectKun, new()
    {
        static Dictionary<int, A> m_assetDict;
        public static Dictionary<int, A> assetDict
        {
            get
            {
                if (m_assetDict == null)
                {
                    m_assetDict = new Dictionary<int, A>();
                }
                return m_assetDict;
            }

            set
            {
                m_assetDict = value;
            }
        }

        public static T Constructer<T, ARG>(ARG arg)
        {
            return (T)typeof(T).GetConstructor(new Type[] { typeof(ARG) }).Invoke(new object[] { arg });
        }
               


        public static void OnMessageEventPull(BinaryReader binaryReader)
        {
            var assetPacket = new AssetPacket<B>();
            assetPacket.Deserialize(binaryReader);
            if (assetPacket.isResources)
            {
                GetAllAssetInResources();
            }

            var assetKuns = new B[assetDict.Count];
            var i = 0;
            foreach (var asset in assetDict.Values)
            {
                // ジェネリック引数付きのコンストラクター
                assetKuns[i++] = Constructer<B, A>(asset);                
            }
            assetPacket = new AssetPacket<B>(assetKuns);

            var messageID = UnityChoseKun.MessageID.SpritePull;
            if (typeof(A)  ==  typeof(Sprite))
            {
                messageID = UnityChoseKun.MessageID.SpritePull;
            }

            UnityChoseKunPlayer.SendMessage<AssetPacket<B>>(messageID, assetPacket);
        }


        public static void GetAllAssetInResources()
        {
            var assets = Resources.FindObjectsOfTypeAll<A>();
            foreach (var asset in assets)
            {
                if (assetDict.ContainsKey(asset.GetInstanceID()) == false)
                {
                    assetDict.Add(asset.GetInstanceID(), asset);
                }
            }

            assets = Resources.LoadAll<A>("");
            foreach (var asset in assets)
            {
                if (assetDict.ContainsKey(asset.GetInstanceID()) == false)
                {
                    assetDict.Add(asset.GetInstanceID(), asset);
                }
            }

            foreach (var asset in assetDict.Values)
            {
                ObjectKun.AddCache(asset);
            }            
        }  
    }
}