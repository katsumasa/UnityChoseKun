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
            Debug.Log("OnMessageEventPull");
            var list = new List<GameObjectKun>();
            foreach (var obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
            {
                AddGameObjectKunInChildren(obj,list);             
            }            
            var msg = new SceneKun();
            if(msg != null)
            {                
                msg.gameObjectKuns = list.ToArray();                
                UnityChoseKunPlayer.SendMessage<SceneKun>(UnityChoseKun.MessageID.GameObjectPull,msg);
            }
        }


        public void OnMessageEventPush(string json)
        {
            Debug.Log("OnMessageEventPush"); 
            var gameObjectKun = JsonUtility.FromJson<GameObjectKun>(json);
            foreach (var obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
            {
                var go = FindGameObjectInChildren(obj,gameObjectKun.instanceID);
                if(go != null){                    
                    gameObjectKun.StoreGameObject(go);
                    return;
                }
            }
        }

        private void AddGameObjectKunInChildren(GameObject go,List<GameObjectKun> list)
        {
            Debug.Log("AddAllGameObject");
            if(go == null || list == null){
                return;
            }            
            var gk = new GameObjectKun(go);
            list.Add(gk);
            if(go.transform != null){
                for(var i = 0; i < go.transform.childCount; i++)
                {
                    AddGameObjectKunInChildren(go.transform.GetChild(i).gameObject,list);
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
