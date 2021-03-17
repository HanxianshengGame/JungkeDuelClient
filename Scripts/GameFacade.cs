using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class GameFacade : MonoBehaviour {

	// Use this for initialization
	private UIManager uiMng;
	private AudioManager audioMng;
	private PlayerManager playerMng;
	private CameraManager cameraMng;
	private RequestManager requestMng;
	private ClientManager clientMng;
	private bool isEnterPlaying = false; 

	private static GameFacade _instance;

	public static GameFacade Instance
	{
		get 
		{ 
			if(_instance == null)
            {
				_instance = GameObject.Find("GameFacade").GetComponent<GameFacade>();
            }
			return _instance; 
		}
	}

	

	//void Awake()
	//{
	//	if (_instance != null)
	//	{
	//		Destroy(this.gameObject);
	//		return;
	//	}

	//	_instance = this;
	//}

	void Start ()
	{
		InitManager();
	}



	// Update is called once per frame
	void Update () {
		UpdateManager();
		if(isEnterPlaying)
        {
			EnterPlaying();
			isEnterPlaying = false;
        }
	}

	private void InitManager()
	{
		uiMng = new UIManager(this);
		audioMng = new AudioManager(this);
		playerMng = new PlayerManager(this);
		cameraMng = new CameraManager(this);
		requestMng = new RequestManager(this);
		clientMng = new ClientManager(this);


		uiMng.OnInit();
		audioMng.OnInit();
		playerMng.OnInit();
		cameraMng.OnInit();
		requestMng.OnInit();
		clientMng.OnInit();
	}

	private void UpdateManager()
    {
		uiMng.Update();
		audioMng.Update();
		playerMng.Update();
		cameraMng.Update();
		requestMng.Update();
		clientMng.Update();
	}


	private void DestroyManager()
	{
		uiMng.OnDestroy();
		audioMng.OnDestroy();
		playerMng.OnDestroy();
		cameraMng.OnDestroy();
		requestMng.OnDestroy();
		clientMng.OnDestroy();
	}


	public void AddRequest(ActionCode actionCode, BaseRequest request)
	{
		requestMng.AddRequest(actionCode, request);
	}	
	

	public void RemoveRequest(ActionCode actionCode)
	{
		requestMng.RemoveRequest(actionCode);
	}
	public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
	{
		clientMng.SendRequest(requestCode,actionCode,data);
	}

	public void HandleResponse(ActionCode actionCode, string data)
	{
		requestMng.HandleResponse(actionCode, data);
	}

	public void ShowMessage(string msg)
	{
		uiMng.ShowMessage(msg);
	}


	public void PlayBgSound(string soundName)
	{
		audioMng.PlayBgSound(soundName);
	}
	public void PlayNormalSound(string soundName)
	{
		audioMng.PlayNormalSound(soundName);
	}

	public void SetUserData(UserData userData)
	{
		playerMng.UserData = userData;
	}

	public UserData GetUserData()
	{
		return playerMng.UserData;
	}

	public void SetCurrentRoleType(RoleType rt)
	{
		playerMng.SetCurrentRoleType(rt); 
	}

	public GameObject GetCurrentRoleGameObject()
	{
		return playerMng.GetCurrentRoleGameObject();
	}

	public void EnterPlayingSync()
	{
		isEnterPlaying = true;
	}
	public void EnterPlaying()
    {
		playerMng.SpawnRoles();
		cameraMng.FollowRole();
    }

	public void StartPlaying()
    {
		playerMng.AddControlScript();
		playerMng.CreateSyncRequest();
    }

	public void SendAttack(int damage)
    {
		playerMng.SendAttack(damage);
    }

	public void GameOver()
    {
		cameraMng.WakethroughScene();
		playerMng.GameOver();
    }

	public void UpdateResult(int totalCount, int winCount)
	{
		playerMng.UpdateResult(totalCount, winCount);
	}
}
