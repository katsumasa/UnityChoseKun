namespace Utj.UnityChoseKun
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking.PlayerConnection;

    public class BasePlayer
    {
        public void SendMessage<T>(UnityChoseKun.MessageID id,object obj)
        {
            var message = new UnityChoseKunMessageData();
            message.id = id;
            message.json = JsonUtility.ToJson(obj);
            var json = JsonUtility.ToJson(message);
            var bytes = System.Text.Encoding.ASCII.GetBytes(json);
            PlayerConnection.instance.Send(UnityChoseKun.kMsgSendPlayerToEditor, bytes);
        }

    }
}