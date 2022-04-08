using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;


namespace  Utj.UnityChoseKun.Engine
{
    using Utj.UnityChoseKun.Engine.Rendering.Universal;


    /// <summary>
    ///  ComponentをSerialize/Deserializeする為のClass
    ///  Program by Katsumasa.Kimura
    /// </summary>
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
    public class ComponentKun : ObjectKun{                    
        
        // 定数の定義
        


        /// <summary>
        /// ComponentKunのタイプの定義
        /// </summary>
        public enum ComponentKunType {            
            Invalid = -1,
            Transform = 0,
            Camera,
            Light,


            SpriteRenderer,

            SkinnedMeshMeshRenderer,
            MeshRenderer,
            Renderer,

            Rigidbody,

            CapsuleCollider,
            MeshCollider,
            Collider,

            Animator,

            ParticleSystem,
            
            Canvas,

            UniversalAdditionalCameraData,
            UniversalAdditionalLightData,

            MissingMono,    // Component == null
            MonoBehaviour,
            Behaviour,
            Component,           
            
            Max,
        };

               

        /// <summary>
        /// ComponentのComponentKunTypeを取得する
        /// </summary>
        /// <params name="component">チェックするComponent</params>
        /// <returns>ComponentのComponentKunType</params>
        public static ComponentKunType GetComponentKunType(Component component)
        {
            if(component == null)
            {
                return ComponentKunType.MissingMono;
            }
            //
            // [NOTE] 基底クラスのチェックが後になるように記述する必要がある
            //
            if (component is Transform)             { return ComponentKunType.Transform; }
            if (component is Camera)                { return ComponentKunType.Camera; }
            if (component is Light)                 { return ComponentKunType.Light; }

            if (component is SpriteRenderer)        { return ComponentKunType.SpriteRenderer;}

            if (component is MeshRenderer)          { return ComponentKunType.MeshRenderer; }
            if (component is SkinnedMeshRenderer)   { return ComponentKunType.SkinnedMeshMeshRenderer; }
            if (component is Renderer)              { return ComponentKunType.Renderer; }

            if (component is Rigidbody) { return ComponentKunType.Rigidbody;}

            if( component is CapsuleCollider) { return ComponentKunType.CapsuleCollider; }
            if (component is MeshCollider) { return ComponentKunType.MeshCollider; }
            if (component is Collider) { return ComponentKunType.Collider; }

            if (component is Animator) { return ComponentKunType.Animator; }

            if(component is ParticleSystem) { return ComponentKunType.ParticleSystem; }

            if (component is Canvas) { return ComponentKunType.Canvas; }


            // === Not Build-in Class ===
            var t = component.GetType();

            if (string.Compare(t.Name, "UniversalAdditionalCameraData") == 0)
            {
                return ComponentKunType.UniversalAdditionalCameraData;
            }
            if(string.Compare(t.Name, "UniversalAdditionalLightData") == 0)
            {
                return ComponentKunType.UniversalAdditionalLightData;
            }
            // ==========================

            if (component is MonoBehaviour){return ComponentKunType.MonoBehaviour;}
            if(component is Behaviour){return ComponentKunType.Behaviour;}
            if(component is Component){return ComponentKunType.Component;}
            
                        
            
            return ComponentKunType.Invalid;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentKunType"></param>
        /// <returns></returns>
        public static ComponentKun Instantiate(ComponentKunType componentKunType,Component component = null)
        {
            switch (componentKunType)
            {
                case ComponentKunType.Transform:
                    {
                        return new TransformKun(component);
                    }

                case ComponentKunType.Camera:
                    {
                        return new CameraKun(component);
                    }

                case ComponentKunType.Light:
                    {
                        return new LightKun(component);
                    }

                case ComponentKunType.SpriteRenderer:
                    {
                        return new SpriteRendererKun(component);
                    }

                case ComponentKunType.SkinnedMeshMeshRenderer:
                    {
                        return new SkinnedMeshRendererKun(component);
                    }

                case ComponentKunType.MeshRenderer:
                    {
                        return new MeshRendererKun(component);
                    }

                case ComponentKunType.Renderer:
                    {
                        return new RendererKun(component);
                    }

                case ComponentKunType.Rigidbody:
                    {
                        return new RigidbodyKun(component);
                    }

                case ComponentKunType.CapsuleCollider:
                    {
                        return new CapsuleColliderKun(component);
                    }

                case ComponentKunType.MeshCollider:
                    {
                        return new MeshColliderKun(component);
                    }

                case ComponentKunType.Collider:
                    {
                        return new ColliderKun(component);
                    }

                case ComponentKunType.Animator:
                    {
                        return new AnimatorKun(component);
                    }

                case ComponentKunType.ParticleSystem:
                    {
                        return new ParticleSystemKun(component);
                    }

                case ComponentKunType.MissingMono:
                    {
                        return new MonoBehaviourKun(component);
                    }

                case ComponentKunType.MonoBehaviour:
                    {
                        return new MonoBehaviourKun(component);
                    }

                case ComponentKunType.Behaviour:
                    {
                        return new BehaviourKun(component);
                    }
                case ComponentKunType.Component:
                    {
                        return new ComponentKun(component);
                    }

                case ComponentKunType.Canvas:
                    {
                        return new CanvasKun(component);
                    }

                case ComponentKunType.UniversalAdditionalCameraData:
                    {
                        return new UniversalAdditionalCameraDataKun(component);
                    }

                case ComponentKunType.UniversalAdditionalLightData:
                    {
                        return new UniversalAdditionalLightDataKun(component);
                    }
                default:
                    {
                        return new BehaviourKun();
                    }
            }
        }


        //
        // Memberの定義
        //

        /// <summary>
        /// ComponentKunの種別
        /// </summary>
        [SerializeField] protected ComponentKunType m_componentKunType; 
        public ComponentKunType componentKunType{
            get{return m_componentKunType;}
            protected set{m_componentKunType = value;}
        }

        // GameObjectKunのコンストラクタ・Deserialize及びAddComponentでこの変数に値が設定される。
        [SerializeField]
        public GameObjectKun m_gameObjectKun;
        public GameObjectKun gameObjectKun
        {
            get
            {
                return m_gameObjectKun;
            }

            set
            {
                m_gameObjectKun = value;
            }
        }


        public TransformKun transformKun
        {
            get 
            {
                if(gameObjectKun == null)
                {
                    return null;
                }
                return gameObjectKun.transformKun; 
            }
        }
        
        /// <summary>
        /// コンストラクタ 
        /// </summary>
        public ComponentKun():this(null){}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        public ComponentKun(Component component):base(component)
        {
            componentKunType = ComponentKunType.Component;                        
        }


        /// <summary>
        /// Componentへ内容を書き戻す
        /// </summary>
        /// <param name="component">書き戻されるComponent</param>
        /// <returns>結果  true : 書き戻しが行われた false : 書き戻しが発生しなかった</returns>        
        public virtual bool WriteBack(Component component)
        {
            return base.WriteBack(component);            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write((int)m_componentKunType);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_componentKunType = (ComponentKunType)binaryReader.ReadInt32();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var componentKun = obj as ComponentKun;
            if(componentKun == null)
            {
                return false;
            }
            if(componentKun.componentKunType != componentKunType)
            {
                return false;
            }
            
            return base.Equals(obj);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static new ComponentKun Allocater()
        {
            return new ComponentKun();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static new ComponentKun[] Allocater(int len)
        {
            return new ComponentKun[len];
        }

    }
}