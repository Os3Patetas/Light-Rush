using com.icypeak.spawner;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.icypeak.obstacle
{
    public class Obstacle : MonoBehaviour
    {
        Rigidbody2D _rb;

        [SerializeField] float _baseSpeed;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

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

            _rb.velocity = Vector2.left * _baseSpeed;
        }

        void Update()
        {
            if(transform.position.x <= -12)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
