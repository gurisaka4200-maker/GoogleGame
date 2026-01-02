using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3.5f;
    public float gravity = -9.81f;
    public float rotationSmoothTime = 0.08f;

    [Header("References")]
    public VirtualJoystick joystick;
    public Transform cameraTransform;
    public Animator animator;
    [HideInInspector] public CharacterController controller;

    [Header("Attack")]
    public float attackCooldown = 0.8f;
    public Transform swordHitOrigin;

    private float verticalVelocity;
    private float turnSmoothVelocity;
    private float lastAttackTime = -99f;

    private void Reset()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        if (controller == null) controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleAttackInput();
    }

    void HandleMovement()
    {
        Vector2 input = joystick != null ? joystick.Direction : Vector2.zero;
        Vector3 direction = new Vector3(input.x, 0f, input.y);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + (cameraTransform ? cameraTransform.eulerAngles.y : 0f);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            float speed = walkSpeed;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            animator?.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
        }
        else
        {
            animator?.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
        }

        // gravity
        if (!controller.isGrounded)
            verticalVelocity += gravity * Time.deltaTime;
        else if (verticalVelocity < 0f)
            verticalVelocity = -1f;

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    void HandleAttackInput()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                Touch t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Began && t.position.x > Screen.width * 0.5f)
                {
                    Attack();
                    lastAttackTime = Time.time;
                    break;
                }
            }
        }
        else if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width * 0.5f)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack()
    {
        animator?.SetTrigger("Attack");
        if (swordHitOrigin != null)
        {
            var hitbox = swordHitOrigin.GetComponent<SwordHitbox>();
            if (hitbox != null) hitbox.TriggerHit();
        }
    }
}
