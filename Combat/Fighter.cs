using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
	public class Fighter : MonoBehaviour, IAction
	{
		[SerializeField] float weaponRange = 1f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] float weaponDamage = 5f;
		Transform target;
    
    private float timeSinceLastAttack;
    
		public void Update()
		{
      timeSinceLastAttack += Time.deltaTime;
    	if (target == null || target.GetComponent<Health>().IsDead) return;

      if (!isInRange())
			{
				GetComponent<Move>().MoveTo(target.position, 1f);
			}
			else
      {
        AttackBehaviour();
        GetComponent<Move>().Cancel();
      }
    }

    private void AttackBehaviour()
    {
      transform.LookAt(target.position);
      if(timeSinceLastAttack >= timeBetweenAttacks)
      {
        timeSinceLastAttack = 0;
        GetComponent<Animator>().ResetTrigger("stopAttack");
        GetComponent<Animator>().SetFloat("randomAttack", Random.Range(0, 2));
        // This will trigger the hit event!   
        GetComponent<Animator>().SetTrigger("attack");
      }
    }

    // Animation Event
    private void Hit()
    {
      if(target == null) return;
      Health healthComponent = target.GetComponent<Health>();
      healthComponent.TakeDamage(weaponDamage);
    }

    private bool isInRange()
	  {
      return Vector3.Distance(transform.position, target.position) < weaponRange;
	  }

	  public void Attack(GameObject combatTarget) 
	  {
		  GetComponent<ActionScheduler>().StartAction(this);
		  target = combatTarget.transform;
	  }

	  public void Cancel()
	  {
      GetComponent<Animator>().ResetTrigger("attack");
      GetComponent<Animator>().SetTrigger("stopAttack");
			target = null;
      GetComponent<Move>().Cancel();
	  }

    public bool CanAttack(GameObject combatTarget) 
    {
      if(combatTarget == null) return false;
      Health targetToTest = combatTarget.GetComponent<Health>();
      return targetToTest != null && !targetToTest.IsDead;
    }
	}
}
