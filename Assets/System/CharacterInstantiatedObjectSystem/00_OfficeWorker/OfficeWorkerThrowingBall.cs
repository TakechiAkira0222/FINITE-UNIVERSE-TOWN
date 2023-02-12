using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.ContactJudgment;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SpecificParameters.OfficeWorker;

using UnityEngine;
using static Takechi.ScriptReference.DamagesThePlayerObject.ReferencingObjectWithContactDetectionThePlayer;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;

namespace Takechi.CharacterController.ThrowingBall
{
    public class OfficeWorkerThrowingBall : ContactJudgmentManagement
    {
        [SerializeField] private PhotonView m_photonView;

        private float power = 500f;
        private float radius = 10f;

        private void Reset() { m_photonView = this.GetComponent<PhotonView>(); }

        private void OnCollisionEnter(Collision collision)
        {
            Vector3 explosionPos = collision.contacts[0].point;

            GameObject[] targets = GameObject.FindGameObjectsWithTag( SearchForPrefabTag.playerCharacterPrefabTag);

            foreach (GameObject obj in targets)
            {
                float dist = Vector3.Distance(obj.transform.position, explosionPos);

                if (dist < radius)
                {
                    Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();

                    if (rb)
                    {
                        rb.AddForce(rb.transform.up * power, ForceMode.Impulse);
                    }
                }
            }

            if (!m_photonView.IsMine) return;
            StartCoroutine( DelayMethod( 0.1f, () => { PhotonNetwork.Destroy(this.gameObject); }));
        }
    }
}