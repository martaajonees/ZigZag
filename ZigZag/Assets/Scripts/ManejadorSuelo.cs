using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorSuelo : MonoBehaviour
{
    [SerializeField] GameObject suelo;
    private Vector3 DireccionActual;
    private Vector3 posicionInicial;
    private float valX, valZ;
    public GameObject estrella;
    public GameObject estrella2;

    void Start()
    {
        valX = suelo.transform.position.x;
        valZ = suelo.transform.position.z;
        DireccionActual = Vector3.forward;
        posicionInicial = transform.position; 
        crearSueloInicial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void crearSueloInicial()
    {
        for (int i = 0; i < 3; i++)
        {
            valZ += 6.0f;
            Instantiate(suelo, new Vector3(valX, 0, valZ), Quaternion.identity);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Suelo")
        {
            StartCoroutine(BorrarSuelo(other.gameObject));
        }
    }

    IEnumerator BorrarSuelo(GameObject DestruirSuelo)
    {
        float aleatorio = Random.Range(0.0f, 1.0f);

        if (aleatorio > 0.5f)
        {
            valX += 6.0f;
            Instantiate(estrella2, new Vector3(valX, 1, valZ+2f), Quaternion.Euler(90, 0, 0));
        } else
        {
            valZ += 6.0f;
            Instantiate(estrella2, new Vector3(valX+2f, 1, valZ), Quaternion.Euler(90, 0, 0));
        }
        Instantiate(DestruirSuelo, new Vector3(valX, 0, valZ), Quaternion.identity);
        Instantiate(estrella, new Vector3(valX, 1, valZ), Quaternion.Euler(90, 0, 0));
        yield return new WaitForSeconds(10f);
        DestruirSuelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        DestruirSuelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2);
        Destroy(DestruirSuelo);
    }
}
