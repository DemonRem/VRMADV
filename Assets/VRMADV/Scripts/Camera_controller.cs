using UnityEngine;

/// <summary>
/// The camera added this script will follow the specified object.
/// The camera can be moved by left mouse drag and mouse wheel.
/// </summary>
[ExecuteInEditMode, DisallowMultipleComponent]
public class Camera_controller : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    public float distance = 2.0f;
    public float polarAngle = 90.0f;
    public float azimuthalAngle = 90.0f;

    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 4.0f;
    [SerializeField] private float minPolarAngle = 5.0f;
    [SerializeField] private float maxPolarAngle = 175.0f;
    [SerializeField] private float mouseXSensitivity = 0.5f;
    [SerializeField] private float mouseYSensitivity = 0.5f;
    [SerializeField] private float scrollSensitivity = 0.001f;
    [SerializeField] private float offsetspeed = 0.05f;
    void LateUpdate()
    {
        if(target != null){
            if (Input.touchCount == 1) {
                Touch touchZero = Input.GetTouch(0);
                if(touchZero.phase == TouchPhase.Moved){
                    Vector2 Fluctuation = new Vector2(touchZero.deltaPosition.x * Time.deltaTime, touchZero.deltaPosition.y * Time.deltaTime);
                    updateAngle(Fluctuation.x, Fluctuation.y);
                }

            } else if(Input.touchCount == 2){
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                updateDistance(touchOne, touchZero);
                updateOffset(touchZero, touchOne);
            }

            Debug.Log(offset);
            var lookAtPos = offset;
            updatePosition(lookAtPos);
            transform.LookAt(lookAtPos);
        }
    }

    void updateOffset(Touch zero, Touch one)
    {
        Vector2 DeltaCenter = (zero.deltaPosition + one.deltaPosition) / 2;
        Vector3 direction = Quaternion.Euler(transform.localEulerAngles.x, 0, 0) * new Vector3(DeltaCenter.x, 0, 0) * (-1) * Time.deltaTime * offsetspeed;
        direction = transform.TransformDirection(direction);
        offset = new Vector3(offset.x + direction.x, offset.y + direction.y, offset.z + direction.z);
        direction = Quaternion.Euler(transform.localEulerAngles.x, 0, 0) * new Vector3(0, DeltaCenter.y ,0) * (-1) * Time.deltaTime * offsetspeed;
        offset = new Vector3(offset.x + direction.x, offset.y + direction.y, offset.z + direction.z);
    }

    void updateAngle(float x, float y)
    {
        x = azimuthalAngle - x * mouseXSensitivity;
        azimuthalAngle = Mathf.Repeat(x, 360);

        y = polarAngle + y * mouseYSensitivity;
        polarAngle = Mathf.Clamp(y, minPolarAngle, maxPolarAngle);
    }

    void updateDistance(Touch x, Touch y)
    {
        Vector2 touchZeroPrevPos = x.position - x.deltaPosition;
        Vector2 touchOnePrevPos = y.position - y.deltaPosition;

        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float touchDeltaMag = (x.position - y.position).magnitude;

        float deltaMagnitudeDiff = distance + (prevTouchDeltaMag - touchDeltaMag) * scrollSensitivity;

        distance = Mathf.Clamp(deltaMagnitudeDiff, minDistance, maxDistance);
    }

    void updatePosition(Vector3 lookAtPos)
    {
        var da = azimuthalAngle * Mathf.Deg2Rad;
        var dp = polarAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da)
        );
    }
}