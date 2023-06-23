using UnityEngine;
using UnityEngine.InputSystem;

namespace VRJam23
{
    public class PlayerInput : MonoBehaviour
    {
        private XRIDefaultInputActions pr_InputActions;

        [SerializeField] private Grabber s_LeftHandGrab;
        [SerializeField] private Grabber s_RightHandGrab;

        private void OnEnable()
        {
            pr_InputActions = new XRIDefaultInputActions();

            pr_InputActions.XRILeftHandInteraction.Select.started += LeftGrab;
            pr_InputActions.XRILeftHandInteraction.Select.canceled += LeftRelease;
            pr_InputActions.XRILeftHandInteraction.Select.Enable();

            pr_InputActions.XRIRightHandInteraction.Select.started += RightGrab;
            pr_InputActions.XRIRightHandInteraction.Select.canceled += RightRelease;
            pr_InputActions.XRIRightHandInteraction.Select.Enable();
        }

        private void LeftGrab(InputAction.CallbackContext obj)
        {
            s_LeftHandGrab.GrabObject();
        }

        private void LeftRelease(InputAction.CallbackContext obj)
        {
            s_LeftHandGrab.ReleaseObject();
        }

        private void RightGrab(InputAction.CallbackContext obj)
        {
            s_RightHandGrab.GrabObject();
        }

        private void RightRelease(InputAction.CallbackContext obj)
        {
            s_RightHandGrab.ReleaseObject();
        }
    }
}
