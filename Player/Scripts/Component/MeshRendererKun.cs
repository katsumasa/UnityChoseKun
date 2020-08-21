using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    public class MeshRendererKun :RendererKun
    {
        public MeshRendererKun():this(null){}

        public MeshRendererKun(Component component):base(component)
        {
            //componentKunType = ComponentKunType.MeshRenderer;
        }
    }
}
