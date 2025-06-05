[System.Serializable]
public class PlayerData
{
    public string playerName;
    public float score;
    public PlayerData(string playerName, float score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}



