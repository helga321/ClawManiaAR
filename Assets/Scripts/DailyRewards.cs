using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class DailyRewards : MonoBehaviour {
    public string timeURL;
    public GameObject buttonDailyReward;
    public Text textDailyReward;
    public GameControl gameControl;
    public int coinPrize;

    string localTime;
    string[] months = new string[12] {
        "January", "February", "March", "April", "May", "June", 
        "July", "August", "September", "October", "November", "December"};

    int timeOffset;
    string lastSaved;
    string playerLocalSave;
    string playerNetSave;
    DateTime playerLocalTime;
    DateTime playerNetTime;

    void Start()
    {
        GetLocalTime();
        PlayerPrefs.SetString("PlayerLoginTime", "2018-01-01-00"); //For Debugging

        //System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        //long cur_time = (long)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        //Debug.Log(cur_time);

        //string localUtcTime = System.DateTimeOffset.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"); 
        //Debug.Log("Local UTC Time : " + localUtcTime);

        //System.TimeSpan diff = System.DateTime.UtcNow - System.DateTime.Now;
        //timeOffset = diff.Hours;

        StartCoroutine(GetInternetTime());   
    }

    void GetLocalTime()
    {
        localTime = DateTime.Now.ToString("yyyy/MM/dd/HH/mm/ss");

        string[] localDates = localTime.Split('/');

        playerLocalSave = localDates[0].ToString() + "-" + localDates[1].ToString() + "-" + localDates[2].ToString() + "-" + localDates[3].ToString();
        playerLocalTime = DateTime.ParseExact(playerLocalSave, "yyyy-MM-dd-HH", null);

    }

    IEnumerator GetInternetTime () {
        UnityWebRequest myWebRequest = UnityWebRequest.Get(timeURL);
        yield return myWebRequest.SendWebRequest();

        if (myWebRequest.isNetworkError || myWebRequest.isHttpError) {
            Debug.Log(myWebRequest.error);
        } else {
            string netTime = myWebRequest.GetResponseHeader("date");

            string[] netDates = netTime.Split(' ');

            int netMonth = 0;
            for (; netMonth < months.Length;netMonth++) {
                if (months[netMonth] == netDates[2]) {
                    netMonth++;
                    break;
                }
            }

            string numNetMonth = "";
            if (netMonth.ToString().Length < 2) {
                numNetMonth = "0" + netMonth;
            }

            timeOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;

            playerNetSave = netDates[3].ToString() + "-" + numNetMonth + "-" + netDates[1].ToString() + "-" + netDates[4].Substring(0, 2);
            playerNetTime = DateTime.ParseExact(playerNetSave, "yyyy-MM-dd-HH", null);

            CheckPlayerLastLogin();
        }
    }

    void CheckPlayerLastLogin () {
        Debug.Log("timeoffset "+ timeOffset);
        Debug.Log("playerLocalSave " + playerLocalSave);
        Debug.Log("playerNetSave " + playerNetSave);

        Debug.Log("PLayerLocalTime " + playerLocalTime);
        Debug.Log("PlayerNetTime " + playerNetTime);

        buttonDailyReward.SetActive(CheckTimeDiff());
    }

    public void GetReward () {
        string playerSavedTime = playerNetSave;
        string playerLastSavedTime = PlayerPrefs.GetString("PlayerLoginTime", "2018-01-01-00");

        //Debug.Log("PlayerLastSavedTime " + playerLastSavedTime);

        TimeSpan diff = DateTime.ParseExact(playerSavedTime, "yyyy-MM-dd-HH", null) - DateTime.ParseExact(playerLastSavedTime, "yyyy-MM-dd-HH", null);
        int timeDiff = diff.Days;

        Debug.Log(timeDiff);

        if (timeDiff >= 1 && CheckTimeDiff()) {
            PlayerPrefs.SetString("PlayerLoginTime", playerSavedTime);
            gameControl.GetCoin(coinPrize);
            Debug.Log("Get Reward");
            textDailyReward.text = "You Got Coins";
        } else {
            Debug.Log("Comeback tomorrow");
            textDailyReward.text = "Comback Tomorrow";
        }
    }

    bool CheckTimeDiff () {
        TimeSpan diff = DateTime.ParseExact(playerLocalSave, "yyyy-MM-dd-HH", null) - DateTime.ParseExact(playerNetSave, "yyyy-MM-dd-HH", null);
        int timeDiff = diff.Hours;

        if (timeDiff == timeOffset)
        {
            return true;
        } else {
            return false;
        }
    }
}
