using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.CryptoRedPath
{
    public class ReferenceCryptoRedPath : MonoBehaviour
    {
        public static class Cleanpath
        {
            public static string nickNameDataPath => Application.persistentDataPath + "/nickNameData.bytes";
            public static string controlsSettingDataPath => Application.persistentDataPath + "/controlsSettingData.bytes";
        }
    }
}

