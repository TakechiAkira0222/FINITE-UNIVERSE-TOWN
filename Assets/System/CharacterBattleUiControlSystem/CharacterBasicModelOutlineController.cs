using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;

namespace Takechi.NetworkInstantiation.OutlineController
{
    public class CharacterBasicModelOutlineController : MonoBehaviour
    {
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        public struct MyColor
        {
            public Color ATeamColor => new Color( 1, 0, 0, 0.1f);
            public Color BTeamColor => new Color( 0, 0, 1, 0.1f);
        }

        private MyColor m_myColor;

        private void Start()
        {
            int number =
                  m_characterStatusManagement.GetMyPhotonView().ControllerActorNr;

            string team =
                    (string)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.teamKey];

            if (team == CharacterTeamStatusName.teamAName) { m_characterStatusManagement.SetModelOulineColor(m_myColor.ATeamColor); }
            else { m_characterStatusManagement.SetModelOulineColor(m_myColor.BTeamColor); }
        }
    }
}
