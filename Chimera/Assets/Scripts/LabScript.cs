using UnityEngine;
using UnityEngine.SceneManagement;

public class LabScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Dungeon(){
        SceneManager.LoadScene("Level Select");
    }
    public void Gacha(){
        SceneManager.LoadScene("Gacha");
    }
    public void Catalog(){
        SceneManager.LoadScene("Chimera Catalog");
    }
}
