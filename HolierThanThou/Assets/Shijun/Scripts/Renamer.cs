// ShijunGuo (2019-06-07)
// Rename Children GameObject under a Parent.
using UnityEngine;

public class Renamer : MonoBehaviour
{
    [SerializeField] private string mainTitle;

    public void RenameChildren()
    {
        int counter = 0;

        foreach (Transform child in transform)
        {
            child.gameObject.name = mainTitle + counter.ToString();

            counter++;
        }
    }
}
