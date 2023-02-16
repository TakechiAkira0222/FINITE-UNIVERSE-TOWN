using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

using Takechi.CharacterController.Parameters;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.CharacterController.ContactJudgment
{
    public class ContactJudgmentManagement : MonoBehaviour
    {
        protected IEnumerator DelayMethod(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }

        /// <summary>
        /// check Teammember
        /// </summary>
        /// <param name="number"></param>
        /// <returns> true == same team </returns>
        protected bool checkTeammember(int number)
        {
            Debug.Log(PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.teamNameKey]);
            return
                (string)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.teamNameKey].ToString() ==
                (string)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamNameKey].ToString();
        }
    }
}
