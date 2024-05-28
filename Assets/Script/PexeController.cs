using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PexeController : MonoBehaviour
{
    [SerializeField] Animator animator;
    GameManager gameManager;
    WiimoteFacade wiimoteFacade;
    Vector3 mainCameraPos;

    Rigidbody rb;
    Vector3 velocityVector;
    bool hooked = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        mainCameraPos = Camera.main.transform.position;
        wiimoteFacade = FindObjectOfType<WiimoteFacade>();
        rb = GetComponent<Rigidbody>();
        float force = Random.Range(5.0f, 10.0f);
        rb.velocity = velocityVector * force;
    }

    // Update is called once per frame
    void Update()
    {
        if (hooked) 
        {
            rb.velocity = Vector3.zero;
            float accelMag = wiimoteFacade.GetAccelVector().sqrMagnitude;
            Debug.Log(accelMag);
            if (accelMag >= 500) 
            {
                hooked = false;
                Vector3 accel = wiimoteFacade.GetAccelVector();
                Debug.LogWarning(accel);
                rb.AddForce(new Vector3(0, 1, -1) * 10,ForceMode.VelocityChange);
                gameManager.fished++;
            }
        }

        float distance = Vector3.Distance(mainCameraPos, transform.position);
        
        if (distance > 30) 
        {
            Destroy(gameObject);
        }
        
    }

    public void SetVelocityVector(Vector3 vel) 
    {
        velocityVector = vel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hook") 
        {
            LeanTween.color(gameObject, Color.yellow, 0f);
            hooked = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hook") 
        {
            LeanTween.color(gameObject, Color.yellow, 0f);
            hooked = true;
        }
    }

}
