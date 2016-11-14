using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float scrollAmount;

	// Update is called once per frame
	void LateUpdate () {
        Vector3 position = transform.position;
#if UNITY_ANDROID
        Camera camera = GetComponent<Camera>();
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Otherwise change the field of view based on the change in distance between the touches.
            camera.fieldOfView += deltaMagnitudeDiff * 0.5f;

            // Clamp the field of view to make sure it's between 0 and 180.
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= 50)
        {
            position.x -= scrollAmount;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - 50)
        {
            position.x += scrollAmount;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= 50)
        {
            position.y += scrollAmount;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - 50)
        {
            position.y -= scrollAmount;
        }
#else
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
#endif
        transform.position = position;
    }
}
