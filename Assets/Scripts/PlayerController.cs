using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] CharacterController controller;

    [Header("Spec")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;

    private Vector3 moveDir;
    private float ySpeed;

    private void Update()
    {
        Move();
        JumpMove();
    }

    private void Move()
    {
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
    }

    private void JumpMove()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded)
        {
            ySpeed = 0;
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveDir.x = inputDir.x;
        moveDir.z = inputDir.y;
    }

    private void OnJump(InputValue value)
    {
        if (controller.isGrounded)
        {
            ySpeed = jumpSpeed;
        }
    }

    // 박스캐스트
    /*[Header("Box Cast")]
    [SerializeField] Vector3 boxSize;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask groundCheck;

    [Header("Gizmo")]
    [SerializeField] bool drawGizmo;

    private void OnDrawGizmos()
    {
        if (!drawGizmo)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    public bool IsGround()
    {
        return Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, groundCheck);
    }*/
}
