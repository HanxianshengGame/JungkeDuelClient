using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class RequestManager: BaseManager  {
    public RequestManager(GameFacade facade) : base(facade)
    {

    }


    private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestDict.Add(actionCode, request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestDict.Remove(actionCode);
    }


    public void HandleResponse(ActionCode actionCode, string data)
    {
        BaseRequest request = requestDict.TryGet<ActionCode, BaseRequest>(actionCode);
        if (request == null)
        {
            Debug.LogWarning("无法得到 RequestCode[" + actionCode+"]对应的request类");
        }
        request.OnResponse(data);
    }
}
