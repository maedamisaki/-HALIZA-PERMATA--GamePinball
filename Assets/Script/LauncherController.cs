using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
  public KeyCode input;

  public float maxTimeHold;
  public float maxForce;

  private Renderer renderer;
  private bool isHold;

  private void Start()
  {
    isHold = false;
    renderer = GetComponent<Renderer>();
  }

  private void OnCollisionStay(Collision collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      ReadInput(collision.gameObject);
    }
  }

  private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

  private void ReadInput(GameObject ball)
  {
    if (Input.GetKey(input) && !isHold)
    {
      StartCoroutine(StartHold(ball));
    }
  }

  private IEnumerator StartHold(GameObject ball)
  {
    isHold = true;

    float force = 0.0f;
    float timeHold = 0.0f;

    while (Input.GetKey(input))
    {
      force = Mathf.Lerp(0, maxForce, timeHold/maxTimeHold);

      yield return new WaitForEndOfFrame();
      timeHold += Time.deltaTime;
    }

    ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
    isHold = false;
  }
}