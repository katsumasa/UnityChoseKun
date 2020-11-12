using System.IO;
using UnityEditor.Networking.PlayerConnection;


namespace Utj.UnityChoseKun
{    

        
    public class UnityChoseKunEditor
    {
        public static void SendMessage<T>(UnityChoseKun.MessageID id,T obj) where T : ISerializerKun
        {
            var memory = new MemoryStream();
            var writer = new BinaryWriter(memory);

            try
            {
                writer.Write((int)id);
                if (obj != null)
                {
                    obj.Serialize(writer);
                }
                var bytes = memory.ToArray();
                EditorConnection.instance.Send(UnityChoseKun.kMsgSendEditorToPlayer, bytes);
            }
            finally
            {
                writer.Close();
                memory.Close();
            }            
        }

        public static void SendMessage(UnityChoseKun.MessageID id)
        {
            var memory = new MemoryStream();
            var writer = new BinaryWriter(memory);
            try
            {
                writer.Write((int)id);
                var bytes = memory.ToArray();
                EditorConnection.instance.Send(UnityChoseKun.kMsgSendEditorToPlayer, bytes);
            }
            finally
            {
                writer.Close();
                memory.Close();
            }
        }

    }
}