using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class PlayerManager: BaseManager  {
    public PlayerManager(GameFacade facade) : base(facade)
    {

    }


    private UserData userData;
    private Dictionary<RoleType, RoleData> roleDataDict = new Dictionary<RoleType,RoleData>();
    private Transform rolePostions; //角色生成点
    private RoleType currentRoleType;
    private GameObject currentRoleGameObject;
    private GameObject playerSyncRequest;
    private GameObject remoteRoleGameObject;
    private ShootRequest shootRequest;
    private AttackRequest attackRequest;

    public void SetCurrentRoleType(RoleType rt)
    {
        currentRoleType = rt;
    }
    //public Vector3 spawnPostion;
    
    public UserData UserData
    {
        set { userData = value; }
        get { return userData;  }
    }

    public void UpdateResult(int totalCount, int winCount)
    {
        userData.TotalCount = totalCount;
        userData.WinCount = winCount;
    }
    public override void OnInit()
    {
        rolePostions = GameObject.Find("RolePositions").transform;
        InitRoleDataDict();
    }

    private void InitRoleDataDict()
    {
        roleDataDict.Add(RoleType.Blue, new RoleData(RoleType.Blue, "Hunter_BLUE", "Arrow_BLUE", "Explosion_BLUE", rolePostions.Find("Position1")));
        roleDataDict.Add(RoleType.Red, new RoleData(RoleType.Red, "Hunter_RED", "Arrow_RED", "Explosion_RED", rolePostions.Find("Position2")));
    }

    public void SpawnRoles()
    {
        foreach(var rd in roleDataDict.Values)
        { 
            var go = GameObject.Instantiate<GameObject>(rd.RolePrefab, rd.SpawnPostion, Quaternion.identity);
            go.tag = "Player";
            if(rd.RoleType == currentRoleType)
            {
                currentRoleGameObject = go;
                currentRoleGameObject.GetComponent<PlayerInfo>().isLocal = true;
            }
            else
            {
                remoteRoleGameObject = go;
            }

        }
    }


    public GameObject GetCurrentRoleGameObject()
    {
        if(currentRoleGameObject != null)
        {
            return currentRoleGameObject;
        }
        else
        {
           return GameObject.Find("Hunter_BLUE");
        }

    }

    private RoleData GetRoleData(RoleType rt)
    {
        RoleData rd = null;
        roleDataDict.TryGetValue(rt, out rd);
        return rd;
    }

    public void AddControlScript()
    {
        currentRoleGameObject.AddComponent<PlayerMove>();
        PlayerAttack playerAttack = currentRoleGameObject.AddComponent<PlayerAttack>();
        RoleType rt = currentRoleGameObject.GetComponent<PlayerInfo>().roleType;
        RoleData rd = GetRoleData(rt);
        playerAttack.arrowPrefab = rd.ArrowPrefab;
        playerAttack.SetPlayMng(this);
    }

    public void CreateSyncRequest()
    {
        playerSyncRequest = new GameObject("PlayerSyncRequest");
        playerSyncRequest.AddComponent<MoveRequest>().SetLocalPlayer(
            currentRoleGameObject.transform, currentRoleGameObject.GetComponent<PlayerMove>()).
            SetRemotePlayer(remoteRoleGameObject.transform);
        shootRequest = playerSyncRequest.AddComponent<ShootRequest>();
        shootRequest.playerMng = this;
        attackRequest = playerSyncRequest.AddComponent<AttackRequest>();
    }

    public void Shoot(GameObject arrowPrefab, Vector3 pos, Quaternion rotation)
    {
        facade.PlayNormalSound(AudioManager.Sound_Timer);
        var arrow = GameObject.Instantiate(arrowPrefab, pos, rotation).GetComponent<Arrow>();
        arrow.isLocal = true;
        shootRequest.SendRequest(arrow.roleType,
            pos, rotation.eulerAngles);
    }

    public void RemoteShoot(RoleType rt, Vector3 pos, Vector3 rotation)
    {
        GameObject arrowPrefab = GetRoleData(rt).ArrowPrefab;
        var transform = GameObject.Instantiate(arrowPrefab, pos, Quaternion.LookRotation(rotation)).transform;
        transform.position = pos;
        transform.eulerAngles = rotation;
    }

    public void SendAttack(int damage)
    {
        attackRequest.SendRequest(damage);
    }

    public void GameOver()
    {
        GameObject.Destroy(currentRoleGameObject);
        GameObject.Destroy(playerSyncRequest);
        GameObject.Destroy(remoteRoleGameObject);
        shootRequest = null;
        attackRequest = null;
    }

}
