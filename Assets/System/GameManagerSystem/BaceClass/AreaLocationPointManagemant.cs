using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

public class AreaLocationPointManagemant : MonoBehaviour
{
    protected List<GameObject> hitTeamAMemberList = new List<GameObject>(4);
    protected List<GameObject> hitTeamBMemberList = new List<GameObject>(4);

    protected IEnumerator uiRotation(GameObject uiCursor)
    {
        while ( uiCursor.activeSelf)
        {
            if (Camera.main != null)
            {
                float size =
                    System.Math.Min(Vector3.Distance(uiCursor.transform.position, Camera.main.transform.position), 30) / 1000;

                uiCursor.transform.LookAt(Camera.main.transform);
                uiCursor.transform.localScale = new Vector3(-size, size, size);
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        int num = other.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

        if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
        {
            hitTeamAMemberList.Add(other.gameObject);
        }
        else
        {
            hitTeamBMemberList.Add(other.gameObject);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
       
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        int num = other.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

        if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
        {
            hitTeamAMemberList.Remove(other.gameObject);
        }
        else
        {
            hitTeamBMemberList.Remove(other.gameObject);
        }
    }

    protected void resetMemberList()
    {
        hitTeamAMemberList.Clear();
        hitTeamBMemberList.Clear();
    }
}
