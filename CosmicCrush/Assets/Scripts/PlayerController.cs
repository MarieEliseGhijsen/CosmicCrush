using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public GameObject massTextObject;
    public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float mass;
    int score;

    //private MonoBehaviour enemyScript = GetMonoBehaviour()

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mass = 0.5f;
        score = 0;

        SetScoreText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetScoreText()
    {
        massTextObject.GetComponent<TextMeshProUGUI>().text = "Score : " + (score).ToString();

        if(mass >= 500)
        {
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Destroy(gameObject);

        //    winTextObject.SetActive(true);
        //    winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        //}

        //Debug.Log("enterde collisosin");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemiesMovement enemyScript = collision.gameObject.GetComponent<EnemiesMovement>();
            //Debug.Log("ENTERED COLLISON - TAG ENEMY");

            if (enemyScript != null)
            {
                if (enemyScript.mass > mass)
                {
                    //Debug.Log("ENEMY bigger");
                    winTextObject.SetActive(true);
                    winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
                }
                else
                {
                    //Debug.Log("ENEMY smaller");
                    mass = mass + enemyScript.mass;
                    score = score + (int)(enemyScript.mass * 100.0f);
                    SetScoreText();
                    Destroy(collision.gameObject);
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(other);

        //if (other.gameObject.CompareTag("Pickup"))
        //{
        //    other.gameObject.SetActive(false);
        //    mass = mass + 1;

        //    SetScoreText();
        //}

        //Debug.Log("enterde collisosin");

        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    EnemiesMovement enemyScript = other.gameObject.GetComponent<EnemiesMovement>();
        //    Debug.Log("ENTERED COLLISON - TAG ENEMY");


        //    if (enemyScript != null)
        //    {
        //        if (enemyScript.mass < mass)
        //        {
        //            Debug.Log("ENEMY bigger");
        //            winTextObject.SetActive(true);
        //            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        //        }
        //        else
        //        {
        //            Debug.Log("ENEMY smaller");
        //            mass = mass + enemyScript.mass;
        //            score = score + (int)(enemyScript.mass * 100.0f);
        //            SetScoreText();
        //            Destroy(other);
        //        }
        //    }


        //}
    }
}