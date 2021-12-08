using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace Utj.UnityChoseKun
{   
    /// <summary>
    /// 
    /// </summary>
    public class ComponentPlayer :  BasePlayer
    {                    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public void OnMessageEventPull(BinaryReader binaryReader)
        {            
            UnityChoseKunPlayer.SendMessage<SceneManagerKun>(UnityChoseKun.MessageID.GameObjectPull, new SceneManagerKun(true));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public void OnMessageEventPush(BinaryReader binaryReader)
        {
            var gameObjectKun = new GameObjectKun();
            gameObjectKun.Deserialize(binaryReader);
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                foreach(var obj in scene.GetRootGameObjects())
                {
                    var go = FindGameObjectInChildren(obj, gameObjectKun.instanceID);
                    if (go != null)
                    {
                        gameObjectKun.WriteBack(go);
                        return;
                    }
                }
            }

        }

        
        /// <summary>
        /// instanceIDをキーにしてGameObjectを再帰的に検索する
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="instanceID"></param>
        /// <returns></returns>
        private GameObject FindGameObjectInChildren(GameObject gameObject,int instanceID)
        {
            if(gameObject == null){
                return null;
            } else if(gameObject.GetInstanceID() == instanceID){
                return gameObject;
            }
            for(var i=0; i < gameObject.transform.childCount; i++){
                var result = FindGameObjectInChildren(gameObject.transform.GetChild(i).gameObject,instanceID) ;
                if(result != null){
                    return result;
                }
            }
            return null;
        }
    }
}
