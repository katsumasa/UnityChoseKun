using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;


namespace Utj.UnityChoseKun {
    
    // <summary>
    // AnimatorをSerialize/Deserializeする為のクラス
    // Programed By Katsumasa.Kimura
    // </summary>
    [System.Serializable]
    public class AnimatorKun : BehaviourKun
    {            
        // 変数の定義
        [SerializeField] string mRuntimeAnimatorController;
        [SerializeField] string mAvatar;
        [SerializeField] bool mApplyRootMotion;
        [SerializeField] AnimatorUpdateMode mUpdateMode;
        [SerializeField] AnimatorCullingMode mCullingMode;


        public string runtimeAnimatorController
        {
            get { return mRuntimeAnimatorController; }
            private set { mRuntimeAnimatorController = value; }
        }
        
        public string avatar 
        {
            get { return mAvatar; }
            private set { mAvatar = value; }
        }
        
        public bool applyRootMotion
        {
            get { return mApplyRootMotion; }
            set { mApplyRootMotion = value; }
        }

        public AnimatorUpdateMode updateMode
        {
            get { return mUpdateMode; }
            set { mUpdateMode = value; }
        }

        public AnimatorCullingMode cullingMode
        {
            get { return mCullingMode; }
            set { mCullingMode = value; }
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AnimatorKun() : this(null) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="component">Animatorオブジェクト</param>
        public AnimatorKun(Component component):base(component)
        {            
            componentKunType = ComponentKunType.Animator;
            runtimeAnimatorController = "None";
            avatar = "None";
            var animator = component as Animator;
            if (animator)
            {
                if(animator.runtimeAnimatorController != null) {
                    runtimeAnimatorController = animator.runtimeAnimatorController.name;
                } else
                {
                    runtimeAnimatorController = "None";
                }                
                
                if (animator.avatar != null)
                {
                    avatar = animator.avatar.name;
                } else
                {
                    avatar = "None";
                }
                applyRootMotion = animator.applyRootMotion;
                updateMode = animator.updateMode;
                cullingMode = animator.cullingMode;
            }
        }

        /// <summary>
        /// AnimatorKunの内容をAnimatorへ書き戻す
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool WriteBack(Component component)
        {
            if (base.WriteBack(component))
            {
                var animator = component as Animator;
                if (animator)
                {                    
                    animator.applyRootMotion = applyRootMotion;
                    animator.updateMode = updateMode;
                    animator.cullingMode = cullingMode;
                    return true;
                }                
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryWriter"></param>
        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(mRuntimeAnimatorController);
            binaryWriter.Write(mAvatar);
            binaryWriter.Write(mApplyRootMotion);
            binaryWriter.Write((Int32)mUpdateMode);
            binaryWriter.Write((Int32)mCullingMode);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="binaryReader"></param>
        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            mRuntimeAnimatorController = binaryReader.ReadString();
            mAvatar = binaryReader.ReadString();
            mApplyRootMotion = binaryReader.ReadBoolean();
            mUpdateMode = (AnimatorUpdateMode)binaryReader.ReadInt32();
            mCullingMode = (AnimatorCullingMode)binaryReader.ReadInt32();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as AnimatorKun;
            if(other == null)
            {
                return false;
            }
            if (!string.Equals(mRuntimeAnimatorController, other.mRuntimeAnimatorController))
            {
                return false;
            }
            if (!string.Equals(mAvatar, other.mAvatar))
            {
                return false;
            }
            if (!bool.Equals(mApplyRootMotion, other.mApplyRootMotion))
            {
                return false;
            }
            if (!int.Equals(mUpdateMode, other.mUpdateMode))
            {
                return false;
            }
            if (!int.Equals(mCullingMode, other.mCullingMode))
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
            return instanceID;
        }
    }
}