using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utj.UnityChoseKun{    
    public class TexturePlayer : BasePlayer
    {
        [System.Serializable]
        public class TextureKunPacket {
            [SerializeField] bool m_isResources;
            [SerializeField] bool m_isScene;
            [SerializeField] TextureKun[] m_textureKuns;
            
            public bool isResources{
                get{return m_isResources;}
                set{m_isResources = value;}
            }
            public bool isScene{
                get{return m_isScene;}
                set{m_isScene = value;}
            }
            public TextureKun[] textureKuns{
                get{return m_textureKuns;}
                set{m_textureKuns = value;}
            }

            public TextureKunPacket(){}
            public TextureKunPacket(TextureKun[] textureKuns)
            {
                this.textureKuns = textureKuns;
            }
        }
        
        static Dictionary<int,Texture> m_textureDict;
        public static Dictionary<int,Texture> textureDict
        {
            get{if(m_textureDict == null){m_textureDict = new Dictionary<int, Texture>();}return m_textureDict;}
            private set{m_textureDict = value;}
        }
        

        ~TexturePlayer(){
            textureDict.Clear();
            textureDict = null;
        }


        public void OnMessageEventPull(string json){
            Debug.Log("TextureKunPlayer:TextureKunPlayer");
            TextureKunPacket textureKunPacket = JsonUtility.FromJson<TextureKunPacket>(json);
            if(textureKunPacket.isScene){
                GetAllTextureInScene();
            }
            if(textureKunPacket.isResources){
                GetAllTextureInResources();
            }
            var textureKuns = new TextureKun[textureDict.Count];
            var i = 0;
            foreach(var texture in textureDict.Values){
                textureKuns[i++] = new TextureKun(texture);                
            }
            
            textureKunPacket = new TextureKunPacket(textureKuns);              
            SendMessage<TextureKunPacket>(UnityChoseKun.MessageID.TexturePull,textureKunPacket);
        }


        public void GetAllTextureInResources()
        {
            var textures = Resources.FindObjectsOfTypeAll(typeof(Texture)) as Texture[];
            Debug.Log("Resourecs Texture count is " + textures.Length);

            foreach(var texture in textures)
            {
                if(textureDict.ContainsKey(texture.GetInstanceID()) == false){
                    textureDict.Add(texture.GetInstanceID(),texture);
                }
            }
        }
        

        public void GetAllTextureInScene()
        {            
            #if UNITY_2019_1_OR_NEWER
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
                        var cnt = shader.GetPropertyCount();
                        Debug.Log(shader.name + " cnt " + cnt);
                        for(var j = 0; j < cnt; j++){
                            var type = shader.GetPropertyType(j);
                            Debug.Log(type);
                            if(type != UnityEngine.Rendering.ShaderPropertyType.Texture){
                                continue;
                            }
                            var nameId = shader.GetPropertyNameId(j);
                            var texture = material.GetTexture(nameId);
                            Debug.Log("nameId:"+nameId);

                            if(textureDict.ContainsKey(texture.GetInstanceID())){
                                continue;
                            }
                            textureDict.Add(texture.GetInstanceID(),texture);
                        }            
                    }
                }
            }
            #endif
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
