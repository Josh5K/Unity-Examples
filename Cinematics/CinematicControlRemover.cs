﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
  public class CinematicControlRemover : MonoBehaviour
  {
    void Start()
    {
      GetComponent<PlayableDirector>().played += DisableControl;
      GetComponent<PlayableDirector>().stopped += EnableControl;
    }

    void DisableControl(PlayableDirector pd)
    {
      GameObject player = GameObject.FindWithTag("Player");
      player.GetComponent<ActionScheduler>().CancelCurrentAction();
      player.GetComponent<PlayerController>().enabled = false;
    }

    void EnableControl(PlayableDirector pd)
    {
      GameObject player = GameObject.FindWithTag("Player");
      player.GetComponent<PlayerController>().enabled = true;
    }
  }
}
