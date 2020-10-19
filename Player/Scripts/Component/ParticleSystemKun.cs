using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


namespace Utj.UnityChoseKun
{
    /// <summary>
    /// ParticleSystemをSerialize/DeSerializeを行う為のClass
    /// Programed by Katsumasa.Kimura
    /// </summary>
    [System.Serializable]
    public class ParticleSystemKun : ComponentKun
    {
        /// <summary>
        /// MainModuleのSerialize/DeSerialize用Class
        /// </summary>
        [System.Serializable]
        public class MainModuleKun
        {
            [SerializeField] public float duration;
            [SerializeField] public bool loop;
            [SerializeField] public int maxParticles;
            [SerializeField] public bool playOnAwake;
            [SerializeField] public bool prewarm;            
        }



        [SerializeField] public MainModuleKun main;
        [SerializeField] public bool emission;
        [SerializeField] public bool shape;
        [SerializeField] public bool velocityOverLifetime;
        [SerializeField] public bool limitVelocityOverLifetime;
        [SerializeField] public bool inheritVelocity;
        [SerializeField] public bool forceOverLifetime;
        [SerializeField] public bool colorOverLifetime;
        [SerializeField] public bool colorBySpeed;
        [SerializeField] public bool sizeOverLifetime;
        [SerializeField] public bool sizeBySpeed;
        [SerializeField] public bool rotationOverLifetime;
        [SerializeField] public bool rotationBySpeed;
        [SerializeField] public bool externalForces;
        [SerializeField] public bool noise;
        [SerializeField] public bool collision;
        [SerializeField] public bool trigger;
        [SerializeField] public bool subEmitters;
        [SerializeField] public bool textureSheetAnimation;
        [SerializeField] public bool trails;
        [SerializeField] public bool customData;

        [SerializeField] public bool isEmitting;
        [SerializeField] public bool isPaused;
        [SerializeField] public bool isPlaying;
        [SerializeField] public bool isStopped;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ParticleSystemKun() : this(null) { }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="component">元となるComponent</param>
        public ParticleSystemKun(Component component) : base(component)
        {
            componentKunType = ComponentKunType.ParticleSystem;

            var particleSystem = component as ParticleSystem;
            if (particleSystem)
            {
                main = new MainModuleKun();
                main.duration       = particleSystem.main.duration;
                main.loop           = particleSystem.main.loop;
                main.maxParticles   = particleSystem.main.maxParticles;
                main.playOnAwake    = particleSystem.main.playOnAwake;
                main.prewarm        = particleSystem.main.prewarm;

                emission = particleSystem.emission.enabled;
                shape                       = particleSystem.shape.enabled;
                velocityOverLifetime        = particleSystem.velocityOverLifetime.enabled;                
                limitVelocityOverLifetime   = particleSystem.limitVelocityOverLifetime.enabled;
                inheritVelocity             = particleSystem.inheritVelocity.enabled;
                forceOverLifetime           = particleSystem.forceOverLifetime.enabled;
                colorOverLifetime           = particleSystem.colorOverLifetime.enabled;
                colorBySpeed                = particleSystem.colorBySpeed.enabled;
                sizeOverLifetime            = particleSystem.sizeOverLifetime.enabled;
                sizeBySpeed                 = particleSystem.sizeBySpeed.enabled;
                rotationOverLifetime        = particleSystem.rotationOverLifetime.enabled;
                rotationBySpeed             = particleSystem.rotationBySpeed.enabled;
                externalForces              = particleSystem.externalForces.enabled;
                noise                       = particleSystem.noise.enabled;
                collision                   = particleSystem.collision.enabled;
                trigger                     = particleSystem.trigger.enabled;
                subEmitters                 = particleSystem.subEmitters.enabled;
                textureSheetAnimation       = particleSystem.textureSheetAnimation.enabled;
                trails                      = particleSystem.trails.enabled;
                customData                  = particleSystem.customData.enabled;

                isEmitting  = particleSystem.isEmitting;
                isPaused    = particleSystem.isPaused;
                isPlaying   = particleSystem.isPlaying;
                isStopped   = particleSystem.isStopped;
            }
        }


        /// <summary>
        /// Componentに書き戻す
        /// </summary>
        /// <param name="component"></param>
        /// <returns>実行結果 true:書き戻しを行った false:書き戻しを行わなかった</returns>
        public override bool WriteBack(Component component)
        {
            if (base.WriteBack(component))
            {
                var particleSystem = component as ParticleSystem;
                var main = particleSystem.main;
                
                main.duration       = this.main.duration;
                main.loop           = this.main.loop;
                main.maxParticles   = this.main.maxParticles;
                main.playOnAwake    = this.main.playOnAwake;
                main.prewarm        = this.main.prewarm;

                var emission = particleSystem.emission;
                emission.enabled = this.emission;

                var shape = particleSystem.shape;
                shape.enabled = this.shape;

                var velocityOverLifetime        = particleSystem.velocityOverLifetime;
                velocityOverLifetime.enabled    = this.limitVelocityOverLifetime;

                var inheritVelocity             = particleSystem.inheritVelocity;
                inheritVelocity.enabled         = this.inheritVelocity;

                var forceOverLifetime           = particleSystem.forceOverLifetime;
                forceOverLifetime.enabled       = this.forceOverLifetime;

                var colorOverLifetime           = particleSystem.colorOverLifetime;
                colorOverLifetime.enabled       = this.colorOverLifetime;

                var colorBySpeed                = particleSystem.colorBySpeed;
                colorBySpeed.enabled            = this.colorBySpeed;

                var sizeOverLifetime            = particleSystem.sizeOverLifetime;
                sizeOverLifetime.enabled        = this.sizeOverLifetime;

                var sizeBySpeed                 = particleSystem.sizeBySpeed;
                sizeBySpeed.enabled             = this.sizeBySpeed;

                var rotationOverLifetime        = particleSystem.rotationOverLifetime;
                rotationOverLifetime.enabled    = this.rotationOverLifetime;

                var rotationBySpeed             = particleSystem.rotationBySpeed;
                rotationBySpeed.enabled         = this.rotationBySpeed;

                var externalForces              = particleSystem.externalForces;
                externalForces.enabled          = this.externalForces;

                var noise                       = particleSystem.noise;
                noise.enabled                   = this.noise;

                var collision                   = particleSystem.collision;
                collision.enabled               = this.collision;

                var trigger                     = particleSystem.trigger;
                trigger.enabled                 = this.trigger;

                var subEmitters                 = particleSystem.subEmitters;
                subEmitters.enabled             = this.subEmitters;

                var textureSheetAnimation       = particleSystem.textureSheetAnimation;
                textureSheetAnimation.enabled   = this.textureSheetAnimation;

                var trails                      = particleSystem.trails;
                trails.enabled                  = this.trails;

                var customData                  = particleSystem.customData;
                customData.enabled              = this.customData;

                return true;
            }
            return false;
        }
    }
}
