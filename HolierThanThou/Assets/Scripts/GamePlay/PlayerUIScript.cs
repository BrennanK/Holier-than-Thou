using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

#if UNITY_EDITOR
public class PlayerUIScript : MonoBehaviour
{

    GameObject player;
    public GameObject multiplier;
    public GameObject speed;
    private float mag;
    private Vector3 vel;
    public GameObject basePoints;
    //public GameObject crownUI;

    void Awake()
    {
        player = GameObject.Find("Player");
        multiplier = GameObject.Find("MultiplierText");
        speed = GameObject.Find("SpeedText");
        basePoints = GameObject.Find("BasePointsText");
        //crownUI = GameObject.Find("DebugCanvas");
    }
    void Start()
    {
        multiplier.GetComponent<Text>().enabled = true;
        speed.GetComponent<Text>().enabled = false;
        basePoints.GetComponentInParent<Image>().enabled = false;
        basePoints.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		//if(multiplier != null && speed != null)
		//{
			vel = player.GetComponent<Rigidbody>().velocity;
			mag = vel.magnitude;
            multiplier.GetComponent<Text>().text = "X" + player.GetComponent<PointTracker>().MultVal().ToString();
            basePoints.GetComponent<Text>().text = (player.GetComponent<PointTracker>().baseVal()-1).ToString();

            if(player.GetComponent<PointTracker>().baseVal() > 1)
            {
                basePoints.GetComponentInParent<Image>().enabled = true;
                basePoints.GetComponentInChildren<Text>().enabled = true;

                if (player.GetComponent<PointTracker>().baseVal() == 2)
                {
                    basePoints.GetComponentInParent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Menu UI/Crown_simple.png", typeof(Sprite));
                }
                else if (player.GetComponent<PointTracker>().baseVal() == 3)
                {
                    basePoints.GetComponentInParent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Menu UI/Crown_Simple2.png", typeof(Sprite));
                }
                else
                {
                    basePoints.GetComponentInParent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Menu UI/Crown_Simple3.png", typeof(Sprite));
                }
            }
            else
            {
                basePoints.GetComponentInParent<Image>().enabled = false;
                basePoints.GetComponent<Text>().enabled = false;
            }

            speed.GetComponent<Text>().text = mag.ToString();
		//}



    }
}
#endif
