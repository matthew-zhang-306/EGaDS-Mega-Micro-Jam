using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NOAH_TEST_NAME_SPACE{
    public class Noah_ThingToAvoid : MonoBehaviour
    {
        private Vector2 movement;
        private float elapsedTime;

        void Start()
        {
            RandomizeMovement();
            elapsedTime = 0f;
        }

        void Update(){
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= 0.33f){
                RandomizeMovement();
                elapsedTime = 0f;
            }
        }

        void FixedUpdate()
        {
            transform.position = ((Vector2) transform.position) + movement * Time.fixedDeltaTime;
        }

        private void RandomizeMovement(){
            // integer between -3 and +3 inclusive
            movement.x = Random.Range(-3, 3);
            movement.y = Random.Range(-3, 3);
        }

        void OnTriggerEnter2D(Collider2D c){
            RandomizeMovement();
        }
    }
}
