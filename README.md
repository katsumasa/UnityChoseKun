# UnityChoseKun

![GitHub package.json version](https://img.shields.io/github/package-json/v/katsumasa/UnityChoseKun?style=plastic)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/katsumasa/UnityChoseKun?style=plastic)

[English Ver. README](Documentation~/UnityChoseKun.md)

## はじめに

Unityで開発したアプリを実機上で実行している時に、GameObjectの内容をほんの少しだけ変更したいだけなのに、ビルド完了迄長時間待たされることで、貴重な開発時間を無駄にして悔しい思いをした事は無いでしょうか。
UnityChoseKunは再ビルドを行うこと無く、UnityEditor上で開発機で実行中のアプリの調整を行う為のEditor拡張です。
開発機が物理的に遠い場所にある人向けに実機で時以降中の画面をUnity Editorで表示する機能も保有しています。

## 概要

実機上で実行しているアプリ内の画面をUnityEditorで表示たり、GameObjectをUnityEditorから調整する事が出来るEditor拡張です。

実機で実行中のアプリ内のComponentを調整</br>
<img width="800" alt="UnityChoseKunDemo02" src="https://user-images.githubusercontent.com/29646672/137236126-f7b9c064-3dcc-41d5-9ce6-9f9175d9d315.gif">

実機(開発機)の画面をUnityEditorに表示</br>
<img width="800" alt="UnityChoseKunDemo03" src="https://user-images.githubusercontent.com/29646672/137236618-7539f774-b200-45e9-a4d5-87e7ceb6b208.gif">

## このプロジェクトで出来ること

本ツールは主に下記の３機能から構成されています。

- 実機で再生されている画面を表示する(Player View)
- 実機で再生されているSceneをHierarchy形式で表示(Player Hierarchy)
- GameObjectに含まれるComponentの内容を編集し実機側へ反映させる。(Player Inspector)

## 動作環境

下記の内容で動作の確認済を行っています。

- Unityのバージョン
  - Unity2018.4.26f1 (一部、機能制限あり)
  - Unity2019.4.11f1
  - Unity2020.2.2f1
  - Unity2020.3.18f1
  - Unity2021.1.0a8
- プラットフォーム
  - Android
    - Pixel3XL
    - Pixel4XL
  - iOS
    - iPhone 6S

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
  - [Behaviour](https://docs.unity3d.com/ja/current/ScriptReference/Behaviour.html)
  - [Bounds](https://docs.unity3d.com/ja/current/ScriptReference/Bounds.html)
  - [Camera](https://docs.unity3d.com/ja/ScriptReference/Camera.html)
  - [Collider](https://docs.unity3d.com/ja/ScriptReference/Collider.html)
  - [Light](https://docs.unity3d.com/ja/ScriptReference/Light.html)
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
  - [Transform](https://docs.unity3d.com/ja/ScriptReference/Transform.html)
  - [Vector2](https://docs.unity3d.com/ja/ScriptReference/Vector2.html)
  - [Vector3](https://docs.unity3d.com/ja/ScriptReference/Vector3.html)
  - [Vector4](https://docs.unity3d.com/ja/ScriptReference/Vector4.html)
  - [UI](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/index.html)
    - [Canvas](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/class-Canvas.html)

## 注意事項・免責事項

- 万が一*不測の事態が起きても一切保証は出来ませんのでご注意下さい。*
- Script Debuggingと併用できません。必ずScript DebuggingをOFFにした上でビルドを行ってください。
- Player Viewを有効にすると*端末が高温になります。* また、画面解像度に応じてCPUリソースを消費します。PlayerViewの再生を行う前に、Player InspectorのScreenから画面解像度を変更することをお勧めします。Pixel4XLの場合、画面解像度を604x288迄落とした場合、CPUリソースにほぼ消費せずに実行できているようでした。
- Texture,Shaderに関しては内容の確認のみで、変更を行うことは出来ません。
- Materialが参照しているShader/Textureを変更する為にはTexture/ShaderのPullを先に実行する必要があります。
- Unity2018では以下の機能に対応していません。
  - Materialが参照しているTextureの差し替え
  - PlayerViewのAsync GPU Readback機能
  
  - ~~DeviceとUnityChoseKunが接続したままの状態で、UnityEditorを終了させると`EditorConnection.instance.DisconnectAll()`内でクラッシュが発生します。これを防ぐ為には、UnityChoseKunの接続先をEditorに変更するなどしてDeviceとの接続を解除する必要があります。~~
  DisconectAll()の実行をコメントアウトしました。これでしばらく様子をみます。

## 使い方

本リポジトリの内容を組込み先のUnityProjectのAssetフォルダ以下に丸々配置して下さい。

### アプリケーションビルド時の設定

- 調整を行うSceneに[UnityChoseKun.prefab](https://github.com/katsumasa/UnityChoseKun/blob/master/Player/Prefabs/UnityChoseKun.prefab)を配置してからアプリをビルドする必要があります。
- [Development BuildとAutoconnect Profiler](https://docs.unity3d.com/ja/current/Manual/BuildSettingsStandalone.html)にチェックを入れてる必要があります。
- [Scripting BackEnd](https://docs.unity3d.com/ja/2018.4/Manual/windowsstore-scriptingbackends.html)にはIL2CPPを指定する必要があります。

### 機能紹介

#### PlayerViewer

実機で表示している内容をUnityEditor上で再生するViewerです。

<img width="800" alt="PlayerView" src="https://user-images.githubusercontent.com/29646672/137237372-637a0a77-5913-4bfc-835e-03737e0a5013.png">

##### 起動方法

MenuからWindow->UnityChoseKun->Player ViewでPlayerView Windowが表示されます。

##### 操作方法

###### Connect To

接続先のデバイスを指定します。接続の仕組みはUnityProfilerと共有していますので、どちらかの接続先を切り替えると、もう片方の接続先も切り替わります。

<img width="20" alt="PlayIcon" src="https://user-images.githubusercontent.com/29646672/137236748-d4c3ad04-c66c-4e42-81f4-547649720f02.png"> 再生の開始/終了</br>
<img width="20" alt="RecIcon" src="https://user-images.githubusercontent.com/29646672/137236785-25596da8-ba35-4cf9-a622-5f2e014baa8a.png"> 録画の開始/終了</br>
<img width="20" alt="ScreenShotIcon" src="https://user-images.githubusercontent.com/29646672/137236826-10a97a17-40b3-41c8-affd-d499e64e7475.png">スクリーンショットを保存する</br>
<img width="20" alt="SaveFolderIcon" src="https://user-images.githubusercontent.com/29646672/137236850-d88a79ec-0e32-46a8-97cd-d736020dd659.png">録画結果の保存先を指定する</br>

###### Enable Async GPU Readback

[Async GPU Readback](https://docs.unity3d.com/ja/2018.4/ScriptReference/Rendering.AsyncGPUReadback.html)の機能を使用して画像処理を行う為、MainThareadの負荷が軽減される場合があります。

###### Refresh Interval

画像の転送処理を行う間隔を指定します。
間隔を開けることでCPU負荷が軽減されます。

###### Record Folder

録画時にファイルを書き出す先のフォルダー

###### Record Count Max

録画に何コマ迄録画するかを指定出来ます。
このコマまで録画を行うと録画は自動的に停止します。

###### Record Count

録画内容をSeekすることが出来ます。

<img width="800" alt="UnityChoseKunDemo04" src="https://user-images.githubusercontent.com/29646672/137240645-7e4f1d5d-1214-4247-b846-971e09f852d1.gif">

##### 注意事項

- Player Viewerは非常に負荷の高い処理です。Player Inspector->Screen->SetScreenからwidthとheightを調整したり、Refresh Intervalで実行間隔を調整してから再生を実行することを*オススメ*します。

#### Player Hierarchy

![HierarchyView](https://user-images.githubusercontent.com/29646672/137240924-d089e4b6-9ff7-4bbe-ba31-f19cc7459aca.jpg)



##### Reload

実機で実行されているアプリケーションのScene情報を分析し、Hierarchy Treeとして展開します。
まず初めにReloadを実行し、Sceneの情報を取得することからスタートします。
GameObjectが数万個存在するような複雑なSceneの場合、Reloadには時間がかかります。

EditorのHierarchy同様にGameObjectの親子関係を変更したり、右クリックからのモーダルダイヤログでGameObjectを生成したり、基本的なComponentを追加することが出来ます。

*NOTE* </br>
アプリケーションに含まれていないComponentを追加するとエラーとなります。

#### Player Inspector

実機で実行しているアプリケーションのオブジェクト及びUnithEngine内のいくつかのClassの内容を編集することが出来るWindowsです。

##### Inspector

Player Hierarchyで選択したGameObjectが持つComponentの内容を編集します。
すべてのComponetの内容を編集出来る訳ではなく、現時点では一部のComponentに限定されています。
非対応のComponentに関しては、Componentのenableのみ編集可能となっています。

![InspectorView](https://user-images.githubusercontent.com/29646672/137236992-9d2f6619-a2bd-4d03-b363-ac11fe5ac99c.jpg)


- [Connect To] : 接続先のデバイスを選択します。 Profilerと共有しています。
- [Add Component] : GameObjectにComponentを追加します。(未実装)

##### Component

![Inspector_Component](https://user-images.githubusercontent.com/29646672/137237020-38558ca0-e30f-4144-b3f1-fef26a69664f.jpg)

Scene上に存在するComponentの種類と数をカウントします。
本Editor拡張で未対応なComponentは規定Classにカウントします。

##### Texture

![Inspector_Texture](https://user-images.githubusercontent.com/29646672/137237036-ab8d310d-830b-415e-9e5a-a106b6a785e7.png)

実行中のアプリからScene内から参照されているTextureとResourcesに含まれているTextureの一覧表示します。

- [Pull] : Scene上のGameObjectから参照及びResourcesに含まれるTextureの一覧を取得します。
  
※*Materialが参照しているTextureを変更する場合は、事前にPullを実行しておく必要があります。*

##### Sprite

![image](https://user-images.githubusercontent.com/29646672/124558681-d4f07380-de75-11eb-9db4-8bc46445f2b7.png)

実機で実行されているアプリのScene上から参照されているSprite及びResourcesに含まれるSpriteの一覧を表示します。
SpriteRendererのSpriteを変更する場合事前にPullを実行する必要があります。

#### Sorting Layer

![image](https://user-images.githubusercontent.com/29646672/124559206-6f50b700-de76-11eb-92b0-2456ce9d6bdc.png)

実機上の[SortingLayer.layers](https://docs.unity3d.com/ja/current/ScriptReference/SortingLayer-layers.html)を取得します。
SpriteRendererのSortingLayerを変更する場合、事前にPULLを実行する必要があります。

##### Shader

![Inspector_Shader](https://user-images.githubusercontent.com/29646672/137237071-3a32615f-f566-492a-9c09-9cd56766b8fa.png)

実機で実行されているアプリのScene上から参照されているShader及びResourcesに含まれるShaderの一覧を表示します。

- [Pull] : Scene上のGameObjectから参照及びResourcesに含まれるShaderの一覧を取得します。

※*Materialが参照しているShaderを変更する場合は、事前にPullを実行しておく必要があります。*

##### Screen

![Inspector_Screen](https://user-images.githubusercontent.com/29646672/137237098-7ca68dd4-42d9-42f2-acd9-887429bfbada.jpg)

Screen Classのstaticメンバーに関する内容を編集することが出来ます。

- [Pull] : Screen Classのstaticメンバーを取得します。
- [Push] : 編集した内容を実機上に書き戻します。

##### Time

![Inspector_Time](https://user-images.githubusercontent.com/29646672/137237225-6cfadb92-41c3-4a04-84b4-eb2bf6c5940e.jpg)

Time Classのstaticメンバーに関する内容を編集することが出来ます。

- [Pull] : Time Classのstaticメンバーを取得します。
- [Push] : 編集した内容を実機上に書き戻します。

##### Application

<img width="800" alt="Inspector_Application" src="https://user-images.githubusercontent.com/29646672/137237244-14fe2c38-e81d-4817-8eda-74bf5ab00661.png">

Application Classのstaticメンバーに関する内容を編集することが出来ます。
また、Application.Quit()を実行することが可能です。

- [Pull] : Application Classのstaticメンバーを取得します。
- [Push] : 編集した内容を実機へ上書きします。
- [Quit] : Application.Quite()を実行します。

##### Android

<img width="800" alt="Inspector_AndroidView" src="https://user-images.githubusercontent.com/29646672/137237284-3a9aa132-6794-464f-9cc3-b4995065c734.jpg">

Androidデバイス固有の機能を編集することが出来ます。

##### QualitySettings

<img width="800" alt="QualitySettingsView" src="https://user-images.githubusercontent.com/29646672/137237300-f2db3084-1dc4-4ba3-bd17-870af7db40f9.jpg">

QualitySettingを編集することが出来ます。

##### OnDemandRendering

renderFrameInterval を調整することが出来ます。
Home画面等画面の動きが激しく無い場合、値を調整することで、バッテリー消費を抑えることが期待出来ます。

#### ScalableBufferManager

ScalableBufferManagerを編集することが出来ます。
この機能を使用する場合、下記の条件を満たしている必要があります。

- Project Settings -> `Enable Frame Timing Stats`にチェックを入れた状態でアプリケーションをビルドしている。
- CameraもしくはRenderTextureの[Allow Dynamic Resolution](https://docs.unity3d.com/ja/2018.4/uploads/Main/DynamicResolution.png)が有効になっている。
- プラットフォーム(及びGraphicドライバ)が下記のいずれかである
  - Xbox One
  - PS4
  - Nintendo Switch
  - iOS/tvOS (Metal のみ)
  - Android (Vulkan のみ)

## 以上、FBやコメントを受けつけております

## _木村 勝将：katsumasa@unity3d.com_
