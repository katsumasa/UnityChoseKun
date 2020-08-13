# UnityChoseKun
## 概要
実機上で実行しているUnityでビルドしたアプリケーションをUnityEditor上で調整する為のツールです。

![img](docs/UnityChoseKunDemo02.gif)

## 動作環境
下記の内容で動作を確認済みです。
- Unity2019.4.5f1
- Pixel3XL,Pixel4XL

## 実装済みの機能
- 実機の内容をUnityEditor上で表示するPlayerViewer
- 幾つかのパラメータを調整することが出来るInspectorViewer

## 調整可能な機能について
現在下記の調整を行うことが出来ます
- [Screen](https://docs.unity3d.com/ja/current/ScriptReference/Screen.html)
- [Time](https://docs.unity3d.com/ja/current/ScriptReference/Time.html)
- Component
  - [Camera](https://docs.unity3d.com/ja/current/ScriptReference/Camera.html)
  - [Light](https://docs.unity3d.com/ja/current/ScriptReference/Light.html)

## 注意事項
- 現状はかなりやっつけ仕事な為、色々不備がある為ご注意ください。
- EditorWindow(PlayerViewer,InspecterViewew)の実装にSerializeObjectを使用していない為、Assemblyのリロードが発生するとEditorWindowの表示が壊れます。大変申し訳ないのですが、そのような場合は、一度EditorWindow開き直してください。

## 使い方
本リポジトリの内容を組み込みたいUnityProjectのAssetフォルダ以下に丸々配置して下さい。
### アプリケーションビルド時
- 以下のスクリプトをScene内のGameObjectにアタッチし[Development Build](https://docs.unity3d.com/ja/current/Manual/BuildSettingsStandalone.html)にチェックを入れててビルドを実行して下さい。
  - [UnityChoseKun/Scripts/UnityChoseKunPlayer.cs](https://github.com/katsumasa/UnityChoseKun/blob/master/UTJ/UnityChoseKun/Scripts/UnityChoseKunPlayer.cs)　「調整用」
  - [UnityPlayerViewerKun/Scripts/UnityPlayerViewerKunPlayer.cs](https://github.com/katsumasa/UnityChoseKun/blob/master/UTJ/UnityPlayerViewerKun/Scripts/UnityPlayerViewerKunPlayer.cs) 「Viewer用」
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

#### InspectorViewer
実機で実行しているアプリケーションのScreen,Time及びGameObjectのComponent(Camera,Light)の内容を確認、編集を行うことが出来ます。
##### 起動方法
MenuからWindow->UTJ->UnityChoseKun->Inspecterで起動します。
##### 操作方法
###### Connect To
接続先のデバイスを指定します。接続の仕組みはUnityProfilerと共有していますので、どちらかの接続先を切り替えると、もう片方の接続先も切り替わります。
###### Screen/Time/Component
Inspecter Viewに表示するクラスを切り替えます
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
##### Component
![img](docs/Inspector_Component.jpg)
###### Pull
実機上で実行されているアプリケーションのGameObjectの一覧を取得します。
###### Push
現在編集中のGameObjectを実機上で実行されているアプリケーションにフィードバックします。
###### Select Component
編集したいComponentを選択します。
###### Select Object
`Select Component`で指定したComponentを持つGameObjectの一覧から編集するGameObjectを選択します。
