using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.ExternalData
{
    public class Save : MonoBehaviour
    {
        protected virtual void DataSave<DataClass>(DataClass saveData , string saveFilePath)
        {
            // セーブデータをJSON形式の文字列に変換
            string jsonString = JsonUtility.ToJson(saveData);
            // 文字列をbyte配列に変換
            byte[] bytes = Encoding.UTF8.GetBytes(jsonString);
            // AES暗号化
            byte[] arrEncrypted = AesEncrypt(bytes);
            // 指定したパスにファイルを作成
            FileStream file = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write);

            //ファイルに保存する
            try
            {
                // ファイルに保存
                file.Write(arrEncrypted, 0, arrEncrypted.Length);
            }
            finally
            {
                // ファイルを閉じる
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        /// AesManagedマネージャーを取得
        private AesManaged GetAesManager()
        {
            //任意の半角英数16文字
            string aesIv = "1234567890123456";
            string aesKey = "1234567890123456";

            AesManaged aes = new AesManaged();
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.IV = Encoding.UTF8.GetBytes(aesIv);
            aes.Key = Encoding.UTF8.GetBytes(aesKey);
            aes.Padding = PaddingMode.PKCS7;
            return aes;
        }

        /// AES暗号化
        public byte[] AesEncrypt(byte[] byteText)
        {
            // AESマネージャーの取得
            AesManaged aes = GetAesManager();
            // 暗号化
            byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

            return encryptText;
        }
    }
}
