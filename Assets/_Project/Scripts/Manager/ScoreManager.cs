using System;
using UnityEngine;

namespace com.icypeak.managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        bool repeatedObject = false;

        float _score;
        public float Score
        {
            get => _score;
            set
            {
                _score = value;
                OnScoreChange?.Invoke();
            }
        }
        public Action OnScoreChange;

        void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        void Start() => _score = 0;
        void OnDestroy()
        {
            if(!repeatedObject)
            {
                Instance = null;
            }
        }
    }
}
