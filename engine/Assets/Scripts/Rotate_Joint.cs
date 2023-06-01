using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using System.Collections.Specialized;

public class Rotate_Joint : MonoBehaviour
{
    public GameObject TCPOBJ;
    public float offset;
    public char axis;
    public int num;
    float[] positions;
    public int direction;

    public float x;
    public float y;
    public float z;

    Quaternion baseSpin;
    // Start is called before the first frame update
    void Start()
    {
        baseSpin = transform.rotation;
        positions = TCPOBJ.GetComponent<TCP_ip>().position;
      
/*        if (axis == 'y')
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, positions[num]*180/pi + offset, transform.rotation.eulerAngles.z);
            transform.Rotate(0,0, 0, Space.Self);
        }
        else if (axis == 'x')
        {
            //transform.rotation = Quaternion.Euler(positions[num]*180/pi + offset, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.Rotate(0, 0, 0, Space.Self);
        }
        else if (axis == 'z')
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, positions[num]*180/pi + offset);
            transform.Rotate(0, 0, 0, Space.Self);
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        float pi = 3.14159F;
         positions = TCPOBJ.GetComponent<TCP_ip>().position;
      //  positions[num] += pi/25;
    
      if (axis == 'y'){
            //    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, direction * positions[num] * 180 / pi + offset, transform.localEulerAngles.z);
            // transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, direction*positions[num]*180/pi + offset, transform.rotation.eulerAngles.z);
            //  transform.Rotate(0, positions[num]*180/pi + offset + transform.localEulerAngles.y, 0, Space.Self);
            //transform.localEulerAngles.y = direction * positions[num] * 180 / pi + offset;
            this.transform.localRotation =  Quaternion.Euler(x, direction * positions[num] * 180 / pi + offset, z);

        }
      else if(axis == 'x'){
            //  transform.rotation = Quaternion.Euler(direction*positions[num]*180/pi + offset, transform.localEulerAngles.y, transform.localEulerAngles.z);
            //   transform.Rotate(positions[num]*180/pi + offset - transform.localEulerAngles.x, 0, 0, Space.Self);
            //transform.localEulerAngles.x = direction * positions[num] * 180 / pi + offset;
            //  transform.localEulerAngles = new Vector3(direction * positions[num] * 180 / pi + offset,transform.localEulerAngles.y , transform.localEulerAngles.z);
            // print(positions[num] * 180 / pi + offset);
            this.transform.localRotation =  Quaternion.Euler(direction * positions[num] * 180 / pi + offset, y, z);

        }
      else if(axis == 'z'){
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, direction* positions[num]*180/pi + offset);
            //   transform.Rotate(0, 0, positions[num]*180/pi + offset - transform.localEulerAngles.z, Space.Self);
            //  transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, direction * positions[num] * 180 / pi + offset);
            this.transform.localRotation =  Quaternion.Euler(x, y, direction * positions[num] * 180 / pi + offset);
        }



    }
  

}
