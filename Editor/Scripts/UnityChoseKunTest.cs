using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using Utj.UnityChoseKun;
using UnityEngine;


public class UnityChoseKunTest
{
    static readonly string mTestSceneName = "Assets/Scenes/Character Setup.unity";
    //static readonly string mTestSceneName = "Assets/Scenes/Main.unity";

    [Test]
    public void ObjectKunTest()
    {
        SerializerKunTest<ObjectKun>(new ObjectKun(), new ObjectKun());
    }


    [Test]
    public void ComponentKunTest()
    {
        var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);
        var array = GetComponentAllInScene<Component>();
        foreach (var component in array)
        {
            SerializerKunTest<ObjectKun>(new ComponentKun(component), new ComponentKun());
        }
    }

    [Test]
    public void AnimatorKunTest()
    {
        var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);
        var array = GetComponentAllInScene<Animator>();
        foreach (var animator in array)
        {
            Debug.Log(animator.name);
            SerializerKunTest<AnimatorKun>(new AnimatorKun(animator), new AnimatorKun());
        }
    }


    [Test]
    public void Vector2KunTest()
    {        
        SerializerKunTest<Vector2Kun>(new Vector2Kun(1, 2), new Vector2Kun());
    }


    [Test]
    public void Vector3KunTest()
    {

        SerializerKunTest<Vector3Kun>(new Vector3Kun(1, 2, 3), new Vector3Kun());
    }


    [Test]
    public void Vector4KunTest()
    {

        SerializerKunTest<Vector4Kun>(new Vector4Kun(1, 2,3,4), new Vector4Kun());
    }

    [Test]
    public void BoundsKunTest()
    {
        SerializerKunTest<BoundsKun>(new BoundsKun(new Vector3Kun(1,2,3),new Vector3Kun(4,5,6)),new BoundsKun());
    }


    [Test]
    public void ColliderKunTest()
    {

        var colliderKun = new ColliderKun();
        colliderKun.enabled = true;
        colliderKun.material = "Material";
        SerializerKunTest<ColliderKun>(colliderKun, new ColliderKun());
    }


    [Test]
    public void CapselColliderKunTest()
    {
        var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);
        var array = GetComponentAllInScene<CapsuleCollider>();
        foreach (var component in array)
        {
            SerializerKunTest<CapsuleColliderKun>(new CapsuleColliderKun(component), new CapsuleColliderKun());
        }
    }


    [Test]
    public void MeshColliderKunTest()
    {
        var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);
        var array = GetComponentAllInScene<MeshCollider>();
        foreach (var component in array)
        {
            SerializerKunTest<MeshColliderKun>(new MeshColliderKun(component), new MeshColliderKun());
        }
    }


    [Test]
    public void ColorKunTest()
    {
        SerializerKunTest<ColorKun>(new ColorKun(1,1,1,1),new ColorKun());
    }


    [Test]
    public void BehaviourKunTest()
    {
        var behaviourKun = new BehaviourKun();
        behaviourKun.enabled = true;
        SerializerKunTest<BehaviourKun>(behaviourKun, new BehaviourKun());
    }


    [Test]
    public void LightKunTest()
    {
        SerializerKunTest<LightKun>(new LightKun(), new LightKun());
    }


    [Test]
    public void TextureKunTest()
    {
        SerializerKunTest<TextureKun>(new TextureKun(), new TextureKun());
    }


    [Test]
    public void MaterialKunTest()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/Character Setup.unity");       
        var renderers = UnityEngine.Component.FindObjectsOfType<Renderer>();
        foreach (var renderer in renderers)
        {
            SerializerKunTest<MaterialKun>(new MaterialKun(renderer.sharedMaterial), new MaterialKun());
        }
    }


    [Test]
    public void Matrix4x4KunTest()
    {
        var matrixKun = new Matrix4x4Kun(new Vector4Kun(1, 2, 3, 4), new Vector4Kun(5, 6, 7, 8), new Vector4Kun(9, 10, 11, 12), new Vector4Kun(13, 14, 15, 16));
        SerializerKunTest<Matrix4x4Kun>(matrixKun, new Matrix4x4Kun());
    }


    [Test]
    public void RectKunTest()
    {
        SerializerKunTest<RectKun>(new RectKun(), new RectKun());
    }


    [Test]
    public void RendererKunTest()
    {
        SerializerKunTest<RendererKun>(new RendererKun(), new RendererKun());
    }

    [Test]
    public void QuaternionKunTest()
    {
        SerializerKunTest<QuaternionKun>(new QuaternionKun(1,2,3,4), new QuaternionKun());
    }


    [Test]
    public void RigidbodyKunTest()
    {
        SerializerKunTest<RigidbodyKun>(new RigidbodyKun(), new RigidbodyKun());
    }

    
    [Test]
    public void ShaderKunTest()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);
        var renderers = UnityEngine.Component.FindObjectsOfType<Renderer>();
        foreach (var renderer in renderers)
        {
            SerializerKunTest<ShaderKun>(new ShaderKun(renderer.sharedMaterial.shader), new ShaderKun());            
        }        
    }


    [Test]
    public void TransformKunTest()
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        var transforms = UnityEngine.Component.FindObjectsOfType<UnityEngine.Transform>();
        foreach (var transform in transforms)
        {
            SerializerKunTest<TransformKun>(new TransformKun(transform), new TransformKun());
        }
    }


    [Test]
    public void SkinnedMeshRendererKunTest()
    {
        var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);        
        var components = UnityEngine.Component.FindObjectsOfType<UnityEngine.SkinnedMeshRenderer>();

        foreach (var component in components)
        {
            SerializerKunTest<SkinnedMeshRendererKun>(new SkinnedMeshRendererKun(component), new SkinnedMeshRendererKun());
        }
    }


    [Test]
    public void CameraKunTest()
    {
        var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);
        var array = GetComponentAllInScene<Camera>();
        foreach (var component in array) {
            SerializerKunTest<CameraKun>(new CameraKun(component), new CameraKun());
        }
    }


    [Test]
    public void MeshRendererKunTest()
    {
        var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);
        var meshRendererskinnedMeshRenderer = UnityEngine.Component.FindObjectOfType<UnityEngine.MeshRenderer>();

        UnityEngine.Assertions.Assert.IsNotNull(meshRendererskinnedMeshRenderer);
        SerializerKunTest<MeshRendererKun>(new MeshRendererKun(meshRendererskinnedMeshRenderer),new MeshRendererKun());
    }

    [Test]
    public void MonoBehaviourKunTest()
    {
        SerializerKunTest<MonoBehaviourKun>(new MonoBehaviourKun(),new MonoBehaviourKun());
    }


    [Test]
    public void ParticleSystemKunTest()
    {
        var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);
        var components = UnityEngine.Component.FindObjectsOfType<UnityEngine.ParticleSystem>();

        foreach (var component in components)
        {
            SerializerKunTest<ParticleSystemKun>(new ParticleSystemKun(component), new ParticleSystemKun());
        }
    }


    [Test]
    public void PhysicMaterialKunTest()
    {

        SerializerKunTest<PhysicMaterialKun>(new PhysicMaterialKun(), new PhysicMaterialKun());
    }


    [Test]
    public void ResolutionKunTest()
    {
        SerializerKunTest<ResolutionKun>(new ResolutionKun(128,128,30), new ResolutionKun());
    }


    [Test]
    public void SceneKunTest()
    {
        var scene  = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);

        //var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        var sceneKun = new SceneKun(scene);
        SerializerKunTest<SceneKun>(sceneKun, new SceneKun());
    }


    [Test]
    public void GameObjectKunTest()
    {
        var list = new List<GameObject>();
        var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(mTestSceneName);
        foreach(var go in scene.GetRootGameObjects())
        {
            AddGameObjectKunInChildren(go, list);
        }
#if false
        var gameObjectKun1 = new GameObjectKun(list[46]);
        SerializerKunTest<GameObjectKun>(gameObjectKun1, new GameObjectKun());

#else
        for (var i = 0; i < list.Count; i++)
        {
            Debug.Log(i + " " + list[i].name);
            var gameObjectKun1 = new GameObjectKun(list[i]);            
            SerializerKunTest<GameObjectKun>(gameObjectKun1, new GameObjectKun());
        }
#endif
    }

    
    public void SerializerKunTest<T>(T objectKun, T cloneKun) where T : ISerializerKun
    {
        var memory = new MemoryStream();
        var writer = new BinaryWriter(memory);
        byte[] bytes;
        try
        {
            objectKun.Serialize(writer);
            bytes = memory.ToArray();
        }
        finally
        {         
            writer.Close();
            memory.Close();
        }


        memory = new MemoryStream(bytes);
        var reader = new BinaryReader(memory);
        try
        {
            cloneKun.Deserialize(reader);
        }
        finally
        {
            reader.Close();
            memory.Close();
        }
        UnityEngine.Assertions.Assert.AreEqual(objectKun, cloneKun);
    }


    private void AddGameObjectKunInChildren(GameObject go, List<GameObject> list)
    {
        if (go == null || list == null)
        {
            return;
        }
        
        list.Add(go);
        if (go.transform != null)
        {
            for (var i = 0; i < go.transform.childCount; i++)
            {
                AddGameObjectKunInChildren(go.transform.GetChild(i).gameObject, list);
            }
        }
    }


    T[] GetComponentAllInScene<T>()where T : Component
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        var list = new List<T>();
        foreach (var go in scene.GetRootGameObjects())
        {
            var array = go.GetComponentsInChildren<T>();
            if (array != null && array.Length != 0)
            {
                list.AddRange(array);
            }
        }
        return list.ToArray();
    }
}
