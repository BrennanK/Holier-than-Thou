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


	/// <summary>  Creates a coroutine that plays tthe explosion effect.</summary>
	/// <param name="duration">  Duration in seconds the effect plays for.</param>
	/// <param name="scale">  Vector3 scale instantaneously affecting the particle effect.</param>
	/// <param name="effect">  The Name of the prefab file that the Particle Effect is stored in.</param>
	/// <param name="yOffset">  A height offset.</param>
	public void StartExplosion(float duration, Vector3 scale, string effect = "PE_BlastZone", float yOffset = 0f)
	{
		StartCoroutine(Explode(duration, scale, effect, yOffset));
	}

	private IEnumerator Explode(float duration, Vector3 scale, string effect = "PE_BlastZone", float yOffset = 0f)
	{
		GameObject particles = Instantiate(
			(GameObject)Resources.Load($"Prefabs/Particle Effects/{effect}"),
			transform //Place it on the Hat component in order for it to not rotate.
			);
		particles.transform.localScale += scale;
		particles.transform.Translate(Vector3.up * yOffset, Space.World);
		particles.SetActive(true);
		yield return new WaitForSeconds(duration);
		Destroy(particles);
	}
}
