using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float suspicionTime = 4f;
    [SerializeField] float waypointTime = 5f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float patrolSpeedFraction = 0.2f;
    [SerializeField] float wayPointTolerance = 1f;

    Fighter fighter;
    Health health;
    GameObject player;

    Vector3 guardLocation;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    float timeSinceLastWaypoint = Mathf.Infinity;
    int currentWaypointIndex = 0;

    void Start()
    {
      fighter = GetComponent<Fighter>();
      health = GetComponent<Health>();
      player = GameObject.FindWithTag("Player");
      guardLocation = transform.position;
    }

    void Update()
    {
      if (health.IsDead) return;
      if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
      {
        timeSinceLastSawPlayer = 0;
        AttackBehaviour();
      }
      else if(suspicionTime > timeSinceLastSawPlayer)
      {
        SuspicionBehaviour();
      }
      else if(waypointTime < timeSinceLastWaypoint)
      {
        //fighter.Cancel();
        PatrolBehaviour();
      }
      timeSinceLastSawPlayer += Time.deltaTime;
      timeSinceLastWaypoint += Time.deltaTime;
    }

    private void PatrolBehaviour()
    {
      Vector3 nextPosition = guardLocation;
      if(patrolPath != null)
      {
        if(AtWaypoint())
        {
          CycleWaypoint();
        }
        nextPosition = GetCurrentWaypoint();
      }
      GetComponent<Move>().StartMoveAction(nextPosition, patrolSpeedFraction);
    }

    private Vector3 GetCurrentWaypoint()
    {
      return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
      currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private bool AtWaypoint()
    {
      timeSinceLastWaypoint = 0;
      float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
      return distanceToWaypoint < wayPointTolerance;
    }

    private void SuspicionBehaviour()
    {
      GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void AttackBehaviour()
    {
      fighter.Attack(player);
    }

    private bool InAttackRangeOfPlayer()
    {
      float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
      return distanceToPlayer < chaseDistance;
    }

    // Called By Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
  }
}