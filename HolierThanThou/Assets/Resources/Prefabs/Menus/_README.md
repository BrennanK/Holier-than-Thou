Drag and Drop the MenuKit into your scene. 

------------------
Part 1: Player 
- Power Up Tracker
    Item Button 1 = GameUI/ ActionsButtonsCanvas/ Item1 Button/ Item1Text
    Item Button 2 = GameUI/ ActionsButtonsCanvas/ Item2 Button/ Item2Text

- Player UI Script
    Multiplier = GameUI/ DebugCanvas/ MultiplierText
    Speed = GameUI/ DebugCanvas/ SpeedText
------------------

------------------
Part 2: Goal 
- Score Manager 
    <Text> Components
    Score Text = GameUI/ PauseAndScoresCanvas/ InGameScoreBoard/ ScoreText
    End Game Score = EndMatchScreen/ EndScores/ Background/ Header/ EndGameScoreText
    Winner Text = EndMatchScreen/ EndScores/ WinnerText

- Game Manager 
    <GameOjbect> Components
    End Match Screen = EndMatchScreen
    Game UI = GameUI

    <Text> Components
    Start Text = GameUI/StartText
    In Game Timer = GameUI/TimerCanvas/TimerText
------------------
