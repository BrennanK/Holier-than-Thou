using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AICustomization : MonoBehaviour
{
	[SerializeField] private GameObject[] equipmentPrefabs;

	[Header("Randomization")]

	[SerializeField] private bool randomize = false;
	[SerializeField] private string[] equipmentTypes = {"Hat", "Body"};

	private string[] equippedItems;
	private static GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
		if (randomize)
		{
			RandomizeEquipment();
		}
		equippedItems = equipmentPrefabs.Select(i => i.name).ToArray();
		InitializeEquipment();
    }

	private bool RandomizeEquipment()
	{
		// Make a container for the prefabs to load in.
		List<GameObject> prefabsOfCurrentType = new List<GameObject>();
		// Get ALL of the prefabs from the prefabs folder.
		if(prefabs == null)
		{
			prefabs = Resources.LoadAll("Prefabs/Equipment").Select(p => (GameObject)p).ToArray();
		}
		int i = 0;
		foreach(string type in equipmentTypes)
		{
			//Get all prefabs of Type "type."
			foreach(GameObject prefab in prefabs)
			{
				if (prefab.name.StartsWith(type))
				{
					prefabsOfCurrentType.Add(prefab);
				}
			}
			//assign one at random to equipmentPrefabs.
			if (prefabsOfCurrentType.Count > 0)
			{
				equipmentPrefabs[i] = prefabsOfCurrentType[Random.Range(0, prefabsOfCurrentType.Count - 1)];
				prefabsOfCurrentType.Clear();
			}
			else
			{
				return false;
			}
			i++;
		}
		return true;
	}

	private void InitializeEquipment()
	{
		if (transform.GetComponent<MeshRenderer>())
		{
			transform.GetComponent<MeshRenderer>().enabled = false;
		}
		int i = 0;
		foreach (string item in equippedItems)
		{
			//find prefab of specific name
			GameObject option = Instantiate(
				(GameObject)Resources.Load($"Prefabs/Equipment/{item}"),
				transform.GetChild(i)
				);
			option.SetActive(true);
			i++;
		}
	}
}
