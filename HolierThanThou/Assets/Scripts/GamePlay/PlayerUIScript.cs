using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUIScript : MonoBehaviour
{
    float mag;
    Vector3 vel;
    public GameObject crownUI;
    public Sprite crown1;
    public Sprite crown2;
    public Sprite crown3;

    void Awake()
    {


    }
    void Start()
    {

        crownUI = GameObject.Find("DebugCanvas");

        crownUI.transform.GetChild(0).GetComponent<Text>().enabled = true;
        crownUI.transform.GetChild(1).GetComponent<Text>().enabled = false;
        crownUI.transform.GetChild(2).GetComponent<Image>().enabled = false;
        crownUI.transform.GetChild(2).GetComponentInChildren<Text>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        vel = transform.GetComponent<Rigidbody>().velocity;
        mag = vel.magnitude;

        crownUI.transform.GetChild(0).GetComponent<Text>().text = "X" + transform.GetComponent<PointTracker>().MultVal().ToString();
        crownUI.transform.GetChild(2).GetComponentInChildren<Text>().text = (transform.GetComponent<PointTracker>().baseVal() - 1).ToString();

        if (transform.GetComponent<PointTracker>().baseVal() > 1)
        {
            crownUI.transform.GetChild(2).GetComponent<Image>().enabled = true;
            crownUI.transform.GetChild(2).GetComponentInChildren<Text>().enabled = true;

            if (transform.GetComponent<PointTracker>().baseVal() == 2)
            {
                crownUI.transform.GetChild(2).GetComponent<Image>().sprite = crown1;
            }
            else if (transform.GetComponent<PointTracker>().baseVal() == 3)
            {
                crownUI.transform.GetChild(2).GetComponent<Image>().sprite = crown2;
            }
            else
            {
                crownUI.transform.GetChild(2).GetComponent<Image>().sprite = crown3;
            }
        }
        else
        {
            crownUI.transform.GetChild(2).GetComponent<Image>().enabled = false;
            crownUI.transform.GetChild(2).GetComponentInChildren<Text>().enabled = false;
        }

        crownUI.transform.GetChild(1).GetComponent<Text>().text = mag.ToString();

    }
}

