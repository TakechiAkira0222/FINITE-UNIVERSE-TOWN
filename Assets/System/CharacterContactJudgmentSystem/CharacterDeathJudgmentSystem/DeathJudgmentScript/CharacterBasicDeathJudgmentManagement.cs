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
using Takechi.CharacterController.SoundEffects;
using Takechi.UI.GameLogTextScrollView;

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
        [Header("=== CharacterBasicSoundEffectsStateManagement ===")]
        [SerializeField] private CharacterBasicSoundEffectsStateManagement m_characterBasicSoundEffectsStateManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private PhotonView m_thisPhotonView;
        [SerializeField] private string m_respawnPointAName = "RespawnPointA";
        [SerializeField] private string m_respawnPointBName = "RespawnPointB";

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement statusManagement => m_characterStatusManagement;
        private CharacterKeyInputStateManagement keyInputStateManagement => m_characterkeyInputStateManagement;
        private CharacterBasicSoundEffectsStateManagement soundEffectsStateManagement => m_characterBasicSoundEffectsStateManagement;
        private GameLogTextScrollViewController logTextScrollViewController => addressManagement.GetGameLogTextScrollViewController();
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private PhotonView thisPhotoView => m_thisPhotonView;
        private Camera myDeathCamera => addressManagement.GetMyDeathCamera();
        private Camera myMainCamera => addressManagement.GetMyMainCamera();
        private GameObject myAvater => addressManagement.GetMyAvater();
        private GameObject handOnlyModelObject => addressManagement.GetHandOnlyModelObject();
        private GameObject networkModelObject => addressManagement.GetNetworkModelObject();
        private GameObject deathEffect => addressManagement.GetDeathEffect();
        private string deathEffectFolderName => addressManagement.GetDeathEffectFolderName();
        private string antiFieldTagName => DeathToPlayer.ColliderTag.antiFieldTagName;
        private string mechanicalWarriorDeathblowBulletsColliderName => DeathToPlayer.ColliderName.mechanicalWarriorDeathblowBulletsColliderName;

        private bool isDead = false;
        private void setIsDead(bool flag)
        {
            isDead = flag;
            Debug.Log($"isDead <color=red>{flag}</color> to set.");
        }

        private bool getIsDead() => isDead;

        #endregion

        #region unity event
        private void Reset()
        {
            m_thisPhotonView = this.transform.GetComponent<PhotonView>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == antiFieldTagName)
            {
                soundEffectsStateManagement.PlayOneShotDeathSound();

                if (!myPhotonView.IsMine) return;
                if (getIsDead()) return;

                // dead process flag
                setIsDead(true);

                if (statusManagement.GetCustomPropertiesTeamName() == CharacterTeamStatusName.teamAName)
                {
                    RespawnProcess(collision, m_respawnPointAName);
                    logTextScrollViewController.AddTextContent($"<color=red>{PhotonNetwork.LocalPlayer.NickName}</color> AntiFieldにより死亡しました。");
                }
                else if((statusManagement.GetCustomPropertiesTeamName() == CharacterTeamStatusName.teamBName))
                {
                    RespawnProcess(collision, m_respawnPointBName);
                    logTextScrollViewController.AddTextContent($"<color=blue>{PhotonNetwork.LocalPlayer.NickName}</color> AntiFieldにより死亡しました。");
                }
                else
                {
                    Debug.LogError(" There are no places to respawn.");
                }
            }
        }
        private void OnTriggerEnter( Collider other)
        {
            if (other.gameObject.name == mechanicalWarriorDeathblowBulletsColliderName)
            {
                int number =
                  other.transform.GetComponent<PhotonView>().ControllerActorNr;

                if (checkTeammember(number)) return;

                soundEffectsStateManagement.PlayOneShotDeathSound();

                if (!myPhotonView.IsMine) return;
                if (getIsDead()) return;

                // dead process flag
                setIsDead(true);

                float power = statusManagement.GetCustomPropertiesTeamAttackPower(number);

                if (statusManagement.GetCustomPropertiesTeamName() == CharacterTeamStatusName.teamAName)
                {
                    RespawnProcess(other, m_respawnPointAName);
                    logTextScrollViewController.AddTextContent($"<color=red>{PhotonNetwork.LocalPlayer.NickName}</color> 必殺技により死亡しました。");
                }
                else if ((statusManagement.GetCustomPropertiesTeamName() == CharacterTeamStatusName.teamBName))
                {
                    RespawnProcess(other, m_respawnPointBName);
                    logTextScrollViewController.AddTextContent($"<color=blue>{PhotonNetwork.LocalPlayer.NickName}</color> 必殺技により死亡しました。");
                }
                else
                {
                    Debug.LogError(" There are no places to respawn.");
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
                RespawnPositionMovement(respawnPoint);
                setIsDead(false);

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
        private void RespawnProcess(Collider collider, string respawnPointName)
        {
            DeathEffectInstantiation(collider.ClosestPoint(collider.transform.position));

            thisPhotoView.RPC(nameof(RPC_StartOfDeathJudgment), RpcTarget.AllBufferedViaServer);

            StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds(),
                () => thisPhotoView.RPC(nameof(RPC_EndOfDeathJudgment), RpcTarget.AllBufferedViaServer, respawnPointName)));
        }

        private void RespawnProcess(Collision collision, string respawnPointName)
        {
            DeathEffectInstantiation(collision.contacts[0].point);

            thisPhotoView.RPC(nameof(RPC_StartOfDeathJudgment), RpcTarget.AllBufferedViaServer);

            StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds(),
               () => thisPhotoView.RPC(nameof(RPC_EndOfDeathJudgment), RpcTarget.AllBufferedViaServer, respawnPointName)));
        }

        /// <summary>
        /// DeathEffect の Instantiate を行います。
        /// </summary>
        /// <param name="point"></param>
        private void DeathEffectInstantiation( Vector3 point)
        {
            if (!myPhotonView.IsMine) return;

            GameObject effct = PhotonNetwork.Instantiate( deathEffectFolderName + deathEffect.name, point, Quaternion.identity);
            StartCoroutine(DelayMethod(statusManagement.GetRespawnTime_Seconds() - 0.5f, () => { PhotonNetwork.Destroy(effct);}));
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
        private void RespawnPositionMovement(string respawnPoint)
        {
            myAvater.transform.position = GameObject.Find(respawnPoint).transform.position;
            Debug.Log($" m_characterStatusManagement<color=yellow>.GetMyAvater</color>().transform.position = <color=green>GameObject</color>.Find({respawnPoint}).transform.position;");

            statusManagement.ResetCharacterInstanceState();
            Debug.Log($" m_characterStatusManagement<color=yellow>.ResetCharacterInstanceState</color>()");
        }

        #endregion
    }
}
