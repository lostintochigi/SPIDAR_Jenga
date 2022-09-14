using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 0.1f;
    public Vector2 panLimit;
    public float ScrollSpeed = 20f;
    public float rotY;
    public Quaternion localRot;

    //Update is called once per frame
    void Update() {

        Vector3 pos = transform.position;

        if (Input.GetKey("w") )//|| Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
            //transform.forward = transform.forward * panSpeed* Time.deltaTime;
        }

        if (Input.GetKey("s") )//|| Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -=panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a") )//|| Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            float dist = Mathf.Sqrt(pos.z * pos.z + pos.x * pos.x);
            rotY -= panSpeed*Time.deltaTime;
            pos.z = -dist * Mathf.Cos(Mathf.Deg2Rad*rotY);
            pos.x = -dist * Mathf.Sin(Mathf.Deg2Rad*rotY);
            localRot = Quaternion.Euler(0f, rotY, 0f);
            //pos.x -=panSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey("d") )//|| Input.mousePosition.x <= panBorderThickness)
        {
            pos.x +=panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rotY -= panSpeed*Time.deltaTime;
            localRot = Quaternion.Euler(0f, rotY, 0f);
            //transform.rotation = localRot;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotY += panSpeed*Time.deltaTime;
            localRot = Quaternion.Euler(0f, rotY, 0f);
            //transform.rotation = localRot;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += scroll*ScrollSpeed*1000f*Time.deltaTime;


        //pos.x=Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        //pos.z=Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
        pos.y=Mathf.Clamp(pos.y, 1.569532f, 25.87351f);
        transform.position =pos;
        transform.rotation = localRot;
    }
}
