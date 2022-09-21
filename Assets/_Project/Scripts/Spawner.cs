using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.icypeak.spawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] GameObject spawnableObstacle;
        [SerializeField] float spawnCooldown;
        [SerializeField] bool isActive;
        float _timer;

        public GameObject[] SpawnableBlocks;
        public float[] PossibleBlockPositions;
        List<int> _originalSpawnOrder;
        public int[] CurrentSpawnOrder;

        void Start()
        {
            _timer = spawnCooldown;
            _originalSpawnOrder = new List<int>();
            for (int i = 0; i < SpawnableBlocks.Length; i++)
            {
                _originalSpawnOrder.Add(i);
            }
            UpdateCurrentSeed();
        }

        void Update()
        {
            if (!isActive) return;

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _timer = spawnCooldown;
                Instantiate(spawnableObstacle, this.transform.position, Quaternion.identity, this.transform);
            }
        }

        public void UpdateCurrentSeed()
        {
            var rnd = new System.Random();

            CurrentSpawnOrder = _originalSpawnOrder.OrderBy(item => rnd.Next()).ToArray();
        }
    }
}
