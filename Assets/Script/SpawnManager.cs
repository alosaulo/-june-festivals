using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] Centers;
    [SerializeField] GameObject[] pexesPrefabs;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnTime;
    bool spawn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn) 
        {
            StartCoroutine("SpawnPexe");
        }
    }

    IEnumerator SpawnPexe() 
    {
        spawn = false;
        int randomFish = Random.Range(0, pexesPrefabs.Length);

        // Gera duas coordenadas aleatórias para a interpolação

        float t1 = Random.Range(0f, 1f);
        float t2 = Random.Range(0f, 1f);

        // Interpola a posição entre os pontos usando as coordenadas aleatórias
        Vector3 randomPosition = InterpolateQuadrilateral(spawnPoints[0].position, spawnPoints[1].position, spawnPoints[2].position, spawnPoints[3].position, t1, t2);
        Vector3 randomCenter = InterpolateQuadrilateral(Centers[0].position, Centers[1].position, Centers[2].position, Centers[3].position, t1, t2);

        PexeController pexe = Instantiate(pexesPrefabs[randomFish], randomPosition, Quaternion.identity).GetComponent<PexeController>();
        Vector3 forceDir = (randomCenter - pexe.transform.position).normalized;
        pexe.SetVelocityVector(forceDir);
        yield return new WaitForSeconds(spawnTime);
        spawn = true;
    }

    Vector3 InterpolateQuadrilateral(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t1, float t2)
    {
        // Interpola entre p0 e p1, e p3 e p2
        Vector3 a = Vector3.Lerp(p0, p1, t1);
        Vector3 b = Vector3.Lerp(p3, p2, t1);

        // Interpola entre a e b para obter a posição final
        return Vector3.Lerp(a, b, t2);
    }

}
