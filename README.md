# UnityChoseKun
[English Ver. README](chosekun_en.md)
## 概要
実機上で実行しているUnityでビルドしたアプリケーションをUnityEditor上で調整する為のツールです。

![img](docs/UnityChoseKunDemo02.gif)
![img](docs/UnityChoseKunDemo03.gif)

## このプロジェクトで出来ること
UnityEditor上で下記の作業を行うことが可能です。
- 実機で再生されている画面の表示(PlayerView)
- 実機で再生されているSceneのHierarchyに表示(Player Hierarchy)
- Hierarchy ViweからGameObjectを選択し、Componentの内容を編集して実機へ反映する(Inspector View)

## 動作環境
下記の内容で動作を確認済みです。
- Unity2019.4.5f1
- Pixel3XL,Pixel4XL


## 調整可能なComponentについて
現在下記の調整を行うことが出来ます
- [Screen](https://docs.unity3d.com/ja/current/ScriptReference/Screen.html)
- [Time](https://docs.unity3d.com/ja/current/ScriptReference/Time.html)
- Component
  - [Camera](https://docs.unity3d.com/ja/current/ScriptReference/Camera.html)
  - [Light](https://docs.unity3d.com/ja/current/ScriptReference/Light.html)
  - [Renderer](https://docs.unity3d.com/ja/current/ScriptReference/Renderer.html)
  - [MeshRenderer](https://docs.unity3d.com/ja/current/ScriptReference/Renderer.html)
  - [SkinnedMeshRenderer](https://docs.unity3d.com/ja/current/ScriptReference/SkinnedMeshRenderer.html)
  - [MonoBehavior](https://docs.unity3d.com/ja/current/ScriptReference/MonoBehaviour.html)
  - [Behavior](https://docs.unity3d.com/ja/current/ScriptReference/Behaviour.html)
  - [Material](https://docs.unity3d.com/ja/current/ScriptReference/Material.html)

## 注意事項
- Script Debuggingと併用できません。
- Player Viewを有効にすると*端末が高温になります。*
- Materialは内容の確認のみで編集した内容は書き戻していません。

## 使い方
本リポジトリの内容を組み込みたいUnityProjectのAssetフォルダ以下に丸々配置して下さい。
### アプリケーションビルド時
- [UnityChoseKun](https://github.com/katsumasa/UnityChoseKun/blob/master/Player/Prefabs/UnityChoseKun.prefab)をSceneに配置してアプリをビルドして下さい。
- ビルド時に[Development Build](https://docs.unity3d.com/ja/current/Manual/BuildSettingsStandalone.html)にチェックを入れてる必要があります。
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
- 非常に負荷の高い処理です。
- InspectorViewerのScreenのSetScreenからwidthとheightを調整したり、Skip Frameで実行間隔を調整してからPlayボタンを押すことをお勧めします。
#### PlayerHierarchy
![img](docs/HierarchyView.jpg)
実機で実行されているアプリケーションのScene情報を分析し、Hierarchy Treeとして展開します。
情報を取得分析する為には、Inspector View->Inspector->Pullを実行する必要があります。

#### InspectorViewer
実機で実行しているアプリケーションのScreen,Time及びGameObjectのComponent(Camera,Light)の内容を確認、編集を行うことが出来ます。
##### 起動方法
MenuからWindow->UTJ->UnityChoseKun->Inspecterで起動します。
##### 操作方法
###### Connect To
接続先のデバイスを指定します。接続の仕組みはUnityProfilerと共有していますので、どちらかの接続先を切り替えると、もう片方の接続先も切り替わります。
###### Inspector/Time/Component
Inspecter Viewに表示するクラスを切り替えます
##### Inspector View(Inspector)
![img](docs/InspectorView.jpg)
Hierarchy Viewから選択されたGameObjectの情報を表示します。
###### Pull
実機上で実行されているアプリケーションのScene情報を分析しHierarchy Viewへ展開します。
###### Push
選択されたGameObjectの内容を実機上へ書き戻します。

##### InspectorViewer(Screen)
![img](docs/Inspector_Screen.jpg)
Screenクラスの表示・編集を行います
###### Pull
実機上で実行されているアプリケーションのScreen Classの内容を取得します。
###### Push
編集した内容を実機上で実行されているアプリケーションにフィードバックします。
##### InspectorViewer(Time)
![img](docs/Inspector_Time.jpg)
###### Pull
実機上で実行されているアプリケーションのTime Classの内容を取得します。
###### Push
編集した内容を実機上で実行されているアプリケーションにフィードバックします。
