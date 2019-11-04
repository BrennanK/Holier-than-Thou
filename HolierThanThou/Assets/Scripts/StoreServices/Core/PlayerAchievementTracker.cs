using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAchievementTracker : MonoBehaviour
{
    private Competitor PC;
    private GameObject goal;
    private ScoreManager scoreManager;
    private float hitTimerStart = 5f;
    private float hitTimer;
    private bool startHitTimer = false;
    public bool hitSomebody = true;//Cant touch me achievement
    public bool ScoredAfterHit = false;//Alley-oop achievement
    public float distFromGoal;
    public bool knockedFirst = false;//Denied achievement
    public bool scoredGoal = false;//Score! achievement
    public bool usedPowerUp = true;// No Hands achievement
    public bool usedJump = true;//Grounded achievement




    private void Start()
    {
        PC = gameObject.GetComponent<Competitor>();
        goal = GameObject.FindGameObjectWithTag("Goal");
        scoreManager = goal.GetComponent<ScoreManager>();
    }


    private void Update()
    {
        if(startHitTimer)
        {
            hitTimer -= Time.deltaTime;
        }
        if(hitTimer > 0)
        {
            if(PC.ScoredGoal)
            {
                ScoredAfterHit = true;
            }
        }
        if(hitTimer <= 0)
        {
            startHitTimer = false;
        }

        if(PC.ScoredGoal)
        {
            scoredGoal = true;
        }
    }


    public void TooglePowerupUsed()
    {
        usedPowerUp = false;
    }

    public void ToggleJumped()
    {
        usedJump = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        var player = other.gameObject.GetComponent<Competitor>();

        if(player)
        { 
            hitSomebody = false;
            hitTimer = hitTimerStart;
            startHitTimer = true;

            if(player.Name == scoreManager.firstPlace && Vector3.Distance(transform.position, goal.transform.position) <= distFromGoal)
            {
                knockedFirst = true;
            }
        }
    }
}
