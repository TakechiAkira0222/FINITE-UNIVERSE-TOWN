using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Takechi.ReflectionCalculation
{
    public class MapReflectionCalculation : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_rb;
        [SerializeField] private float m_boundsPower = 5;
        [SerializeField] private int m_reflectionCalculationFrame = 100;

        private Vector3 m_positionOfThePreviousFrame = Vector3.zero;

        private void Reset()
        {
            m_rb = this.transform.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            m_positionOfThePreviousFrame = m_rb.transform.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            StartCoroutine(AttackTowardsTarget(collision));
        }

        private IEnumerator AttackTowardsTarget(Collision collision)
        {
            // 衝突位置から自機へ向かうベクトルを求める
            Vector3 boundVec = m_positionOfThePreviousFrame - m_rb.transform.position;

            // 逆方向にはねる
            Vector3 forceDir = m_boundsPower * boundVec.normalized;

            for (int turn = 0; turn < m_reflectionCalculationFrame; turn++)
            {
                m_rb.AddForce((forceDir * m_boundsPower) / m_reflectionCalculationFrame, ForceMode.Impulse);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
