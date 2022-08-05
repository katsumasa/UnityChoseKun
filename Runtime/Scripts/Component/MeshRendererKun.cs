using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;


namespace Utj.UnityChoseKun.Engine
{

    /// <summary>
    /// MeshRendererをSerialize/Deserializeする為のClass
    /// </summary>
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
    public class MeshRendererKun : RendererKun
    {
        public MeshRendererKun():this(null){}

        public MeshRendererKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.MeshRenderer;
        }


        /// <summary>
        /// Componentを書き戻す
        /// </summary>
        /// <param name="component">書き戻すComponent</param>
        /// <returns>結果 true : 下記戻した false : 書き戻す必要がなかった</returns>
        public override bool WriteBack(Component component){
            if(base.WriteBack(component)){
                return true;
            }
            return false;
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
