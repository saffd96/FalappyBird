using System;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float force = 2f;
    [SerializeField] private int scoreAmount = 1;

    public int ScoreAmount => scoreAmount;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        AddVelocity();
    }

    private void Update()
    {
        CheckPlayerDeath();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PipeDestroyer))
        {
            Destroy(gameObject);
        }
    }

    private void CheckPlayerDeath()
    {
        if (GameManager.IsPlayerDead && rb.velocity != Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void AddVelocity()
    {
        rb.velocity = Vector2.left * force;
    }
}
