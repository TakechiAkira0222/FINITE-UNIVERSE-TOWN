using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using UnityEngine;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;

namespace Takechi.NetworkInstantiation.OutlineController
{
    public class CharacterBasicModelOutlineController : MonoBehaviour
    {
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        private void Start()
        {
            int number =
                  m_characterStatusManagement.GetMyPhotonView().ControllerActorNr;
            string team =
                    (string)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.teamKey];

            if (team == CharacterStatusTeamName.teamAName) { m_characterStatusManagement.SetModelOulineColor(Color.red); }
            else { m_characterStatusManagement.SetModelOulineColor(Color.blue); }
        }
    }
}
