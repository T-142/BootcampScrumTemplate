using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class PlayerMove : MonoBehaviour
    {
        public float speed = 6f;            // The speed that the player will move at.

        public float h;
        public float v;
        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
        public bool walking;
        bool isgrounded;
        public Vector3 jump;
        public float jumpForce = 2.0f;

        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.

        //DASH
        public float dashSpeed;
        private float dashTime;
        public float startDashTime;
        private Vector3 direction;

        void Awake()
        {

            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask("Floor");
            jump = Vector3.up;

            // Set up references.
            anim = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)&&isgrounded)
            {
                isgrounded = false;
                playerRigidbody.AddRelativeForce(jump*jumpForce, ForceMode.Impulse);
                anim.SetBool("Jump", true);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Run", true);
                speed = 10.0f;
            }
            else
            {
                anim.SetBool("Run", false);
                speed = 4.0f;
            }

            Turning();
        }
        void FixedUpdate()
        {
            
            // Store the input axes.
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            anim.SetFloat("Forward", v);
            anim.SetFloat("Turn", h);

            // Move the player around the scene.
            Move(h, v);

            // Turn the player to face the mouse cursor.
            //Turning();

            // Animate the player.
            Animating(h, v);
        }


        void Move(float h, float v)
        {
            // Set the movement vector based on the axis input.
            movement.Set(h, 0f, v);

            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition(transform.position + movement);


        }


        void Turning()
        {

            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;
                direction = playerToMouse;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);

                if (dashTime <= 0)
                {
                    dashTime = startDashTime;
                    playerRigidbody.velocity = Vector3.zero;
                    if (Input.GetKey(KeyCode.F))
                    {
                        playerRigidbody.velocity = direction * dashSpeed;
                    }
                }
                else
                {
                    dashTime -= Time.deltaTime;
                }

            }
        }


        void Animating(float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.

            if (h != 0 || v != 0)
            {
                walking = true;
            }
            else { walking = false; }

            // Tell the animator whether or not the player is walking.

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                anim.SetBool("Jump", false);
                isgrounded = true;
            }
        }
    }
}
