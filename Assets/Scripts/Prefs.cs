using UnityEngine;

public static class Prefs
{
    private const string PlayerDataKey = "PlayerData";

    public static PlayerData LoadPlayer()
    {
        string json = PlayerPrefs.GetString(PlayerDataKey, string.Empty);
        return json == string.Empty ? new PlayerData() : JsonUtility.FromJson<PlayerData>(json);
    }

    public static void SavePlayer(PlayerData player)
    {
        PlayerPrefs.SetString(PlayerDataKey, JsonUtility.ToJson(player));
    }
}