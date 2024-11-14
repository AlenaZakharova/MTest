using System;

[Serializable]
public class PlayerData
{
    public int VictoryCount;
    public void AddVictory()
    {
        VictoryCount++;
        Prefs.SavePlayer(this);
    }
}