using UnityEngine;

public class Camera_controller : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;        // 透視投影モードでの有効視野の変化の速さ
    public float orthoZoomSpeed = 0.5f;        // 平行投影モードでの平行投影サイズの変化の速さ


    void Update()
    {
        // 端末に 2 つのタッチがあるならば...　
        if (Input.touchCount == 2)
        {
            // 両方のタッチを格納します
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // 各タッチの前フレームでの位置をもとめます
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 各フレームのタッチ間のベクター (距離) の大きさをもとめます
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 各フレーム間の距離の差をもとめます
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // カメラが平行投影ならば...　
            if (GetComponent<Camera>().orthographic)
            {
                // ... タッチ間の距離の変化に基づいて平行投影サイズを変更します
                Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // 平行投影サイズが決して 0 未満にならないように気を付けてください
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
            }
            else
            {
                // そうでない場合は、タッチ間の距離の変化に基づいて有効視野を変更します
                Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // 有効視野を 0 から 180 の間に固定するように気を付けてください
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}
