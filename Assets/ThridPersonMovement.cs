using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThridPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float gravity;
    public float currentGravity;
    public float constantGravity;
    public float maxGravity;

    private Vector3 grivtyDirection = Vector3.down;
    private Vector3 gravtyMovement;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    void Update()
    {
        CalculateGravity();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime + gravtyMovement);
        }
    }

    private bool IsGrounded()
    {
        return controller.isGrounded;
    }

    private void CalculateGravity()
    {
        if (IsGrounded())
        {
            currentGravity = constantGravity;
        }else
        {
            if(gravity > maxGravity)
            {
                currentGravity -= gravity * Time.deltaTime;
            }
        }

        gravtyMovement = grivtyDirection * -currentGravity;
    }
}
