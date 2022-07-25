using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private CharacterController controller;

    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1;
    public float laneDistance = 4;

    public float jumpForce;
    public float Gravity = -20;

    public float maxSpeed = 50;
    public float slowSpeed = 15;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!PlayerManager.isGameStarted)
            return;
        //napred
        controller.Move(direction * Time.deltaTime);

        if (forwardSpeed < maxSpeed || forwardSpeed > 15)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }
        else if (forwardSpeed < 15)
        {
            forwardSpeed += 2.1f * Time.deltaTime;
        }

        direction.z = forwardSpeed;



        //desno
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        //levo

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        //pokupi poziciju
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;


        //pomeri
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        } else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);
        controller.center = controller.center;

        //skakanje
        if (controller.isGrounded)
        {
            direction.y = -1;
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            //gravitacija
            direction.y += Gravity * Time.deltaTime;
        }

        

    }


    private void Jump()
    {
        direction.y = jumpForce;

    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }


        if (hit.transform.tag == "Prepreka")
        {
            forwardSpeed = 15;

            
        }
    }


}
