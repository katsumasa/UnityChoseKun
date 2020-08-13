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

### 調整可能な機能について
現在下記の調整を行うことが出来ます
- [Screen](https://docs.unity3d.com/ja/current/ScriptReference/Screen.html)
- [Time](https://docs.unity3d.com/ja/current/ScriptReference/Time.html)
- [Camera](https://docs.unity3d.com/ja/current/ScriptReference/Camera.html)
- [Light](https://docs.unity3d.com/ja/current/ScriptReference/Light.html)

## 使い方
- [UTJ](https://github.com/katsumasa/UnityChoseKun/tree/master/UTJ)以下を任意のプロジェクトのAssetフォルダー以下においてください。
### アプリケーションビルド時
- 以下のスクリプトをScene内のGameObjectにアタッチし[Development Build](https://docs.unity3d.com/ja/current/Manual/BuildSettingsStandalone.html)にチェックを入れててビルドを実行して下さい。
  - [UnityChoseKun/Scripts/UnityChoseKunPlayer.cs](https://github.com/katsumasa/UnityChoseKun/blob/master/UTJ/UnityChoseKun/Scripts/UnityChoseKunPlayer.cs)　「調整用」
  - [UnityPlayerViewerKun/Scripts/UnityPlayerViewerKunPlayer.cs](https://github.com/katsumasa/UnityChoseKun/blob/master/UTJ/UnityPlayerViewerKun/Scripts/UnityPlayerViewerKunPlayer.cs) 「Viewer用」
### アプリケーション実行時
#### PlayerViewer
##### 起動方法
Window/UTJ/UnityChoseKun/PlayerView
#### InspectorViewer
##### 起動方法
Window/UTJ/UnityChoseKun/Inspecter
 
