using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bird : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private static readonly int YVelocity = Animator.StringToHash("YVelocity");
    private static readonly int AnimationJump = Animator.StringToHash("Jump");
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int JumpVariety = Animator.StringToHash("JumpVariety");

    private int jumpVariety;

    public static event Action<int> OnCenterTrigger;
    public static event Action OnPipeCollision;
    public static event Action OnFirstJump;
    public static event Action OnJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.IsPlayerDead) return;

        animator.SetFloat(YVelocity, rb.velocity.y);
#if UNITY_STANDALONE || UNITY_EDITOR
        PcControl();
#elif UNITY_ANDROID
        MobileControl();
#endif
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PipeCenter) && other.transform.parent.TryGetComponent(out Pipe pipe))
        {
            OnCenterTrigger?.Invoke(pipe.ScoreAmount);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.CompareTag(Tags.Pipe) && !other.collider.CompareTag(Tags.LevelBorder)) return;

        animator.SetTrigger(Death);

        OnPipeCollision?.Invoke();
    }

    private void PcControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            JumpLogic();
        }
    }

    private void MobileControl()
    {
        if (Input.touchCount <= 0) return;

        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            JumpLogic();
        }
    }

    private void JumpLogic()
    {
        if (!GameManager.IsGameStarted)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            OnFirstJump?.Invoke();
        }

        jumpVariety = Random.Range(1, 3);
        animator.SetInteger(JumpVariety, jumpVariety);
        
        animator.SetTrigger(AnimationJump);

        Jump();

        OnJump?.Invoke();
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }
}
