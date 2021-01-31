using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Configuration;

public class Connection : MonoBehaviour
{
    public Text debug;
    static TcpListener listener;
    public void attemptConnect()
    {

        try
        {
            TcpClient client = new TcpClient("192.168.0.173", 7777);

            Byte[] data = System.Text.Encoding.ASCII.GetBytes("ThisIsATest");

            NetworkStream stream = client.GetStream();

            stream.Write(data, 0, data.Length);

            data = new Byte[256];

            String responseData = String.Empty;

            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            debug.text = responseData;
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
        listener = new TcpListener(7777);
        Debug.Log("newServer");
        listener.Start();
        for (int i = 0; i < 2; i++)
        {
            Thread t = new Thread(new ThreadStart(ConnectToClient));
            t.Start();
        }

    }
    public void ConnectToClient()
    {
        while (true)
        {
            Debug.Log("newClient");
            Socket soc = listener.AcceptSocket();
            Debug.Log("Connection accepted.");
            debug.text = "connected";
            try
            {
                Stream s = new NetworkStream(soc);
                StreamReader sr = new StreamReader(s);
                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true; // enable automatic flushing
                
                sw.WriteLine("Test");
                
                while (true)
                {
                    string msg = sr.ReadLine();
                    if (msg == "" || msg == null) break;
                    Debug.Log(msg);
                }
                s.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            soc.Close();
        }
    }

}

