using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RegisterRequest : BaseRequest {

	// Use this for initialization
	private RegisterPanel registerPanel;
	public override void Awake()
	{
		requestCode = RequestCode.User;
		actionCode = ActionCode.Register;
		registerPanel = transform.GetComponent<RegisterPanel>();
		base.Awake();
	}

	public void SendRequest(string username, string password)
	{
		string data = username + ',' + password;
		base.SendRequest(data);
	}

	public override void OnResponse(string data)
	{
		ReturnCode returnCode = (ReturnCode)int.Parse(data);
		registerPanel.OnRegisterResponse(returnCode);
	}


	// Update is called once per frame
	void Update () {
		
	}
}
