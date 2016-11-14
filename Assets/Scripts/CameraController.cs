using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float scrollAmount;

	// Update is called once per frame
	void LateUpdate () {
        Vector3 position = transform.position;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= 10)
        {
            position.x -= scrollAmount;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - 10)
        {
            position.x += scrollAmount;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= 10)
        {
            position.y += scrollAmount;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - 10)
        {
            position.y -= scrollAmount;
        }
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f || Input.GetKey("."))
        {
            position.z -= scrollAmount * 2;
        }
        else if (scroll < 0f || Input.GetKey("/"))
        {
            position.z += scrollAmount * 2;
        }
        transform.position = position;
    }
}
