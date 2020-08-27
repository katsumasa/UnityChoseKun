using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    public class Test2 : MonoBehaviour
    {
        [SerializeField]Material material;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(this.name);
            Debug.Log(typeof(MonoBehaviour).Name);
            Debug.Log(typeof(MonoBehaviour).FullName);

            var shaderPlayer = new ShaderPlayer();            
            shaderPlayer.GetAllShader();
            
            var shaderKun = new ShaderKun(material.shader);
            Debug.Log("");                        
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
