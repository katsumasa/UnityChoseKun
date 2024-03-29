# UnityChoseKun

![GitHub package.json version](https://img.shields.io/github/package-json/v/katsumasa/UnityChoseKun?style=plastic)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/katsumasa/UnityChoseKun?style=plastic)
## Introduction

Have you ever experienced time being wasted to complete a build every time for a small setting changed in the GamObject? This tool not only allows to make changes to your GameObject without rebuilding every time, but also can while the actual device is running.

## Overview

Tool used to control application built with Unity running on a real device (Switch,Android,iOS,etc) from UnityEditor.

Can adjust the component inside the application that are running on the actual device:
<img width="800" alt="UnityChoseKunDemo02" src="https://user-images.githubusercontent.com/29646672/137236126-f7b9c064-3dcc-41d5-9ce6-9f9175d9d315.gif">

Could show UnityEditor from the device's screen:
<img width="800" alt="UnityChoseKunDemo03" src="https://user-images.githubusercontent.com/29646672/137236618-7539f774-b200-45e9-a4d5-87e7ceb6b208.gif">

## What you can do with this project

The following tasks can be performed in the UnityEditor
- Display the screen rendered on the actual device (PlayerView).
- Display hierarchy of the scene being played in the actual device (Hierarchy View)
- Reflect the changes made to the selected GameObject's component to the actual device (Inspector View).

## Operating Environment

- Unity version
  - Unity2018.4.26f1 (Some functions are limited)
  - Unity2019.4.40f1
  - Unity2020.2.2f1
  - Unity2020.3.27f1
  - Unity2021.2.15f1
  - Unity2021.3.1f1
  - Unity2022.3.0f1
- Platform
  - Android
    - Pixel3XL
    - Pixel4XL
  - iOS
    - iPhone 6S

## Adjustable Components
You can make adjustments on the following components
- [Application](https://docs.unity3d.com/ScriptReference/Application.html)
- [OnDemandRendering](https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Rendering.OnDemandRendering.html)
- [QualitySettings](https://docs.unity3d.com/ScriptReference/QualitySettings.html)
- [Screen](https://docs.unity3d.com/ScriptReference/Screen.html)
- [Shader](https://docs.unity3d.com/ScriptReference/Shader.html)
- [Sprite](https://docs.unity3d.com/2019.4/Documentation/ScriptReference/SpriteRenderer-sprite.html)
- [SystemInfo](https://docs.unity3d.com/ScriptReference/SystemInfo.html)
- [ScalableBufferManager](https://docs.unity3d.com/ScriptReference/ScalableBufferManager.html)
- [SortingLayer](https://docs.unity3d.com/ScriptReference/SortingLayer.html)
- [Texture](https://docs.unity3d.com/ScriptReference/Texture.html)
- [Time](https://docs.unity3d.com/ScriptReference/Time.html)
- [Component](https://docs.unity3d.com/ScriptReference/Component.html)
  - [Animator](https://docs.unity3d.com/ScriptReference/Animator.html)
  - [Behaviour](https://docs.unity3d.com/ScriptReference/Behaviour.html)
  - [Bounds](https://docs.unity3d.com/ScriptReference/Bounds.html)
  - [Camera](https://docs.unity3d.com/ScriptReference/Camera.html)
  - [Collider](https://docs.unity3d.com/ScriptReference/Collider.html)
  - [Light](https://docs.unity3d.com/ScriptReference/Light.html)
  - [Material](https://docs.unity3d.com/ScriptReference/Material.html)
  - [Matrix4x4](https://docs.unity3d.com/ScriptReference/Matrix4x4.html)
  - [MeshRenderer](https://docs.unity3d.com/ScriptReference/MeshRenderer.html)
  - [MonoBehavior](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html)
  - [Object](https://docs.unity3d.com/ScriptReference/Object.html)
  - [ParticleSystem](https://docs.unity3d.com/ScriptReference/ParticleSystem.html)
  - [PhysicMaterial](https://docs.unity3d.com/ScriptReference/PhysicMaterial.html)
  - [Quartanion](https://docs.unity3d.com/ScriptReference/Quaternion.html)
  - [Rect](https://docs.unity3d.com/ScriptReference/Rect.html)
  - [Renderer](https://docs.unity3d.com/ScriptReference/Renderer.html)
  - [Resolution](https://docs.unity3d.com/ScriptReference/Resolution.html)
  - [Rigidbody](https://docs.unity3d.com/ScriptReference/Rigidbody.html)
  - [SkinnedMeshRenderer](https://docs.unity3d.com/ScriptReference/SkinnedMeshRenderer.html)
  - [SpriteRenderer](https://docs.unity3d.com/ScriptReference/SpriteRenderer-sprite.html)
  - [Transform](https://docs.unity3d.com/ScriptReference/Transform.html)
  - [Vector2](https://docs.unity3d.com/ScriptReference/Vector2.html)
  - [Vector3](https://docs.unity3d.com/ScriptReference/Vector3.html)
  - [Vector4](https://docs.unity3d.com/ScriptReference/Vector4.html)

Here is a step on how to add new component/class:
[Howto_add_class_en.md](https://github.com/katsumasa/UnityChoseKun/blob/master/Documentation~/Howto_add_class_en.md)

## Caution

- Will not take any responsibility for any damage that are caused by using this tool.
- Cannot be used with Script Debugging.
- Enabling the Player View makes the device __hot__. The ammount of CPU resources being used depends on the screen resolution.  Before playing the PlayerView, it is recommended to change the screen resolution from Screen in Player Inspector.  In Pixel4XL's case, the CPU resources were'nt used at all when the screen resolution was reduced to 604x288.
- Material is only for checking the content, not writing back the edited content.
- In order to change the referenced Material's Shader/Texture, you need to Pull them first.
- Following functions cannot be made in Unity2018:
  - Replace referenced texture.
  - Readback feature of PlayerView's Async GPU.
- If you quit Unity Editor while the device and UnityChoseKun remain connected, crash will occur inside `EditorConnection.instance.DisconnectAll()`
To prevent that, disconnect from the device by changing the access point of UnityChoseKun to the Editor.

## Setup

UnityChoseKun uses a separate RemoteConnect package. Please obtain it together with UnityChoseKun. Setup is completed by adding the obtained package to any Unity project.

```
git clone https://github.com/katsumasa/RemoteConnect.git
git clone https://github.com/katsumasa/UnityChoseKun.git
```

## How to use
Place the entire contents of this repository under the Asset folder of the UnityProject.

### Building

- Put [UnityChoseKun.prefab](https://github.com/katsumasa/UnityChoseKun/blob/master/Player/Prefabs/UnityChoseKun.prefab) in a Scene and build the app.
- You must have the check box checked [Development Build and Autoconnect Profiler](https://docs.unity3d.com/2019.4/Documentation/Manual/BuildSettingsStandalone.html) when building.
- You must specify IL2CPP [Scripting BackEnd](https://docs.unity3d.com/2018.4/Documentation/Manual/windowsstore-scriptingbackends.html).

### Features

#### PlayerViewer

Viewer that plays the content displayed on the actual device in UnityEditor.

<img width="800" alt="PlayerView" src="https://user-images.githubusercontent.com/29646672/137237372-637a0a77-5913-4bfc-835e-03737e0a5013.png">

#### Launch Method

From Menu, choose Window->UnityChoseKun->Player View. The PlayerView Window shows up.

#### How to operate

###### Connect To

Specify the device you want to connect. The connection mechanism is shared with UnityProfiler, so when you switch to one of them, the other one will switch as well.

<img width="20" alt="PlayIcon" src="https://user-images.githubusercontent.com/29646672/137236748-d4c3ad04-c66c-4e42-81f4-547649720f02.png">　Play Begin/End</br>
<img width="20" alt="RecIcon" src="https://user-images.githubusercontent.com/29646672/137236785-25596da8-ba35-4cf9-a622-5f2e014baa8a.png">　Record Begin/End</br>
<img width="20" alt="ScreenShotIcon" src="https://user-images.githubusercontent.com/29646672/137236826-10a97a17-40b3-41c8-affd-d499e64e7475.png">　Save Screenshot</br>
<img width="20" alt="SaveFolderIcon" src="https://user-images.githubusercontent.com/29646672/137236850-d88a79ec-0e32-46a8-97cd-d736020dd659.png">　Specify the path of the recording results</br>

###### Enable Async GPU Readback

If you check this box, you will be able to use [Async GPU Readback](https://docs.unity3d.com/2018.4/Documentation/ScriptReference/Rendering.AsyncGPUReadback.html) to process images. This may reduce the load of the MainTharead.

###### Reflesh Interval

Specify the process interval images being transfered.
By giving interval, it may lead to reducing CPU load.

###### Record Folder

Folder where the recorded results will export to.

###### Record Count Max

Specify which frame to start record.
The recording will automatically stop once you have set the frame.

###### Record Count

You can seek the recorded result.

<img width="800" alt="UnityChoseKunDemo04" src="https://user-images.githubusercontent.com/29646672/137240645-7e4f1d5d-1214-4247-b846-971e09f852d1.gif">


##### Warning

- This is a very high-load process.
- We  __recommend__ you to adjust the width and height in SetScreen or skip frame and press Play.

#### PlayerHierarchy

![HierarchyView](https://user-images.githubusercontent.com/29646672/137240924-d089e4b6-9ff7-4bbe-ba31-f19cc7459aca.jpg)

##### Reload

Analyzes scene informartion of the application that are running on actual device, then extract is as a Hierachy Tree.
The first step is to run Reload to gather information of the scene.
It'll take some time to Reload if the scene holds tens of thousands of GameObjects.

You could change the parent-child relationship of GameObjectsthe same way as in the Editor's Hierarchy does.
Also can generate GameObjects or add basic Components with the right-click modal dialog.

*NOTE* </br>
Adding a Component that is not included in the application will cause an error.

#### Player Inspector

Windows that allows you to edit contens inside the class of UnityEngine that are running on actual device.

##### Inspector

Edits the content of selected GameObject's Component in Player Hierarchy.
Currently, there are few components are editable. But enable can be editable for almost any components.

![InspectorView](https://user-images.githubusercontent.com/29646672/137236992-9d2f6619-a2bd-4d03-b363-ac11fe5ac99c.jpg)

- [Connect To] : Select the device you wish to connectc. It's shared with the Profiler
- [Add Component] : Add Component to GameObject (Currently in progress).

##### Component

![Inspector_Component](https://user-images.githubusercontent.com/29646672/137237020-38558ca0-e30f-4144-b3f1-fef26a69664f.jpg)

Counts the type and number of components that exists in a scene.
Components that are not supported by this tool are counted in the Specified Class.

##### Texture

![Inspector_Texture](https://user-images.githubusercontent.com/29646672/137237036-ab8d310d-830b-415e-9e5a-a106b6a785e7.png)

Lists the textures that are referenced in a scene as well as textures that are included in resources runned by an app.

- [Pull] : Obtain list of references form GameObjects that are in the scene and textures included in resources.
  
※*You need to run the Pull command before changing the Texture that Material is referencing*

##### Sprite

![image](https://user-images.githubusercontent.com/29646672/124558681-d4f07380-de75-11eb-9db4-8bc46445f2b7.png)

#### Sorting Layer

![image](https://user-images.githubusercontent.com/29646672/124559206-6f50b700-de76-11eb-92b0-2456ce9d6bdc.png)

##### Shader

![Inspector_Shader](https://user-images.githubusercontent.com/29646672/137237071-3a32615f-f566-492a-9c09-9cd56766b8fa.png)

List of Shader that are included in Resources and scene of the app running on actual device.

- [Pull] : Obtain list of references form GameObjects that are in the scene and shaders included in resources.

※*You need to run the Pull command before changing the Sahder that Material is referencing。*

##### Screen

![Inspector_Screen](https://user-images.githubusercontent.com/29646672/137237098-7ca68dd4-42d9-42f2-acd9-887429bfbada.jpg)

Edit the static members of the Screen Class.

- [Pull] : Get the static members of the Screen Class.
- [Push] : Edited information will show on the actual device.

##### Time

![Inspector_Time](https://user-images.githubusercontent.com/29646672/137237225-6cfadb92-41c3-4a04-84b4-eb2bf6c5940e.jpg)

Edit the static members of the Time Class.

- [Pull] : Get the static members of the Time Class.
- [Push] : Edited information will show on the actual device.

##### Application

<img width="800" alt="Inspector_Application" src="https://user-images.githubusercontent.com/29646672/137237244-14fe2c38-e81d-4817-8eda-74bf5ab00661.png">


Edit the static members of the Application Class.
Also could run Application.Quit().

- [Pull] : Get the static members of the Application Class.
- [Push] : Edited information will show on the actual device.
- [Quit] : Run Application.Quite().

##### Android

<img width="800" alt="Inspector_AndroidView" src="https://user-images.githubusercontent.com/29646672/137237284-3a9aa132-6794-464f-9cc3-b4995065c734.jpg">


You can edit the Android device's specific features.

##### QualitySettings

<img width="800" alt="QualitySettingsView" src="https://user-images.githubusercontent.com/29646672/137237300-f2db3084-1dc4-4ba3-bd17-870af7db40f9.jpg">

You can edit QualitySetting.

##### OnDemandRendering

#### ScalableBufferManager

## Thats all! Appreciate your comments and feedback!

__Katsumasa Kimura: katsumasa@unity3d.com__
