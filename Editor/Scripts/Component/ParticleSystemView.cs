using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    public class ParticleSystemView : ComponentView
    {
        static class Style
        {
            public static GUIContent DurationContent = new GUIContent("Duration");
            public static GUIContent LoopingContent = new GUIContent("Looping");
            public static GUIContent MaxParticle = new GUIContent("Max Particles");
            public static GUIContent PlayOnAwake = new GUIContent("Play On Awake");
            public static GUIContent Prewarm = new GUIContent("Prewarm");

            public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_ParticleSystem Icon");


        }


        ParticleSystemKun particleSystemKun
        {
            get { return componentKun as ParticleSystemKun; }
            set { componentKun = value; }
        }




        public ParticleSystemView():base()
        {
            mComponentIcon = Style.ComponentIcon;
        }


        private bool DrawModule(string name,bool enabled)
        {
            var rect = EditorGUILayout.BeginHorizontal();
            GUI.Box(new Rect(rect.x + 10, rect.y, rect.width - 10, rect.height), "", EditorStyles.miniButton);
            enabled = EditorGUILayout.ToggleLeft(new GUIContent(name), enabled);
            EditorGUILayout.EndHorizontal();
            
            return enabled;
        }



        public override bool OnGUI()
        {
            if (base.OnGUI())
            {
                EditorGUI.BeginChangeCheck();
                using (new EditorGUI.IndentLevelScope()) 
                {
                    particleSystemKun.main.duration = EditorGUILayout.FloatField(Style.DurationContent, particleSystemKun.main.duration);
                    particleSystemKun.main.loop = EditorGUILayout.Toggle(Style.LoopingContent,particleSystemKun.main.loop);
                    particleSystemKun.main.prewarm = EditorGUILayout.Toggle(Style.Prewarm, particleSystemKun.main.prewarm);
                    particleSystemKun.main.maxParticles = EditorGUILayout.IntField(Style.MaxParticle,particleSystemKun.main.maxParticles);
                    particleSystemKun.main.playOnAwake = EditorGUILayout.Toggle(Style.PlayOnAwake,particleSystemKun.main.playOnAwake);
                    
                    EditorGUILayout.Space();
                    EditorGUILayout.ToggleLeft(new GUIContent("Is Emitting"), particleSystemKun.isEmitting);                    
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.ToggleLeft(new GUIContent("Is Paused"), particleSystemKun.isPaused);
                    EditorGUILayout.ToggleLeft(new GUIContent("Is Playing"), particleSystemKun.isPlaying);
                    EditorGUILayout.ToggleLeft(new GUIContent("Is Stopped"), particleSystemKun.isStopped);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Modules");
                    //using (new EditorGUI.IndentLevelScope())
                    {
                        particleSystemKun.emission = DrawModule("Emission", particleSystemKun.emission);
                        particleSystemKun.shape = DrawModule("Shape", particleSystemKun.shape);
                        particleSystemKun.velocityOverLifetime = DrawModule("Velocity over Lifetime", particleSystemKun.velocityOverLifetime);
                        particleSystemKun.limitVelocityOverLifetime = DrawModule("Limit Velocity over Lifetime", particleSystemKun.limitVelocityOverLifetime);
                        particleSystemKun.inheritVelocity = DrawModule("Inherit Velocity", particleSystemKun.inheritVelocity);
                        particleSystemKun.forceOverLifetime = DrawModule("Force over Lifetime", particleSystemKun.forceOverLifetime);
                        particleSystemKun.colorOverLifetime = DrawModule("Color over Lifetime", particleSystemKun.colorOverLifetime);
                        particleSystemKun.colorBySpeed = DrawModule("Color by Speed", particleSystemKun.colorBySpeed);
                        particleSystemKun.sizeOverLifetime = DrawModule("Size over Lifetime", particleSystemKun.sizeOverLifetime);
                        particleSystemKun.sizeBySpeed = DrawModule("Size by Speed", particleSystemKun.sizeBySpeed);
                        particleSystemKun.rotationOverLifetime = DrawModule("Rotation over Lifetime", particleSystemKun.rotationOverLifetime);
                        particleSystemKun.rotationBySpeed = DrawModule("Rotation by ppeed", particleSystemKun.rotationBySpeed);
                        particleSystemKun.externalForces = DrawModule("External Forces", particleSystemKun.externalForces);
                        particleSystemKun.noise = DrawModule("Noise", particleSystemKun.noise);
                        particleSystemKun.collision = DrawModule("Collision", particleSystemKun.collision);
                        particleSystemKun.trigger = DrawModule("Trigger", particleSystemKun.trigger);
                        particleSystemKun.subEmitters = DrawModule("Sub Emitters", particleSystemKun.subEmitters);
                        particleSystemKun.textureSheetAnimation = DrawModule("Texture Sheet Animation", particleSystemKun.textureSheetAnimation);
                        particleSystemKun.trails = DrawModule("Trails", particleSystemKun.trails);
                        particleSystemKun.customData = DrawModule("Custom Data", particleSystemKun.customData);
                    }
                }
                if (EditorGUI.EndChangeCheck())
                {
                    componentKun.dirty = true;
                }
            }
            return true;
        }
    }
}