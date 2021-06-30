# UnityChoseKun
## Introduction
Have you ever experienced time being wasted to complete a build every time for a small setting changed in the GamObject? This tool not only allows to make changes to your GameObject without rebuilding every time, but also can while the actual device is running.    

## Overview

Tool used to control application built with Unity running on a real device (Switch,Android,iOS,etc) from UnityEditor.

Can adjust the component inside the application that are running on the actual device:
![img](docs/UnityChoseKunDemo02.gif)

Could show UnityEditor from the device's screen:
![img](docs/UnityChoseKunDemo03.gif)

## What you can do with this project
The following tasks can be performed in the UnityEditor
- Display the screen rendered on the actual device (PlayerView).
- Display hierarchy of the scene being played in the actual device (Hierarchy View)
- Reflect the changes made to the selected GameObject's component to the actual device (Inspector View).

## Operating Environment
- Unity version
  - Unity2018.4.26f1 (Some functions are limited)
  - Unity2019.4.11f1
- Platform
  - Android
    - Pixel3XL
    - Pixel4XL
  - iOS
    - iPhone 6S

## Adjustable Components
You can make adjustments on the following components
- [Application](https://docs.unity3d.com/ScriptReference/Application.html)
- [Screen](https://docs.unity3d.com/ScriptReference/Screen.html)
- [Time](https://docs.unity3d.com/ScriptReference/Time.html)
- [Shader](https://docs.unity3d.com/ScriptReference/Shader.html)
- [Texture](https://docs.unity3d.com/ScriptReference/Texture.html)
- [QualitySettings](https://docs.unity3d.com/ScriptReference/QualitySettings.html)
- [SystemInfo](https://docs.unity3d.com/ScriptReference/SystemInfo.html)
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
  - [Transform](https://docs.unity3d.com/ScriptReference/Transform.html)
  - [Vector2](https://docs.unity3d.com/ScriptReference/Vector2.html)
  - [Vector3](https://docs.unity3d.com/ScriptReference/Vector3.html)
  - [Vector4](https://docs.unity3d.com/ScriptReference/Vector4.html)

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



## How to use
Place the entire contents of this repository under the Asset folder of the UnityProject.

### Building
- Put [UnityChoseKun.prefab](https://github.com/katsumasa/UnityChoseKun/blob/master/Player/Prefabs/UnityChoseKun.prefab) in a Scene and build the app.
- You must have the check box checked [Development Build and Autoconnect Profiler](https://docs.unity3d.com/2019.4/Documentation/Manual/BuildSettingsStandalone.html) when building.
- You must specify IL2CPP [Scripting BackEnd](https://docs.unity3d.com/2018.4/Documentation/Manual/windowsstore-scriptingbackends.html).

### Features
#### PlayerViewer
Viewer that plays the content displayed on the actual device in UnityEditor.
![img](docs/PlayerView.png)

#### Launch Method
From Menu, choose Window->UnityChoseKun->Player View. The PlayerView Window shows up.

#### How to operate

###### Connect To
Specify the device you want to connect. The connection mechanism is shared with UnityProfiler, so when you switch to one of them, the other one will switch as well.

![img](docs/PlayIcon.png)　Play Begin/End</br>
![img](docs/RecIcon.png)　Record Begin/End</br>
![img](docs/ScreenShotIcon.png)　Save Screenshot</br>
![img](docs/SaveFolderIcon.png)　Specify the path of the recording results</br>

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

![img](docs/UnityChoseKunDemo04.gif)


##### Warning

- This is a very high-load process.
- We  __recommend__ you to adjust the width and height in SetScreen or skip frame and press Play.

#### PlayerHierarchy

![img](docs/HierarchyView.jpg) </br>

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

![img](docs/InspectorView.jpg)

- [Connect To] : Select the device you wish to connectc. It's shared with the Profiler
- [Add Component] : Add Component to GameObject (Currently in progress).

##### Component

![img](docs/Inspector_Component.jpg)

Counts the type and number of components that exists in a scene.
Components that are not supported by this tool are counted in the Specified Class.

##### Texture

![imag](docs/Inspector_Texture.png)

Lists the textures that are referenced in a scene as well as textures that are included in resources runned by an app.

- [Pull] : Obtain list of references form GameObjects that are in the scene and textures included in resources.
  
※*You need to run the Pull command before changing the Texture that Material is referencing*

##### Shader

![img](docs/Inspector_Shader.png)

List of Shader that are included in Resources and scene of the app running on actual device.

- [Pull] : Obtain list of references form GameObjects that are in the scene and shaders included in resources.

※*You need to run the Pull command before changing the Sahder that Material is referencing。*

##### Screen

![img](docs/Inspector_Screen.png)

Edit the static members of the Screen Class.

- [Pull] : Get the static members of the Screen Class.
- [Push] : Edited information will show on the actual device.

##### Time

![img](docs/Inspector_Time.jpg)

Edit the static members of the Time Class.

- [Pull] : Get the static members of the Time Class.
- [Push] : Edited information will show on the actual device.

##### Application

![img](docs/Inspector_Application.png)

Edit the static members of the Application Class.
Also could run Application.Quit().

- [Pull] : Get the static members of the Application Class.
- [Push] : Edited information will show on the actual device.
- [Quit] : Run Application.Quite().

##### Android

![img](docs/Inspector_AndroidView.jpg)

You can edit the Android device's specific features.

##### QualitySettings

![img](docs/QualitySettingsView.jpg)

You can edit QualitySetting.

## Thats all! Appreciate your comments and feedback!

__Katsumasa Kimura: katsumasa@unity3d.com__
