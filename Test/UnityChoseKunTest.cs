using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    
    public class UnityChoseKunTest : MonoBehaviour
    {
        [SerializeField] Transform m_TargetTransform;


        // Start is called before the first frame update
        void Start()
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            var sceneKun = new SceneKun(scene);


            var transformKun = new TransformKun(transform);
            byte[] bytes;
            TransformKun transformKun2;

            UnityChoseKun.ObjectToBytes<TransformKun>(transformKun, out bytes);
            UnityChoseKun.BytesToObject<TransformKun>(bytes, out transformKun2);
            Debug.Log(transformKun2);

            var gameObjectKun1 = new GameObjectKun(gameObject);
            GameObjectKun gameObjectKun2;
            UnityChoseKun.ObjectToBytes<GameObjectKun>(gameObjectKun1, out bytes);
            UnityChoseKun.BytesToObject<GameObjectKun>(bytes, out gameObjectKun2);
            var json = JsonUtility.ToJson(gameObjectKun1);
            gameObjectKun2 = JsonUtility.FromJson<GameObjectKun>(json);


            

            transformKun2 = gameObjectKun2.transformKun;

            Debug.Log(gameObjectKun2);

            var meshCollider = GetComponent<MeshCollider>();
            if (meshCollider)
            {
                var meshColliderKun = new MeshColliderKun(meshCollider);
            }


            transformKun.GetTransform(m_TargetTransform.GetInstanceID());

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}