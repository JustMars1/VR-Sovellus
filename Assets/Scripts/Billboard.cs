using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform camTf;

    void Awake() 
    {
        camTf = Camera.main.transform;
    }

    void LateUpdate() 
    {
        transform.rotation = camTf.rotation;
    }
}
