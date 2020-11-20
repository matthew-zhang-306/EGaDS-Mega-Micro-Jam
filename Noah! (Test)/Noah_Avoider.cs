using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NOAH_TEST_NAME_SPACE{
    public class Noah_Avoider : MonoBehaviour
    {
        private MinigameManager minigameManager;
        private Minigame minigame;

        //movement variables
        public float moveSpeed = 5f;
        public Rigidbody2D rb;
        Vector2 movement;
        
        private void Start()
        {
            minigameManager = FindObjectOfType<MinigameManager>();
            minigame = minigameManager.minigame;

            minigame.gameWin = true;
        }

        void Update(){
            //first up: get movement
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        void FixedUpdate(){
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }

        void OnTriggerEnter2D(Collider2D c){
            if(minigame.gameWin){
                minigame.gameWin = false;
                moveSpeed = 0f;
            }
        }
    }
}