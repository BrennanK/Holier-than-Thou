using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
	private float m_duration;

	public ExplosionEffect()
	{
		m_duration = 1f;
	}

	public ExplosionEffect(float _duration)
	{
		m_duration = _duration;
	}

	public void StartExplosion(float duration, Vector3 scale, float yOffset = 0f)
	{
		StartCoroutine(Explode(duration, scale, yOffset));
	}

	private IEnumerator Explode(float duration, Vector3 scale, float yOffset = 0f)
	{
		GameObject particles = Instantiate(
			(GameObject)Resources.Load($"Prefabs/Particle Effects/PE_BlastZone"),
			transform //Place it on the Hat component in order for it to not rotate.
			);
		particles.transform.localScale += scale;
		particles.transform.Translate(Vector3.up * yOffset, Space.World);
		particles.SetActive(true);
		yield return new WaitForSeconds(duration);
		Destroy(particles);
	}
}
