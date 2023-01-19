using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        /// <returns></returns>
        protected bool checkTeammember(int number)
        {
            return
                (string)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.teamKey].ToString() ==
                (string)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamKey].ToString();
        }
    }
}
