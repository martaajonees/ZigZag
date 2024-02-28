using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 10f;
    public GameObject starPrefab;
    public int num_estrellas = 30;
    public float radio = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateStars();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);
    }

    void GenerateStars()
    {
        for (int i = 0; i < num_estrellas; i++)
        {
            Vector3 randomPosition = Random.onUnitSphere * radio;
            randomPosition.y = 0f; // Asegura que las estrellas estén en el mismo plano que el suelo
            Instantiate(starPrefab, randomPosition, Quaternion.identity);
        }
    }
}
