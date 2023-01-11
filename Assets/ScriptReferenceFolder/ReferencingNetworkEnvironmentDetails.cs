using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.NetworkEnvironment
{
    public class ReferencingNetworkEnvironmentDetails : MonoBehaviour
    {
        public static class NetworkSyncSettings
        {
            /// <summary>
            /// connection Synchronization Time
            /// </summary>
            public const int connectionSynchronizationTime = 1000;

            /// <summary>
            /// instantiationSetting Synchronization Time
            /// </summary>
            public const int instantiationSettingSynchronizationTime = 2000;
        }
    }
}