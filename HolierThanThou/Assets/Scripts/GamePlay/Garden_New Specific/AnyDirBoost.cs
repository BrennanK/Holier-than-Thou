using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDirBoost : MonoBehaviour
{
    private Rigidbody rb;
    public float boostVal;

	public GameObject particle;
	

	// Start is called before the first frame update
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

		rb = other.GetComponentInParent<Rigidbody>();

		Instantiate(particle, transform.position, particle.transform.rotation);

		StartCoroutine(ResetParticleDirection());

		if (rb.velocity.magnitude <= 30)
		{
			rb.AddForce(rb.velocity * boostVal);
		}
		else if(rb.velocity.magnitude <= 40)
		{
			rb.AddForce(rb.velocity * (boostVal/2));
		}
    }

	IEnumerator ResetParticleDirection ()
	{
		yield return new WaitForSeconds(.25f);
	}
}
