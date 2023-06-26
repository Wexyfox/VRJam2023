using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class EnemyAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource pr_AudioSource;
        [SerializeField] private AudioClip[] pr_EnemyAudioClips;

        [Header("Delay")]
        [SerializeField] private int pr_EnemyAudioDelayLower;
        [SerializeField] private int pr_EnemyAudioDelayUpper;
        [SerializeField] private int pr_EnemyAudioDelay;

        private bool pr_AudioLoopActive;
        private int pr_RandomInt;

        private void OnEnable()
        {
            GameEvents.GameEnded += GameEnded;
        }

        private void OnDisable()
        {
            GameEvents.GameEnded -= GameEnded;
        }

        private void GameEnded()
        {
            LoopStop();
        }

        private void Start()
        {
            pr_AudioLoopActive = true;
            AudioLoop();
        }

        private async void AudioLoop()
        {
            while (pr_AudioLoopActive)
            {
                pr_EnemyAudioDelay = Random.Range(pr_EnemyAudioDelayLower, pr_EnemyAudioDelayUpper);
                await Task.Delay(pr_EnemyAudioDelay);

                if (!pr_AudioLoopActive)
                {
                    return;
                }

                pr_RandomInt = Random.Range(0, pr_EnemyAudioClips.Length);
                pr_AudioSource.clip = pr_EnemyAudioClips[pr_RandomInt];
                pr_AudioSource.Play();
            }
        }

        public void LoopStop()
        {
            pr_AudioLoopActive = false;
        }
    }
}
