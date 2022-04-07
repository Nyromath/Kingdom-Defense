using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    CharacterController ThirdPersonController;
    Animator playerAnimator;

    float CharSpeed = 7.5f;
    float HoriMove;
    float VertMove;
    float TurnSmoothVelocity;
    float TurnSmoothTime = 0.1f;
    bool Attacking;
    [SerializeField] Transform cam;
    bool lockCursor = true;

    void Start()
    {
        ThirdPersonController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementMethod();
        AttackingMethod();
        MouseState();
    }

    private void MouseState()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    void MovementMethod()
    {
        HoriMove = Input.GetAxis("Horizontal");
        VertMove = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(HoriMove, 0, VertMove).normalized;

        if (direction.magnitude > 0)
        {
            float TargetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float SmoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0, SmoothAngle, 0);
            Vector3 MoveDir = Quaternion.Euler(0f, SmoothAngle, 0f) * Vector3.forward;
            ThirdPersonController.Move(MoveDir * Time.deltaTime * CharSpeed);
            playerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            playerAnimator.SetBool("IsMoving", false);
        }
    }

    void AttackingMethod()
    {
        Attacking = Input.GetKeyDown("mouse 0");
        playerAnimator.SetBool("IsAttacking", Attacking);
    }
}
