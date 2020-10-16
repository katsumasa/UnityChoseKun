using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class MeshRendererKun :RendererKun
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
    }
}
