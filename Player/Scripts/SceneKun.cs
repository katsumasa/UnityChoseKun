﻿namespace  Utj.UnityChoseKun 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [System.Serializable]
    public class SceneKun
    {
        public string name;
        public int rootCount;
        public GameObjectKun [] gameObjectKuns;     

        // コンストラクタ
        public SceneKun()
        {
            name = "None";
            rootCount = 0;
        }

        public SceneKun(Scene scene):this()
        {
            this.name = scene.name;
            this.rootCount = scene.rootCount;
            var  list =  new List<GameObjectKun>();                        
            foreach (var obj in scene.GetRootGameObjects())
            {
                AddGameObjectKunInChildren(obj,list);             
            }
            this.gameObjectKuns = list.ToArray();            
        }

        private void AddGameObjectKunInChildren(GameObject go,List<GameObjectKun> list)
        {            
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
    }


}