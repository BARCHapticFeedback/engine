using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    Rigidbody rb;
    public Transform Controller;
    public float transformx;
    public float transformy;
    public float transformz;
   
    public float rotateByx;
    public float rotateByy;
    public float rotateByz;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(Controller.position);
        rb.MoveRotation(Controller.rotation * Quaternion.Euler(rotateByx, rotateByy,rotateByz));

    }
}
