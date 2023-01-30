using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviourScript : MonoBehaviour
{
    public GameObject sun;
    public float initVelocityZ;
    public float initAngularVelocity;
    private float f;    //���L����
    private float m;    //�f���̎���
    private float M;    //���z�̎���
    private float r;    //���z�Ƙf���̋���
    public float G = 6.67E-4f;    //���L���͒萔

    //�O�Օ`��
    LineRenderer lineRenderer;
    private Vector3 linePosition;
    private Quaternion linePositionRot;
    private int nVertex = 360;


    // Use this for initialization
    void Start()
    {
        //������^����
        Vector3 initVelocity = new Vector3(0f, 0f, initVelocityZ); ;
        GetComponent<Rigidbody>().velocity = initVelocity;

        m = GetComponent<Rigidbody>().mass;
        M = sun.GetComponent<Rigidbody>().mass;

        //���]������
        GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().transform.rotation
                                                    * Vector3.up * -1 * initAngularVelocity;

        //�O�Ղ�`�悷��
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.positionCount = nVertex;
        linePosition = GetComponent<Transform>().position - sun.GetComponent<Transform>().position;
        linePositionRot = Quaternion.AngleAxis(360 / nVertex, Vector3.up);
        for (int i = 0; i < nVertex; i++)
        {
            lineRenderer.SetPosition(i, linePosition);  //���_�̍��W�ɃI�u�W�F�N�g�̍��W���Z�b�g
            linePosition = linePositionRot * linePosition;
        }
    }

    void FixedUpdate()
    {
        //�f�����瑾�z�Ɍ������x�N�g�����v�Z
        Vector3 direction = sun.transform.position - GetComponent<Transform>().position;

        //���z�Ƙf���̋���r���v�Z
        r = direction.magnitude;
        //Debug.Log("���� r = " + r.ToString());

        //�P�ʃx�N�g���ɕϊ��i��Ŗ��L���͂̕����Ƃ��Ďg���j
        direction.Normalize();

        //���L���͂��v�Z
        f = G * m * M / (r * r);
        //Debug.Log("���L���� f = " + f.ToString());

        //�����x��^����
        GetComponent<Rigidbody>().AddForce(f * direction, ForceMode.Force);
    }

}
