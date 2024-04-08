using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager instance = new DataManager();
    public static DataManager Instance => instance;

    public MusicData musicData;
    public List<RoleInfo> roleInfoList;
    public PlayerData playerData;
    public RoleInfo selectedRole;
    private DataManager() 
    {
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        roleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
    }

    
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }
}
