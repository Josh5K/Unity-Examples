using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
  public class FollowCamera : MonoBehaviour
  {
    [SerializeField] Transform target;
    [SerializeField] float speed = 75f;
    [SerializeField] float yOffset = 100f;

    void Start()
    {
      
    }

    void Update()
    {
      transform.position = target.position;
      var v3 = new Vector3(0f, Input.GetAxis("Horizontal"), 0f);
      transform.Rotate(v3 * speed * Time.deltaTime);
    }
  }
}
