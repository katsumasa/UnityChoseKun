namespace Utj.UnityChoseKun
{
    using System.IO;

    /// <summary>
    /// Serialize/Deserializeを行う為のインターフェース
    /// </summary>
    public interface ISerializerKun
    {

        // ルール
        // メンバーが配列の場合、必ず要素数(4[byte])から始まる
        // 但し、nullの場合は-1
        // メンバーがクラスの場合、nullのケースを考慮して配列の場合同様に-1or1から始まる
        

        /// <summary>
        /// Objectをシリアライズする
        /// </summary>
        /// <param name="binaryWriter">BinaryWriter</param>
        void Serialize(BinaryWriter binaryWriter);


        /// <summary>
        /// Objectをデシリアライズする
        /// </summary>
        /// <param name="binaryReader">BinaryReader</param>
        void Deserialize(BinaryReader binaryReader);
        

    }
}