using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour{
    private static PlayerData instance;

    public static PlayerData Instance { get { return instance; } }

    private int playerCoin = 0;
    private int firstPlay = 0;

    void Awake(){
        if(instance!=this && instance != null){
            Destroy(this.gameObject);
        } else{
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public int PlayerCoin{
        set { playerCoin = value; }
        get { return playerCoin; }
    }

    public int FirstPlay{
        set { firstPlay = value; }
        get { return firstPlay; }
    }
}
