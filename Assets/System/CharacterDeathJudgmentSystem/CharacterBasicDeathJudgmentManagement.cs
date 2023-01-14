using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Takechi.CharacterController.Parameters;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using Takechi.CharacterController.KeyInputStete;

namespace Takechi.CharacterController.DeathJudgment
{
    public class CharacterBasicDeathJudgmentManagement : MonoBehaviour
    {
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        [SerializeField] private string m_tagName = "AntiField";
        [SerializeField] private string m_RespawnPointAName = "RespawnPointA";
        [SerializeField] private string m_RespawnPointBName = "RespawnPointB";

        private void OnCollisionEnter(Collision collision)
        {
            if ( collision.gameObject.tag == m_tagName)
            {
                if ((string)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
                {
                    StartOfDeathJudgment( m_RespawnPointAName);
                    Invoke(nameof(EndOfDeathJudgment), m_characterStatusManagement.GetRespawnTime_Seconds());
                }
                else
                {
                    StartOfDeathJudgment( m_RespawnPointBName);
                    Invoke(nameof(EndOfDeathJudgment), m_characterStatusManagement.GetRespawnTime_Seconds());
                }
            }
        }

        private void StartOfDeathJudgment(string respawnPoint)
        {
            m_characterStatusManagement.GetToFade().OnFadeOut("‘Ò‹@’†");
            m_characterStatusManagement.GetMyAvater().transform.position = GameObject.Find(respawnPoint).transform.position;
            m_characterStatusManagement.GetMyRigidbody().isKinematic = true;
            m_characterKeyInputStateManagement.SetOperation(false);
        }

        private void EndOfDeathJudgment()
        {
            m_characterStatusManagement.GetToFade().OnFadeIn("‘Ò‹@’†");
            m_characterStatusManagement.ResetCharacterParameters();
            m_characterStatusManagement.GetMyRigidbody().isKinematic = false;
            m_characterKeyInputStateManagement.SetOperation(true);
        }
    }
}
