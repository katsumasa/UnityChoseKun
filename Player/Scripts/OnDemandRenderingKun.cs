using System.IO;
using UnityEngine;
using UnityEngine.Rendering;


namespace Utj.UnityChoseKun
{
    /// <summary>
    /// OnDemandRenderingをSerialize/Deserializeする為のClass
    /// </summary>
    [System.Serializable]
    public class OnDemandRenderingKun : ISerializerKun
    {
        
        // メンバー変数        
        [SerializeField] int mEffectiveRenderFrameRate;
        [SerializeField] int mRenderFrameInterval;
        [SerializeField] bool mWillCurrentFrameRender;
        [SerializeField] bool mIsDirty;


        // プロパティ

        public int effectiveRenderFrameRate
        {
            get { return mEffectiveRenderFrameRate; }
        }
        
        public int renderFrameInterval
        {
            get { return mRenderFrameInterval; }
            set { mRenderFrameInterval = value; }
        }
        
        
        public bool willCurrentFrameRender
        {
            get { return mWillCurrentFrameRender; }
        }


        public bool isDirty
        {
            get { return mIsDirty; }
            set { mIsDirty = value; }
        }


        // メンバー関数の定義

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OnDemandRenderingKun() : this(false) { }
        

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="isSet">true:値を設定する</param>
        public OnDemandRenderingKun(bool isSet):base()
        {
            if (isSet)
            {
#if UNITY_2019_3_OR_NEWER
                mEffectiveRenderFrameRate = OnDemandRendering.effectiveRenderFrameRate;
                mRenderFrameInterval = OnDemandRendering.renderFrameInterval;
                mWillCurrentFrameRender = OnDemandRendering.willCurrentFrameRender;
#endif
            }
        }


        /// <summary>
        /// 値を書き戻す
        /// </summary>
        public void WriteBack()
        {
            if (mIsDirty)
            {
#if UNITY_2019_3_OR_NEWER
                OnDemandRendering.renderFrameInterval = mRenderFrameInterval;
#endif
            }
        }


        /// <summary>
        /// Serializeを行う
        /// </summary>
        /// <param name="binaryWriter"></param>
        public virtual void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(mEffectiveRenderFrameRate);
            binaryWriter.Write(mRenderFrameInterval);
            binaryWriter.Write(mWillCurrentFrameRender);
            binaryWriter.Write(mIsDirty);
        }


        /// <summary>
        /// Deserializeを行う
        /// </summary>
        /// <param name="binaryReader"></param>
        public virtual void Deserialize(BinaryReader binaryReader)
        {
            mEffectiveRenderFrameRate = binaryReader.ReadInt32();
            mRenderFrameInterval = binaryReader.ReadInt32();
            mWillCurrentFrameRender = binaryReader.ReadBoolean();
            mIsDirty = binaryReader.ReadBoolean();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as OnDemandRenderingKun;
            if(other == null)
            {
                return false;
            }
            if (!int.Equals(mEffectiveRenderFrameRate, other.mEffectiveRenderFrameRate))
            {
                return false;
            }
            if (!int.Equals(mRenderFrameInterval, other.mRenderFrameInterval))
            {
                return false;
            }
            if (!bool.Equals(mWillCurrentFrameRender, other.mWillCurrentFrameRender))
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}