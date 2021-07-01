namespace  Utj.UnityChoseKun 
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using UnityEngine;

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

            

            MissingMono,    // Component == null
            MonoBehaviour,
            Behaviour,
            Component,           
            
            Max,
        };

        
        class ComponentPair {
            public System.Type componentType;
            public System.Type componenKunType;
           
            
            public ComponentPair(System.Type componentType,System.Type componenKunType){
                this.componentType = componentType;
                this.componenKunType = componenKunType;
            }
        }

        
        static readonly Dictionary<ComponentKunType,ComponentPair> componentPairDict = new Dictionary<ComponentKunType, ComponentPair>() 
        {
            {ComponentKunType.Transform,new ComponentPair(typeof(Transform),typeof(TransformKun))},
            {ComponentKunType.Camera,new ComponentPair(typeof(Camera),typeof(CameraKun))},
            {ComponentKunType.Light,new ComponentPair(typeof(Light),typeof(LightKun))},

            {ComponentKunType.SpriteRenderer,new ComponentPair(typeof(SpriteRenderer),typeof(SpriteRendererKun))},

            {ComponentKunType.SkinnedMeshMeshRenderer,new ComponentPair(typeof(SkinnedMeshRenderer),typeof(SkinnedMeshRendererKun))},            
            {ComponentKunType.MeshRenderer,new ComponentPair(typeof(MeshRenderer),typeof(MeshRendererKun))},
            
            {ComponentKunType.Renderer,new ComponentPair(typeof(Renderer),typeof(RendererKun))},
            
            {ComponentKunType.Rigidbody,new ComponentPair(typeof(Rigidbody),typeof(RigidbodyKun))},

            {ComponentKunType.CapsuleCollider,new ComponentPair(typeof(CapsuleCollider),typeof(CapsuleColliderKun))},
            {ComponentKunType.MeshCollider,new ComponentPair(typeof(MeshCollider),typeof(MeshColliderKun))},
            {ComponentKunType.Collider,new ComponentPair(typeof(Collider),typeof(ColliderKun))},

            {ComponentKunType.Animator,new ComponentPair(typeof(Animator),typeof(AnimatorKun)) },

            {ComponentKunType.ParticleSystem,new ComponentPair(typeof(ParticleSystem),typeof(ParticleSystemKun)) },

            {ComponentKunType.MissingMono,new ComponentPair(typeof(MonoBehaviour),typeof(MonoBehaviourKun))},
            {ComponentKunType.MonoBehaviour,new ComponentPair(typeof(MonoBehaviour),typeof(MonoBehaviourKun))},
            {ComponentKunType.Behaviour,new ComponentPair(typeof(Behaviour),typeof(BehaviourKun))},
            {ComponentKunType.Component,new ComponentPair(typeof(Component),typeof(ComponentKun))},
            
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
            if (component is Transform) { return ComponentKunType.Transform; }
            if (component is Camera) { return ComponentKunType.Camera; }
            if (component is Light) { return ComponentKunType.Light; }

            if (component is SpriteRenderer) { return ComponentKunType.SpriteRenderer;}

            if (component is MeshRenderer) { return ComponentKunType.MeshRenderer; }
            if (component is SkinnedMeshRenderer) { return ComponentKunType.SkinnedMeshMeshRenderer; }
            if (component is Renderer) { return ComponentKunType.Renderer; }

            if (component is Rigidbody) { return ComponentKunType.Rigidbody;}

            if( component is CapsuleCollider) { return ComponentKunType.CapsuleCollider; }
            if (component is MeshCollider) { return ComponentKunType.MeshCollider; }
            if (component is Collider) { return ComponentKunType.Collider; }

            if (component is Animator) { return ComponentKunType.Animator; }

            if(component is ParticleSystem) { return ComponentKunType.ParticleSystem; }

            

            if (component is MonoBehaviour){return ComponentKunType.MonoBehaviour;}
            if(component is Behaviour){return ComponentKunType.Behaviour;}
            if(component is Component){return ComponentKunType.Component;}
            return ComponentKunType.Invalid;
        }


        /// <summary> 
        /// ComponentのSystem.Typeを取得する 
        /// </summary>
        /// <params name="componentKunType">Componentと一致するComponentKunType</params>
        /// <returns>ComponentのSystem.Type</returns>
        public static System.Type GetComponentSystemType(ComponentKunType componentKunType)
        {            
            return componentPairDict[componentKunType].componentType;
        }


        /// <summary> 
        /// ComponentのSystemTypeを取得する
        /// </summary>
        /// <params name="component">チェックされるComponent</param>
        /// <returns>ComponentのSystem.Type</returns>
        public static System.Type GetComponentSystemType(Component component)
        {            
            return GetComponentSystemType(GetComponentKunType(component));            
        }


        /// <summary>
        /// ComponentKunTypeからComponentKunのSystem.Typeを取得する
        /// </summary>
        /// <params name="componentKunType">チェックするComponentKunType</params>
        /// <returns>ComponentKunのSystem.Type</returns>
        public static System.Type GetComponetKunSyetemType(ComponentKunType componentKunType)
        {
            if (!componentPairDict.ContainsKey(componentKunType))
            {
                UnityChoseKun.LogError("NotContainKey");
            }

            return componentPairDict[componentKunType].componenKunType;
        }


        /// <summary>
        /// ComponentのSystem,Typeを取得する
        /// </summary>
        /// <params name="component">チェックするComponent</params>
        /// <returns>ComponentのSystem.Type</returns>
        public static System.Type GetComponetKunSyetemType(Component component)
        {            
            return GetComponetKunSyetemType(GetComponentKunType(component));           
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentKunType"></param>
        /// <returns></returns>
        public static ComponentKun Instantiate(ComponentKunType componentKunType)
        {
            switch (componentKunType)
            {
                case ComponentKunType.Transform:
                    {
                        return new TransformKun();
                    }

                case ComponentKunType.Camera:
                    {
                        return new CameraKun();
                    }
                case ComponentKunType.Light:
                    {
                        return new LightKun();
                    }

                case ComponentKunType.SpriteRenderer:
                    {
                        return new SpriteRendererKun();
                    }

                case ComponentKunType.SkinnedMeshMeshRenderer:
                    {
                        return new SkinnedMeshRendererKun();
                    }

                case ComponentKunType.MeshRenderer:
                    {
                        return new MeshRendererKun();
                    }

                case ComponentKunType.Renderer:
                    {
                        return new RendererKun();
                    }

                case ComponentKunType.Rigidbody:
                    {
                        return new RigidbodyKun();
                    }

                case ComponentKunType.CapsuleCollider:
                    {
                        return new CapsuleColliderKun();
                    }

                case ComponentKunType.MeshCollider:
                    {
                        return new MeshColliderKun();
                    }


                case ComponentKunType.Collider:
                    {
                        return new ColliderKun();
                    }

                case ComponentKunType.Animator:
                    {
                        return new AnimatorKun();
                    }

                case ComponentKunType.ParticleSystem:
                    {
                        return new ParticleSystemKun();
                    }

                case ComponentKunType.MissingMono:
                    {
                        return new MonoBehaviourKun();
                    }

                case ComponentKunType.MonoBehaviour:
                    {
                        return new MonoBehaviourKun();
                    }

                case ComponentKunType.Behaviour:
                    {
                        return new BehaviourKun();
                    }
                case ComponentKunType.Component:
                    {
                        return new ComponentKun();
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