using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class UpdateRoomRequest : BaseRequest {
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.ListRoom;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }

    public override void OnResponse(string data)
    { 
        //TODO 可以在此添加到4个人
        UserData ud1 = null;
        UserData ud2 = null;
        string[] udStrArray = data.Split('|');
        ud1 = new UserData(udStrArray[0]);
        if (udStrArray.Length > 1 ) {
            ud2 = new UserData(udStrArray[1]);
        }
        //foreach (var udStr in udStrArray)
        //{
            
        //}

        roomPanel.SetAllPlayerResSync(ud1, ud2);
    }
}
