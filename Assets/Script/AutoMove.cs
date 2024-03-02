using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 1f; // kecepatan objek
    public float distance = 1f; // jarak maksimal objek bergerak

    private Vector3 startPosition; // posisi awal objek
    private float direction = 1f; // arah gerakan objek (1 = ke kanan, -1 = ke kiri)

    void Start()
    {
        startPosition = transform.position; // menyimpan posisi awal objek
    }

    void Update()
    {
        // mengubah posisi objek sesuai arah gerakan
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // membalik arah gerakan jika objek mencapai batas kiri atau kanan
        if (Vector3.Distance(transform.position, startPosition) >= distance)
        {
            direction *= -1f;
        }
    }
}