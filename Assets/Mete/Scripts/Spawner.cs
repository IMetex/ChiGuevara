using System.Collections;
using UnityEngine;

namespace Mete.Scripts
{
    public class Spawner : MonoBehaviour
    {
        public GameObject[] spawnableObjects; 
        public float initialSpawnTime = 3f; 
        public float minSpawnTime = 0.5f;
        public float spawnTimeDecreaseRate = 0.1f; 

        private void Start()
        {
            StartCoroutine(SpawnObjects());
        }

        IEnumerator SpawnObjects()
        {
            WaitForSeconds wait = new WaitForSeconds(initialSpawnTime);

            while (true)
            {
                yield return wait;

                // Spawn random object
                SpawnRandomObject();

                // Decrease spawn time
                initialSpawnTime = Mathf.Max(minSpawnTime, initialSpawnTime - spawnTimeDecreaseRate);
                wait = new WaitForSeconds(initialSpawnTime);
            }
        }

        void SpawnRandomObject()
        {
            GameObject objectToSpawn = spawnableObjects[Random.Range(0, spawnableObjects.Length)];
            
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        }
    }
}