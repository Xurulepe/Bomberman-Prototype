using System;
using UnityEngine;

namespace Game.Enemy.AI
{
    public class IdleBehaviour : MonoBehaviour
    {
        private float idleTime;
        private float idleTimer;
        private float idleInterval;
        private float nextIdleTime;

        public bool CanStartIdle
        {
            get { return Time.time >= nextIdleTime; }
        }

        public event Action OnIdleComplete;

        private void OnEnable()
        {
            idleTimer = 0f;
        }

        public void SetIdleSettings(float idleTime, float idleInterval)
        {
            this.idleTime = idleTime;
            this.idleInterval = idleInterval;
        }

        private void Update()
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleTime)
            {
                OnIdleComplete?.Invoke();

                nextIdleTime = Time.time + idleInterval;

                enabled = false;
            }
        }
    }
}
