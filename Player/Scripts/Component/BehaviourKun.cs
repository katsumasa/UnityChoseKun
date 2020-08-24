using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun {

    [System.Serializable]
    public class BehaviourKun : ComponentKun
    {
                
        public bool enabled;
        
        
        public BehaviourKun():this(null){}
        public BehaviourKun(Component component):base(component)
        {
            var behaviour = component as Behaviour;
            if(behaviour!=null){
                enabled = behaviour.enabled;                        
                var systemType = BehaviourKun.GetComponentSystemType(behaviour);
                //name = systemType.Name;
                name = behaviour.ToString();
            }
        }
        
        public override void WriteBack(Component component)
        {
            base.WriteBack(component);
            var behaviour = component as Behaviour;
            behaviour.enabled = enabled;
        }        
    }
}