using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    public class ScriptableObjectKun : ObjectKun
    {
        public ScriptableObjectKun(ScriptableObject scriptableObject):base(scriptableObject)
        {
            if (scriptableObject != null)
            {
                name = scriptableObject.name;
            }
        }
    }
}