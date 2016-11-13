using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float scrollAmount;

	// Update is called once per frame
	void LateUpdate () {
        Vector3 position = transform.position;
        //if (Input.mousePosition.x <= 0)
        if (Input.GetKey("left"))
        {
            position.x -= scrollAmount;
        }
        //else if (Input.mousePosition.x >= Screen.width)
        else if (Input.GetKey("right"))
        {
            position.x += scrollAmount;
        }
        //if (Input.mousePosition.y <= 0)
        if (Input.GetKey("down"))
        {
            position.y += scrollAmount;
        }
        //else if (Input.mousePosition.y >= Screen.height)
        else if (Input.GetKey("up"))
        {
            position.y -= scrollAmount;
        }
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f || Input.GetKey("."))
        {
            position.z += scrollAmount;
        }
        else if (scroll < 0f || Input.GetKey("/"))
        {
            position.z -= scrollAmount;
        }
        transform.position = position;
    }
}
