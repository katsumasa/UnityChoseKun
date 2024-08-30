# UnityChoseKun

![GitHub package.json version](https://img.shields.io/github/package-json/v/katsumasa/UnityChoseKun?style=plastic)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/katsumasa/UnityChoseKun?style=plastic)

[English Ver. README](Documentation~/UnityChoseKun.md)

## 概要

Unityで開発したアプリを実機上で実行している時に、GameObjectの内容をほんの少しだけ変更したいだけなのに、ビルド完了迄長時間待たされることで、貴重な開発時間を無駄にして悔しい思いをした事は無いでしょうか。
UnityChoseKunは再ビルドを行うこと無く、UnityEditor上で開発機で実行中のアプリの調整を行う為のEditor拡張です。

<img width="852" alt="UnityChoseKunDemo02" src="https://user-images.githubusercontent.com/29646672/137236126-f7b9c064-3dcc-41d5-9ce6-9f9175d9d315.gif">

## Demo

https://user-images.githubusercontent.com/29646672/220570479-85ea8eff-75cf-401b-92bb-4cb5ae4cd205.mp4


## このプロジェクトで出来ること

- 実機で再生されているSceneをHierarchy形式で表示(Player Hierarchy)
- GameObjectに含まれるComponentの内容等、アプリケーションの内容を編集し実機側へ反映させる。(Player Inspector)

## 動作環境

下記の内容で動作の確認済を行っています。

- Unityのバージョン
  - Unity2018.4.26f1 (一部、機能制限あり)
  - Unity2019.4.40f1
  - Unity2020.2.2f1
  - Unity2020.3.27f1
  - Unity2021.2.15f1
  - Unity2021.3.1f1
  - Unity2022.3.43f1
- プラットフォーム
  - Android
    - Pixel3XL
    - Pixel4XL
  - iOS
    - iPhone 6S

Universal RPに関しては下記の組み合わせで動作確認を行っています。

| Unity       | URP    |
|:-----------:|:------:|
| 2020.3.27f1 | 10.8.1 |
| 2021.2.15f1 | 12.1.5 |
| 2021.3.1f1  | 12.1.5 |

## 対応しているClassについて

- [Application](https://docs.unity3d.com/ja/current/ScriptReference/Application.html)
- [OnDemandRendering](https://docs.unity3d.com/ja/current/ScriptReference/Rendering.OnDemandRendering.html)
- [QualitySettings](https://docs.unity3d.com/ja/ScriptReference/QualitySettings.html)
- [Screen](https://docs.unity3d.com/ja/ScriptReference/Screen.html)
- [Shader](https://docs.unity3d.com/ja/ScriptReference/Shader.html)
- [Sprite](https://docs.unity3d.com/ja/current/ScriptReference/SpriteRenderer-sprite.html)
- [SystemInfo](https://docs.unity3d.com/ja/current/ScriptReference/SystemInfo.html)
- [ScalableBufferManager](https://docs.unity3d.com/ja/ScriptReference/ScalableBufferManager.html)
- [SortingLayer](https://docs.unity3d.com/ja/current/ScriptReference/SortingLayer.html)
- [Texture](https://docs.unity3d.com/ja/ScriptReference/Texture.html)
- [Time](https://docs.unity3d.com/ja/ScriptReference/Time.html)
- [Component](docs.unity3d.com/ja/ScriptReference/Component.html)
  - [Animator](https://docs.unity3d.com/ja/ScriptReference/Animator.html)
  - AnimationCurve
  - [Behaviour](https://docs.unity3d.com/ja/current/ScriptReference/Behaviour.html)
  - [Bounds](https://docs.unity3d.com/ja/current/ScriptReference/Bounds.html)
  - [Camera](https://docs.unity3d.com/ja/ScriptReference/Camera.html)
  - [Collider](https://docs.unity3d.com/ja/ScriptReference/Collider.html)
  - Keyframe
  - [Light](https://docs.unity3d.com/ja/ScriptReference/Light.html)
  - LayerMask
  - [Material](https://docs.unity3d.com/ja/ScriptReference/Material.html)
  - [Matrix4x4](https://docs.unity3d.com/ja/current/ScriptReference/Matrix4x4.html)
  - [MeshRenderer](https://docs.unity3d.com/ja/current/ScriptReference/MeshRenderer.html)
  - [MonoBehavior](https://docs.unity3d.com/ja/ScriptReference/MonoBehaviour.html)
  - [Object](https://docs.unity3d.com/ja/current/ScriptReference/Object.html)
  - [ParticleSystem](https://docs.unity3d.com/ja/ScriptReference/ParticleSystem.html)
  - [PhysicMaterial](https://docs.unity3d.com/ja/ScriptReference/PhysicMaterial.html)
  - [Quartanion](https://docs.unity3d.com/ja/ScriptReference/Quaternion.html)
  - [Rect](https://docs.unity3d.com/ja/ScriptReference/Rect.html)
  - [Renderer](https://docs.unity3d.com/ja/ScriptReference/Renderer.html)
  - [Resolution](https://docs.unity3d.com/ja/ScriptReference/Resolution.html)
  - [Rigidbody](https://docs.unity3d.com/ja/ScriptReference/Rigidbody.html)
  - [SkinnedMeshRenderer](https://docs.unity3d.com/ja/ScriptReference/SkinnedMeshRenderer.html)
  - [SpriteRenderer](https://docs.unity3d.com/ja/current/ScriptReference/SpriteRenderer.html)
  - TextureCurve
  - [Transform](https://docs.unity3d.com/ja/ScriptReference/Transform.html)
  - [Vector2](https://docs.unity3d.com/ja/ScriptReference/Vector2.html)
  - [Vector3](https://docs.unity3d.com/ja/ScriptReference/Vector3.html)
  - [Vector4](https://docs.unity3d.com/ja/ScriptReference/Vector4.html)
  - [UI](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/index.html)
    - [Canvas](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/class-Canvas.html)
- Render Core Library
  - Volume
  - VolumeProfile
  - VolumeComponent
  - VolumeParameter
    - [x] BoolParameter
    - [x] LayerMaskParameter
    - [x] IntParameter
    - [x] NoInterpIntParameter
    - [x] MinIntParameter
    - [x] NoInterpMinIntParameter
    - [x] MaxIntParameter
    - [x] NoInterpMaxIntParameter
    - [x] ClampedIntParameter
    - [x] NoInterpClampedIntParameter
    - [x] FloatParameter
    - [x] NoInterpFloatParameter
    - [x] MinFloatParameter
    - [x] NoInterpMinFloatParameter
    - [x] MaxFloatParameter
    - [x] NoInterpMaxFloatParameter
    - [x] ClampedFloatParameter
    - [x] NoInterpClampedFloatParameter
    - [x] FloatRangeParameter
    - [x] NoInterpFloatRangeParameter
    - [x] ColorParameter 
    - [x] NoInterpColorParameter
    - [x] Vector2Parameter
    - [x] NoInterpVector2Parameter
    - [x] Vector3Parameter
    - [x] NoInterpVector3Parameter
    - [x] Vector4Parameter
    - [x] NoInterpVector4Parameter
    - [ ] TextureParameter
    - [ ] NoInterpTextureParameter
    - [ ] Texture2DParameter
    - [ ] Texture3DParameter
    - [ ] RenderTextureParameter
    - [ ] NoInterpRenderTextureParameter
    - [ ] CubemapParameter
    - [ ] NoInterpCubemapParameter
    - [ ] AnimationCurveParameter
    - [ ] TextureCurveParameter
- Universal Render Pipeline
  - ScriptableRenderData
  - UniversalAdditionalCameraData
  - UniversalAdditionalLightData
  - UniversalRenderPipelineAsset
  - UniversalRenderPipelineGlobal

Classの追加方法に関してはこちらをご覧ください：
[Howto_add_class.md](https://github.com/katsumasa/UnityChoseKun/blob/master/Howto_add_class.md)

## 注意事項・免責事項

- 万が一*不測の事態が起きても一切保証は出来ませんのでご注意下さい。*
- Script Debuggingと併用できません。必ずScript DebuggingをOFFにした上でビルドを行ってください。
- Texture,Shaderに関しては内容の確認のみで、変更を行うことは出来ません。
- Materialが参照しているShader/Textureを変更する為にはTexture/ShaderのPullを先に実行する必要があります。
- Unity2018では以下の機能に対応していません。
  - Materialが参照しているTextureの差し替え

## セットアップ

UnityChoseKunは別途RemoteConnectパッケージを使用します。UnityChoseKunと合わせて取得して下さい。
取得したパッケージを任意のUnityプロジェクトへ追加することでセットアップは完了です。

### パッケージの取得方法

UnityChoseKunはGitHubのリポジトリで管理されています。
セットアップ方法にはいくつかありますが、PackageManagerから取得する方法がもっとも簡単でお勧めです。

#### コンソールからリポジトリを取得する

コンソールからリポジトリを取得する場合、下記のコマンドを実行します。

```:console
git clone https://github.com/katsumasa/RemoteConnect.git
git clone https://github.com/katsumasa/UnityChoseKun.git
```

#### GitHubから直接取得する

1. Webブラウザーで[UnityChoseKun](https://github.com/katsumasa/UnityChoseKun)と[RemotoConnect](https://github.com/katsumasa/RemoteConnect)のWebページを開く
2. 画面右上緑色の`Code`と記述されているプルダウンメニューから`Download ZIP`を選択しZIPファイルをダウンロード
3. ZIPファイルを解凍しUnityProjectのAssetフォルダ以下へ配置する。

#### [お勧め]PackageManagerから取得する

1. `Window > Package Manager`でPackage Managerを開く
2. Package Manager左上の`+`のプルダウンメニューから`Add package form git URL...`を選択する
3. ダイアログへhttps://github.com/katsumasa/RemoteConnect.git を設定し、`Add`ボタンを押す
4. Package Manager左上の`+`のプルダウンメニューから`Add package form git URL...`を選択する
5. ダイアログへhttps://github.com/katsumasa/UnityChoseKun.git を設定し、`Add`ボタンを押す

<img width="800" alt="image" src="https://user-images.githubusercontent.com/29646672/183788409-3c1e745a-ac84-49f0-96be-3c4d26ed369f.png">

### アプリケーションビルド時の設定

- 調整を行うSceneに[UnityChoseKun.prefab](https://github.com/katsumasa/UnityChoseKun/blob/master/Player/Prefabs/UnityChoseKun.prefab)を配置する。
- [Development BuildとAutoconnect Profiler](https://docs.unity3d.com/ja/current/Manual/BuildSettingsStandalone.html)にチェックを入れる。
- URPなどPackageManagerで管理されているClassの調整を行う場合、[Scripting BackEnd](https://docs.unity3d.com/ja/2018.4/Manual/windowsstore-scriptingbackends.html)にはMonoを指定する必要があります。

## 機能紹介

### Player Hierarchy

EditorのHierarchy同様にGameObjectの親子関係を変更したり、右クリックからのモーダルダイヤログでGameObjectを生成したり、基本的なComponentを追加することが出来ます。
Window > UTJ > UnityChoseKun > Player Hierarchyより起動します。

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/137240924-d089e4b6-9ff7-4bbe-ba31-f19cc7459aca.jpg">

#### Reload

実機で実行されているアプリケーションのScene情報を分析し、Hierarchy Treeとして展開します。
まず初めにReloadを実行し、Sceneの情報を取得することからスタートします。

##### *NOTE*

- GameObjectが数万個存在するような複雑なSceneの場合、Reloadには時間がかかることに注意して下さい。
- アプリケーションに含まれていないComponentを追加するとエラーとなります。

### Player Inspector

実機で実行しているアプリケーションに対する情報の表示や内容の編集を行う為のWindowです。
プルダウンメニューから表示する内容を切り替えて使用します。  
Window > UTJ > UnityChoseKun > Player Inspectorより起動します。

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183793924-afa0646c-5348-4c48-a19a-2934cc9bf5f3.png">

#### Inspector

Player Hierarchyで選択したGameObjectが持つComponentの内容を編集します。
すべてのComponetの内容を編集出来る訳ではなく、現時点では一部のComponentに限定されています。
非対応のComponentに関しては、Componentのenableのみ編集可能となっています。

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183794558-9c8eccb9-9c4c-41e7-902a-71f1c27eb340.png">

- [Connect To] : 接続先のデバイスを選択します。 Profilerと共有しています。
- [Add Component] : GameObjectにComponentを追加します。(未実装)

#### UnityEngine.Application

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183864921-576f98e7-012f-42a0-8d72-b70e17b9f89c.png">

Application Classのstaticメンバーに関する内容を確認することが出来ます。（編集は出来ません)
また、Application.Quit()を実行することが可能です。

- [Pull] : Application Classのstaticメンバーを取得します。
- [Quit] : Application.Quite()を実行します。

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183794833-2095509b-baf6-4bf8-a4ee-326b01c525e4.png">

### UnityEngine.Android.Permisson

プラットフォームがAndroidの場合、パーミッションの内容を確認することが出来ます。（編集は出来ません)

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183795746-54fefac2-22a5-4097-8b4c-435ba7d2839f.png">

- [Pull] : Android.Permissonのメンバーを取得します。

#### UnityEngine.Component

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183796160-8bf2b10a-78ba-4b8b-b5cc-21ed4b071bfb.png">

Scene上に存在するComponentの種類と数をカウントします。
本Editor拡張で未対応なComponentは規定Classにカウントします。

- [Analayze] : Player Hierarchyの情報からScene内のGameObjectに含まれているComponentを種類別にカウントします

#### UnityEngine.QualitySettings

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183796486-5b495e59-b575-4240-ba53-044ee76abefb.png">

QualitySettingの内容を確認することができます。（編集は出来ません)

- [Pull] : QualitySettingsのメンバーを取得します。

#### UnityEngine.Rendering.GraphicsSettings

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183797004-c39e487e-7192-4fa7-a41b-956e6e1ab2de.png">

GraphicsSettingsの内容を確認することができます。（編集は出来ません）

- [Pull] : GraphicsSettingsのメンバーを取得します。

##### UnityEngine.Rendering.OnDemandRendering

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183797592-040a4d0b-fb93-47ea-982b-8d700e58eb47.png">

OnDemandRenderingのパラメーターの内容の表示と編集を行うことが可能です。

- [renderFrameInterval] : 現在のフレームレートの間隔を取得または設定します。Application.targetFrameRateまたはQualitySettings.vSyncCountの値にレンダリングを戻すには、これを0または1に設定します。

#### ScalableBufferManager

<img width="385" alt="image" src="https://user-images.githubusercontent.com/29646672/183798870-74027094-e9ac-4a57-a213-c23cb9c55deb.png">

ScalableBufferManagerを編集することが出来ます。

- [Pull] : ScalableBufferManagerのパラメーターを取得します。

*NOTE*:この機能を使用する場合、下記の条件を満たしている必要があります。  

- Project Settings -> `Enable Frame Timing Stats`にチェックを入れた状態でアプリケーションをビルドしている。
- CameraもしくはRenderTextureの[Allow Dynamic Resolution](https://docs.unity3d.com/ja/2018.4/uploads/Main/DynamicResolution.png)が有効になっている。
- プラットフォーム(及びGraphicドライバ)が下記のいずれかである
  - Xbox One
  - PS4
  - Nintendo Switch
  - iOS/tvOS (Metal のみ)
  - Android (Vulkan のみ)

##### UnityEngine.Screen

Screen Classのstaticメンバーに関する内容を編集することが出来ます。

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183799340-576bbf10-1d98-4193-8451-d54a7fa1ae9d.png">

- [Pull] : Screen Classのstaticメンバーを取得します。
- [Push] : 編集した内容を実機上に書き戻します。

##### UnityEngine.Shader

アプリケーションから参照されているShader及びResourcesに含まれるShaderの一覧を表示します。

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183800592-298c8320-3ca9-41a1-aa63-b30509db0a3b.png">

- [Pull] : Scene上のGameObjectから参照及びResourcesに含まれるShaderの一覧を取得します。

※*Materialが参照しているShaderを変更する場合は、事前にPullを実行しておく必要があります。*

#### UnityEngine.SortingLayer

実機上の[SortingLayer.layers](https://docs.unity3d.com/ja/current/ScriptReference/SortingLayer-layers.html)を取得します。

<img width="385" alt="image" src="https://user-images.githubusercontent.com/29646672/183801143-980cabdf-785e-416b-b8f8-3d4485c83138.png">

- [Pull] : アプリケーションで使用されているSorting Layerの値を取得します。

※SpriteRendererのSortingLayerを変更する場合、事前にPULLを実行する必要があります。

##### UnityEngine.Sprite

実機で実行されているアプリのScene上から参照されているSprite及びResourcesに含まれるSpriteの一覧を表示します。

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183801515-e9023ac3-ffde-497f-9bf0-73abacd63c2b.png">

- [Pull] : アプリケーションで使用されているSpriteの一覧を取得します。

※SpriteRendererのSpriteを変更する場合事前にPullを実行する必要があります。

#### UnityEngine.SystemInfo

[SystemInfo](https://docs.unity3d.com/ja/current/ScriptReference/Device.SystemInfo.html)のメンバーの値を取得します。

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183801676-db187c2c-78b9-4dc0-87e8-6f6cda341808.png">

- [Pull] : 実行中のアプリケーションからSystemInfoのメンバーの値を取得します。

##### UnityEngine.Texture

Scene内から参照されているTextureとResourcesに含まれているTextureの一覧表示します。

<img width="400" alt="image" src="https://user-images.githubusercontent.com/29646672/183800913-c036d071-1c75-4d69-929b-3327c1f0b09a.png">

- [Pull] : Scene上のGameObjectから参照及びResourcesに含まれるTextureの一覧を取得します。
  
※*Materialが参照しているTextureを変更する場合は、事前にPullを実行しておく必要があります。*

##### UnityEngine.Time

<img width="385" alt="image" src="https://user-images.githubusercontent.com/29646672/183802337-3cd64b21-8b27-4111-b4b8-8b575e550ab5.png">

[Time](https://docs.unity3d.com/ja/current/ScriptReference/Time.html) Classのstaticメンバーに関する内容を編集することが出来ます。

- [Pull] : Time Classのstaticメンバーを取得します。
- [Push] : 編集した内容を実機上に書き戻します。

※編集可能なメンバーはRead Only以外のメンバーに限定されます。詳しくはスクリプトリファレンスをご確認下さい。

#### UnityEngine.Profiling.Profiler

UnityEngine.Profiling.ProfilerクラスのAPIを使用してMemory関連の情報を取得します。

<img width="400" alt="image" src="https://github.com/user-attachments/assets/cef5ef47-0d27-4f75-a6de-8cbac1d01476">

- [Pull] : 実行中にのアプリケーションからMemory関連の情報を取得します。
- [Export Objects RuntimeMemory] : `Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object))` で取得した全てのUnityEngine.Objectに対する`Profiler.GetRuntimeMemorySizeLong`の結果をCSV形式で出力します。


## FAQ

### [UnityPlayerSync](https://github.com/katsumasa/UnityPlayerSync)と[UnityChoseKun](https://github.com/katsumasa/UnityChoseKun)の違いを教えて下さい。

UnityChoseKunはアプリケーション上のHierarchyの情報及び必要最低限のComponentの情報を取得し、その情報をアプリケーションへダイレクトに反映し、UnityPlayerSyncはアプリケーションのHierarchyをUnityEditor上にそのまま再現し、変更された内容をアプリケーションに反映します。
その為、UnityPlayerSyncはUnityChoseKunよりも得られる情報量が多い一方、アプリケーションとUnityEditorの同期にかかる時間はUnityChoseKunの方が短くなっています。

例えば、アプリケーションのパラメーターを調整してパフォーマンスチューニングや見た目の調整を行うような用途であればUnityChoseKunが適しています。
一方、UnityPlayerSyncは通常のUnityEditorのワークフローと殆ど変わらないGUIで操作することが出来る為、エンジニア以外のクリエーターでも直観的に操作が出来るというメリットがあります。

## その他

- 要望・ご意見・不具合に関しては[Issue](https://github.com/katsumasa/UnityChoseKun/issues)から報告をお願いします。約束は出来ませんが可能な限り対応します。
- 不具合報告に関してはそれを再現する為のプロジェクトの添付及び再現手順などの記述をお願いします。こちらで再現が取れない場合、対応出来ない場合があります。
