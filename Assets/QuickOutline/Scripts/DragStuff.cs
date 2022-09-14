//Taken from https://sharpcoderblog.com/blog/drag-rigidbody-with-mouse-cursor-unity-3d-tutorial

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using System;

public class DragStuff : MonoBehaviour
{
    public float forceAmount = 500;
    public DoSomething dosomething;

    Rigidbody selectedRigidbody;
    Rigidbody selectedRigidbody1;
    Camera targetCamera;
    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;
    public float panSpeed = 200f;
    public float right_mouse;

    public float Motor_power;

    public float right_mouse2; //Inverted value of right_mouse stores 1f if right_mouse is 0f and vice-versa.

    Vector3 vel_copy;
    

    // Start is called before the first frame update
    void Start()
    {
        targetCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (!targetCamera)
            return;
        
        right_mouse = Input.GetKey("mouse 1")? 0F:1F;
        right_mouse2 = 1F - right_mouse;


        if (Input.GetMouseButtonDown(0))
        {
            //Check if we are hovering over Rigidbody, if so, select it
            selectedRigidbody = GetRigidbodyFromMouseClick();
        }

        if (Input.GetMouseButtonUp(0) && selectedRigidbody)
        {
            //Release selected Rigidbody if there any
            selectedRigidbody = null;
        }
    }

    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            
            Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
            selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
            //selectedRigidbody.velocity = new Vector3((((originalRigidbodyPos.x + mousePositionOffset.x - selectedRigidbody.transform.position.x) * forceAmount * Time.deltaTime * right_mouse)), ((originalRigidbodyPos.y + mousePositionOffset.y - selectedRigidbody.transform.position.y) * forceAmount * Time.deltaTime)* right_mouse, ((originalRigidbodyPos.z + mousePositionOffset.z - selectedRigidbody.transform.position.z) * Time.deltaTime * right_mouse2));
            //selectedRigidbody.velocity = new Vector3((((originalRigidbodyPos.x + mousePositionOffset.x - selectedRigidbody.transform.position.x) * forceAmount * Time.deltaTime * right_mouse)), ((originalRigidbodyPos.y + mousePositionOffset.y - selectedRigidbody.transform.position.y) * forceAmount * Time.deltaTime)* right_mouse);
            vel_copy = selectedRigidbody.velocity;
            vel_copy.x = vel_copy.x*right_mouse;
            vel_copy.y = vel_copy.y*right_mouse;
            selectedRigidbody.velocity = vel_copy;
            Motor_power = Mathf.Abs(Mathf.Round((vel_copy.x)*Time.deltaTime*100f));
            
            if(vel_copy.x<0){
                dosomething.DataSend("motor 0 " + (Motor_power.ToString()) +" "+ (Motor_power.ToString()) + " 0\n");
                //Debug.Log("motor 0 " + (Motor_power.ToString()) +" "+ (Motor_power.ToString()) + " 0\n");
            }
            if(vel_copy.x>0)
            {
                dosomething.DataSend("motor " + (Motor_power.ToString()) +" 0 0 "+ (Motor_power.ToString()) + "\n");
                //Debug.Log("motor " + (Motor_power.ToString()) +" 0 0 "+ (Motor_power.ToString()) + "\n");
            }               
        }
    }

    Rigidbody GetRigidbodyFromMouseClick()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                originalRigidbodyPos = hitInfo.collider.transform.position;
                return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            }
        }
        return null;
    }
}