using Photon.Pun;
using Photon.Realtime;

using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

using Takechi.ScriptReference.CustomPropertyKey;
using TakechiEngine.PUN.ServerConnect.Joined;

using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Takechi.UI.CanvasMune.DisplayListUpdate
{
    public class DisplayListUpdateManagement : TakechiJoinedPunCallbacks
    {
        public override void OnEnable()
        {
            base.OnEnable();
        }

        /// <summary>
        /// �\�����̃��X�g�̍X�V
        /// </summary>
        protected virtual void RefreshTheListViewing()
        {
          
        }

        protected virtual IEnumerator ListUpdate()
        {
            Debug.Log(" <color=green>IEnumerator ListUpdate to Stert</color>");
            yield return null;
        }

        /// <summary>
        /// �q�I�u�W�F�N�g�̑S�������܂��B
        /// </summary>
        /// <param name="parentObject"></param>
        protected void DestroyChildObjects(GameObject parentObject)
        {
            foreach (Transform n in parentObject.transform) { GameObject.Destroy(n.gameObject); }
        }

        /// <summary>
        /// ���X�g�̃R���e���c�ɂ���āA�C���X�^���X���A�������܂��B
        /// </summary>
        /// <param name="prefab"> </param>
        /// <param name="lists"></param>
        /// <param name="p"></param>
        protected void InstantiateTheContentsOfList<obj, contents>(obj prefab, List<contents> lists, Transform p) where obj : MonoBehaviour
        { 
            foreach (contents n in lists) { Instantiate(prefab, p); }
        }

        /// <summary>
        /// ���X�g�̃R���e���c�ɂ���āA�C���X�^���X�𐶐����A�\���e�L�X�g��ύX���܂��B
        /// </summary>
        /// <param name="prefab"> </param>
        /// <param name="lists"></param>
        /// <param name="p"></param>
        protected void InstantiateTheContentsOfListAndChangeText<obj, contents>(obj prefab, List<contents> lists, Transform p) where obj : MonoBehaviour
        {
            if ( prefab as Text != null && lists as List<Player> != null)
            {
                foreach (contents n in lists)
                {
                    Text text = Instantiate(prefab, p) as Text;
                    Player player = n as Player;
                    text.text = player.NickName;
                }
            }
            else if (prefab as Button != null && lists as List<Player> != null)
            {
                foreach (contents n in lists)
                {
                    Button button = Instantiate(prefab, p) as Button;
                    Player player = n as Player;
                    button.transform.GetChild(0).GetComponent<Text>().text = player.NickName;
                }
            }
            else if(lists as List<Player> != null)
            {
                foreach (contents n in lists)
                {
                    Player player = n as Player;
                    prefab.gameObject.name = player.NickName;
                }
            }
            else
            {
                foreach (contents n in lists)
                {
                    Instantiate(prefab, p);
                }
            }
        }

        /// <summary>
        /// memberList�̍ő�ɒB���Ă��邩�ǂ����̊m�F���܂��B
        /// </summary>
        /// <param name="teamMemberList"> teamMemberList</param>
        /// <returns> �ő�l���̎��ɁATrue��Ԃ��܂��B </returns>
        protected bool MaximumOfMembers(List<Player> teamMemberList)
        {
            return teamMemberList.Count >= PhotonNetwork.CurrentRoom.MaxPlayers / 2;
        }
    }
}
