using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static String NextSceneToLoad;

    // This should be used most of the time. type the needed scene in the inspector
    public static void LoadScene(String scene) 
    {
        NextSceneToLoad = scene;
        SceneManager.LoadScene("Loading Screen");
    }

    // Use this for the "Level x" buttons in the Level Select scene.
    // Type the dungeon # in the inspector 
    public static void DungeonQueue(int SelectedLevel)
    {
        // replaces the globals level check
        if (SelectedLevel > Globals.highestClearedLevel)
        {
            Debug.Log("You have not unlocked this level yet!");
        } 
        else
        {
            if (SelectedLevel > 0)
            {
                NextSceneToLoad = "Dungeon" + SelectedLevel;
                Globals.levelSelected = SelectedLevel;
            }
            else
            {
                NextSceneToLoad = "Dungeon";
            }
            Debug.Log("Selected " + NextSceneToLoad);
            SceneManager.LoadScene("Chimera Select");
        }
    }

    // Mostly identical to LoadScene, however it uses the previously saved NextSceneToLoad
    // to keep the dungeon # for the next loading screen call. 
    // It also checks to make sure the party isn't empty before entering the dungeon. 
    public static void LoadDungeon()
    {
        if (Globals.party_indexes.Count > 0)
        {
            SceneManager.LoadScene("Loading Screen");
        } 
        else
        {
            Debug.Log("You must pick at least one Chimera before going into the dungeon!");
        }
        
    }
}
