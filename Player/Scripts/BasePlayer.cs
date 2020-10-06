namespace Utj.UnityChoseKun
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking.PlayerConnection;

    public class BasePlayer
    {
        public void SendMessage<T>(UnityChoseKun.MessageID id,T obj)
        {
            var message = new UnityChoseKunMessageData();
            message.id = id;
            UnityChoseKun.ObjectToBytes<T>(obj, out message.bytes);

            byte[] bytes;
            UnityChoseKun.ObjectToBytes<UnityChoseKunMessageData>(message,out bytes);
            PlayerConnection.instance.Send(UnityChoseKun.kMsgSendPlayerToEditor, bytes);
        }

    }
}