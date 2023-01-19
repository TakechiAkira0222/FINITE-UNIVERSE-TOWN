using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TakechiEngine.PUN;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.ContactJudgment;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.DamagesThePlayerObject.ReferencingObjectWithContactDetectionThePlayer;

namespace Takechi.CharacterController.DeathJudgment
{
    public class CharacterBasicDeathJudgmentManagement : ContactJudgmentManagement
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement  m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement === ")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterkeyInputStateManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private string m_respawnPointAName = "RespawnPointA";
        [SerializeField] private string m_respawnPointBName = "RespawnPointB";

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private CharacterKeyInputStateManagement keyInputStateManagement => m_characterkeyInputStateManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Camera myDeathCamera => addressManagement.GetMyDeathCamera();
        private Camera myMainCamera  => addressManagement.GetMyMainCamera();
        private GameObject myAvater  => addressManagement.GetMyAvater();
        private GameObject handOnlyModelObject => addressManagement.GetHandOnlyModelObject();
        private GameObject networkModelObject => addressManagement.GetNetworkModelObject();
        private GameObject deathEffect => addressManagement.GetDeathEffect();
        private string deathEffectFolderName => addressManagement.GetDeathEffectFolderName();
        private string antiFieldTagName => DeathToPlayer.antiFieldTagName;
        private string mechanicalWarriorDeathblowBulletsTagName => DeathToPlayer.MechanicalWarriorDeathblowBulletsTagName;

        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == antiFieldTagName)
            {
                if ((string)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
                {
                    EffectInstantiation(collision.contacts[0].point);

                    StartOfDeathJudgment();
                    StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds(),
                        () => EndOfDeathJudgment(m_respawnPointAName)));
                }
                else
                {
                    EffectInstantiation(collision.contacts[0].point);

                    StartOfDeathJudgment();
                    StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds(),
                        () => EndOfDeathJudgment(m_respawnPointBName)));
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if ( other.gameObject.tag == mechanicalWarriorDeathblowBulletsTagName)
            {
                int number =
                  other.transform.GetComponent<PhotonView>().ControllerActorNr;

                if ( checkTeammember(number)) return;

                float power =
                    (float)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.attackPowerKey];

                if ((string)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
                {
                    EffectInstantiation(other.ClosestPoint(other.transform.position));

                    StartOfDeathJudgment();
                    StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds(),
                        () => EndOfDeathJudgment(m_respawnPointAName)));
                }
                else
                {
                    EffectInstantiation(other.ClosestPoint(other.transform.position));

                    StartOfDeathJudgment();
                    StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds(),
                        () => EndOfDeathJudgment(m_respawnPointBName)));
                }
            }
        }

        private void StartOfDeathJudgment()
        {
            if (myPhotonView.IsMine)
            {
                handOnlyModelObject.SetActive(false);
                DrawingCameraSettings(true);
                CharacterStateChange(true);
            }
            else
            {
                networkModelObject.SetActive(false);
            }
        }
        private void EndOfDeathJudgment(string respawnPoint)
        {
            if (myPhotonView.IsMine)
            {
                handOnlyModelObject.SetActive(true);
                DrawingCameraSettings(false);
                CharacterStateChange(false);
                RespawnMovement(respawnPoint);

                addressManagement.GetToFade().OnFadeIn("resumption");
                Debug.Log(" m_characterStatusManagement.<color=yellow>.GetToFade</color>().<color=yellow>.OnFadeIn</color>(resumption)");

                m_characterStatusManagement.ResetCharacterParameters();
                m_characterStatusManagement.UpdateLocalPlayerCustomProrerties();
            }
            else
            {
                networkModelObject.SetActive(true);
            }
        }

        #region recursive function
        /// <summary>
        /// Effect のInstantiate を行います。
        /// </summary>
        /// <param name="point"></param>
        private void EffectInstantiation( Vector3 point)
        {
            if (!myPhotonView.IsMine) return;

            GameObject effct = PhotonNetwork.Instantiate( deathEffectFolderName + deathEffect.name, point, Quaternion.identity);
            StartCoroutine(DelayMethod(0.5f, () => { PhotonNetwork.Destroy(effct);}));
        }
        /// <summary>
        /// instans の描画状態を、変更します。
        /// </summary>
        /// <param name="flag"></param>
        private void DrawingCameraSettings(bool flag)
        {
            myDeathCamera.gameObject.SetActive(flag);
            Debug.Log($" m_characterStatusManagement.<color=yellow>GetMyDeathCamera</color>(<color=blue>{flag}</color>)");

            myMainCamera.gameObject.SetActive(!flag);
            Debug.Log($" m_characterStatusManagement.<color=yellow>GetMyMainCamera</color>(<color=blue>{!flag}</color>)");
        }
        /// <summary>
        ///　character State Change
        /// </summary>
        /// <param name="flag"></param>
        private void CharacterStateChange(bool flag)
        {
            statusManagement.SetIsKinematic(flag);
            Debug.Log($" m_characterStatusManagement.<color=yellow>GetMyRigidbody</color>() = {flag}");

            keyInputStateManagement.SetOperation(!flag);
            Debug.Log($" keyInputStateManagement.<color=yellow>.SetOperation</color>() = {!flag}");
        }
        /// <summary>
        /// characterを、リスポーン状態に変更します。
        /// </summary>
        /// <param name="respawnPoint"></param>
        private void RespawnMovement(string respawnPoint)
        {
            myAvater.transform.position = GameObject.Find(respawnPoint).transform.position;
            Debug.Log($" m_characterStatusManagement<color=yellow>.GetMyAvater</color>().transform.position = <color=green>GameObject</color>.Find({respawnPoint}).transform.position;");

            statusManagement.ResetCharacterInstanceState();
            Debug.Log($" m_characterStatusManagement<color=yellow>.ResetCharacterInstanceState</color>()");
        }

        #endregion
    }
}
