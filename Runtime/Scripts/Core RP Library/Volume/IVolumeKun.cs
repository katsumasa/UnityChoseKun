using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utj.UnityChoseKun.Engine.Rendering
{
    public interface IVolumeKun
    {
        bool isGlobal { get; set; }

        /// <summary>
        /// The colliders of the volume if <see cref="isGlobal"/> is false
        /// </summary>
        List<ColliderKun> colliders { get; }
    }
}