using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competitor : MonoBehaviour
{
    //Name and scoring Variables
    public string Name;
    public int Score;
    private bool scoredGoal;

    //Powerup constructor intakes
    public Transform origin;
    public bool navMeshOff;

    //Variables for power up effects
    public float blastedDuration;
    public bool untouchable;
    public bool inivisible;
    public bool ballOfSteel;
    public bool superBounce;
    public Material startMaterial;
    
    //trail is the trail render on the ball
    TrailRenderer trails;
    Rigidbody myRB;
    

    //Called when a ball hits the thanoswall collider
    

    private void Awake()
    {
        this.transform.localScale = new Vector3(.025f, .025f, .025f);
        origin = this.transform;
        navMeshOff = false;
        untouchable = false;
        inivisible = false;
        myRB = GetComponent<Rigidbody>();
        trails = GetComponent<TrailRenderer>();
    }

    public void Start()
    {
        if (transform.childCount > 1)
        {
            Transform t = transform.GetChild(1);
            if (t.childCount > 0)
            {
                t = t.GetChild(0);
                startMaterial = t.GetComponent<MeshRenderer>().material;
                return;
            }
        }
        startMaterial = transform.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if(trails != null)
        {
            trails.time = Mathf.Clamp(myRB.velocity.magnitude * .05f, 0, .3f);
            if (myRB.velocity.magnitude > 25)
            {
                trails.material.color = Color.red;
                trails.enabled = true;
            }
            else if (myRB.velocity.magnitude > 20)
            {
                trails.material.color = Color.green;
                trails.enabled = true;
            }
            else if (myRB.velocity.magnitude > 15)
            {
                trails.material.color = Color.blue;
                trails.enabled = true;
            }
            else
            {
                trails.material.color = Color.white;
            }
        }
    }

    public bool ScoredGoal
    {
        get { return scoredGoal; }

        set { scoredGoal = value; }
    }

    public void BallOfSteel(Transform origin, float duration)
    {
        StartCoroutine(Unbouncable(origin, duration));
    }

    public void BeenBlasted()
    {
        StartCoroutine(TurnNavMeshBackOn(blastedDuration));
    }

    public void BeenChilled(Competitor competitor, float duration)
    {
        StartCoroutine(TurnMovementControlBackOn(competitor, duration));
    }

    public void CantTouchMe(float duration)
    {
        StartCoroutine(Untouchable(duration));
    }

    public void CantFindMe(Transform origin, float duration)
    {
        StartCoroutine(Invis(origin, duration));
    }

    public void WentFast(Transform origin, float duration, float speedMultiplier)
    {
        StartCoroutine(ResetSpeed(origin, duration, speedMultiplier));
    }

    public void BeenSlowed(Competitor competitor, float duration, float speedMultiplier)
    {
        StartCoroutine(ReverseMovementSpeed(competitor, duration, speedMultiplier));
    }

    public void NormalBounce(Transform origin, float duration, float bounceMultiplier)
    {
        StartCoroutine(ReverseBounceMultiplier(origin, duration, bounceMultiplier));
    }

    public void DisMine(float duration, GameObject disMine, Vector3 position, Quaternion rotation)
    {
        StartCoroutine(MineDelayActivation(duration, disMine, position, rotation));
    }

    IEnumerator Invis(Transform origin, float duration)
    {
        var playerM = origin.GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        Material[] playerH = new Material[0];
        if (origin.GetChild(0).GetChild(0).gameObject.GetComponentInChildren<MeshRenderer>())
        {
            playerH = origin.GetChild(0).GetChild(0).gameObject.GetComponentInChildren<MeshRenderer>().materials;
        }
        Color originalColor = new Color(playerM.color.r, playerM.color.g, playerM.color.b, 1f);

        inivisible = true;

        yield return new WaitForSeconds(duration);
        if (origin.tag == "Player")
        {

            playerM.DisableKeyword("_ALPHATEST_ON");
            playerM.DisableKeyword("_ALPHABLEND_ON");
            playerM.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            playerM.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            playerM.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            playerM.SetInt("_Zwrite", 1);
            playerM.SetColor("_Color", originalColor);
            playerM.renderQueue = -1;
            playerM.SetFloat("_Mode", 0);

            if (playerH.Length > 0)
            {
                for (int i = 0; i < playerH.Length; i++)
                {
                    Color originalHColor = new Color(playerH[i].color.r, playerH[i].color.g, playerH[i].color.b, 1f);
                    playerH[i].DisableKeyword("_ALPHATEST_ON");
                    playerH[i].DisableKeyword("_ALPHABLEND_ON");
                    playerH[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    playerH[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    playerH[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    playerH[i].SetInt("_Zwrite", 1);
                    playerH[i].SetColor("_Color", originalHColor);
                    playerH[i].renderQueue = -1;
                    playerH[i].SetFloat("_Mode", 0);
                }
            }
        }
        else
        {
            origin.GetChild(1).GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;

            if (origin.GetChild(0).GetChild(0).gameObject.GetComponentInChildren<MeshRenderer>())
            {
                origin.GetChild(0).GetChild(0).gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
            }

        }

        inivisible = false;
    }

    private IEnumerator Untouchable(float duration)
    {
        untouchable = true;
        yield return new WaitForSeconds(duration);
        untouchable = false;

    }

    private IEnumerator Unbouncable(Transform origin, float duration)
    {
        ballOfSteel = true;
        yield return new WaitForSeconds(duration);

        origin.GetComponent<MeshRenderer>().material = startMaterial;
        //origin.GetComponentInParent<BounceFunction>().enabled = true;
        origin.GetComponentInParent<Bounce>().enabled = true;
        ballOfSteel = false;

    }

    private IEnumerator TurnNavMeshBackOn(float duration)
    {
        yield return new WaitForSeconds(duration);
        navMeshOff = false;

    }

    private IEnumerator TurnMovementControlBackOn(Competitor competitor, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (competitor.GetComponent<RigidBodyControl>())
        {
            competitor.GetComponent<Rigidbody>().freezeRotation = false;
            competitor.GetComponent<RigidBodyControl>().enabled = true;
        }
        else
        {
            competitor.GetComponent<Rigidbody>().freezeRotation = false;
            competitor.GetComponent<AIStateMachine>().enabled = true;
        }

    }

    private IEnumerator ResetSpeed(Transform origin, float duration, float speedMultiplier)
    {
        yield return new WaitForSeconds(duration);

        if (origin.GetComponent<RigidBodyControl>())
        {
            origin.GetComponent<RigidBodyControl>().speed /= speedMultiplier;
        }
        else
        {
            origin.GetComponent<AIStateMachine>().Velocity /= speedMultiplier;
        }
    }

    private IEnumerator ReverseMovementSpeed(Competitor competitor, float duration, float speedMultiplier)
    {
        yield return new WaitForSeconds(duration);

        if (competitor.GetComponent<RigidBodyControl>())
        {
            competitor.GetComponent<RigidBodyControl>().speed /= speedMultiplier;
        }
        else
        {
            competitor.GetComponent<AIStateMachine>().Velocity /= speedMultiplier;
        }
    }

    private IEnumerator ReverseBounceMultiplier(Transform origin, float duration, float bounceMultiplier)
    {
        superBounce = true;
        yield return new WaitForSeconds(duration);
        origin.GetComponent<Bounce>().bouceOffForce /= bounceMultiplier;
        superBounce = false;
    }

    private IEnumerator MineDelayActivation(float duration, GameObject disMine, Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(duration);
        Instantiate(disMine, position, rotation);
    }

}
