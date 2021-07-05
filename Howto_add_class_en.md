# How to add new Class 

To add a new class to UnityChoseKun requires following steps:

- Create a Class for Serialize/Deserialize
- Create a Class to display in PlayerInspectorView
- Edit ComponentKun.cs
- Edit ComponentView.cs

Here is an example of how to add a Class named `Hoge` that inherits from `MonoBehaviour`.

Hoge is a string variable that contains the name text, which is a Class that outputs text to the console in Update and it's defined as:

```:Hoge.cs
using UnityEngine;

public class Hoge : MonoBehaviour
{
    public string text;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(text);    
    }
}
```

## Creating Class for Serialize/Deserialize

Need this Class to go back and forth between Unity Editor and the actual Device. These are the important points that need to be checked for this Class:

- The `namespace` is `Utj.UnityChoseKun`
- Add `System.Serializable`Attribute to the Class
- Define the variables you want to exchange between the Device and Unity Editor.  Also to add `Serializable` Attribute to the variable.
- Define Constructor. (We'll need to prepare two tpyes: 1.With no arguement 2.Original Class of Serialize as an argument)
- Override `WriteBack` Method
- Override `Serialize`/`Deserialize` Method 
- Override `Equals` Method
- Override `GetHashCode` Method

We'll be using `BinaryWriter`/`BinaryReader` for `Serialize`/`Deserialize` respectively. Note that you need to match the order of the variables to be processed. `WriteBack` Method describes the process of writing back the contents from HogeKun to Hoge. Hoge has the following implementation:

```:HogeKun.cs
using System.IO;
using UnityEngine;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class HogeKun : MonoBehaviourKun
    {
        [SerializeField] string text;

        public HogeKun(Hoge hoge):base(hoge)
        {
            componentKunType = ComponentKunType.Hoge;
            if (hoge != null)
            {
                text = hoge.text;
            }
            else
            {
                text = "";
            }
        }

        public HogeKun() : this(null)
        {
            text = "";
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(text);
        }


        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            text = binaryReader.ReadString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as HogeKun;
            if(other == null)
            {
                return false;
            }
            if(text.Equals(other.text) == false)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
```

## Editing ComponentKun.cs

Edit [ComponentKun.cs](https://github.com/katsumasa/UnityChoseKun/blob/master/Player/Scripts/Component/ComponentKun.cs) and register Hoge and HogeKun with UnityChoseKun.

### Definition of ComponentKunType

Add the definition of ClassType you want to add to `ComponentKunType`.

```:cs
public enum ComponentKunType {            
    Invalid = -1,
    Transform = 0,
    Camera,
    Light,            
    SkinnedMeshMeshRenderer,
    MeshRenderer,
    Renderer,

    Rigidbody,

    CapsuleCollider,
    MeshCollider,
    Collider,

    Animator,

    ParticleSystem,

    Hoge, // <== Add Hoge

    MissingMono,    // Component == null
    MonoBehaviour,
    Behaviour,
    Component,           
    
    Max,
};
```

### Add to componentPairDict

Register `ComponentKunType.Hoge`, `typeof(Hoge)`, `typeof(HogeKun)` in componentPairDict and associate them.

```:cs
static readonly Dictionary<ComponentKunType,ComponentPair> componentPairDict = new Dictionary<ComponentKunType, ComponentPair>() 
{
    // ...
    {ComponentKunType.Component,new ComponentPair(typeof(Component),typeof(ComponentKun))},
    {ComponentKunType.Hoge,new ComponentPair(typeof(Hoge),typeof(HogeKun))},    
};
```

### Add to GetComponentKunType

Associates `Component` with `ChoseKunType`.
Please note that the `Class` that you wish to add needs to be set before the `BaseClass`.
Hoge's Base Class is MonoBehaviour, so add it before MonoBehaviour.

```:cs
public static ComponentKunType GetComponentKunType(Component component)
{
    if(component == null)
    {
        return ComponentKunType.MissingMono;
    }
    // ...

    if (component is Hoge){return ComponentKunType.Hoge;} // <== Add

    if (component is MonoBehaviour){return ComponentKunType.MonoBehaviour;}
    if(component is Behaviour){return ComponentKunType.Behaviour;}
    if(component is Component){return ComponentKunType.Component;}
    return ComponentKunType.Invalid;

}
```

### Add Instantiate

```:cs
 public static ComponentKun Instantiate(ComponentKunType componentKunType)
{
    switch (componentKunType)
    {
        // ...
        case ComponentKunType.Hoge:
        {
            return new HogeKun();
        }
        // ...
    }
}
```

## Class to display in Player Inspector View

In this Class, you'll need to override the following Methods:

- OnGUI()
- GetComponentKun()/SetComponentKun()

### OnGUI

This is the content to be display/edit with Player Inspector.
Hoge displays only one string type variable.

### GetComponentKun/SetComponentKun

Allows to exchange the variables of ComponentKun and HogeKun.
HogeView.cs will look something similar to the following :
※Note that HogeView.cs must be created under th `Editor` folder.

```:HogeView.cs
using UnityEditor;

namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class HogeView : MonoBehaviourView
    {
        public HogeKun hogeKun
        {
            get { return behaviourKun as HogeKun; }
            set { behaviourKun = value; }
        }


        public override void SetComponentKun(ComponentKun componentKun)
        {
            hogeKun = (HogeKun)componentKun;
        }


        public override ComponentKun GetComponentKun()
        {
            return hogeKun;
        }


        public override bool OnGUI()
        {
            base.OnGUI();
            hogeKun.text = EditorGUILayout.TextField(hogeKun.text);
            return true;
        }
    }
}
```

## Edit ComponentView.cs

Edit [ComponentView.cs](https://github.com/katsumasa/UnityChoseKun/blob/master/Editor/Scripts/Component/ComponentView.cs) and register the Class for PlayerInspector that was created above.  
Add to componentViewTbls.

```:ComponentView.cs
static Dictionary<ComponentKun.ComponentKunType,System.Type> componentViewTbls = new Dictionary<BehaviourKun.ComponentKunType, System.Type>{
{
　// ...
  {ComponentKun.ComponentKunType.MissingMono,  typeof(MissingMonoView) },
  
  {ComponentKun.ComponentKunType.Hoge,typeof(HogeView) }, // <== Add
};
```

That'll be all.
The added Class will appear in PlayerInspector as the image shown below:

![image](https://user-images.githubusercontent.com/29646672/123935827-8b181100-d9cf-11eb-9060-d6b6a58be84b.png)
