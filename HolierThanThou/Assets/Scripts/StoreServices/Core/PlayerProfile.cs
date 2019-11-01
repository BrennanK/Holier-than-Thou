[System.Serializable]
public class PlayerProfile
{
    public int gamesPlayed;
    public int gamesWon;
    public bool hitSomebody;
    public bool denied;
    public bool AlleyOop;


    public PlayerProfile()
    {
        gamesPlayed = 0;
        gamesWon = 0;
        hitSomebody = false;
    }

    public PlayerProfile(int _gamesPlayed, int _gamesWon)
    {
        this.gamesPlayed = _gamesPlayed;
        this.gamesWon = _gamesWon;
        //this.hitSomebody = _hitSomebody;
    }

    public void IncrementProfileData(PlayerProfile _incrementData)
    {
        this.gamesPlayed += _incrementData.gamesPlayed;
        this.gamesWon += _incrementData.gamesWon;
    }
}