using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private Transform u_TransformSpawn;

        private List<GameObject> pr_SpawnStack;
        private AudioSource u_AudioSource;

        private int pr_MillisecondDelayLower = 400;
        private int pr_MillisecondDelayUpper = 1100;
        private int pr_MillisecondDelay;

        private int pr_RandomIndex;

        private Vector3 pr_SpawnPosition;
        private Quaternion pr_SpawnRotation;

        private Rigidbody u_Rigidbody;
        private float pr_SpawnForceLower = 1f;
        private float pr_SpawnForceUpper = 2.3f;
        private float pr_SpawnForce;

        private bool pr_SpawnLoopRun = false;

        private void OnEnable()
        {
            GameEvents.GameStart += GameStart;
            GameEvents.GameEnded += GameEnded;
        }

        private void OnDisable()
        {
            GameEvents.GameStart -= GameStart;
            GameEvents.GameEnded -= GameEnded;
        }

        private void GameStart()
        {
            u_AudioSource = u_TransformSpawn.GetComponent<AudioSource>();
            pr_SpawnStack = new List<GameObject>();
            pr_SpawnLoopRun = true;
            SpawningLoop();
        }

        private void GameEnded()
        {
            pr_SpawnLoopRun = false;
            pr_SpawnStack.Clear();
        }

        public void AddPrefabToSpawnStack(GameObject pa_ObjectPrefab)
        {
            if (pa_ObjectPrefab == null) return;

            bool l_Add = Random.Range(0f, 1f) > 0.5f;
            if (l_Add)
            {
                pr_SpawnStack.Add(pa_ObjectPrefab);
            }
        }

        private async void SpawningLoop()
        {
            pr_MillisecondDelay = Random.Range(pr_MillisecondDelayLower, pr_MillisecondDelayUpper);
            while (pr_SpawnLoopRun)
            {
                await Task.Delay(pr_MillisecondDelay);

                while (pr_SpawnStack.Count > 4)
                {
                    Spawn();
                }

                if (pr_SpawnStack.Count > 0)
                {
                    Spawn();
                }

                pr_MillisecondDelay = Random.Range(pr_MillisecondDelayLower, pr_MillisecondDelayUpper);
            }
        }

        private void Spawn()
        {
            pr_RandomIndex = Random.Range(0, pr_SpawnStack.Count);
            pr_SpawnPosition = u_TransformSpawn.position;
            u_AudioSource.Play();

            GameObject l_TempObject = Instantiate(pr_SpawnStack[pr_RandomIndex], pr_SpawnPosition, pr_SpawnRotation);
            pr_SpawnStack.RemoveAt(pr_RandomIndex);

            u_Rigidbody = l_TempObject.GetComponent<Rigidbody>();

            pr_SpawnForce = Random.Range(pr_SpawnForceLower, pr_SpawnForceUpper);
            u_Rigidbody.velocity = Vector3.forward * pr_SpawnForce;
        }
    }
}
