using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.ContactJudgment;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SpecificParameters.OfficeWorker;
using Takechi.CharacterController.SpecificSoundEffects.OfficeWorker;

using UnityEngine;
using static Takechi.ScriptReference.DamagesThePlayerObject.ReferencingObjectWithContactDetectionThePlayer;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;

namespace Takechi.CharacterController.ThrowingBall
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(PhotonView))]
    public class OfficeWorkerThrowingBall : ContactJudgmentManagement
    {
        #region SerializeField
        [Header("=== OfficeWorkerSpecificSoundEffects ===")]
        [SerializeField] private OfficeWorkerSpecificSoundEffects officeWorkerSpecificSoundEffects;
        [SerializeField] private MeshRenderer m_meshRenderer;
        [SerializeField] private Collider m_collider;
        [SerializeField] private Rigidbody m_rigidbody;
        [SerializeField] private ParticleSystem m_burstEffect;
        [SerializeField] private PhotonView  m_thisPhotonViwe;
        [SerializeField] private AudioSource m_audioSource;

        #endregion

        private AudioSource audioSource => m_audioSource;
        private PhotonView thisPhotonViwe => m_thisPhotonViwe;

        private float power = 500f;
        private float radius = 10f;

        private void Reset() 
        {
            m_thisPhotonViwe = this.GetComponent<PhotonView>();
            m_audioSource = this.GetComponent<AudioSource>();
            m_rigidbody = this.GetComponent<Rigidbody>();
            m_meshRenderer = this.GetComponent<MeshRenderer>();
            m_collider = this.GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            PlayOneShotWatermelonBreaking();
            m_burstEffect.Play();
            m_meshRenderer.enabled = false;
            m_collider.enabled = false;
            m_rigidbody.useGravity = false;

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

            if (!m_thisPhotonViwe.IsMine) return;
            StartCoroutine( DelayMethod( 1.2f, () => { PhotonNetwork.Destroy(this.gameObject); }));
        }

        public void PlayOneShotWatermelonBreaking()
        {
            audioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetWatermelonBreakingClip(), officeWorkerSpecificSoundEffects.GetWatermelonBreakingVolume());
        }
    }
}