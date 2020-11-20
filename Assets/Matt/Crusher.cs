using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matt {

  [RequireComponent(typeof(SpriteRenderer))]
  public class Crusher : MonoBehaviour
  {
    public SpriteRenderer sr;
    public GameObject hitBox;

    public float moveOffset;
    public float moveTime;

    private Vector3 upPosition;
    private Vector3 downPosition;

    public float inputBufferTime;
    private float upBuffer;
    private float downBuffer;

    [HideInInspector] public bool canInput = true;
    private bool isMoving;
    private bool isDown;

    private Coroutine currentMove;


    void Start() {
      upPosition = transform.position;
      downPosition = upPosition + Vector3.down * moveOffset;

      sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
      if (!canInput)
        return;

      // up input
      if (Input.GetKeyDown(KeyCode.W))
        upBuffer = inputBufferTime;
      else
        upBuffer = Mathf.Max(0, upBuffer - Time.deltaTime);

      // down input
      if (Input.GetKeyDown(KeyCode.S))
        downBuffer = inputBufferTime;
      else
        downBuffer = Mathf.Max(0, downBuffer - Time.deltaTime);
    
      // movement
      if (!isMoving && (isDown ? upBuffer > 0 : downBuffer > 0)) {
        Move();
      }
    }


    void Move() {
      if (currentMove != null)
        StopCoroutine(currentMove);
      currentMove = StartCoroutine(DoMove());
    }

    IEnumerator DoMove() {
      isMoving = true;
      isDown = !isDown;

      if (isDown)
        OnStartDescend();
      else
        OnStartLift();
      
      Vector3 startPos = transform.position;
      Vector3 targetPos = isDown ? downPosition : upPosition;
      for (float t = 0; t < moveTime; t += Time.deltaTime) {
        transform.position = Vector3.Lerp(startPos, targetPos, t / moveTime);
        
        if (!isDown && isMoving && t / moveTime > 0.7f)
          isMoving = false;
        
        yield return null;
      }
      transform.position = targetPos;

      if (isDown)
        OnEndDescend();
      else
        OnEndLift();

      isMoving = false;
      currentMove = null;
    }


    void OnStartDescend() {

    }

    void OnEndDescend() {
      Instantiate(hitBox, transform.position + Vector3.down * sr.bounds.extents.y, Quaternion.identity, transform);
    }

    void OnStartLift() {

    }

    void OnEndLift() {

    }


    void OnDrawGizmos() {
      if (sr == null) {
        sr = GetComponent<SpriteRenderer>();
      }
      Gizmos.color = Color.red;
      Gizmos.DrawRay(transform.position, Vector3.down * moveOffset);
      Gizmos.DrawWireCube(transform.position + Vector3.down * moveOffset, new Vector3(sr.bounds.size.x, sr.bounds.size.y, 1f));
    }
  }
}