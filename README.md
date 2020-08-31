# UnityChoseKun
[English Ver. README](chosekun_en.md)
## はじめに
Unityで開発したアプリを実機上で実行している時に、GameObjectの内容をほんの少しだけ変更したいだけなのに、UnityEditorで再ビルドを行う必要があり、長時間待たされ、貴重な開発時間を無駄にして悔しい思いをしている開発者の皆様、お待たせしました。
UnityChoseKunはそのような無駄な時間を削減する為の開発支援ツールです。

## 概要
実機上で実行しているアプリ内のGameObjectをUnityEditorから調整する事が出来る、開発支援ツールです。

![img](docs/UnityChoseKunDemo02.gif)
![img](docs/UnityChoseKunDemo03.gif)

## このプロジェクトで出来ること
本ツールは主に下記の３機能から構成されています。
- 実機で再生されている画面の表示(PlayerView)
- 実機で再生されているSceneのHierarchyに表示(PlayerHierarchy)
- GameObjectに含まれるComponentの内容を編集し実機側へ反映する(PlayerInspector)

## 動作環境
下記の内容で動作を確認済みです。
- Unity2019.4.5f1
- Pixel3XL,Pixel4XL
- Scripting Backend *IL2CPP* (Monoの場合クラッシュします。)


## 調整可能なComponentについて
現在下記の調整を行うことが出来ます
- [Screen](https://docs.unity3d.com/ja/ScriptReference/Screen.html)
- [Time](https://docs.unity3d.com/ja/ScriptReference/Time.html)
- [Shader](https://docs.unity3d.com/ja/ScriptReference/Shader.html)
- [Texture](https://docs.unity3d.com/ja/ScriptReference/Texture.html)
- [Component](docs.unity3d.com/ja/ScriptReference/Component.html)
  - [Camera](https://docs.unity3d.com/ja/ScriptReference/Camera.html)
  - [Light](https://docs.unity3d.com/ja/ScriptReference/Light.html)
  - [Renderer](https://docs.unity3d.com/ja/ScriptReference/Renderer.html)
  - [MeshRenderer](https://docs.unity3d.com/ja/ScriptReference/Renderer.html)
  - [SkinnedMeshRenderer](https://docs.unity3d.com/ja/ScriptReference/SkinnedMeshRenderer.html)
  - [MonoBehavior](https://docs.unity3d.com/ja/ScriptReference/MonoBehaviour.html)
  - [Behavior](https://docs.unity3d.com/ja/ScriptReference/Behaviour.html)
  - [Material](https://docs.unity3d.com/ja/ScriptReference/Material.html)

## 注意事項
- 開発中のツールの為、作りこみが不十分です。万が一*不測の事態が起きても一切保証は出来ませんのでご注意下さい。*
- Script Debuggingと併用できません。必ずScript DebuggingをOFFにした上でビルドを行ってください。
- Player Viewを有効にすると*端末が高温になります。* また、画面解像度に応じてCPUリソースを消費します。PlayerViewの再生を行う前に、PlayerInspectorのScreenから画面解像度を変更することをお勧めします。Pixel4XLの場合、画面解像度を604x288に落とした場合、CPUリソースにほぼ消費しないで実行できるようでした。
- Texture,Shaderに関しては内容の確認のみで、変更を行うことは出来ません。
- Materialが参照しているShader/Textureを変更する為にはTexture/ShaderのPullを先に実行する必要があります。

## 使い方
本リポジトリの内容を組込み先のUnityProjectのAssetフォルダ以下に丸々配置して下さい。
### アプリケーションビルド時
- [UnityChoseKun.prefab](https://github.com/katsumasa/UnityChoseKun/blob/master/Player/Prefabs/UnityChoseKun.prefab)をSceneに配置してアプリをビルドして下さい。
- [Development BuildとAutoconnect Profiler](https://docs.unity3d.com/ja/current/Manual/BuildSettingsStandalone.html)にチェックを入れてる必要があります。
- [Scripting BackEnd](https://docs.unity3d.com/ja/2018.4/Manual/windowsstore-scriptingbackends.html)はIL2CPPを選択して下さい。

### アプリケーション実行時
#### PlayerViewer
実機で表示している内容をUnityEditor上で再生するViewerです。
![img](docs/PlayerView.jpg)
##### 起動方法
MenuからWindow->UTJ->UnityChoseKun->PlayerViewで起動します。
##### 操作方法
###### Connect To
接続先のデバイスを指定します。接続の仕組みはUnityProfilerと共有していますので、どちらかの接続先を切り替えると、もう片方の接続先も切り替わります。
###### Enable Async GPU Readback
チェックを入れると[Async GPU Readback](https://docs.unity3d.com/ja/2018.4/ScriptReference/Rendering.AsyncGPUReadback.html)の機能を使用して画像処理を行う為、MainThareadの負荷が軽減される場合があります。
###### Skip frame
画像の転送処理を行う間隔を指定します。
###### Play
画像の転送処理を開始します。
###### Stop
画像の転送処理を停止します。
##### 注意事項
- InspectorViewerは非常に負荷の高い処理です。InspectorViewerのScreenのSetScreenからwidthとheightを調整したり、Skip Frameで実行間隔を調整してからPlayボタンを押すことをお勧めします。
- Scripting BackendにはIL2CPPを使用して下さい。
#### PlayerHierarchy
![img](docs/HierarchyView.jpg)
<br>
実機で実行されているアプリケーションのScene情報を分析し、Hierarchy Treeとして展開します。
情報を取得分析する為には、PlayerInspectorからInspector画面でPullを実行する必要があります。

#### PlayerInspector
実機で実行しているアプリケーションのScreen,Time及びGameObjectのComponent(Camera,Light)の内容を確認、編集を行うことが出来ます。
##### 起動方法
MenuからWindow->UTJ->UnityChoseKun->Inspecterで起動します。
##### 操作方法
###### Connect To
接続先のデバイスを指定します。接続の仕組みはUnityProfilerと共有していますので、どちらかの接続先を切り替えると、もう片方の接続先も切り替わります。
###### Inspector/Texture/Shader/Time/Component
Inspecter Viewに表示するクラスを切り替えます
##### Inspector
![img](docs/InspectorView.jpg)
Hierarchy Viewから選択されたGameObjectの情報を表示します。
###### Pull
実機上で実行されているアプリケーションのScene情報を分析しPlayer Hierarchyへ展開します。
###### Push
選択されたGameObjectの内容を実機上へ書き戻します。

##### Texture
実機上で実行されるアプリケーションからTextureの一覧を取得します。
対象はScene上及びResourcesとなります。
Materialが参照しているTextureを変更する場合は、TextureのPullを実行する必要があります。

##### Shader
実機上で実行されるアプリケーションからShaderの一覧を取得します。。
対象はScene上及びResourcesとなります。
Materialが参照しているShaderを変更する場合は、ShaderのPullを実行する必要があります。

##### PlayerInspector(Screen)
![img](docs/Inspector_Screen.jpg)
Screenクラスの表示・編集を行います
###### Pull
実機上で実行されているアプリケーションのScreen Classの内容を取得します。
###### Push
編集した内容を実機上で実行されているアプリケーションにフィードバックします。
##### PlayerInspector(Time)
![img](docs/Inspector_Time.jpg)
###### Pull
実機上で実行されているアプリケーションのTime Classの内容を取得します。
###### Push
編集した内容を実機上で実行されているアプリケーションにフィードバックします。
