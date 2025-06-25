[System.Serializable]
public class PlayerData
{
    public string playerName;
    public float score;
    public int trickpoints;
    public float distance;
    public PlayerData(string playerName, float score, int trickpoints, float distance)
    {
        this.playerName = playerName;
        this.score = score;
        this.trickpoints = trickpoints;
        this.distance = distance;
    }
}



