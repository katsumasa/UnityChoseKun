# Classの追加方法

UnityChoseKunに新しいClassを追加する場合、次の作業が必要となります。

- Serialize/Desrialize用のClassの作成
- PlayerInspecterViewに表示する為のClassの作成
- ComponentKun.csの編集
- ComponentView.csの編集

ここでは、 `MonoBehaviour`を継承した`Hoge`という名前のClassを追加する方法を例とします。

Hogeはstring型のtextという名前の変数を持ち、Update内でコンソールにtextを出力するだけのClassで、次のように定義されています。

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

## Serialize/Desrialize用のClassの作成

UnityEditorとDeviceの間を行き来する為のClassを作成します。このClassで重要なポイントは下記の通りです。

- `namespace`が`Utj.UnityChoseKun`である
- classに`System.Serializable`Attributeを追加する
- DeviceとUnityEditor間でやり取りを行いたい変数を定義する。変数には`Serializable` Attributeがを追加する。
- コンストラクタの定義（コンストラクタは引数無しとSerialize元のClassを引数とした２種類を用意します。）
- `WriteBack` Methodのoverride
- `Serialize`/`Deserialize` Methodのoverride
- `Equals`Methodのoverride
- `GetHashCode`Methodのoverride

`Serialize`/`Deserialize`にはそれぞれ`BinaryWriter`/`BinaryReader`を使用します。
処理する変数の順番を一致させる必要があることに注意して下さい。
`WriteBack`MethodではHogeKunからHogeへ内容を書き戻す処理を記載します。

実際のHogeKunは下記のような実装になります。

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


        public override bool WriteBack(Component component)
        {
            base.WriteBack(component);
            var hoge = component as Hoge;
            hoge.text = text;

            return true;
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

## ComponentKun.csの編集

[ComponentKun.cs](https://github.com/katsumasa/UnityChoseKun/blob/master/Player/Scripts/Component/ComponentKun.cs)を編集し、HogeとHogeKunをUnityChoseKunに登録します。

### ComponentKunTypeの定義

`ComponentKunType`に追加したいClassTypeの定義を追加します。

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

### componentPairDictへの追加

`ComponentKunType.Hoge`,`typeof(Hoge)`,`typeof(HogeKun)`をcomponentPairDictへ登録して関連付けを行います。

```:cs
static readonly Dictionary<ComponentKunType,ComponentPair> componentPairDict = new Dictionary<ComponentKunType, ComponentPair>() 
{
    // 省略
    {ComponentKunType.Component,new ComponentPair(typeof(Component),typeof(ComponentKun))},
    {ComponentKunType.Hoge,new ComponentPair(typeof(Hoge),typeof(HogeKun))},    
};
```

### GetComponentKunTypeへの追加

`Component`と`ChoseKunType`の関連付けを行います。
注意すべき点としては、追加する`Class`は`BaseClass`の手前に設定する必要があるということです。
HogeのBase ClassはMonoBehaviourですのでMonoBehaviourの手前に追加します。

```:cs
public static ComponentKunType GetComponentKunType(Component component)
{
    if(component == null)
    {
        return ComponentKunType.MissingMono;
    }
    // 省略

    if (component is Hoge){return ComponentKunType.Hoge;} // <== 追加

    if (component is MonoBehaviour){return ComponentKunType.MonoBehaviour;}
    if(component is Behaviour){return ComponentKunType.Behaviour;}
    if(component is Component){return ComponentKunType.Component;}
    return ComponentKunType.Invalid;

}
```

### Instantiateの追加

```:cs
 public static ComponentKun Instantiate(ComponentKunType componentKunType)
{
    switch (componentKunType)
    {
        // 省略
        case ComponentKunType.Hoge:
        {
            return new HogeKun();
        }
        // 省略
    }
}
```

## Player Inspector Viewへの表示を行うClass

このClassでは下記のMethodをoverrideする必要があります。

- OnGUI()
- GetComponentKun()/SetComponentKun()

### OnGUI

Player Inspectorで表示・編集を行う内容です。
Hogeではstring型の変数を一つ表示するだけです。

### GetComponentKun/SetComponentKun

ComponentKun型とHogeKun型の変数のやり取りを行う変数です。
HogeView.csは下記のようになります。
※HogeView.csは`Editor`フォルダ以下へ作成する必要があることに注意して下さい。

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

## ComponentView.csの編集

[ComponentView.cs](https://github.com/katsumasa/UnityChoseKun/blob/master/Editor/Scripts/Component/ComponentView.cs)を編集して上記で作成したPlayerInspector用のClassを登録します。
追加する箇所はcomponentViewTblsです。

```:ComponentView.cs
static Dictionary<ComponentKun.ComponentKunType,System.Type> componentViewTbls = new Dictionary<BehaviourKun.ComponentKunType, System.Type>{
{
　// 省略
  {ComponentKun.ComponentKunType.MissingMono,  typeof(MissingMonoView) },
  
  {ComponentKun.ComponentKunType.Hoge,typeof(HogeView) }, // <== Add
};
```

以上で完了です。
追加したClassはPlayerInspecterで下記のように表示されます。

![image](https://user-images.githubusercontent.com/29646672/123935827-8b181100-d9cf-11eb-9060-d6b6a58be84b.png)
