using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine
{

    public class AssetPlayer<A, B> : BasePlayer
        where A : UnityEngine.Object
        where B : ObjectKun, new()
    {
        static Dictionary<int, A> m_assetDict;
        public Dictionary<int, A> assetDict
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


        UnityChoseKun.MessageID m_messageID;



        public AssetPlayer(UnityChoseKun.MessageID messageID)
        {
            m_messageID = messageID;
        }

        ~AssetPlayer()
        {
            for (var i = 0; i < assetDict.Count; i++)
            {
                if (assetDict[i] != null)
                {
                    //Object.Destroy(assetDict[i]);
                    assetDict[i] = null;
                }
            }
            assetDict.Clear();
            assetDict = null;
        }


        public void OnMessageEventPull(BinaryReader binaryReader)
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
            UnityChoseKunPlayer.SendMessage<AssetPacket<B>>(m_messageID, assetPacket);
        }


        public void GetAllAssetInResources()
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