﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;


#if !UNITY_EDITOR
    using Windows.Networking.Sockets;
    using Windows.Networking;
    using Newtonsoft.Json.Serialization;
#endif

public class Connection : MonoBehaviour
{
#if !UNITY_EDITOR
        DatagramSocket socket;
        string listenPort = "5005";



#endif
    // use this for initialization
#if !UNITY_EDITOR
    async void Start()
    {
#endif
#if UNITY_EDITOR
    void Start()
    {
#endif

    

#if !UNITY_EDITOR
            Debug.Log("Waiting for a connection...");

            socket = new DatagramSocket();
            socket.MessageReceived += Socket_MessageReceived;
       

            try
            {
            await socket.BindEndpointAsync(null, listenPort);
                
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                Debug.Log(SocketError.GetStatus(e.HResult).ToString());
                return;
            }

            Debug.Log("exit start");
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

#if !UNITY_EDITOR
        private async void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender,
            Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
        {

        try
        {
            //Read the message that was received from the UDP echo client.
            Stream streamIn = args.GetDataStream().AsStreamForRead();
            StreamReader reader = new StreamReader(streamIn);
            string jsonData = await reader.ReadToEndAsync();
            //Debug.Log("MESSAGE: " + jsonData);
            //UnityEngine.Debug.LogError(jsonData);

              DataStruct newData = Newtonsoft.Json.JsonConvert.DeserializeObject<DataStruct>(jsonData);
              UdpEvent.onDataRecieved(newData);
        }

        catch (Exception e)
        {
            Exception mye = e;
            String tst = mye.Message;
            string sts = "";
        }
        }
#endif
}