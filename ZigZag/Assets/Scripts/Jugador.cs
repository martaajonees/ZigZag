using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    // publica
    public Camera camara;
    public GameObject suelo;
    public float velocidad = 7.0f;
    public GameObject estrella; // Añadir la estrella
    // privada
    private Vector3 offset;
    private Vector3 DireccionActual;
    private float valX, valZ;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offset = camara.transform.position - transform.position;
        crearSueloInicial();
        DireccionActual = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        camara.transform.position = transform.position + offset;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CambiarDireccion();
        }
        transform.Translate(DireccionActual * velocidad * Time.deltaTime);
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Suelo")
        {
           StartCoroutine(BorrarSuelo(other.gameObject));
        }
    }

    IEnumerator BorrarSuelo(GameObject suelo)
    {
        float aleatorio = Random.Range(0.0f, 1.0f);
        
        if(aleatorio > 0.5f)
        {
            valX += 6.0f;
        }
        else
        {
            valZ += 6.0f;
        }
        Instantiate(suelo, new Vector3(valX, 0, valZ), Quaternion.identity);
        Instantiate(estrella, new Vector3(valX, 1, valZ), Quaternion.Euler(90, 0, 0));
        yield return new WaitForSeconds(4);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(8);
        Destroy(suelo);
    }

    void crearSueloInicial()
    {
        for (int i = 0; i < 3; i++)
        {
            valZ += 6.0f;
            Instantiate(suelo, new Vector3(valX, 0, valZ), Quaternion.identity);
        }
    }

    void CambiarDireccion()
    {
        if(DireccionActual == Vector3.forward)
        {
            DireccionActual = Vector3.right;
        }
        else
        {
            DireccionActual = Vector3.forward;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Estrella pequeña
        if (other.gameObject.CompareTag("Estrella"))
        {
            Destroy(other.gameObject);           
        }
    }

    
}
