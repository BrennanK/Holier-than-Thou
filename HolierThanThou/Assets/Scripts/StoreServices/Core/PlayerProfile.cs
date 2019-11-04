[System.Serializable]
public class PlayerProfile
{
    public int gamesPlayed;
    public int gamesWon;
    public bool hitSomebody;
    public bool denied;
    public bool AlleyOop;
    public bool playerLast;
    public bool scoredGoal;
    public bool placedFourth;
    public bool usedPowerUp;
    public bool hasJumped;


    public PlayerProfile()
    {
        gamesPlayed = 0;
        gamesWon = 0;
        hitSomebody = false;
    }

    public PlayerProfile(int _gamesPlayed, int _gamesWon, bool _hitSomebody, bool _denied, bool _AlleyOop, bool _playerLast, bool _scoredGoal, bool _placedFourth, bool _usedPowerUP, bool _hasJumped)
    {
        this.gamesPlayed = _gamesPlayed;
        this.gamesWon = _gamesWon;
        this.hitSomebody = _hitSomebody;
        this.denied = _denied;
        this.AlleyOop = _AlleyOop;
        this.playerLast = _playerLast;
        this.scoredGoal = _scoredGoal;
        this.placedFourth = _placedFourth;
        this.usedPowerUp = _usedPowerUP;
        this.hasJumped = _hasJumped; 
    }

    public void IncrementProfileData(PlayerProfile _incrementData)
    {
        this.gamesPlayed += _incrementData.gamesPlayed;
        this.gamesWon += _incrementData.gamesWon;
    }
}