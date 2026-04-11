using System;
using UnityEngine;

namespace Game.Enemy.AI
{
    public class BalloonAI : MonoBehaviour
    {
        private Balloon controller;
        private IdleBehaviour idleBehaviour;
        private PatrolBehaviour patrolBehaviour;
        private ChaseBehaviour chaseBehaviour;

        private void Awake()
        {
            controller = GetComponent<Balloon>();
            idleBehaviour = GetComponent<IdleBehaviour>();
            patrolBehaviour = GetComponent<PatrolBehaviour>();
            chaseBehaviour = GetComponent<ChaseBehaviour>();
        }

        private void Start()
        {
            idleBehaviour.OnIdleComplete += HandleIdleCompleted;
            patrolBehaviour.OnWallColision += HandleWallColision;
            patrolBehaviour.OnPlayerDetected += HandlePlayerDetected;
            chaseBehaviour.OnTargetLost += HandleTargetLost;

            controller.ChangeState(EnemyState.Idle);
        }

        private void Update()
        {
            switch (controller.CurrentState)
            {
                case EnemyState.Idle:
                    idleBehaviour.enabled = true;
                    patrolBehaviour.enabled = false;
                    chaseBehaviour.enabled = false;

                    break;

                case EnemyState.Patroling:
                    patrolBehaviour.enabled = true;
                    idleBehaviour.enabled = false;
                    chaseBehaviour.enabled = false;

                    break;

                case EnemyState.Chasing:
                    chaseBehaviour.enabled = true;
                    idleBehaviour.enabled = false;
                    patrolBehaviour.enabled = false;

                    break;
            }
        }

        private void HandleIdleCompleted()
        {
            controller.ChangeState(EnemyState.Patroling);
        }

        private void HandleWallColision()
        {
            if (idleBehaviour.CanStartIdle)
            {
                controller.ChangeState(EnemyState.Idle);
            }
        }

        private void HandlePlayerDetected()
        {
            controller.ChangeState(EnemyState.Chasing);
        }

        private void HandleTargetLost()
        {
            controller.ChangeState(EnemyState.Patroling);
        }
    }
}
