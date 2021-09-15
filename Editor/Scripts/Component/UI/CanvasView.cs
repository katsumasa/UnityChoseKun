using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnityChoseKun
{
    [System.Serializable]
    public class CanvasView : BehaviourView
    {
        private static class Styles
        {
            public static readonly Texture2D ComponentIcon = (Texture2D)EditorGUIUtility.Load("d_Canvas Icon");
            public static readonly GUIContent RenderMode = new GUIContent("Render Mode");
            public static readonly GUIContent PixelPerfect = new GUIContent("Pixel Perfect");
            public static readonly GUIContent SortOrder = new GUIContent("Sort Order");
            public static readonly GUIContent TargetDisplay = new GUIContent("Target Display ");
            public static readonly GUIContent AdditionalCanvasShaderChannels = new GUIContent("AdditionalCanvasShaderChannels");
            public static readonly GUIContent RenderCamera = new GUIContent("Render Camera");
            public static readonly GUIContent EventCamera = new GUIContent("Event Camera");
            public static readonly GUIContent SortingLayer = new GUIContent("Sorting Layer");

            public static readonly string[] Displays =
            {
                "Display 1",
                "Display 2",
                "Display 3",
                "Display 4",
                "Display 5",
                "Display 6",
                "Display 7",
                "Display 8",
            };
        }

        public CanvasKun canvasKun
        {
            get { return behaviourKun as CanvasKun; }
            set { behaviourKun = value; }
        }

        public CanvasView():base()
        {
            componentIcon = Styles.ComponentIcon;
        }

        public override bool OnGUI()
        {
            if (base.OnGUI())
            {
                EditorGUI.BeginChangeCheck();
                canvasKun.renderMode = (RenderMode)EditorGUILayout.EnumPopup(Styles.RenderMode, canvasKun.renderMode);                
                using (new EditorGUI.IndentLevelScope())
                {
                    switch (canvasKun.renderMode)
                    {
                        case RenderMode.ScreenSpaceOverlay:
                            canvasKun.pixelPerfect = EditorGUILayout.Toggle(Styles.PixelPerfect, canvasKun.pixelPerfect);
                            canvasKun.sortingOrder = EditorGUILayout.IntField(Styles.SortOrder, canvasKun.sortingOrder);
                            canvasKun.targetDisplay = EditorGUILayout.Popup(Styles.TargetDisplay, canvasKun.targetDisplay, Styles.Displays);
                            break;

                        case RenderMode.ScreenSpaceCamera:
                            canvasKun.pixelPerfect = EditorGUILayout.Toggle(Styles.PixelPerfect, canvasKun.pixelPerfect);

                            CameraDisplay(Styles.RenderCamera);

                            canvasKun.sortingOrder = EditorGUILayout.IntField(Styles.SortOrder, canvasKun.sortingOrder);
                            break;

                        case RenderMode.WorldSpace:
                            
                            CameraDisplay(Styles.EventCamera);

                            if (SortingLayerKun.layers == null)
                            {
                                EditorGUILayout.TextField(Styles.SortingLayer,canvasKun.sortingLayerName);
                            } 
                            else
                            {
                                string[] names = new string[SortingLayerKun.layers.Length];
                                for (var i = 0; i < names.Length; i++)
                                {
                                    names[i] = SortingLayerKun.layers[i].name;
                                }

                                int id = -1;
                                for (var i = 0; i < SortingLayerKun.layers.Length; i++)
                                {
                                    if (SortingLayerKun.layers[i].id == canvasKun.sortingLayerID)
                                    {
                                        id = i;
                                        break;
                                    }
                                }
                                id = EditorGUILayout.Popup(Styles.SortingLayer,id, names);
                                if (id != -1)
                                {
                                    canvasKun.sortingLayerID = SortingLayerKun.layers[id].id;
                                    canvasKun.sortingLayerName = names[id];
                                }
                            }
                            break;                            
                    }
                }
#if UNITY_2019_1_OR_NEWER
                canvasKun.additionalShaderChannels = (AdditionalCanvasShaderChannels)EditorGUILayout.EnumFlagsField(Styles.AdditionalCanvasShaderChannels, canvasKun.additionalShaderChannels);
#else
                canvasKun.additionalShaderChannels = (AdditionalCanvasShaderChannels)EditorGUILayout.EnumMaskField(Styles.AdditionalCanvasShaderChannels, canvasKun.additionalShaderChannels);
#endif
                if (EditorGUI.EndChangeCheck())
                {
                    canvasKun.dirty = true;
                }

            }
            return true;
        }


        void CameraDisplay(GUIContent gUIContent)
        {

            if (CameraKun.allCameras != null)
            {
                int idx = -1;
                string[] names = new string[CameraKun.allCameras.Length];
                for (var i = 0; i < CameraKun.allCameras.Length; i++)
                {
                    names[i] = CameraKun.allCameras[i].gameObjectKun.name + "(Camera)";

                    if(canvasKun.worldCamera.instanceID == CameraKun.allCameras[i].instanceID)
                    {
                        idx = i;
                    }
                }
                EditorGUI.BeginChangeCheck();
                idx = EditorGUILayout.Popup(gUIContent, idx, names);
                if (EditorGUI.EndChangeCheck())
                {
                    canvasKun.worldCamera = CameraKun.allCameras[idx];
                    canvasKun.dirty = true;
                }

            }
            else
            {
                var name = "None";
                if(canvasKun.worldCamera != null)
                {
                    name = canvasKun.worldCamera.gameObjectKun.name;
                }                
                EditorGUILayout.TextField(gUIContent, name);
                
            }
        }
    }
}