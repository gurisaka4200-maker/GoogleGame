using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2.2f, -3.8f);
    public float smoothTime = 0.12f;
    public float rotationSpeed = 0.2f;
    private Vector3 velocity = Vector3.zero;
    private float yaw = 0f;

    void LateUpdate()
    {
        if (!target) return;

        // Touch swipe to rotate camera (single touch)
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved)
            {
                yaw += t.deltaPosition.x * rotationSpeed * Time.deltaTime;
            }
        }
        else if (Application.isEditor)
        {
            if (Input.GetMouseButton(1))
            {
                yaw += Input.GetAxis("Mouse X") * rotationSpeed * 10f * Time.deltaTime;
            }
        }

        Quaternion camRot = Quaternion.Euler(20f, yaw + target.eulerAngles.y, 0f);
        Vector3 desiredPos = target.position + camRot * offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime);
        transform.LookAt(target.position + Vector3.up * 1.3f);
    }
}
