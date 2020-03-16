using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;
    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigibody;
    private float camRayLenght = 10000f;
    private int floorMask;
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigibody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
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

            playerRigibody.MoveRotation(newRotation);
        }
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigibody.MovePosition(transform.position + transform.TransformDirection(movement));
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0 || v != 0;
        anim.SetBool("isWalking", walking);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        //Turning();
        Animating(h, v);

    }
}
