namespace Utj.UnityChoseKun
{    
    using UnityEngine;    
    using UnityEditor.Networking.PlayerConnection;


    public class UnityChoseKunEditor
    {
        public static void SendMessage<T>(UnityChoseKun.MessageID id,T obj)
        {
            var message = new UnityChoseKunMessageData();
            message.id = id;
            if (obj != null)
            {
                UnityChoseKun.ObjectToBytes(obj,out message.bytes);
            }
            byte[] bytes;
            UnityChoseKun.ObjectToBytes<UnityChoseKunMessageData>(message, out bytes);


            {
                UnityChoseKunMessageData check1;
                UnityChoseKun.BytesToObject<UnityChoseKunMessageData>(bytes,out check1);

                T check2;

                UnityChoseKun.BytesToObject<T>(check1.bytes, out check2);
                Debug.Log(check2);
            }




            EditorConnection.instance.Send(UnityChoseKun.kMsgSendEditorToPlayer, bytes);
        }

        public static void SendMessage(UnityChoseKun.MessageID id)
        {
            var message = new UnityChoseKunMessageData();
            message.id = id;
            byte[] bytes;
            UnityChoseKun.ObjectToBytes<UnityChoseKunMessageData>(message, out bytes);
            EditorConnection.instance.Send(UnityChoseKun.kMsgSendEditorToPlayer, bytes);
        }

    }
}