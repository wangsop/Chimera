using UnityEngine;

public class ChimeraCatalogScreenManager : MonoBehaviour
{
    public Canvas detailsCanvas;
    public Canvas overviewCanvas;

    public void Start()
    {
        SwitchToOverview();
    }
    public void SwitchToOverview()
    {
        overviewCanvas.transform.gameObject.SetActive(true);
        detailsCanvas.transform.gameObject.SetActive(false);
    }

    public void SwitchToDetails(int index = -1)
    {
        overviewCanvas.transform.gameObject.SetActive(false);
        detailsCanvas.transform.gameObject.SetActive(true);

        detailsCanvas.GetComponent<CatalogPopulateChimeraDetails>().Initialize(index);

        Debug.Log(index);
    }
}
