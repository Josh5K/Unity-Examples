using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
  public class Health : MonoBehaviour
  {
    [SerializeField] float health = 100f;

    public bool IsDead { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
      health = Mathf.Max(health - damage, 0);
      print(health);
      if (health <= 0)
      {
        Die();
      }
    }

    private void Die()
    {
      if (IsDead) return;

      IsDead = true;
      GetComponent<Animator>().SetTrigger("death");
      GetComponent<ActionScheduler>().CancelCurrentAction();
    }
  }
}
