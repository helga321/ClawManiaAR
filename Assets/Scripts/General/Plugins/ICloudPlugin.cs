using UnityEngine;
using System.Runtime.InteropServices;
public static class ICloudPlugin{

	[DllImport ("__Internal")]
	private static extern void iCloudKV_Synchronize();

	[DllImport ("__Internal")]
	private static extern void iCloudKV_SetInt(string key, int value);

	[DllImport ("__Internal")]
	private static extern void iCloudKV_SetFloat(string key, float value);

	[DllImport ("__Internal")]
	private static extern int iCloudKV_GetInt(string key);

	[DllImport ("__Internal")]
	private static extern float iCloudKV_GetFloat(string key);

	[DllImport ("__Internal")]
	private static extern void iCloudKV_Reset();

	private static int testValue;

	public static void LoadData(){
		Debug.Log ("LOAD DATA FROM CLOUD");
		iCloudKV_Synchronize ();
        PlayerData.Instance.PlayerCoin = iCloudKV_GetInt(ShortCode.iCloud_PlayerCoin);
	}

    public static int LoadIntData(string key){
        Debug.Log("LOAD " + key + " DATA FROM CLOUD");
        iCloudKV_Synchronize();
        return iCloudKV_GetInt(key);
    }

    public static float LoadFloatData(string key){
        Debug.Log("LOAD " + key + " DATA FROM CLOUD");
        iCloudKV_Synchronize();
        return iCloudKV_GetFloat(key);
    }

	public static void SaveData(string key,int value){
		Debug.Log ("SAVE DATA TO CLOUD");
		iCloudKV_SetInt(key, value);
		iCloudKV_Synchronize();
	}
}