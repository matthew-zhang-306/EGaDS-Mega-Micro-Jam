using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NOAH_TEST_NAME_SPACE{
    public class Noah_ThingSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] spawnpoints;
        [SerializeField] private GameObject thing;
        private float elapsedTime;
        [SerializeField] private float spawnFreq;

        void Start(){
            elapsedTime = 0;
            foreach(GameObject obj in spawnpoints){
                Instantiate(thing, obj.transform);
            }
        }

        // Update is called once per frame
        void Update()
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= spawnFreq){
                elapsedTime = 0f;
                Instantiate(thing, spawnpoints[Random.Range(0,spawnpoints.Length)].transform);
            }
        }
    }
}
