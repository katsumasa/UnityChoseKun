using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Utj.UnityChoseKun;

[System.Serializable]
public struct TestData1 {
    public int a;
    public int b;

}

[System.Serializable]
public struct TestData2 {
    public int c;
    public  TestData1 data1;
}


public class Test : CameraEditor
{
    static Test window;
    static CameraEditor cameraEditor;

    static Camera camera;

    static TestData2  data2;


    [MenuItem("Window/TestWindow")]
    static void Open()
    {
    }


    
}
