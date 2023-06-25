using UnityEngine;

namespace VRJam23
{
    public class PlaneMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody u_Rigidbody;

        private bool pr_Moving = false;
        private float pr_BaseMovementSpeed = 0.05f;
        private float pr_DifficultyScalar = 1f;
        private Vector3 pr_NewPosition;

        private void OnEnable()
        {
            GameEvents.GameStart += GameStart;
            GameEvents.GameEnded += GameEnded;
            GameEvents.DifficultyScalarChange += DifficultyScalarChange;
        }

        private void OnDisable()
        {
            GameEvents.GameStart -= GameStart;
            GameEvents.GameEnded -= GameEnded;
            GameEvents.DifficultyScalarChange -= DifficultyScalarChange;
        }

        private void GameStart()
        {
            pr_Moving = true;
        }

        private void GameEnded()
        {
            pr_Moving = false;
        }

        public void StartMoving()
        {
            pr_Moving = true;
        }

        private void DifficultyScalarChange(float pa_NewDifficultyScalar)
        {
            pr_DifficultyScalar = pa_NewDifficultyScalar;
        }

        private void FixedUpdate()
        {
            if (!pr_Moving) return;

            pr_NewPosition = transform.position;
            pr_NewPosition.z += pr_BaseMovementSpeed * pr_DifficultyScalar;
            u_Rigidbody.MovePosition(pr_NewPosition);
        }
    }
}
