using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

namespace Utj.UnityChoseKun
{

    public class RenderTextureKun : TextureKun
    {
        [SerializeField] int m_antiAliasing;
        [SerializeField] bool m_autoGenerateMips;
        [SerializeField] bool m_bindTextureMS;
        [SerializeField] int m_depth;
        [SerializeField] bool m_enableRandomWrite;
        [SerializeField] bool m_sRGB;
        [SerializeField] GraphicsFormat m_stencilFormat;
        [SerializeField] bool m_useDynamicScale;
        [SerializeField] bool m_useMipMap;
        [SerializeField] int m_volumeDepth;
        [SerializeField] VRTextureUsage m_vrUsage;


        public int antiAliasing
        {
            get { return m_antiAliasing; }
            set { m_antiAliasing = value; }
        }

        public bool autoGenerateMips
        {
            get { return m_autoGenerateMips; }
            set { m_autoGenerateMips = value; }
        }

        public bool bindTextureMS
        {
            get { return m_bindTextureMS; }
            set { m_bindTextureMS = value; }
        }

        public int depth
        {
            get { return m_depth; }
            set { m_depth = value; }
        }

        public bool enableRandomWrite
        {
            get { return m_enableRandomWrite; }
            set { m_enableRandomWrite = value; }
        }

        public bool sRGB
        {
            get { return m_sRGB; }
        }

        public GraphicsFormat stencilFormat
        {
            get { return m_stencilFormat; }
            set { m_stencilFormat = value; }
        }

        public bool useDynamicScale
        {
            get { return m_useDynamicScale; }
            set { m_useDynamicScale = value; }
        }

        public bool useMipMap
        {
            get { return m_useMipMap; }
            set { m_useMipMap = value; }
        }

        public int volumeDepth
        {
            get { return m_volumeDepth; }
            set { m_volumeDepth = value; }
        }

        public VRTextureUsage vrUsage
        {
            get { return m_vrUsage; }
            set { m_vrUsage = value; }
        }

        public RenderTextureKun() : this(null) { }

        public RenderTextureKun(Object obj) : base(obj)
        {
            var renderTexture = obj as RenderTexture;
            if (renderTexture)
            {
                m_antiAliasing = renderTexture.antiAliasing;
                m_autoGenerateMips = renderTexture.autoGenerateMips;
                m_bindTextureMS = renderTexture.bindTextureMS;
                m_depth = renderTexture.depth;
                m_enableRandomWrite = renderTexture.enableRandomWrite;
                m_sRGB = renderTexture.sRGB;
                m_stencilFormat = renderTexture.stencilFormat;
                m_useDynamicScale = renderTexture.useDynamicScale;
                m_useMipMap = renderTexture.useMipMap;
                m_volumeDepth = renderTexture.volumeDepth;
                m_vrUsage = renderTexture.vrUsage;
            }
        }

        public override bool WriteBack(UnityEngine.Object obj)
        {
            if (base.WriteBack(obj))
            {
                var renderTexture = obj as RenderTexture;
                if (renderTexture)
                {
                    renderTexture.antiAliasing = m_antiAliasing;
                    renderTexture.autoGenerateMips = m_autoGenerateMips;
                    renderTexture.bindTextureMS = m_bindTextureMS;
                    renderTexture.depth = m_depth;
                    renderTexture.enableRandomWrite = m_enableRandomWrite;
                    renderTexture.stencilFormat = m_stencilFormat;
                    renderTexture.useDynamicScale = m_useDynamicScale;
                    renderTexture.useMipMap = m_useMipMap;
                    renderTexture.volumeDepth = m_volumeDepth;
                    renderTexture.vrUsage = m_vrUsage;

                }
                return true;
            }
            return false;
        }

        public override void Serialize(BinaryWriter binaryWriter)
        {
            base.Serialize(binaryWriter);
            binaryWriter.Write(m_antiAliasing);
            binaryWriter.Write(m_autoGenerateMips);
            binaryWriter.Write(m_bindTextureMS);
            binaryWriter.Write(m_depth);
            binaryWriter.Write(m_enableRandomWrite);
            binaryWriter.Write(m_sRGB);
            binaryWriter.Write((int)m_stencilFormat);
            binaryWriter.Write(m_useDynamicScale);
            binaryWriter.Write(m_useMipMap);
            binaryWriter.Write(m_volumeDepth);
            binaryWriter.Write((int)m_vrUsage);
        }


        public override void Deserialize(BinaryReader binaryReader)
        {
            base.Deserialize(binaryReader);
            m_antiAliasing = binaryReader.ReadInt32();
            m_autoGenerateMips = binaryReader.ReadBoolean();
            m_bindTextureMS = binaryReader.ReadBoolean();
            m_depth = binaryReader.ReadInt32();
            m_enableRandomWrite = binaryReader.ReadBoolean();
            m_sRGB = binaryReader.ReadBoolean();
            m_stencilFormat = (GraphicsFormat)binaryReader.ReadInt32();
            m_useDynamicScale = binaryReader.ReadBoolean();
            m_useMipMap = binaryReader.ReadBoolean();
            m_volumeDepth = binaryReader.ReadInt32();
            m_vrUsage = (VRTextureUsage)binaryReader.ReadInt32();
        }


        public override bool Equals(object obj)
        {
            if(base.Equals(obj) == false)
            {
                return false;
            }
            var renderTextureKun = obj as RenderTextureKun;
            if(renderTextureKun == null)
            {
                return false;
            }
            if(m_antiAliasing.Equals(renderTextureKun.m_antiAliasing) == false)
            {
                return false;
            }
            if(m_autoGenerateMips.Equals(renderTextureKun.m_autoGenerateMips) == false)
            {
                return false;
            }
            if(m_bindTextureMS.Equals(renderTextureKun.m_bindTextureMS)== false)
            {
                return false;
            }
            if(m_depth.Equals(renderTextureKun.m_depth) == false)
            {
                return false;
            }
            if(m_enableRandomWrite.Equals(renderTextureKun.m_enableRandomWrite) == false)
            {
                return false;
            }
            if (m_sRGB.Equals(renderTextureKun.m_sRGB) == false)
            {
                return false;
            }
            if(m_stencilFormat.Equals(renderTextureKun.m_stencilFormat) == false)
            {
                return false;
            }
            if (m_useDynamicScale.Equals(renderTextureKun.m_useDynamicScale) == false)
            {
                return false;
            }
            if(m_useMipMap.Equals(renderTextureKun.m_useMipMap)==false)
            {
                return false;
            }
            if (m_volumeDepth.Equals(renderTextureKun.m_volumeDepth) == false)
            {
                return false;
            }
            if (m_vrUsage.Equals(renderTextureKun.m_vrUsage) == false)
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
}