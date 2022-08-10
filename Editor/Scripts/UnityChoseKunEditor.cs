using System.IO;

namespace Utj.UnityChoseKun
{    

        
    public class UnityChoseKunEditor
    {
        public static void SendMessage<T>(UnityChoseKun.MessageID id,T obj) where T : ISerializerKun
        {
            using (var memory = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memory))
                {
                    writer.Write((int)id);
                    if (obj != null)
                    {
                        obj.Serialize(writer);
                    }
                    Utj.UnityChoseKun.Editor.UnityChoseKunEditorWindow.Send(memory.GetBuffer());
                }
            }
        }

        public static void SendMessage(UnityChoseKun.MessageID id)
        {
            using (var memory = new MemoryStream()) 
            {
                using (var writer = new BinaryWriter(memory)){                    
                    writer.Write((int)id);
                    var bytes = memory.ToArray();
                    Utj.UnityChoseKun.Editor.UnityChoseKunEditorWindow.Send(memory.GetBuffer());
                }
                
            }
        }
    }
}