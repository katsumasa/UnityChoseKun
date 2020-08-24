using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    public class MonoBehaviourKun : BehaviourKun
    {
        public MonoBehaviourKun():this(null){}
        public MonoBehaviourKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.MonoBehaviour;
        }
    }
}