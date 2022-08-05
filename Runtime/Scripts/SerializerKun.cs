using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utj.UnityChoseKun
{



    /// <summary>
    /// SerializeのユーティリティーClass
    /// </summary>
    public static class SerializerKun
    {
        public delegate ISerializerKun Allocater();
        public delegate ISerializerKun[] Allocaters(int len);



        public static void Serialize(BinaryWriter binaryWriter, int[] values)
        {
            if (values == null)
            {
                binaryWriter.Write((Int32)(-1));
            }
            else
            {
                Int32 len = values.Length;
                binaryWriter.Write(len);
                for (var i = 0; i < len; i++)
                {
                    binaryWriter.Write(values[i]);
                }
            }
        }


        public static void Serialize(BinaryWriter binaryWriter, float[] values)
        {
            if (values == null)
            {
                binaryWriter.Write((Int32)(-1));
            }
            else
            {
                Int32 len = values.Length;
                binaryWriter.Write(len);
                for (var i = 0; i < len; i++)
                {
                    binaryWriter.Write(values[i]);
                }
            }
        }

        public static void Serialize(BinaryWriter binaryWriter, bool[] values)
        {
            if(values == null)
            {
                binaryWriter.Write((Int32)(-1));
            } else
            {
                Int32 len = values.Length;
                binaryWriter.Write(len);
                for(var i = 0; i < len; i++)
                {
                    binaryWriter.Write(values[i]);
                }
            }
        }



        public static void Serialize(BinaryWriter binaryWriter, string[] values)
        {
            if (values == null)
            {
                binaryWriter.Write((Int32)(-1));
            }
            else
            {
                Int32 len = values.Length;
                binaryWriter.Write(len);
                for (var i = 0; i < len; i++)
                {
                    binaryWriter.Write(values[i]);
                }
            }
        }

        

        /// <summary>
        /// オブジェクトの配列をSerializeする
        /// </summary>
        /// <typeparam name="T">Objectの型(ISerializeを継承している必要がある)</typeparam>
        /// <param name="binaryWriter">BinaryWriter</param>
        /// <param name="objs">Objectの配列</param>
        public static void Serialize<T>(BinaryWriter binaryWriter, T[] objs) where T : ISerializerKun
        {
            if (objs == null)
            {
                binaryWriter.Write((int)-1);
            }
            else
            {
                binaryWriter.Write(objs.Length);
                for (var i = 0; i < objs.Length; i++)
                {
                    Serialize<T>(binaryWriter, objs[i]);                    
                }
            }
        }


        /// <summary>
        /// オブジェクトのSerializeを行う
        /// </summary>
        /// <typeparam name="T">オブジェクトの型(ISerializeを継承している必要がある)</typeparam>
        /// <param name="binaryWriter"></param>
        /// <param name="obj"></param>
        public static void Serialize<T>(BinaryWriter binaryWriter, T obj) where T : ISerializerKun
        {
            if (obj == null)
            {
                binaryWriter.Write(false);
            }
            else
            {
                binaryWriter.Write(true);
                obj.Serialize(binaryWriter);                
            }
        }

        public static void Serialize(BinaryWriter binaryWriter, string obj)
        {
            if (obj == null)
            {
                binaryWriter.Write(false);
            }
            else
            {
                binaryWriter.Write(true);
                binaryWriter.Write(obj);
            }
        }


        public static bool[] DesirializeBooleans(BinaryReader binaryReader)
        {
            var len = binaryReader.ReadInt32();
            if(len == -1)
            {
                return null;
            }
            var arrays = new bool[len];
            for(var i = 0; i < len; i++)
            {
                arrays[i] = binaryReader.ReadBoolean();
            }
            return arrays;
        }


        public static string[] DesirializeStrings(BinaryReader binaryReader)
        {
            var len = binaryReader.ReadInt32();
            if (len == -1)
            {
                return null;
            }
            var arrays = new string[len];
            for (var i = 0; i < len; i++)
            {
                arrays[i] = binaryReader.ReadString();
            }
            return arrays;
        }



        public static Int32[] DesirializeInt32s(BinaryReader binaryReader)
        {
            var len = binaryReader.ReadInt32();
            if (len == -1)
            {
                return null;
            }
            var arrays = new Int32[len];
            for (var i = 0; i < len; i++)
            {
                arrays[i] = binaryReader.ReadInt32();
            }
            return arrays;
        }



        public static float[] DesirializeSingles(BinaryReader binaryReader)
        {
            var len = binaryReader.ReadInt32();
            if(len == -1)
            {
                return null;
            }
            var arrays = new float[len];
            for(var i = 0; i < len; i++)
            {
                arrays[i] = binaryReader.ReadSingle();
            }
            return arrays;
        }


        public static string DesirializeString(BinaryReader binaryReader)
        {
            bool result = binaryReader.ReadBoolean();
            if (result)
            {
                return binaryReader.ReadString();
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="binaryReader"></param>
        /// <returns></returns>
        public static T DesirializeObject<T>(BinaryReader binaryReader) where T : ISerializerKun,new()
        {
            var check = binaryReader.ReadBoolean();
            if (check == false)
            {
                return default(T);
            }
            var obj = new T();
            obj.Deserialize(binaryReader);
            return obj;
        }



        /// <summary>
        /// オブジェクトのDesirializeを行う
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        /// <param name="allocater">オブジェクトのアロケーター</param>
        /// <returns></returns>
        public static ISerializerKun DesirializeObject(BinaryReader binaryReader, Allocater allocater)
        {
            var check = binaryReader.ReadBoolean();
            if (check == false)
            {
                return null;
            }
            var obj = allocater();
            obj.Deserialize(binaryReader);
            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="binaryReader"></param>
        /// <returns></returns>
        public static T[] DesirializeObjects<T>(BinaryReader binaryReader) where T : ISerializerKun, new()
        {
            var len = binaryReader.ReadInt32();
            if (len == -1)
            {
                return null;
            }
            var arrays = new T[len];
            for (var i = 0; i < len; i++)
            {
                arrays[i] = DesirializeObject<T>(binaryReader);                
            }
            return arrays;
        }


        /// <summary>
        /// オブジェクトの配列をDesirializeを実行する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="binaryReader"></param>
        /// <param name="allocaters">オブジェクトの配列を確保する為のアロケーター</param>
        /// <param name="allocater">オブジェクトを確保する為のアロケーター</param>
        /// <returns></returns>
        public static T[] DesirializeObjects<T>(BinaryReader binaryReader, Allocaters allocaters, Allocater allocater) where T : ISerializerKun , new()
        {
            var len = binaryReader.ReadInt32();
            if (len == -1)
            {
                return null;
            }
            //var arrays = allocaters(len);
            var arrays = new T[len];
            for (var i = 0; i < len; i++)
            {
                arrays[i] = (T)DesirializeObject(binaryReader, allocater);
            }
            return arrays;
        }






        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equals<T>(T[] a,T[] b)
        {
            if(a != null)
            {
                if(b == null)
                {
                    return false;
                }
                if(a.Length != b.Length)
                {
                    return false;
                }
                for(var i = 0; i < a.Length; i++)
                {
                    if(a[i] == null)
                    {
                        if(b[i] != null)
                        {
                            return false;
                        }
                    }
                    if (!a[i].Equals(b[i]))
                    {
                        return false;
                    }                    
                }
            } else if(b != null)
            {
                return false;
            }
            


            return true;
        }

    }

}
