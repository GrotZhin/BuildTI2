using System;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int score;
    public int trickpoints;
    public float distance;
    public PlayerData(string playerName, int score, int trickPoints, float distance)
    {
        this.playerName = playerName;
        this.score = score;
        this.trickpoints = trickPoints;
        this.distance = distance;
    }
}



