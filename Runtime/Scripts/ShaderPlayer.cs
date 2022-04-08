using System.IO;
using System.Collections.Generic;
using UnityEngine;


namespace Utj.UnityChoseKun.Engine
{

    // <summary>
    // ShaderKunの配列を送る為の構造体
    // </summary>
    [System.Serializable]
    public class ShaderKunPacket : ISerializerKun
    {
        [SerializeField] public ShaderKun[] shaderKuns;


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            if(shaderKuns == null)
            {
                binaryWriter.Write(-1);
            } else
            {
                binaryWriter.Write(shaderKuns.Length);
                for(var i = 0; i < shaderKuns.Length; i++)
                {
                    shaderKuns[i].Serialize(binaryWriter);
                }
            }
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            var len = binaryReader.ReadInt32();
            if(len != -1)
            {
                shaderKuns = new ShaderKun[len];
                for(var i = 0; i < len; i++)
                {
                    shaderKuns[i] = new ShaderKun();
                    shaderKuns[i].Deserialize(binaryReader);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as ShaderKunPacket;
            if(other == null)
            {
                return false;
            }
            if(shaderKuns != null)
            {
                if(other.shaderKuns == null)
                {
                    return false;
                }
                if(shaderKuns.Length != other.shaderKuns.Length)
                {
                    return false;
                }
                for(var i = 0; i < shaderKuns.Length; i++)
                {
                    if (!ShaderKun.Equals(shaderKuns[i], other.shaderKuns[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
    

    /// <summary>
    /// 
    /// </summary>
    public class ShaderPlayer : BasePlayer
    {
        Dictionary<string,Shader> m_shaderDict;

        Dictionary<string,Shader> shaderDict{
            get{if(m_shaderDict == null){m_shaderDict = new Dictionary<string, Shader>();}return m_shaderDict;}
            set {m_shaderDict = value;}            
        }


        /// <summary>
        /// 
        /// </summary>
        public ShaderPlayer():base(){
            shaderDict = new Dictionary<string, Shader>();
        }


        /// <summary>
        /// 
        /// </summary>
        ~ShaderPlayer()
        {
            shaderDict = null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public void OnMessageEventPull(BinaryReader binaryReader){
            GetAllShader();
            var shaderKuns = new ShaderKun[shaderDict.Count];
            var  i = 0;
            foreach(var shader in shaderDict.Values){            
                shaderKuns[i] = new ShaderKun(shader);
                i++;                
            }
            var shaderKunPacket = new ShaderKunPacket();
            shaderKunPacket.shaderKuns = shaderKuns;
            UnityChoseKunPlayer.SendMessage<ShaderKunPacket>(UnityChoseKun.MessageID.ShaderPull,shaderKunPacket);
        }

        
        /// <summary>
        /// 
        /// </summary>
        public void GetAllShader()
        {
            GetAllShaderInResources();
            GetAllShaderInScene();
        }


        /// <summary>
        /// 
        /// </summary>
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


        /// <summary>
        /// 
        /// </summary>
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


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <param name="components"></param>
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