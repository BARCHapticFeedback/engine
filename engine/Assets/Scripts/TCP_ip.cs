using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System;

public class TCP_ip : MonoBehaviour
{
    Thread thread;
    public int connectionPort = 25001;
    TcpListener server;
    TcpClient client;
    bool running;
    public float[] position = { 0, 0, 0, 0, 0, 0 };
     [SerializeField] GameObject tool;
  public  string info;
    int clear = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.unityLogger.logEnabled = true;
        ThreadStart ts = new ThreadStart(GetData);
        thread = new Thread(ts);
        thread.Start();
        // UnityEngine.Debug.Log("print");
    

    }
    void Update()
    {
       // UnityEngine.Debug.unityLogger.logEnabled = true;
       
    
        if(clear == 1)
        {
            tool.GetComponent<Detect_Collisions>().clearstring();
            clear = 0;
        }
        if (tool)
        {

            info = tool.GetComponent<Detect_Collisions>().INFO;
            //   UnityEngine.Debug.Log("found");

        }
        else
        {
            UnityEngine.Debug.Log("no find");
        }
    }
 
    void GetData()
    {

        // Create the server
        try
        {
            server = new TcpListener(IPAddress.Any, connectionPort);
            server.Start();

            // Create a client to get the data stream
            client = server.AcceptTcpClient();
            
            // Start listening
            running = true;
            while (running)
            {
                Connection();

            }
            print("Stop Server");
            server.Stop();
        }
        catch (SocketException e) {
            UnityEngine.Debug.Log(e);
        }
    }

    void Connection()
    {
       // float[] end_signal = {500, 500, 500, 500, 500, 500};
      
        byte[] data;
       
        // Read data from the network stream
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

        // Decode the bytes into a string
        if(client == null)
        {
            running = false;
            return;
        }
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        if(info == "")
        {
            data = System.Text.Encoding.ASCII.GetBytes("0");
            nwStream.Write(data, 0, data.Length);
            UnityEngine.Debug.Log("empty");
        }
        else
        {
            info = "1;" + info;
            data = System.Text.Encoding.ASCII.GetBytes(info);
            nwStream.Write(data, 0, data.Length);
          //  UnityEngine.Debug.Log(info);
        }
        // Make sure we're not getting an empty string
        //dataReceived.Trim();
        if (dataReceived != null && dataReceived != "")
        {
            
            // Convert the received string of data to the format we are using
            position = ParseData(dataReceived);
           
        }

        else
        {
            print("NULL");
        }
        //if(position == end_signal)
        //{
        //    running = false;
        //}
      
        clear = 1;
        
    }

    // Use-case specific function, need to re-write this to interpret whatever data is being sent
    public static float[] ParseData(string dataString)
    {
       
        // Remove the parentheses
        if (dataString.StartsWith("[") && dataString.EndsWith("]"))
        {
            dataString = dataString.Substring(1, dataString.Length - 2);
        }

        // Split the elements into an array
        string[] stringArray = dataString.Split(',');

        // Store as a Vector3\
        float[] result = { float.Parse(stringArray[0]), float.Parse(stringArray[1]), float.Parse(stringArray[2]), float.Parse(stringArray[3]), float.Parse(stringArray[4]), float.Parse(stringArray[5]) };
        //Vector3 result = new Vector3(
        //    float.Parse(stringArray[0]),
        //    float.Parse(stringArray[1]),
        //    float.Parse(stringArray[2]));
      
        return result;
    }
    void OnApplicationQuit()
    {
        try
        {
            client.Close();

        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
        }

        // You must close the tcp listener
        try
        {
            server.Stop();

        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
        }
    }

    // Position is the data being received in this example
}
