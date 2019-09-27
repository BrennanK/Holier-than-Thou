// An edited Scritps by Shijun (2019-09-25)
using System.Collections.Generic;
using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    [SerializeField] private GameObject projectionSystem;

    //Find the positions to read and write decorations.
    [SerializeField] private Transform[] accessorySlots;

    //Find the inventory to load decorations.
        //The List itself is the type of accessories for target positions.
        //The Dimension 0 of the Array is the serial number for specific accessory.
        //The Dimension 1 of the Array is the doability of the specific accessory:
            //If the int is bigger than 0, it is the price for the accessory.
            //If the int is lower than 0, it the status (unlocked) for the accessory.
    private List<int[,]> accessories = new List<int[,]>();

    /// <summary>
    /// Find and return the accessible positions under Player GameObject.
    /// </summary>
    /// <returns></returns>
    private int InitialDecorationSlots()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        accessorySlots = new Transform[player.transform.childCount];
        int counter = 0;
        foreach (Transform child in player.transform)
        {
            accessorySlots[counter] = child;
            counter++;
        }
        return counter++;
    }

    /// <summary>
    /// Generate the accessories lists based on the amount of accessible positions.
    /// </summary>
    private void InitialAccessories()
    {




        ////int[,] tArray = new int[];
        //for (int i = 0; i < InitialDecorationSlots(); i++)
        //{
        //    //accessories.Add(int[,])
        //}
    }
}
