namespace Utj.UnityChoseKun
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    
    public class ComponentPlayer :  BasePlayer
    {                           
        public void OnMessageEventPull(string json)
        {                
            Debug.Log("ComponentPlayer:OnMessageEventPull");            
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();            
            UnityChoseKunPlayer.SendMessage<SceneKun>(UnityChoseKun.MessageID.GameObjectPull,new SceneKun(scene));            
        }


        public void OnMessageEventPush(string json)
        {
            Debug.Log("OnMessageEventPush"); 
            var gameObjectKun = JsonUtility.FromJson<GameObjectKun>(json);
            foreach (var obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
            {
                var go = FindGameObjectInChildren(obj,gameObjectKun.instanceID);
                if(go != null){                    
                    gameObjectKun.WriteBack(go);
                    return;
                }
            }
        }


        // <summary> instanceIDをキーにしてGameObjectを再帰的に検索する </summary>
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
