using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
	private static DoNotDestroy m_Instance = null;
	void Awake()
	{
		if(m_Instance == null)
		{
			m_Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
