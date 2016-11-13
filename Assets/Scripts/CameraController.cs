using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject gameMap;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(5.5f, 5.5f, -1);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        float scrollAmount = GetComponent<Camera>().orthographicSize / 100;
        if (Input.mousePosition.x <= 0)
        {
            offset.x -= scrollAmount;
        }
        else if (Input.mousePosition.x >= Screen.width)
        {
            offset.x += scrollAmount;
        }
        if (Input.mousePosition.y <= 0)
        {
            offset.y -= scrollAmount;
        }
        else if (Input.mousePosition.y >= Screen.height)
        {
            offset.y += scrollAmount;
        }
        transform.position = gameMap.transform.position + offset;
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            GetComponent<Camera>().orthographicSize /= 1.25f;
        }
        else if (scroll < 0f)
        {
            GetComponent<Camera>().orthographicSize *= 1.25f;
        }
    }
}
