using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{
    public class ShaderKunPacket {
        public ShaderKun[] shaderKuns;
    }
    
    public class ShaderPlayer : BasePlayer
    {
        Dictionary<string,Shader> m_shaderDict;
        Dictionary<string,Shader> shaderDict{
            get{if(m_shaderDict == null){m_shaderDict = new Dictionary<string, Shader>();}return m_shaderDict;}
            set {m_shaderDict = value;}            
        }

        public ShaderPlayer():base(){
            shaderDict = new Dictionary<string, Shader>();
        }

        ~ShaderPlayer()
        {
            shaderDict = null;
        }

        public void OnMessageEventPull(string json){
            GetAllShader();
            var shaderKuns = new ShaderKun[shaderDict.Count];
            var  i = 0;
            foreach(var shader in shaderDict.Values){            
                shaderKuns[i] = new ShaderKun(shader);
                i++;                
            }
            var shaderKunPacket = new ShaderKunPacket();
            shaderKunPacket.shaderKuns = shaderKuns;
            SendMessage<ShaderKunPacket>(UnityChoseKun.MessageID.ShaderPull,shaderKunPacket);
        }

        public void OnMessageEventPush(string json){

        }

        public void GetAllShader()
        {
            GetAllShaderInResources();
            GetAllShaderInScene();
        }

        public void GetAllShaderInResources()
        {
            var shaders = Resources.FindObjectsOfTypeAll(typeof(Shader)) as Shader[];
            foreach(var shader in shaders)
            {
                if(shaderDict.ContainsKey(shader.name) == false){
                    shaderDict.Add(shader.name,shader);
                }
            }
        }


        public void GetAllShaderInScene()
        {            
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            var components = new List<Renderer>();
            foreach(var go in scene.GetRootGameObjects()){
                GetSComponentsInChildren<Renderer>(go,components);
            }
            foreach(var renderer in components){
                if(renderer.materials!=null){
                    for(var i = 0; i < renderer.materials.Length; i++){
                        var material = renderer.materials[i];
                        var shader = material.shader;
                        if(shaderDict.ContainsKey(shader.name) == false){
                            shaderDict.Add(shader.name,shader);
                        }
                    }
                }
            }

        }

        void GetSComponentsInChildren<T>(GameObject go,List<T> components)
        {            
            go.GetComponents<T>(components);
            for(var i = 0; i < go.transform.childCount; i++)
            {
                var child = go.transform.GetChild(i).gameObject;
                GetSComponentsInChildren<T>(child,components);
            }
        }
    }
}