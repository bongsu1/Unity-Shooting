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
    [SerializeField] WeaponHolder weaponHolder;

    [Header("Spec")]
    [SerializeField] float moveSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpSpeed;

    [Header("Test")]
    [SerializeField] MultiAimConstraint aim;
    [SerializeField] MultiAimConstraint hand;
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
        weaponHolder.Fire();
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

    // 테스트용
    /*enum State { Fire, Reload }
    State state = State.Fire;*/

    Coroutine keep;
    private void OnFire(InputValue value)
    {
        // 테스트용
        /*if (state == State.Fire)
            return;*/
        
        if (value.isPressed)
        {
            keep = StartCoroutine(KeepPush());
        }
        else
        {
            StopCoroutine(keep);
        }

        // 테스트용
        /*state = State.Fire;
        hand.weight = 0;
        StopAllCoroutines();
        StartCoroutine(Draw(-rotateSpeed, state));*/
    }

    IEnumerator KeepPush()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnReload(InputValue value)
    {
        // 테스트용
        /*if (state == State.Reload)
            return;*/

        Reload();

        // 테스트용
        /*state = State.Reload;
        hand.weight = 1;
        StopAllCoroutines();
        StartCoroutine(Draw(rotateSpeed, state));*/
    }

    // 테스트용
    /*IEnumerator Draw(float rotate, State state)
    {
        if (state == State.Fire)
            yield return new WaitForSeconds(1f);

        while (true)
        {
            aim.weight += rotate;
            if (aim.weight < 0.01f || aim.weight > 0.99f)
                break;

            yield return new WaitForEndOfFrame();
        }
    }*/
}
