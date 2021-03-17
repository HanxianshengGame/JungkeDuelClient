using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class MoveRequest : BaseRequest {

    private Transform localPlayerTransfrom;
    private PlayerMove localPlayerMove;
    private int syncRate = 30;

    private Transform remotePlayerTransform;
    private Animator remotePlayerAnimator;

    private bool isSyncRemotePlayer = false;
    private Vector3 pos;
    private Vector3 rotation;
    private float forward;

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Move;
        base.Awake();
    }
    public void Start()
    {
        InvokeRepeating("SyncLocalPlayer", 0, 1.0f / syncRate);
    }

    private void FixedUpdate()
    {
        if(isSyncRemotePlayer)
        {
            SyncRemotePlayer();
            isSyncRemotePlayer = false;
        }
    }

    public void SyncLocalPlayer()
    {
        var pos = localPlayerTransfrom.position;
        var rotate = localPlayerTransfrom.eulerAngles;
        var forward = localPlayerMove.forward;
        SendRequest(pos.x, pos.y, pos.z, rotate.x, rotate.y, rotate.z, forward);
    }
    public MoveRequest SetLocalPlayer(Transform localPlayerTransform, PlayerMove localPlayerMove)
    {
        this.localPlayerTransfrom = localPlayerTransform;
        this.localPlayerMove = localPlayerMove;
        return this;
    }
    public MoveRequest SetRemotePlayer(Transform romotePlayerTransform)
    {
        this.remotePlayerTransform = romotePlayerTransform;
        this.remotePlayerAnimator = romotePlayerTransform.GetComponent<Animator>();
        return this;
    }

    private void SyncRemotePlayer()
    {
        remotePlayerTransform.position = pos;
        remotePlayerTransform.eulerAngles = rotation;
        remotePlayerAnimator.SetFloat("Forward", forward);
    }


    public void SendRequest(float x, float y, float z,
        float rotationX,float rotationY, float rotationZ,
        float forward)
    {
        string data = string.Format("{0},{1},{2}|{3},{4},{5}-{6}",
            x, y, z, rotationX, rotationY, rotationZ, forward);
        base.SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        string[] strs = data.Split('|');
        pos = UnityTools.ParseVector3(strs[0]);
        rotation = UnityTools.ParseVector3(strs[1]);
        forward = float.Parse(strs[2]);
        isSyncRemotePlayer = true;
    }



}
