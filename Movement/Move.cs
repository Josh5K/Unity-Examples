using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
  public class Move : MonoBehaviour, IAction
  {
    [SerializeField] Transform target;
    [SerializeField] float maxSpeed = 5.66f;
    
    NavMeshAgent navMeshAgent;
    Health health;

    void Start()
    {
      navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
      health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
      if (health.IsDead)navMeshAgent.enabled = false;
      UpdateAnimator();
    }

    public void Cancel()
    {
      navMeshAgent.isStopped = true;
    }

    public void StartMoveAction(Vector3 destination, float speedFraction)
    {
      GetComponent<ActionScheduler>().StartAction(this);
      MoveTo(destination, speedFraction);
    }

    public void MoveTo(Vector3 destination, float speedFraction)
    {
      navMeshAgent.destination = destination;
      navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
      navMeshAgent.isStopped = false;
    }

    private void UpdateAnimator()
    {
      Vector3 velocity = navMeshAgent.velocity;
      Vector3 localVelocity = transform.InverseTransformDirection(velocity);
      float speed = localVelocity.z;
      GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
  }
}
