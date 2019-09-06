using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GottaGoFast : PowerUp
{
    private float duration = 5f;
    private float playerStartSpeed;
    private float playerNewSpeed;

    public override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
      
        Debug.Log("Gotta go fast! Power Up Used!");

    }

    



    public override void ActivateSecondPowerUp()
    {
        base.ActivateSecondPowerUp();
        Debug.Log("Gotta go fast! Power Up Used!");

    }


    IEnumerator ChangeSpeed()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<JoystickPlayerExample>();
        playerStartSpeed = player.speed;
        playerNewSpeed = player.speed + (player.speed * .25f);

        while (duration > 0)
        {
            player.speed = player.speed + (player.speed * .25f);

            duration -= Time.deltaTime;

            yield return new WaitForSeconds(3);
        }
       

    }
}
