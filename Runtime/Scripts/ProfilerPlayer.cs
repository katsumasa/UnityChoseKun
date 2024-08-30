using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Profiling;


namespace Utj.UnityChoseKun.Engine
{
    public class ProfilerPlayer
    {
        public static void OnMessageEventPull(BinaryReader _binaryReader)
        {
            var profilerKun = CollectInfos();
            UnityChoseKunPlayer.SendMessage<ProfilerKun>(UnityChoseKun.MessageID.ProfilerPlayerPull, profilerKun);
        }

        public static ProfilerKun CollectInfos()
        {
            var profilerKun = new ProfilerKun();
            profilerKun.monoHeapSizeLong = Profiler.GetMonoHeapSizeLong();
            profilerKun.monoUsedSizeLong = Profiler.GetMonoUsedSizeLong();
            profilerKun.totalReservedMemoryLong = Profiler.GetTotalReservedMemoryLong();
            profilerKun.totalAllocatedMemoryLong = Profiler.GetTotalAllocatedMemoryLong();
            profilerKun.totalUnusedReservedMemoryLong = Profiler.GetTotalUnusedReservedMemoryLong();

            var objects = Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)) as UnityEngine.Object[];
            profilerKun.objectRuntimeMemorys = new ObjectRuntimeMemory[objects.Length];
            for (var i = 0; i < objects.Length; i++)
            {
                profilerKun.objectRuntimeMemorys[i] = new ObjectRuntimeMemory(objects[i]);
            }
            return profilerKun;
        }
    }
}