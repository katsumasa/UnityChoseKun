using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Utj.UnityChoseKun.Engine;

public class PlayModeTests
{
    [Test]
    public void ProfilerPlayerTest()
    {        
        var profilerKun = ProfilerPlayer.CollectInfos();
        var ms = new MemoryStream();


        var bw = new BinaryWriter(ms);
        profilerKun.Serialize(bw);
        ms.Seek(0, SeekOrigin.Begin);
        var br = new BinaryReader(ms);
        profilerKun.Deserialize(br);

    }
}
