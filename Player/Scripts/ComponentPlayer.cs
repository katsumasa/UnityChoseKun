using System.IO;
using UnityEngine;


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
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            var sceneKun = new SceneKun(scene);
            UnityChoseKunPlayer.SendMessage<SceneKun>(UnityChoseKun.MessageID.GameObjectPull,sceneKun);            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public void OnMessageEventPush(BinaryReader binaryReader)
        {
            var gameObjectKun = new GameObjectKun();
            gameObjectKun.Deserialize(binaryReader);            
            foreach (var obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
            {
                var go = FindGameObjectInChildren(obj,gameObjectKun.instanceID);
                if(go != null){                    
                    gameObjectKun.WriteBack(go);
                    return;
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
