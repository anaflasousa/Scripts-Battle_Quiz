using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform playerBody;

    float xRotation;
    bool canMouseMove = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (canMouseMove)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void CancelCamera(bool value)
    {
        canMouseMove = value;
    }
}
