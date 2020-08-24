namespace Utj.UnityChoseKun
{    
    using UnityEngine;    
    using UnityEditor.Networking.PlayerConnection;


    public class UnityChoseKunEditor
    {
        public static void SendMessage<T>(UnityChoseKun.MessageID id,object obj)
        {
            var message = new UnityChoseKunMessageData();
            message.id = id;
            if (obj != null)
            {
                message.json = JsonUtility.ToJson(obj);
            }
            var json = JsonUtility.ToJson(message);
            var bytes = System.Text.Encoding.ASCII.GetBytes(json);
            EditorConnection.instance.Send(UnityChoseKun.kMsgSendEditorToPlayer, bytes);
        }

        public static void SendMessage(UnityChoseKun.MessageID id)
        {
            var message = new UnityChoseKunMessageData();
            message.id = id;            
            var json = JsonUtility.ToJson(message);
            var bytes = System.Text.Encoding.ASCII.GetBytes(json);
            EditorConnection.instance.Send(UnityChoseKun.kMsgSendEditorToPlayer, bytes);
        }

    }
}