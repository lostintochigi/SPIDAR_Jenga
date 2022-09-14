using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 0.1f;
    public Vector2 panLimit;
    public float ScrollSpeed = 20f;
    public float rotY;
    public Quaternion localRot;

    public float rot_speed;
    public float invert=1f;
    void Start(){
        invert = PlayerPrefs.GetFloat("inv_val");
    }

    //Update is called once per frame
    void Update() {

        Vector3 pos = transform.position;

        if (Input.GetKey("w") )//|| Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.position += transform.forward * panSpeed* Time.deltaTime; //transfor.forward takes rotation into account while moving
        }

        if (Input.GetKey("s") )//|| Input.mousePosition.y <= panBorderThickness)
        {
            transform.position -= transform.forward * panSpeed* Time.deltaTime; //There is no transform.back hence - sign to reverse transform.forward
        }
        
        if (Input.GetKey("d") )//|| Input.mousePosition.x <= panBorderThickness)
        {
            transform.position += transform.right * panSpeed* Time.deltaTime;   //transform.right takes rotation into account while moving
        }

        if (Input.GetKey("a") )//|| Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.position -= transform.right * panSpeed* Time.deltaTime;   //There is no transform.right hence - sign to reverse transform.right
        }

        if(Input.GetKey("mouse 2"))
        {
            float rot_speed = -Input.GetAxis("Mouse X");
            rotY += invert*rot_speed*2000f*Time.deltaTime;
            localRot = Quaternion.Euler(0f, rotY, 0f);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            float dist = Mathf.Sqrt(pos.z * pos.z + pos.x * pos.x);
            rotY -= panSpeed*Time.deltaTime;
            // 2022-01-06 
            pos.z = -dist * Mathf.Cos(Mathf.Deg2Rad*rotY);
            pos.x = -dist * Mathf.Sin(Mathf.Deg2Rad*rotY);
            transform.position = pos;
            // 2022-01-06 end
            localRot = Quaternion.Euler(0f, rotY, 0f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            float dist = Mathf.Sqrt(pos.z * pos.z + pos.x * pos.x);
            rotY += panSpeed*Time.deltaTime;
            // 2022-01-06 
            pos.z = -dist * Mathf.Cos(Mathf.Deg2Rad*rotY);
            pos.x = -dist * Mathf.Sin(Mathf.Deg2Rad*rotY);
            transform.position = pos;
            // 2022-01-06 end
            localRot = Quaternion.Euler(0f, rotY, 0f);
            Debug.Log((transform.position).ToString());
        }
        if(Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            pos.y += scroll*ScrollSpeed*100f*Time.deltaTime;
            pos.y=Mathf.Clamp(pos.y, 1.569532f, 25.87351f);
            transform.position = pos;    
            Debug.Log((transform.position).ToString());
        }
        if(Input.GetKey("mouse 1"))
        {
            float Hz_speed = -Input.GetAxis("Mouse Y");
            transform.position += Hz_speed*transform.forward * 2000f * Time.deltaTime;
        }
        //pos.x=Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        //pos.z=Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
        
        transform.rotation = localRot;
        
    }
}
