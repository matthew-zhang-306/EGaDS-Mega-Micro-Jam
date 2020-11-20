using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matt {
  
  [RequireComponent(typeof(BoxCollider2D))]
  public class Hitbox : MonoBehaviour
  {
    public BoxCollider2D coll;
    public float aliveTime;

    private void Start() {
      coll = GetComponent<BoxCollider2D>();
    }

    private void Update() {
      aliveTime -= Time.deltaTime;
      if (aliveTime < 0) {
        Destroy(gameObject);
      }
    }
  }
}