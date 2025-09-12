using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [field: SerializeField] private SerializableDictionary<Collectable, int> Collectables { get; set; }

    /// <summary>
    /// Finds and returns the count of a resource in the dictionary
    /// </summary> 
    /// <param name="type">Resource type to check</param>
    /// <returns>Return the number of a resource in the dictionary if any or 0 if none</returns>
    public int GetResourceCount(Collectable type)
    {
        if (Collectables.TryGetValue(type, out int currentCount))
        {
            return currentCount;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Here we check if the reasource is already in the inventory if so then we add to that key value pair if there does not exist 
    /// a key for the thing we are picking up then we add that key value pair to the dictionairy and add to the value. 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int AddResources(Collectable type, int count)
    {
        if (Collectables.TryGetValue(type, out int currentCount))
        {
            return Collectables[type] += count;
        }
        else
        {
            Collectables.Add(type, count);
            return count;
        }
    }
}
