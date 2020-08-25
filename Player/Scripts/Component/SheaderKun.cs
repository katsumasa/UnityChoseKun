using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    [System.Serializable]
    public class ShaderKun : ObjectKun
    {
        [SerializeField] bool m_isSupported ;
        public bool isSupported {
            get {return m_isSupported;}
            private set {m_isSupported = value;}
        }

        [SerializeField] int m_maximumLOD ;
        public int maximumLOD {
            get{return m_maximumLOD;}
            set{m_maximumLOD = value;}
        }
        [SerializeField] int m_passCount ;
        public int passCount {
            get{return m_passCount;}
            private set{m_passCount = value;}
        }

        [SerializeField] int m_renderQueue ;
        public int renderQueue {
            get{return m_renderQueue;}
            private set {m_renderQueue = value;}
        }

        public ShaderKun():this(null){}
        public ShaderKun(UnityEngine.Object obj):base(obj)
        {
            // TODO : プロパティの表示
            var shader = obj as Shader;
            if(shader){
                name = shader.name;
                isSupported = shader.isSupported;
                maximumLOD = shader.maximumLOD;
                passCount = shader.passCount;
                renderQueue = shader.renderQueue;
            }
        }
        
    }
}