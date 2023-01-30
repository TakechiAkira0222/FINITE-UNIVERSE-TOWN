using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBehaviourScript : MonoBehaviour
{
    public GameObject sun;
    public GameObject earth;
    public float initVelocityZ;
    public float initAngularVelocity;
    private float f_s;  //太陽から受ける万有引力
    private float f_e;  //地球から受ける万有引力
    private float m;    //月の質量
    private float M_s;  //太陽の質量
    private float M_e;  //地球の質量
    private float r_s;  //太陽と月の距離
    private float r_e;  //地球と月の距離
    public float G_s = 6.67E-4f;    //万有引力定数
    public float G_e = 66.7f;    //万有引力定数

    // Use this for initialization
    void Start()
    {
        //質量の情報を取得
        m = GetComponent<Rigidbody>().mass;
        M_s = sun.GetComponent<Rigidbody>().mass;
        M_e = earth.GetComponent<Rigidbody>().mass;

        //初速を与える
        Vector3 initVelocity = new Vector3(0f, 0f, initVelocityZ); ;
        GetComponent<Rigidbody>().velocity = initVelocity;

        //自転させる
        GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().transform.rotation
                                                    * Vector3.up * -1 * initAngularVelocity;

    }

    void FixedUpdate()
    {
        //月から太陽に向かうベクトルを計算
        Vector3 direction_s = sun.transform.position - GetComponent<Transform>().position;
        //月から地球に向かうベクトルを計算
        Vector3 direction_e = earth.transform.position - GetComponent<Transform>().position;

        //距離rを計算
        r_s = direction_s.magnitude;
        r_e = direction_e.magnitude;
        //Debug.Log("距離 r = " + r.ToString());

        //単位ベクトルに変換（後で万有引力の方向として使う）
        direction_s.Normalize();
        direction_e.Normalize();

        //万有引力を計算
        f_s = G_s * m * M_s / (r_s * r_s);
        f_e = G_e * m * M_e / (r_e * r_e);
        //Debug.Log("万有引力 f = " + f.ToString());

        //加速度を与える
        GetComponent<Rigidbody>().AddForce((f_s * direction_s) + (f_e * direction_e), ForceMode.Force);
    }
}