using System;

public enum RequestCode {
   None,
   User, 
}


public enum ActionCode {
    None,
    Login,
    Register,
    Quit,

}


public enum ReturnCode {
    Failure,
    Success
}

[Serializable]
public class JsonDataRequestEntity {  
    public int requestCode;
    public int actionCode;
    public string data;
}

[Serializable]
public class JsonDataResonseEntity {  
    public int actionCode;
    public string data;
}






