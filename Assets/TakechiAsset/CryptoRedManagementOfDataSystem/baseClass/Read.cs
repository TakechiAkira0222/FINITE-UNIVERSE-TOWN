using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.ExternalData
{
    public class Read : Save
    {
        protected virtual DataClass LoadData<DataClass>(DataClass saveData , string saveFilePath)
        {
            //セーブファイルがあるか
            if (File.Exists(saveFilePath))
            {
                //ファイルモードをオープンにする
                FileStream file = new FileStream(saveFilePath, FileMode.Open, FileAccess.Read);
                try
                {
                    // ファイル読み込み
                    byte[] arrRead = File.ReadAllBytes(saveFilePath);

                    // 復号化
                    byte[] arrDecrypt = AesDecrypt(arrRead);

                    // byte配列を文字列に変換
                    string decryptStr = Encoding.UTF8.GetString(arrDecrypt);

                    // JSON形式の文字列をセーブデータのクラスに変換
                    saveData = JsonUtility.FromJson<DataClass>(decryptStr);

                    return saveData;
                }
                catch
                {
                    Debug.LogError("The save data may have been tampered with.");
                    return saveData;
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
            else
            {
                Debug.Log(" no save file ");
                return saveData;
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

        /// AES復号化
        public byte[] AesDecrypt(byte[] byteText)
        {
            // AESマネージャー取得
            var aes = GetAesManager();
            // 復号化
            byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

            return decryptText;
        }
    }
}
