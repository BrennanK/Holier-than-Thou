using UnityEngine;

public interface IInitializer<T>
{
	bool Equals(T obj);
	void Initialize(GameObject[] obj);
}
