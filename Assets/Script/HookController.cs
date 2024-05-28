using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] float speed;
    GameManager gameManager;
    Rigidbody rb;
    bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        rb= GetComponent<Rigidbody>();
        rb.velocity = rb.transform.forward*speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (collided == false)
        {
            collided = true;
            if (other.tag == "Fish")
            {
                //rb.velocity = Vector3.zero;
                //rb.angularVelocity = Vector3.zero;
                //transform.parent = other.transform;
                Destroy(gameObject);
                return;
            }
            gameManager.trys--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collided == false)
        {
            collided = true;
            if (collision.gameObject.tag == "Fish") 
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.parent = collision.transform;
                return;
            }
            gameManager.trys--;
            Destroy(gameObject);
        }
    }


}
