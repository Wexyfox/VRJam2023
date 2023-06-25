using UnityEngine;

namespace VRJam23
{
    public class PlaneMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody u_Rigidbody;

        private bool pr_Moving = false;
        private float pr_MovementDistance = 0.05f;
        private Vector3 pr_NewPosition;

        private void OnEnable()
        {
            GameEvents.GameStart += GameStart;
            GameEvents.GameEnded += GameEnded;
            GameEvents.SpeedChange += SpeedChange;
        }

        private void OnDisable()
        {
            GameEvents.GameStart -= GameStart;
            GameEvents.GameEnded -= GameEnded;
            GameEvents.SpeedChange -= SpeedChange;
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

        private void SpeedChange(float pa_NewSpeed)
        {
            pr_MovementDistance = pa_NewSpeed;
        }

        private void FixedUpdate()
        {
            if (!pr_Moving) return;

            pr_NewPosition = transform.position;
            pr_NewPosition.z += pr_MovementDistance;
            u_Rigidbody.MovePosition(pr_NewPosition);
        }
    }
}
