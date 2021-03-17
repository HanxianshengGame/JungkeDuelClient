using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class BaseRequest : MonoBehaviour {

	// Use this for initialization
	protected RequestCode requestCode = RequestCode.None;
	protected ActionCode actionCode = ActionCode.None;
	protected GameFacade _facade;

	protected GameFacade facade
	{
		get
		{
			if (_facade == null)
			{
				_facade = GameFacade.Instance;
			}

			return _facade;
		}
	}


	public virtual void Awake()
	{
		GameFacade.Instance.AddRequest(actionCode, this);
	}
	public virtual void SendRequest()
	{

	}
	public virtual void SendRequest(string data)
	{
		facade.SendRequest(requestCode, actionCode, data);
	}

	public virtual void OnResponse(string data) { }

	public void OnDestroy()
	{
		if(_facade != null)
			_facade.RemoveRequest(actionCode);
	}
}
