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

    private float maxBounce;

    public void Start()
    {
        maxBounce = bouceOffForce;
    }

    public void SetMaxmiumBounce()
    {
        bouceOffForce = maxBounce;
    }

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
				gameObject.GetComponent<Rigidbody>().AddForce(bounce/2 * Mathf.Clamp(bouceOffForce * GetComponent<Rigidbody>().velocity.magnitude, 300, 1000));
				
				if(gameObject.GetComponent<RigidBodyControl>() && collision.gameObject.GetComponent<AIStateMachine>())
				{
					collision.gameObject.GetComponent<Rigidbody>().AddForce(-bounce * Mathf.Clamp(bouceOffForce * GetComponent<Rigidbody>().velocity.magnitude, 300, 1000));
				}
            }
        }
    }
}