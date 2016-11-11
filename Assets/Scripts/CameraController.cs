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
        if(Input.mousePosition.x <= 0)
        {
            offset.x -= 0.1f;
        }
        else if (Input.mousePosition.x >= Screen.width)
        {
            offset.x += 0.1f;
        }
        if (Input.mousePosition.y <= 0)
        {
            offset.y -= 0.1f;
        }
        else if (Input.mousePosition.y >= Screen.height)
        {
            offset.y += 0.1f;
        }
        transform.position = gameMap.transform.position + offset;
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            GetComponent<Camera>().orthographicSize -= 0.5f;
        }
        else if (scroll < 0f)
        {
            GetComponent<Camera>().orthographicSize += 0.5f;
        }
    }
}
