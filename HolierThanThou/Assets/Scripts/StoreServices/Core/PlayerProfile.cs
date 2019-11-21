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
    //new achievements
    public int crownsCollected;
    public int crownsStolen;
    public int LBRWins;
    public bool bump;
    public bool moBump;
    public bool doDaBump;
    public int georgiaGoals;
    public bool allHail;
    public bool makeWay;




    public PlayerProfile()
    {
        gamesPlayed = 0;
        gamesWon = 0;
        hitSomebody = false;
    }

    public PlayerProfile(int _gamesPlayed, int _gamesWon, bool _hitSomebody, bool _denied, bool _AlleyOop, bool _playerLast, bool _scoredGoal, bool _placedFourth, bool _usedPowerUP, bool _hasJumped,
        int _crownsCollected, int _crownsStolen, int _LBRWins, bool _bump, bool _moBump, bool _doDaBump, int _georgiaGoals, bool _allHail, bool _makeWay)
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
        this.crownsCollected = _crownsCollected;
        this.crownsStolen = _crownsStolen;
        this.LBRWins = _LBRWins;
        this.bump = _bump;
        this.moBump = _moBump;
        this.doDaBump = _doDaBump;
        this.georgiaGoals = _georgiaGoals;
        this.allHail = _allHail;
        this.makeWay = _makeWay;
    }

    public void IncrementProfileData(PlayerProfile _incrementData)
    {
        this.gamesPlayed += _incrementData.gamesPlayed;
        this.gamesWon += _incrementData.gamesWon;
    }
}