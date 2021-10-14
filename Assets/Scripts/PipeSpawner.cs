using UnityEngine;
using Random = UnityEngine.Random;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private Pipe pipePrefab;

    [SerializeField] private float spawnTime;
    [SerializeField] private float yOffset;

    private Vector2 position;
    private float spawnTimer;

    private void Awake()
    {
        position = transform.position;
    }

    private void Update()
    {
        if (GameManager.IsPlayerDead || !GameManager.IsGameStarted) return;

        if (spawnTimer >= spawnTime)
        {
            Spawn();
            spawnTimer = 0;
        }

        spawnTimer += Time.deltaTime;
    }

    private void Spawn()
    {
        Instantiate(pipePrefab, GetPosition(), Quaternion.identity, transform);
    }

    private Vector2 GetPosition()
    {
        return new Vector2(position.x, position.y + Random.Range(-yOffset, yOffset));
    }
}
