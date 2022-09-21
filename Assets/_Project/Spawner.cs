using UnityEngine;

namespace com.icypeak.spawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] GameObject obstaclePrefab;
        [SerializeField] float spawnCooldown;
        [SerializeField] bool isActive;
        float _timer;

        void Start()
        {
            _timer = spawnCooldown;
        }

        void Update()
        {
            if (!isActive) return;

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _timer = spawnCooldown;
                Instantiate(obstaclePrefab, this.transform.position, Quaternion.identity);
            }
        }
    }
}
