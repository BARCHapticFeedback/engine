
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public class Detect_Collisions : MonoBehaviour
{
   public string INFO = "";
   
    public GameObject org;
    void OnCollisionStay(Collision other)
    {
        INFO = "";
        Vector3 Norm;
        Vector3 Norm_local;
        //float angle = BitVector32.SignedAngle()
      //  print("Collision");
        // Print how many points are colliding with this transform
      //  Debug.Log("Points colliding: " + other.contacts.Length);

        // Print the normal of the first point in the collision.
        Norm = other.contacts[0].normal;
        Norm_local = org.transform.InverseTransformVector(Norm);
        //Debug.Log("Normal of the first point: " + Norm_local);
       
        // Draw a different colored ray for every normal in the collision
        
        foreach (var item in other.contacts)
        {
            Norm_local = org.transform.InverseTransformVector(item.normal);
            Norm_local.Normalize();
            Debug.DrawRay(item.point, item.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
           
            if(other.gameObject.tag == "Obj")
            {
                INFO = INFO + "OBJ;";
                INFO = INFO + Norm_local + ";" + org.transform.InverseTransformPoint(item.point) + ";";
                //(item.point - org.transform.position)
            }
            else if(other.gameObject.tag == "Bar")
            {
                INFO = INFO + "BAR;";
                INFO = INFO + Norm_local + ";" + org.transform.InverseTransformPoint(item.point) + ";";
            }
            Debug.Log(INFO);
        }
       
        //  string jsontest = JsonUtility.ToJson(INFO);
        // UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/api/rawCoords", jsontest);
        // www.SetRequestHeader("Contect-Type","application/json");
      

    }
    void OnCollisionExit(Collision Other)
    {
        INFO = "";
    }
    void Update()
    {
        //Debug.Log(INFO);
    }
    public void clearstring()
    {
       // INFO = "";
    }
    /*    IEnumerator Post(string url, string bodyJsonString)
        {
            var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
           // Debug.Log("Status Code: " + request.responseCode);
            request.Dispose();
        }*/
    void Start()
    {

        
    }

}
