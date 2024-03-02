using System;
using System.Collections;
using UnityEngine;

public class BumperController : MonoBehaviour
{
	// menyimpan variabel bola sebagai referensi untuk 
	public Collider bola;
     public float multiplier;
     public Animator animator;

     private void Awake(){
      animator= GetComponent<Animator>();
     }
	
private void OnCollisionEnter(Collision collision)
{
  if (collision.gameObject.CompareTag("Player"))
  {
    Debug.Log("Bola collide");
		// ambil rigidbody nya lalu kali kecepatannya sebanyak multiplier agar bisa memantul lebih cepat
		Rigidbody bolaRig = collision.gameObject.GetComponent<Rigidbody>();
    bolaRig.velocity *= multiplier;
    animator.SetTrigger("Bump");
  }
}
}