using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSwitcher : MonoBehaviour
{
    private GameObject[] hatEntity;

    // Start is called before the first frame update
    void Start()
    {
        Transform hats = GetComponent<Transform>();
        hatEntity = new GameObject[hats.childCount];
        int counter = 0;
        foreach (Transform hat in hats)
        {
            hatEntity[counter] = hat.gameObject;
            counter++;
        }
    }

    public void SwitchHatEntity(int index)
    {
        foreach(GameObject hat in hatEntity)
        {
            hat.SetActive(false);
        }
        hatEntity[index].SetActive(true);
    }
}
