using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;

    [Header("Spec")]
    [SerializeField] float moveSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpSpeed;

    [Header("Test")]
    [SerializeField] MultiAimConstraint aim;
    [SerializeField] float rotateSpeed;

    private Vector3 moveDir;
    private float ySpeed;
    private bool isWalk;

    private void Update()
    {
        Move();
        JumpMove();
    }

    private void Move()
    {
        if (isWalk)
        {
            controller.Move(transform.right * moveDir.x * walkSpeed * Time.deltaTime);
            controller.Move(transform.forward * moveDir.z * walkSpeed * Time.deltaTime);
            animator.SetFloat("XSpeed", moveDir.x * walkSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("YSpeed", moveDir.z * walkSpeed, 0.1f, Time.deltaTime);
        }
        else
        {
            controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
            controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
            animator.SetFloat("XSpeed", moveDir.x * moveSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat("YSpeed", moveDir.z * moveSpeed, 0.1f, Time.deltaTime);
        }
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

    public void Fire()
    {
        // 총쏘는 로직 구현
        animator.SetTrigger("Fire");
    } 

    public void Reload()
    {
        // TODO : 재장전 구현
        animator.SetTrigger("Reload");
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

    private void OnWalk(InputValue value)
    {
        if (value.isPressed)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
    }

    private void OnFire(InputValue value)
    {
        Fire();

        // 테스트용
        StopAllCoroutines();
        StartCoroutine(Draw(-rotateSpeed * Time.deltaTime));
    }

    private void OnReload(InputValue value)
    {
        Reload();

        // 테스트용
        StopAllCoroutines();
        StartCoroutine(Draw(rotateSpeed * Time.deltaTime));
    }

    // 테스트용
    IEnumerator Draw(float time)
    {
        while (aim.weight > 0.01f && aim.weight < 0.99f)
        {
            aim.weight += time;
            yield return new WaitForSeconds(Time.deltaTime);
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
