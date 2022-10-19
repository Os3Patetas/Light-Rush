using System;
using UnityEngine;

namespace com.icypeak.managers
{
    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager Instance;
        bool repeatedObject = false;

        [SerializeField] float percentageDifficultyProgression;

        [SerializeField] float obstacleBaseSpeed;
        float obstacleSpeed;
        public Vector2 ObstacleVelocity => new Vector2(-obstacleSpeed, 0);

        [SerializeField] float spawnBaseCooldown;
        public float SpawnCooldown;

        public Action onDifficultyIncrease;

        void Awake()
        {
            if(Instance != null && Instance != this)
            {
                repeatedObject = true;
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            SpawnCooldown = spawnBaseCooldown;
            obstacleSpeed = obstacleBaseSpeed;
            print(obstacleBaseSpeed);
            print(spawnBaseCooldown);
        }

        void Update()
        {
            print(ObstacleVelocity);
            print(SpawnCooldown);    
        }

        void OnDestroy()
        {
            if(!repeatedObject)
            {
                Instance = null;
            }
        }

        public void IncreaseDifficulty()
        {
            obstacleSpeed *= 1 + (percentageDifficultyProgression / 100);
            SpawnCooldown = obstacleBaseSpeed * spawnBaseCooldown / obstacleSpeed;

            onDifficultyIncrease?.Invoke();
        }
    }
}
