using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement
{
  public class PlayerMovement : MonoBehaviour
  {
    public CharacterController controller;
    
    public float speed = 12f;
    public float jumpHeight = 1f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
   

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
      isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
      print(isGrounded);
      if (isGrounded && velocity.y < 0)
      {
        velocity.y = -2f;
      }

      float x = Input.GetAxis("Horizontal");
      float z = Input.GetAxis("Vertical");

      Vector3 move = transform.right * x + transform.forward * z;
      controller.Move(move * speed * Time.deltaTime);

      if (Input.GetButtonDown("Jump") && isGrounded)
      {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
      }

      velocity.y += gravity * Time.deltaTime;
      controller.Move(velocity * Time.deltaTime);

      UpdateAnimator();
    }

    private void UpdateAnimator()
    {
      Vector3 velocity = controller.velocity;
      Vector3 localVelocity = transform.InverseTransformDirection(velocity);
      float speed = localVelocity.x;
      GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    private void JumpBehaviour()
    {

    }
  }
}
