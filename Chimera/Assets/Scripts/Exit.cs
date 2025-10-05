using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject playCanvas;
    public GameObject endCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ChimeraScript>() == null)
        {
            return;
        }
        Time.timeScale = 0f; //pause the game
        //display end-of-game stats
        endCanvas.SetActive(true);
        playCanvas.SetActive(false);
        Globals.currency += 500;
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        LoadingManager.LoadScene("Lab");
    }
}
