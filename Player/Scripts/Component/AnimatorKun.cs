using System.Collections;
using System.Collections.Generic;
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
        public string runtimeAnimatorController
        {
            get { return mRuntimeAnimatorController; }
            private set { mRuntimeAnimatorController = value; }
        }

        [SerializeField] string mAvatar;
        public string avatar 
        {
            get { return mAvatar; }
            private set { mAvatar = value; }
        }


        [SerializeField] bool mApplyRootMotion;
        public bool applyRootMotion
        {
            get { return mApplyRootMotion; }
            set { mApplyRootMotion = value; }
        }


        [SerializeField] AnimatorUpdateMode mUpdateMode;
        public AnimatorUpdateMode updateMode
        {
            get { return mUpdateMode; }
            set { mUpdateMode = value; }
        }


        [SerializeField]
        AnimatorCullingMode mCullingMode;
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
                    Debug.Log("animator.enabled" + animator.enabled);
                    animator.applyRootMotion = applyRootMotion;
                    animator.updateMode = updateMode;
                    animator.cullingMode = cullingMode;
                    return true;
                }                
            }
            return false;
        }
    }
}