using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Takechi.CharacterController.Parameters;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using Takechi.CharacterController.KeyInputStete;
using TakechiEngine.PUN;
using System.IO;

namespace Takechi.CharacterController.DeathJudgment
{
    public class CharacterBasicDeathJudgmentManagement : TakechiPunCallbacks
    {
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        [SerializeField] private string m_tagName = "AntiField";
        [SerializeField] private string m_respawnPointAName = "RespawnPointA";
        [SerializeField] private string m_respawnPointBName = "RespawnPointB";
        [SerializeField] private GameObject m_deathEffect;

        private void OnCollisionEnter(Collision collision)
        {
            if ( collision.gameObject.tag == m_tagName)
            {
                if ((string)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
                {
                    EffectInstantiation(collision.contacts[0].point);

                    StartOfDeathJudgment( );
                    StartCoroutine( DelayMethod(m_characterStatusManagement.GetRespawnTime_Seconds(), 
                        () => EndOfDeathJudgment( m_respawnPointAName)));
                }
                else
                {
                    EffectInstantiation(collision.contacts[0].point);

                    StartOfDeathJudgment( );
                    StartCoroutine(DelayMethod(m_characterStatusManagement.GetRespawnTime_Seconds(), 
                        () => EndOfDeathJudgment(m_respawnPointBName)));
                }
            }
        }

        private void StartOfDeathJudgment()
        {
            if (m_characterStatusManagement.GetMyPhotonView().IsMine)
            {
                m_characterStatusManagement.GetHandOnlyModelObject().SetActive(false);
                DrawingCameraSettings(true);
                CharacterStateChange(true);
            }
            else
            {
                m_characterStatusManagement.GetNetworkModelObject().SetActive(false);
            }
        }

        private void EndOfDeathJudgment(string respawnPoint)
        {
            if (m_characterStatusManagement.GetMyPhotonView().IsMine)
            {
                m_characterStatusManagement.GetHandOnlyModelObject().SetActive(true);
                DrawingCameraSettings(false);
                CharacterStateChange(false);
                RespawnMovement(respawnPoint);

                m_characterStatusManagement.GetToFade().OnFadeIn("resumption");
                Debug.Log(" m_characterStatusManagement.<color=yellow>.GetToFade</color>().<color=yellow>.OnFadeIn</color>(resumption)");

                m_characterStatusManagement.ResetCharacterParameters();
                m_characterStatusManagement.UpdateLocalPlayerCustomProrerties();
            }
            else
            {
                m_characterStatusManagement.GetNetworkModelObject().SetActive(true);
            }
        }

        #region recursive function

        /// <summary>
        /// Effect のInstantiate を行います。
        /// </summary>
        /// <param name="point"></param>
        private void EffectInstantiation( Vector3 point)
        {
            if (!m_characterStatusManagement.GetMyPhotonView().IsMine) return;

            GameObject effct = PhotonNetwork.Instantiate
              (m_characterStatusManagement.GetDeathEffectFolderName() + m_deathEffect.name,
              point, Quaternion.identity);

            StartCoroutine(DelayMethod(0.5f, () => { PhotonNetwork.Destroy(effct); }));
        }

        /// <summary>
        /// instans の描画状態を、変更します。
        /// </summary>
        /// <param name="flag"></param>
        private void DrawingCameraSettings(bool flag)
        {
            m_characterStatusManagement.GetMyDeathCamera().gameObject.SetActive(flag);
            Debug.Log($" m_characterStatusManagement.<color=yellow>GetMyDeathCamera</color>(<color=blue>{flag}</color>)");

            m_characterStatusManagement.GetMyMainCamera().gameObject.SetActive(!flag);
            Debug.Log($" m_characterStatusManagement.<color=yellow>GetMyMainCamera</color>(<color=blue>{!flag}</color>)");
        }

        /// <summary>
        ///　character State Change
        /// </summary>
        /// <param name="flag"></param>
        private void CharacterStateChange(bool flag)
        {
            m_characterStatusManagement.GetMyRigidbody().isKinematic = flag;
            Debug.Log($" m_characterStatusManagement.<color=yellow>GetMyRigidbody</color>() = {flag}");

            m_characterKeyInputStateManagement.SetOperation(!flag);
            Debug.Log($" m_characterKeyInputStateManagement.<color=yellow>.SetOperation</color>() = {!flag}");
        }

      

        /// <summary>
        /// characterを、リスポーン状態に変更します。
        /// </summary>
        /// <param name="respawnPoint"></param>
        private void RespawnMovement(string respawnPoint)
        {
            m_characterStatusManagement.GetMyAvater().transform.position = GameObject.Find(respawnPoint).transform.position;
            Debug.Log($" m_characterStatusManagement<color=yellow>.GetMyAvater</color>().transform.position = <color=green>GameObject</color>.Find({respawnPoint}).transform.position;");

            m_characterStatusManagement.ResetCharacterInstanceState();
            Debug.Log($" m_characterStatusManagement<color=yellow>.ResetCharacterInstanceState</color>()");
        }

        #endregion
    }
}
