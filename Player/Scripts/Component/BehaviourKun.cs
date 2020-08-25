using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun {

    [System.Serializable]
    public class BehaviourKun : ComponentKun
    {
        [SerializeField] bool m_enabled;
        public bool enabled{
            get {return m_enabled;}
            set {m_enabled = value;}
        }
        
        
        public BehaviourKun():this(null){}
        public BehaviourKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.Behaviour;
            var behaviour = component as Behaviour;
            if(behaviour!=null){
                enabled = behaviour.enabled;                        
                name = behaviour.ToString();
            }
        }
        
        public override bool WriteBack(Component component)
        {
            if(base.WriteBack(component)){
                var behaviour = component as Behaviour;
                behaviour.enabled = enabled;
                return true;
            }
            return false;
        } 
    }
}