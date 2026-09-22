using UnityEngine;
using UnityEngine.AI;

public class EnemiesMovement : MonoBehaviour
{
    //public Transform player;
    private Vector3 dir;
    private float mass; //MIN : 10 ;;;; SMA : 0-100, MED : 100-200, LAR : 200-300
    private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dir.x = Random.Range(-1.0f, 1.0f);
        dir.y = 0.0f;
        dir.z = Random.Range(-1.0f, 1.0f);

        mass = Random.Range(10.0f, 150.0f);
        speed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}