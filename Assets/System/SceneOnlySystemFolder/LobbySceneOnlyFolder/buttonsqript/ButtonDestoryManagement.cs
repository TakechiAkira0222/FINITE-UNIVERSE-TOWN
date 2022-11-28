using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDestoryManagement : MonoBehaviour
{
    public void OnDestroyManagement(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
