using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject eyeball;

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = new Vector3(eyeball.transform.position.x, eyeball.transform.position.y, transform.position.z);
    }
}
