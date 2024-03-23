using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData ins = null;

    //public ServiceManager serviceManager = null;
    public FacebookData facebookData = null;

    public UserData userData = null;

    public static GameData Instance
    {
        get
        {
            if(ins == null)
            {
                ins = new GameObject("GameData").AddComponent<GameData>();
                ins.Initailize();
                DontDestroyOnLoad(ins.gameObject);
            }
            return ins;
        }
    }

    private void Initailize()
    {
        //serviceManager = ServiceManager.Instance;
        userData = new UserData();
        facebookData = new FacebookData();
    }

    public void SaveFacebookDataToCloud()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //if (serviceManager.facebookService.IsLoggedIn == true)
            //    serviceManager.fireBaseManager.WriteNewUser(this.facebookData.ID, this.facebookData);
        }
    }
}
