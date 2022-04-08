using System.IO;
using UnityEngine;


namespace Utj.UnityChoseKun.Engine
{
    
    /// <summary>
    /// MonoBehaviourKunをSerialize/Deserializeする為のClass
    /// Programed by Katsumasa.Kimura
    /// </summary>
    [System.Serializable]
    public class MonoBehaviourKun : BehaviourKun
    {
        public MonoBehaviourKun():this(null){}
        

        public MonoBehaviourKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.MonoBehaviour;
        }


        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
        }


        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
        }
    }
}