using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;

public class AccountAuthentication : MonoBehaviour
{
    public void attemptConnect()
    {

        try
        {
            TcpClient client = new TcpClient("localhost", 8888);

            Byte[] data = System.Text.Encoding.ASCII.GetBytes("ThisIsATest");

            NetworkStream stream = client.GetStream();

            stream.Write(data, 0, data.Length);

            data = new Byte[256];

            String responseData = String.Empty;

            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Debug.Log(responseData);


            //byte[] bytes = new byte[1024];
            //int bytesRead = stream.Read(bytes, 0, bytes.Length);

            //Debug.Log(Encoding.ASCII.GetString(bytes, 0, bytesRead));
            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    public void Connect()
    {
        Debug.Log("hi");
        ThreadStart childref = new ThreadStart(attemptConnect);
        Thread childThread = new Thread(childref);
        childThread.Start();

    }
    public void CreateServer()
    {
        Debug.Log("hi");
        ThreadStart childref = new ThreadStart(attemptConnect);
        Thread childThread = new Thread(childref);
        childThread.Start();

    }

}

