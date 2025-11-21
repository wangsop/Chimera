using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndexManager {
    public Button button;
    public int index;
    public ButtonIndexManager(Button b, int i, ChimeraTeamManager c)
    {
        button = b;
        index = i;
        button.onClick.AddListener(() => c.DeleteEntry(index));
    }

    public void decrementIndex()
    {
        index--;
    }

}
public class ChimeraTeamManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject contentRect;

    private GameObject icon;
    private List<GameObject> entries;
    private List<ButtonIndexManager> buttons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon = this.transform.GetChild(1).gameObject;
        entries = new List<GameObject>();
        buttons = new List<ButtonIndexManager>();
    }

    public void CreateNewEntry(NewChimeraStats stats)
    {
        GameObject currentEntry = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;

        currentEntry.transform.SetParent(contentRect.transform, false);
        GameObject head = currentEntry.transform.GetChild(0).gameObject;
        GameObject body = currentEntry.transform.GetChild(1).gameObject;
        GameObject tail = currentEntry.transform.GetChild(2).gameObject;
        GameObject button = currentEntry.transform.GetChild(3).gameObject;

        head.GetComponent<Image>().sprite = stats.Head.GetComponent<SpriteRenderer>().sprite;
        body.GetComponent<Image>().sprite = stats.Body.GetComponent<SpriteRenderer>().sprite;
        tail.GetComponent<Image>().sprite = stats.Tail.GetComponent<SpriteRenderer>().sprite;

        icon.transform.position = new Vector3(icon.transform.position.x, icon.transform.position.y - prefab.GetComponent<RectTransform>().rect.height, icon.transform.position.z);
        int count = entries.Count;
        buttons.Add(new ButtonIndexManager(button.GetComponent<Button>(), count, this));
        entries.Add(currentEntry);
        if (entries.Count == 5)
        {
            icon.SetActive(false);
        }
    }

    public void DeleteEntry(int index)
    {
        Debug.Log(index);
        GameObject deletedEntry = entries[index];
        Destroy(deletedEntry);
        entries.RemoveAt(index);
        buttons.RemoveAt(index);
        Globals.party_indexes.RemoveAt(index);

        foreach (ButtonIndexManager b in buttons) {
            if (b.index > index)
            {
                b.decrementIndex();
            }
        }

        if (entries.Count < 5)
        {
            icon.SetActive(true);
        } else if (entries.Count == 0)
        {
            return;
        }
        icon.transform.position = new Vector3(icon.transform.position.x, icon.transform.position.y + prefab.GetComponent<RectTransform>().rect.height, icon.transform.position.z);
    }
}
