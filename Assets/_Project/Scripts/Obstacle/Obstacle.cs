using com.icypeak.managers;
using com.icypeak.spawner;
using UnityEngine;

namespace com.icypeak.obstacle
{
    public class Obstacle : MonoBehaviour
    {
        Rigidbody2D _rb;

        void Awake() =>
            _rb = GetComponent<Rigidbody2D>();

        void Start()
        {
            Spawner parentSpawner = this.GetComponentInParent<Spawner>();
            int aux = 0;
            foreach (var item in parentSpawner.CurrentSpawnOrder)
            {
                Instantiate(parentSpawner.SpawnableBlocks[item],
                    this.transform.position + new Vector3(0, parentSpawner.PossibleBlockPositions[aux], 0),
                    Quaternion.identity,
                    this.transform);
                aux++;
            }
            parentSpawner.UpdateCurrentSeed();

            _rb.velocity = DifficultyManager.Instance.ObstacleVelocity;
        }

        void Update()
        {
            if (transform.position.x <= -12)
            {
                Destroy(this.gameObject);
            }
        }

        void UpdateVelocity() =>
            _rb.velocity = DifficultyManager.Instance.ObstacleVelocity;

        void OnEnable()
        {
            if (DifficultyManager.Instance != null)
            {
                DifficultyManager.Instance.onDifficultyIncrease += UpdateVelocity;
            }
        }
        void OnDisable()
        {
            if (DifficultyManager.Instance != null)
            {
                DifficultyManager.Instance.onDifficultyIncrease -= UpdateVelocity;
            }
        }
    }
}
