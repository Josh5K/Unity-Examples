﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
  public class CinematicTrigger : MonoBehaviour
  {
    bool didTrigger = false;
    private void OnTriggerEnter(Collider other) 
    {
      if(other.tag == "Player" && !didTrigger)
      {
        didTrigger = true;
        GetComponent<PlayableDirector>().Play();
      }
    }
  }
}