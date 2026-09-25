using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class EnemiesMovement : MonoBehaviour
{
    //public Transform player;
    public float mass; //MIN : 10 ;;;; SMA : 0-100, MED : 100-200, LAR : 200-300

    private Rigidbody rb;
    private Vector3 dir;
    private float speed;

    private float timer = 0;

    public Material small;
    public Material med;
    public Material big;

    Transform sphere;
    Vector3 radius;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        dir.x = Random.Range(-1.0f, 1.0f);
        dir.y = 0.0f;
        dir.z = Random.Range(-1.0f, 1.0f);

        mass = Random.Range(0.1f, 1.0f);
        radius = new Vector3(mass, mass, mass);

        sphere = GetComponent<Transform>();
        sphere.transform.localScale = radius;

        //mass = 0.1f;
        speed = 1.0f;

        //small = Resources.Load("Assets\\Materials\\SmallEnemy.mat", typeof(Material)) as Material;
        //med = Resources.Load("Assets\\Materials\\MediumEnemy.mat", typeof(Material)) as Material;
        //big = Resources.Load("Assets\\Materials\\LargeEnemy.mat", typeof(Material)) as Material;

        ColourSet();
    }

    private void ColourSet()
    {
        Renderer skin = GetComponent<Renderer>();
        Material material = GetComponent<Renderer>().material;

        if (mass >= 0.0f && mass < 1.0f)
        {
            skin.material = small;
        }
        else if (mass >= 1.0f && mass < 2.0f)
        {
            skin.material = med;
        }
        else if (mass >= 2.0f && mass < 3.0f)
        {
            skin.material = big;
        }
        else
        {
            mass = 2.9f;
            ColourSet();
            //skin.material.color = Color.blue;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < 2.0f)
        {
            rb.AddForce(dir * speed);
        }
        else
        {
            dir.x = Random.Range(-1.0f, 1.0f);
            dir.z = Random.Range(-1.0f, 1.0f);
            timer = 0.0f;
        }

        timer = timer + 0.1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Destroy(gameObject);

        //    winTextObject.SetActive(true);
        //    winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        //}

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemiesMovement enemyScript = collision.gameObject.GetComponent<EnemiesMovement>();


            if (enemyScript != null)
            {
                if (enemyScript.mass < mass)
                {
                    mass = mass + enemyScript.mass;

                    Vector3 newR = radius + new Vector3(mass, mass, mass);
                    radius = newR;
                    sphere.transform.localScale = radius;

                    if (newR.x > 3.0f && newR.y > 3.0f && newR.z > 3.0f)
                    {
                        sphere.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                    }

                    Destroy(collision.gameObject);
                    ColourSet();
                }
            }


        }
    }
}