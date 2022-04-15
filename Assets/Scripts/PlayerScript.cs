using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //declaring variables
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

    //variables for melee combat hit detection
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public GameObject ui;

    void Start()
    {
        //initializing variables
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

    //locks the cursor so it isn't visible while game is in play
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

    //handles player movement using camera position and user inputs
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

    //handles player attacks
    void AttackingMethod()
    {
        Attacking = Input.GetKeyDown("mouse 0");
        playerAnimator.SetBool("IsAttacking", Attacking);

        if (Attacking)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

            foreach(Collider enemy in hitEnemies)
            {
                //calls public methods in other scripts to trigger their function
                enemy.GetComponent<GoblinScript>().Death();
                ui.GetComponent<UIScript>().ChangeScore();
            }
        }
    }
}
