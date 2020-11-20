using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Matt {
  public class Minigamer : MonoBehaviour
  {
    private int crushes;
    private bool win;

    public Minigame minigame;

    public int Crushes {
      get { return crushes; }
      set {
        crushes = value;
        if (crushes >= 4)
          minigame.gameWin = true;
      }
    }


    private void Start() {
      minigame.gameWin = false;
    }
  }
}
