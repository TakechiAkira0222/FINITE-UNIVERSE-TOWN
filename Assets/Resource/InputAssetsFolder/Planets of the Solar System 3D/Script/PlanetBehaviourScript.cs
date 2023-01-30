using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviourScript : MonoBehaviour
{
    public GameObject sun;
    public float initVelocityZ;
    public float initAngularVelocity;
    private float f;    //万有引力
    private float m;    //惑星の質量
    private float M;    //太陽の質量
    private float r;    //太陽と惑星の距離
    public float G = 6.67E-4f;    //万有引力定数

    //軌跡描画
    LineRenderer lineRenderer;
    private Vector3 linePosition;
    private Quaternion linePositionRot;
    private int nVertex = 360;


    // Use this for initialization
    void Start()
    {
        //初速を与える
        Vector3 initVelocity = new Vector3(0f, 0f, initVelocityZ); ;
        GetComponent<Rigidbody>().velocity = initVelocity;

        m = GetComponent<Rigidbody>().mass;
        M = sun.GetComponent<Rigidbody>().mass;

        //自転させる
        GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().transform.rotation
                                                    * Vector3.up * -1 * initAngularVelocity;

        //軌跡を描画する
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.positionCount = nVertex;
        linePosition = GetComponent<Transform>().position - sun.GetComponent<Transform>().position;
        linePositionRot = Quaternion.AngleAxis(360 / nVertex, Vector3.up);
        for (int i = 0; i < nVertex; i++)
        {
            lineRenderer.SetPosition(i, linePosition);  //頂点の座標にオブジェクトの座標をセット
            linePosition = linePositionRot * linePosition;
        }
    }

    void FixedUpdate()
    {
        //惑星から太陽に向かうベクトルを計算
        Vector3 direction = sun.transform.position - GetComponent<Transform>().position;

        //太陽と惑星の距離rを計算
        r = direction.magnitude;
        //Debug.Log("距離 r = " + r.ToString());

        //単位ベクトルに変換（後で万有引力の方向として使う）
        direction.Normalize();

        //万有引力を計算
        f = G * m * M / (r * r);
        //Debug.Log("万有引力 f = " + f.ToString());

        //加速度を与える
        GetComponent<Rigidbody>().AddForce(f * direction, ForceMode.Force);
    }

}
