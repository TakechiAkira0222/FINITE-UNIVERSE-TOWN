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
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement === ")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterkeyInputStateManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private PhotonView m_thisPhotonView;
        [SerializeField] private string m_respawnPointAName = "RespawnPointA";
        [SerializeField] private string m_respawnPointBName = "RespawnPointB";

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private CharacterKeyInputStateManagement keyInputStateManagement => m_characterkeyInputStateManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private PhotonView thisPhotoView => m_thisPhotonView; 
        private Camera     myDeathCamera => addressManagement.GetMyDeathCamera();
        private Camera     myMainCamera  => addressManagement.GetMyMainCamera();
        private GameObject myAvater  => addressManagement.GetMyAvater();
        private GameObject handOnlyModelObject => addressManagement.GetHandOnlyModelObject();
        private GameObject networkModelObject  => addressManagement.GetNetworkModelObject();
        private GameObject deathEffect => addressManagement.GetDeathEffect();
        private string deathEffectFolderName => addressManagement.GetDeathEffectFolderName();
        private string antiFieldTagName => DeathToPlayer.antiFieldTagName;
        private string mechanicalWarriorDeathblowBulletsTagName => DeathToPlayer.MechanicalWarriorDeathblowBulletsTagName;

        #endregion

        #region unity event
        private void Reset()
        {
            m_thisPhotonView = this.transform.GetComponent<PhotonView>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!myPhotonView.IsMine) return;

            if (collision.gameObject.tag == antiFieldTagName)
            {
                if ( statusManagement.GetCustomPropertiesTeamName() == CharacterTeamStatusName.teamAName)
                {
                    EffectInstantiation(collision.contacts[0].point);

                    thisPhotoView.RPC(nameof(RPC_StartOfDeathJudgment), RpcTarget.AllBufferedViaServer);

                    StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds(),
                       () => thisPhotoView.RPC(nameof(RPC_EndOfDeathJudgment), RpcTarget.AllBufferedViaServer, m_respawnPointAName)));
                }
                else
                {
                    EffectInstantiation(collision.contacts[0].point);

                    thisPhotoView.RPC(nameof(RPC_StartOfDeathJudgment), RpcTarget.AllBufferedViaServer);

                    StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds(),
                        () => thisPhotoView.RPC(nameof(RPC_EndOfDeathJudgment), RpcTarget.AllBufferedViaServer, m_respawnPointAName)));
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!myPhotonView.IsMine) return;

            if ( other.gameObject.tag == mechanicalWarriorDeathblowBulletsTagName)
            {
                int number =
                  other.transform.GetComponent<PhotonView>().ControllerActorNr;

                if ( checkTeammember(number)) return;

                float power = statusManagement.GetCustomPropertiesTeamAttackPower(number);

                if (statusManagement.GetCustomPropertiesTeamName() == CharacterTeamStatusName.teamAName)
                {
                    EffectInstantiation(other.ClosestPoint(other.transform.position));

                    thisPhotoView.RPC(nameof(RPC_StartOfDeathJudgment), RpcTarget.AllBufferedViaServer);

                    StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds(),
                        () => thisPhotoView.RPC(nameof(RPC_EndOfDeathJudgment), RpcTarget.AllBufferedViaServer, m_respawnPointAName)));
                }
                else
                {
                    EffectInstantiation(other.ClosestPoint(other.transform.position));

                    thisPhotoView.RPC(nameof(RPC_StartOfDeathJudgment), RpcTarget.AllBufferedViaServer);

                    StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds(),
                        () => thisPhotoView.RPC(nameof(RPC_EndOfDeathJudgment), RpcTarget.AllBufferedViaServer, m_respawnPointBName)));
                }
            }
        }

        #endregion

        #region RPC function
        [PunRPC]
        private void RPC_StartOfDeathJudgment()
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

        [PunRPC]
        private void RPC_EndOfDeathJudgment(string respawnPoint)
        {
            if (myPhotonView.IsMine)
            {
                handOnlyModelObject.SetActive(true);
                DrawingCameraSettings(false);
                CharacterStateChange(false);
                RespawnMovement(respawnPoint);

                addressManagement.GetToFade().OnFadeIn("resumption");
                Debug.Log(" m_characterStatusManagement.<color=yellow>.GetToFade</color>().<color=yellow>.OnFadeIn</color>(resumption)");

                statusManagement.ResetCharacterParameters();
                statusManagement.ResetLocalPlayerCustomProperties();
                statusManagement.UpdateLocalPlayerCustomProrerties();
            }
            else
            {
                networkModelObject.SetActive(true);
            }
        }

        #endregion

        #region recursive function
        /// <summary>
        /// Effect のInstantiate を行います。
        /// </summary>
        /// <param name="point"></param>
        private void EffectInstantiation( Vector3 point)
        {
            if (!myPhotonView.IsMine) return;

            GameObject effct = PhotonNetwork.Instantiate( deathEffectFolderName + deathEffect.name, point, Quaternion.identity);
            StartCoroutine(DelayMethod( 0.5f, () => { PhotonNetwork.Destroy(effct);}));
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
