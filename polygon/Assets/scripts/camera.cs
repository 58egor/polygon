using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Rigidbody body;
    public float RangeY;
    public float RangeZ;
    Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vec= body.position;
        vec.y += RangeY;
        vec.z += RangeZ;
        cam.position = vec;
        Quaternion rot = transform.rotation;
        //rot.y = body.rotation.y;
        //transform.rotation = rot;

    }
}
