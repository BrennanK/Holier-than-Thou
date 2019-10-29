using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [Tooltip("Force applied to player and AI on Collision multiplied by Magnitude")]
    public float bouceOffForce = 40;
    [Tooltip("Vertical bounce force. If none desired make Zero")]
    public float upBounceForce = .75f;
    [Tooltip("Force applied to player and AI when hitting things tagged 'Wall'")]
    public float obsticalBounceForce = 1000f;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 bounce;
        //Gets the direction towards what they collided with
        bounce = transform.position - collision.transform.position;

        if (gameObject.CompareTag("Wall"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce((-collision.contacts[0].normal + (Vector3.up * upBounceForce)) * obsticalBounceForce);
        }
        else if (collision.gameObject.GetComponent<Bounce>() && collision.gameObject.GetComponent<Competitor>())
        {
            if (!collision.gameObject.GetComponent<Competitor>().ballOfSteel)
            {

                //if that thing is something has a rigidbody and is an enemy then it adds force in oposite directions.
                //The important thing about this code is that it is played on all colliding objects at the same time
                if (collision.gameObject.GetComponent<Rigidbody>())
                {
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(-bounce * bouceOffForce * GetComponent<Rigidbody>().velocity.magnitude);

                }
            }
        }
    }
}