using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRJam23
{
    public class DriverAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource u_AudioSource;

        private List<AudioClip> pr_MandatoryClipQueue;

        private void Start()
        {
            u_AudioSource.clip = null;
            pr_MandatoryClipQueue = new List<AudioClip>();
        }

        private IEnumerator WaitForSound()
        {
            yield return new WaitUntil(() => u_AudioSource.isPlaying == false);
            u_AudioSource.clip = null;
        }

        private void PlaySounds()
        {
            u_AudioSource.Play();
            StartCoroutine(WaitForSound());

            while (pr_MandatoryClipQueue.Count > 0)
            {
                u_AudioSource.clip = pr_MandatoryClipQueue[0];
                u_AudioSource.Play();
                StartCoroutine(WaitForSound());
                pr_MandatoryClipQueue.RemoveAt(0);
            }
        }

        public void AddMandatoryCip(AudioClip pa_NewMandatoryAudio)
        {
            if (u_AudioSource.clip == null)
            {
                u_AudioSource.clip = pa_NewMandatoryAudio;
                PlaySounds();
            }
            else
            {
                pr_MandatoryClipQueue.Add(pa_NewMandatoryAudio);
            } 
        }

        public void Quip(AudioClip pa_Quip)
        {
            if (u_AudioSource.clip != null) return;

            u_AudioSource.clip = pa_Quip;
            PlaySounds();
        }

        public bool Quipable()
        {
            return u_AudioSource.clip == null && pr_MandatoryClipQueue.Count == 0;
        }
    }
}
