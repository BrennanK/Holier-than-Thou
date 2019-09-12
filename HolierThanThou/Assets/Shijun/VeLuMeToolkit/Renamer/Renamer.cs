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
            if (counter < 9)
            {
                child.gameObject.name = mainTitle + "0" + (counter + 1).ToString();
            }
            else
            {
               child.gameObject.name = mainTitle + (counter + 1).ToString();
            }
            counter++;
        }
    }
}
