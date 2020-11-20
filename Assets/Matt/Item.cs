using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matt {

  [RequireComponent(typeof(Rigidbody2D))]
  public class Item : MonoBehaviour
  {
    public Minigamer minigamer;
    public float moveSpeed;
    public float fallPoint;
    public float gravity;

    public Rigidbody2D rb;

    private void Start() {
      rb = GetComponent<Rigidbody2D>();
      rb.velocity = Vector2.right * moveSpeed;
    }

    private void FixedUpdate() {
      if (transform.position.x >= fallPoint)
        rb.velocity += Vector2.down * gravity;
    }

    private void OnTriggerEnter2D(Collider2D other) {
      if (other.CompareTag("Player")) {
        minigamer.Crushes++;
        Destroy(gameObject);
      }
    }


    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(new Vector3(fallPoint, transform.position.y, 0), 0.5f);
    }
  }
}