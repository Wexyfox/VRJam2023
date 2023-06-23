using UnityEngine;
using UnityEngine.Events;

namespace VRJam23
{
    public class VRTriggerBasedButton : MonoBehaviour
    {
        [SerializeField] private GameObject u_ButtonPress;
        public UnityEvent Pressed;
        public UnityEvent Released;

        private AudioSource soundEffect;
        private bool isPressed = false;

        void Start()
        {
            soundEffect = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider pa_Other)
        {
            if (isPressed) return;
            if (!pa_Other.CompareTag("Player")) return;

            u_ButtonPress.transform.localPosition = new Vector3(0, 0.004f, 0);
            Pressed.Invoke();
            soundEffect.Play();
            isPressed = true;
        }

        private void OnTriggerExit(Collider pa_Other)
        {
            if (!pa_Other.CompareTag("Player")) return;

            u_ButtonPress.transform.localPosition = new Vector3(0, 0.013f, 0);
            Released.Invoke();
            isPressed = false;
        }
    }
}
