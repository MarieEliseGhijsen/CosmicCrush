using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public GameObject massTextObject;
    public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int mass;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mass = 0;

        SetMassText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetMassText()
    {
        massTextObject.GetComponent<TextMeshProUGUI>().text = "Mass : " + mass.ToString();

        if(mass >= 500)
        {
            winTextObject.SetActive(true);

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            mass = mass + 1;

            SetMassText();
        }
    }
}