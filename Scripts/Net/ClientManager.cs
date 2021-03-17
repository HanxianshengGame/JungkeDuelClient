using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Common;
using UnityEngine;

public class ClientManager: BaseManager
{
    private const string IP = "39.105.35.17";
    private const int PORT = 2525;
    private Socket clientSocket;
    private Message msg = new Message();

    public ClientManager(GameFacade facade) : base(facade)
    {

    }
    public override void OnInit()
    {
        base.OnInit();
        clientSocket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP,PORT);
            Start();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法连接到服务器， 请检查您的网络！" + e);
        }

    }

    private void Start()
    {
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);

    }

    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || !clientSocket.Connected) 
                return;
            int count = clientSocket.EndReceive(ar);
            msg.ReadMessage(count, OnProcessDataCallback);
            Start();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void OnProcessDataCallback(ActionCode actionCode, string data)
    {
        facade.HandleResponse(actionCode, data);
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode,
    string data) {
        JsonDataRequestEntity dataEntity = new JsonDataRequestEntity();
        dataEntity.requestCode = (int)requestCode;
        dataEntity.actionCode = (int)actionCode;
        dataEntity.data = data;
        string jsonData =  JsonUtility.ToJson(dataEntity); 
        byte[] bytes = MessageHandler.PackData(jsonData);
        _clientSocket.Send(bytes);
    }
    
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
           clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法关闭与服务器的连接!!!" + e);
        }
    }
}
