using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DailyRewards : MonoBehaviour {
    public string timeURL;

    public int timeOffset;

	void Start()
	{
        string localTime = System.DateTime.Now.ToString("yyyy/MM/dd HH/mm/ss");
        Debug.Log("Local Time : " + localTime);

        //System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        //long cur_time = (long)(System.DateTime.UtcNow - epochStart).TotalSeconds;

        //Debug.Log(cur_time);
        string localUtcTime = System.DateTimeOffset.UtcNow.ToString("yyyy/MM/dd HH/mm/ss"); 
        Debug.Log("Local UTC Time : " + localUtcTime);

        System.TimeSpan diff = System.DateTime.UtcNow - System.DateTime.Now;
        timeOffset = diff.Hours;

        StartCoroutine(GetInternetTime());   
	}

    IEnumerator GetInternetTime () {
        UnityWebRequest myWebRequest = UnityWebRequest.Get(timeURL);
        yield return myWebRequest.SendWebRequest();

        if (myWebRequest.isNetworkError || myWebRequest.isHttpError) {
            Debug.Log(myWebRequest.error);
        } else {
            Debug.Log("Success");
            string netTime = myWebRequest.GetResponseHeader("date");
            Debug.Log("Internet Time GMT : "+netTime);
            Debug.Log(netTime.Substring(17,8));

            int tempHour = int.Parse(netTime.Substring(17, 2)); 
            tempHour -= timeOffset; //GMT + 7
            int hour = 0;

            if (tempHour > 24) {
                hour = tempHour - 24;
            } else {
                hour = tempHour;
            }

            int min = int.Parse(netTime.Substring(20, 2));
            int sec = int.Parse(netTime.Substring(23, 2));
            Debug.Log("Result Internet Time GMT + " + timeOffset + " : " + hour + ":" + min + ":" + sec);
        }
    }
}
