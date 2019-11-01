using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
public class PlayerUIScript : MonoBehaviour
{
    GameObject player;
    public Text multiplier;
    public Text speed;
    private float mag;
    private Vector3 vel;
    public Text quickness;
    public Text basePoints;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
		if(multiplier != null && speed != null)
		{
			vel = player.GetComponent<Rigidbody>().velocity;
			mag = vel.magnitude;
			multiplier.text = player.GetComponent<PointTracker>().MultVal().ToString();
            basePoints.text = player.GetComponent<PointTracker>().baseVal().ToString();
            quickness.text = player.GetComponent<PointTracker>().GetBounceVal().ToString();
            speed.text = mag.ToString();
		}
    }
}
#endif
