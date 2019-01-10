using UnityEngine;
using System.Collections;
 
public class Camera_controller : MonoBehaviour {

  public float moveSpeed = 0.05f;
  
  private GameObject camera;
  private Vector2[] beforePoint;
  private Vector2[] nowPoint;
  private Vector2[] diffpoint;
  private float horizontalPosition;
  private float varticalPosition;
  void Start() { 
    camera = Camera.main.gameObject;
    beforePoint = new Vector2[2];
    nowPoint = new Vector2[2];
  }
 
  void Update () {
    if(Input.touchCount > 0){ 
      if(Input.GetTouch(1).phase == TouchPhase.Began){ //わからん
        beforePoint[0] = Input.GetTouch(0).position;
       beforePoint[1] = Input.GetTouch(1).position;
     }

      if (Input.GetTouch(1).phase == TouchPhase.Moved){
        nowPoint[0] = Input.GetTouch(0).position;
        nowPoint[1] = Input.GetTouch(1).position;

        if(nowPoint[0].x - beforePoint[0].x != 0){
          horizontalPosition = nowPoint[0].x - beforePoint[0].x;
          horizontalPosition *= moveSpeed * Time.deltaTime;
          Vector3 direction = Quaternion.Euler(0, camera.transform.localEulerAngles.y, 0) * new Vector3(horizontalPosition, 0, 0) * (-1); //よくわからん
          direction = transform.TransformDirection(direction); //わからん
          camera.transform.position = new Vector3(camera.transform.position.x + direction.x, camera.transform.position.y + direction.y, camera.transform.position.z + direction.z);
        }
        beforePoint[0] = nowPoint[0];
        beforePoint[1] = nowPoint[1];
      }
    }
  }

}