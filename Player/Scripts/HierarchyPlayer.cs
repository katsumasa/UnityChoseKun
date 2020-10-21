using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class HierarchyMessage
    {
        public enum MessageID
        {
            Duplicate,
            Delete,
            CreateEmpty,
        }

        [SerializeField] public MessageID messageID;
        [SerializeField] public int baseID;
        [SerializeField] public int parentID;
        [SerializeField] public int classID;
    }



    public class HierarchyPlayer : BasePlayer
    {
        public void OnMessageEventPush(byte[] bytes)
        {
            HierarchyMessage message;

            UnityChoseKun.BytesToObject<HierarchyMessage>(bytes,out message);
            switch (message.messageID)
            {
                case HierarchyMessage.MessageID.Duplicate:
                    {
                        var go = FindGameObjectInScene(message.baseID);
                        if(go != null)
                        {
                            var clone = GameObject.Instantiate(go);
                            clone.transform.parent = go.transform.parent;
                            clone.transform.localPosition = go.transform.localPosition;
                            clone.transform.localRotation = go.transform.localRotation;
                            clone.transform.localScale = go.transform.localScale;
                        }
                    }
                    break;
            }

            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            var sceneKun = new SceneKun(scene);
            UnityChoseKunPlayer.SendMessage<SceneKun>(UnityChoseKun.MessageID.GameObjectPull, sceneKun);

        }


        /// <summary>
        /// instanceIDをキーにしてScene内のGameObjectを検索する
        /// </summary>
        /// <param name="instanceID">instanceID</param>
        /// <returns>instanceIDが一致するGameObject</returns>
        public static GameObject FindGameObjectInScene(int instanceID)
        {
            foreach (var obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
            {
                var go = FindGameObjectInChildren(obj, instanceID);
                if(go != null)
                {
                    return go;
                }
            }
            return null;
        }


        /// <summary>
        /// instanceIDをキーにしてGameObjectを検索する
        /// </summary>
        /// <param name="gameObject">検索の起点となるGameObject</param>
        /// <param name="instanceID">検索するinstanceID</param>
        /// <returns>instanceIDと一致するGameObject</returns>
        public static GameObject FindGameObjectInChildren(GameObject gameObject, int instanceID)
        {
            if (gameObject == null)
            {
                return null;
            }
            else if (gameObject.GetInstanceID() == instanceID)
            {
                return gameObject;
            }
            for (var i = 0; i < gameObject.transform.childCount; i++)
            {
                var result = FindGameObjectInChildren(gameObject.transform.GetChild(i).gameObject, instanceID);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
    }
}