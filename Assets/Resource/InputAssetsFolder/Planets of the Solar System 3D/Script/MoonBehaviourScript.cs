using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBehaviourScript : MonoBehaviour
{
    public GameObject sun;
    public GameObject earth;
    public float initVelocityZ;
    public float initAngularVelocity;
    private float f_s;  //���z����󂯂閜�L����
    private float f_e;  //�n������󂯂閜�L����
    private float m;    //���̎���
    private float M_s;  //���z�̎���
    private float M_e;  //�n���̎���
    private float r_s;  //���z�ƌ��̋���
    private float r_e;  //�n���ƌ��̋���
    public float G_s = 6.67E-4f;    //���L���͒萔
    public float G_e = 66.7f;    //���L���͒萔

    // Use this for initialization
    void Start()
    {
        //���ʂ̏����擾
        m = GetComponent<Rigidbody>().mass;
        M_s = sun.GetComponent<Rigidbody>().mass;
        M_e = earth.GetComponent<Rigidbody>().mass;

        //������^����
        Vector3 initVelocity = new Vector3(0f, 0f, initVelocityZ); ;
        GetComponent<Rigidbody>().velocity = initVelocity;

        //���]������
        GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().transform.rotation
                                                    * Vector3.up * -1 * initAngularVelocity;

    }

    void FixedUpdate()
    {
        //�����瑾�z�Ɍ������x�N�g�����v�Z
        Vector3 direction_s = sun.transform.position - GetComponent<Transform>().position;
        //������n���Ɍ������x�N�g�����v�Z
        Vector3 direction_e = earth.transform.position - GetComponent<Transform>().position;

        //����r���v�Z
        r_s = direction_s.magnitude;
        r_e = direction_e.magnitude;
        //Debug.Log("���� r = " + r.ToString());

        //�P�ʃx�N�g���ɕϊ��i��Ŗ��L���͂̕����Ƃ��Ďg���j
        direction_s.Normalize();
        direction_e.Normalize();

        //���L���͂��v�Z
        f_s = G_s * m * M_s / (r_s * r_s);
        f_e = G_e * m * M_e / (r_e * r_e);
        //Debug.Log("���L���� f = " + f.ToString());

        //�����x��^����
        GetComponent<Rigidbody>().AddForce((f_s * direction_s) + (f_e * direction_e), ForceMode.Force);
    }
}