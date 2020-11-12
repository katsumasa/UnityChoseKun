using System.IO;
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
        public class MainModuleKun : ISerializerKun
        {
            [SerializeField] public float duration;
            [SerializeField] public bool loop;
            [SerializeField] public int maxParticles;
            [SerializeField] public bool playOnAwake;
            [SerializeField] public bool prewarm;


            public virtual void Serialize(BinaryWriter binaryWriter)
            {
                binaryWriter.Write(duration);
                binaryWriter.Write(loop);
                binaryWriter.Write(maxParticles);
                binaryWriter.Write(playOnAwake);
                binaryWriter.Write(prewarm);
            }


            public virtual void Deserialize(BinaryReader binaryReader)
            {
                duration = binaryReader.ReadSingle();
                loop = binaryReader.ReadBoolean();
                maxParticles = binaryReader.ReadInt32();
                playOnAwake = binaryReader.ReadBoolean();
                prewarm = binaryReader.ReadBoolean();
            }


            public override bool Equals(object obj)
            {
                var other = obj as MainModuleKun;
                if(other == null)
                {
                    return false;
                }
                if(duration.Equals(other.duration)== false)
                {
                    return false;
                }
                if (loop.Equals(other.loop) == false)
                {
                    return false;
                }
                if (maxParticles.Equals(other.maxParticles) == false)
                {
                    return false;
                }

                if (playOnAwake.Equals(other.playOnAwake) == false)
                {
                    return false;
                }
                if (prewarm.Equals(other.prewarm) == false)
                {
                    return false;
                }

                return true;
            }


            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
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
            main = new MainModuleKun();
            if (particleSystem)
            {
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
                
                main.duration                   = this.main.duration;
                main.loop                       = this.main.loop;
                main.maxParticles               = this.main.maxParticles;
                main.playOnAwake                = this.main.playOnAwake;
                main.prewarm                    = this.main.prewarm;

                var emission                    = particleSystem.emission;
                emission.enabled                = this.emission;

                var shape                       = particleSystem.shape;
                shape.enabled                   = this.shape;

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


        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            SerializerKun.Serialize<MainModuleKun>(binaryWriter, main);                        
            binaryWriter.Write(emission);
            binaryWriter.Write(shape);
            binaryWriter.Write(velocityOverLifetime);
            binaryWriter.Write(limitVelocityOverLifetime);
            binaryWriter.Write(inheritVelocity);
            binaryWriter.Write(forceOverLifetime);
            binaryWriter.Write(colorOverLifetime);
            binaryWriter.Write(colorBySpeed);
            binaryWriter.Write(sizeOverLifetime);
            binaryWriter.Write(sizeBySpeed);
            binaryWriter.Write(rotationOverLifetime);
            binaryWriter.Write(rotationBySpeed);
            binaryWriter.Write(externalForces);
            binaryWriter.Write(noise);
            binaryWriter.Write(collision);
            binaryWriter.Write(trigger);
            binaryWriter.Write(subEmitters);
            binaryWriter.Write(textureSheetAnimation);
            binaryWriter.Write(trails);
            binaryWriter.Write(customData);
            binaryWriter.Write(isEmitting);
            binaryWriter.Write(isPaused);
            binaryWriter.Write(isPlaying);
            binaryWriter.Write(isStopped);
        }


        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            main = SerializerKun.DesirializeObject<MainModuleKun>(binaryReader);
            emission = binaryReader.ReadBoolean();
            shape = binaryReader.ReadBoolean();
            velocityOverLifetime = binaryReader.ReadBoolean();
            limitVelocityOverLifetime = binaryReader.ReadBoolean();
            inheritVelocity = binaryReader.ReadBoolean();
            forceOverLifetime = binaryReader.ReadBoolean();
            colorOverLifetime = binaryReader.ReadBoolean();
            colorBySpeed = binaryReader.ReadBoolean();
            sizeOverLifetime = binaryReader.ReadBoolean();
            sizeBySpeed = binaryReader.ReadBoolean();

            rotationOverLifetime = binaryReader.ReadBoolean();
            rotationBySpeed = binaryReader.ReadBoolean();
            externalForces = binaryReader.ReadBoolean();
            noise = binaryReader.ReadBoolean();
            collision = binaryReader.ReadBoolean();
            trigger = binaryReader.ReadBoolean();
            subEmitters = binaryReader.ReadBoolean();
            textureSheetAnimation = binaryReader.ReadBoolean();
            trails = binaryReader.ReadBoolean();
            customData = binaryReader.ReadBoolean();
            isEmitting = binaryReader.ReadBoolean();
            isPaused = binaryReader.ReadBoolean();
            isPlaying = binaryReader.ReadBoolean();
            isStopped = binaryReader.ReadBoolean();
        }


        public override bool Equals(object obj)
        {
            var other = obj as ParticleSystemKun;
            if(other == null)
            {
                return false;
            }

            if(!MainModuleKun.Equals(main,other.main))            
            {
                return false;
            }
            if (!emission.Equals(other.emission))
            {
                return false;
            }
            if (!shape.Equals(other.shape))
            {
                return false;
            }
            if (!velocityOverLifetime.Equals(other.velocityOverLifetime))
            {
                return false;
            }
            if (!limitVelocityOverLifetime.Equals(other.limitVelocityOverLifetime))
            {
                return false;
            }
            if (!inheritVelocity.Equals(other.inheritVelocity) )
            {
                return false;
            }
            if (!forceOverLifetime.Equals(other.forceOverLifetime))
            {
                return false;
            }
            if (!colorOverLifetime.Equals(other.colorOverLifetime))
            {
                return false;
            }
            if (!colorBySpeed.Equals(other.colorBySpeed))
            {
                return false;
            }
            if (!sizeOverLifetime.Equals(other.sizeOverLifetime))
            {
                return false;
            }
            if (!sizeBySpeed.Equals(other.sizeBySpeed))
            {
                return false;
            }
            if (!rotationOverLifetime.Equals(other.rotationOverLifetime))
            {
                return false;
            }
            if (!rotationBySpeed.Equals(other.rotationBySpeed))
            {
                return false;
            }
            if (!externalForces.Equals(other.externalForces))
            {
                return false;
            }
            if (noise.Equals(other.noise) == false)
            {
                return false;
            }
            if(!collision.Equals(other.collision)){
                return false;
            }
            if (!trigger.Equals(other.trigger))
            {
                return false;
            }            
            if (!subEmitters.Equals(other.subEmitters))
            {
                return false;
            }
            if (!textureSheetAnimation.Equals(other.textureSheetAnimation))
            {
                return false;
            }
            if (!trails.Equals(other.trails))
            {
                return false;
            }
            if (!customData.Equals(other.customData))
            {
                return false;
            }
            if (!isEmitting.Equals(other.isEmitting))
            {
                return false;
            }
            if (!isPaused.Equals(other.isPaused))
            {
                return false;
            }
            if (!isPlaying.Equals(other.isPlaying))
            {
                return false;
            }
            if (!isStopped.Equals(other.isStopped))
            {
                return false;
            }

            return base.Equals(obj);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
