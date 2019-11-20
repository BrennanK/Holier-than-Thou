using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

#if UNITY_EDITOR
public class PlayerUIScript : MonoBehaviour
{

    GameObject player;
    public Text multiplier;
    public Text speed;
    private float mag;
    private Vector3 vel;
    public Text basePoints;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        multiplier.transform.parent.gameObject.SetActive(true);
        speed.gameObject.SetActive(false);
        basePoints.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if(multiplier != null && speed != null)
		{
			vel = player.GetComponent<Rigidbody>().velocity;
			mag = vel.magnitude;
			multiplier.text = "X" + player.GetComponent<PointTracker>().MultVal().ToString();
            basePoints.text = (player.GetComponent<PointTracker>().baseVal()-1).ToString();

            if(player.GetComponent<PointTracker>().baseVal() > 1)
            {
                basePoints.transform.parent.gameObject.SetActive(true);

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
                basePoints.transform.parent.gameObject.SetActive(false);
            }

            speed.text = mag.ToString();
		}



    }
}
#endif
