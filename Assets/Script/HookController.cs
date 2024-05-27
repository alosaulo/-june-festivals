using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 initialDirection;
    Vector3 targetPosition;
    private float amplitude = 1f; // Amplitude da onda seno
    private float frequency = 1f; // Frequência da onda seno
    private bool isMoving = false;
    private float travelTime;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.velocity = rb.transform.forward*speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            //MoveInSinusoidalPath();
        }
    }

    public void SetDirection(Vector3 direction, Vector3 target)
    {
        initialDirection = direction;
        targetPosition = target;

        isMoving = true;

        // Calcula o tempo de viagem baseado na distância e velocidade
        travelTime = Vector3.Distance(transform.position, targetPosition) / speed;
    }

    private void MoveInSinusoidalPath()
    {
        float elapsedTime = Time.time;

        // Movimento linear
        Vector3 linearMovement = initialDirection * speed * Time.deltaTime;

        // Movimento sinoidal
        float sineWave = Mathf.Sin(elapsedTime * frequency) * amplitude;
        Vector3 sineMovement = transform.up * sineWave * Time.deltaTime;

        // Atualiza a posição do projétil
        transform.position += linearMovement + sineMovement;

        // Verifica se o projétil alcançou o alvo
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            // Adicione qualquer comportamento adicional ao alcançar o alvo, como desativar o projétil ou ativar um efeito
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fish") 
        { 
            
        }
        Destroy(gameObject);
    }


}
