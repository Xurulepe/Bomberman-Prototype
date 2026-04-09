using UnityEngine;

namespace Game.Enemy.AI
{
    public class SlimeAI : MonoBehaviour
    {
        private Slime controller;
        private IdleBehaviour idleBehaviour;
        private PatrolBehaviour patrolBehaviour;

        private void Awake()
        {
            controller = GetComponent<Slime>();
            idleBehaviour = GetComponent<IdleBehaviour>();
            patrolBehaviour = GetComponent<PatrolBehaviour>();
        }

        private void Start()
        {
            idleBehaviour.OnIdleComplete += HandleIdleCompleted;
            patrolBehaviour.OnWallColision += HandleWallColision;
        }

        private void Update()
        {
            switch (controller.CurrentState)
            {
                case EnemyState.Idle:
                    idleBehaviour.enabled = true;
                    patrolBehaviour.enabled = false;

                    break;

                case EnemyState.Patroling:
                    patrolBehaviour.enabled = true;
                    idleBehaviour.enabled = false;

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
    }
}
