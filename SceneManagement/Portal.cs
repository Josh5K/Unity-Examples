using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
  public class Portal : MonoBehaviour
  {
    [SerializeField] int sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
          StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
      DontDestroyOnLoad(gameObject.transform.parent);
      yield return SceneManager.LoadSceneAsync(sceneToLoad);
      print("Scene Loaded!");
      Destroy(gameObject.transform.parent.gameObject);
    }
  }
}
