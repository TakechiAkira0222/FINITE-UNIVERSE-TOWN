using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.AnimationSystem.IK.SyncPosition
{
    /// <summary>
    /// �V���N�����̍��W��������Ik�̍��W�Ɏw�肵�܂��B
    /// </summary>
    [RequireComponent(typeof(PhotonTransformView))]
    public class CoordinateSubstitution : MonoBehaviour
    {
        [SerializeField] private Transform  m_targetPosition;
        [SerializeField, Tooltip(" parentView")] private PhotonView m_photonView;

        private void Reset()
        {
            m_photonView = this.transform.parent.GetComponent<PhotonView>();
        }

        private void Update()
        {
            //:: Prevent rewriting by others
            if (!m_photonView.IsMine)
                return;

            this.transform.position = m_targetPosition.position;
            this.transform.rotation = m_targetPosition.rotation;
        }
    }
}