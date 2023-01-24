using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;

namespace Takechi.CharacterController.OenOneWayContact
{
    public class OneWayContactJudgmentManager : MonoBehaviour
    {
        [SerializeField] private List<Collider> m_colliders = new List<Collider>(4);

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag != SearchForPrefabTag.playerCharacterPrefabTag) return;
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            foreach (Collider collider in m_colliders) { collider.enabled = false; }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag != SearchForPrefabTag.playerCharacterPrefabTag) return;
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            foreach (Collider collider in m_colliders) { collider.enabled = true; }
        }
    }
}
