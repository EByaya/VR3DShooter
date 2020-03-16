using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurn : MonoBehaviour {

    // Use this for initialization
    private float camRayLenght = 1000f;
    private int floorMask;
    private Rigidbody mainCameraRigibody;

    private void Awake()
    {
        mainCameraRigibody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Environment");
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLenght, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            mainCameraRigibody.MoveRotation(newRotation);
        }
    }

    void FixedUpdate()
    {
        
        Turning();
        

    }
}
