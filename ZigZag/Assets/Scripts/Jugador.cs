using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    // publica
    public Camera camara;
    public GameObject suelo;
    public float velocidad = 7.0f;
    public GameObject estrella; // Añadir la estrella
    public Text contador;
    public AudioSource sonidoEstrella;
    public int vidas = 3;
    public Text contadorVidas;
    // privada
    private Vector3 offset;
    private Vector3 DireccionActual;
    private float valX, valZ;
    private Rigidbody rb;
    private int puntos = 0;
    private int resetHeight = -10;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offset = camara.transform.position - transform.position;
        crearSueloInicial();
        DireccionActual = Vector3.forward;
        ActualizarContadorVidas();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < resetHeight)
        {
            PerderVida();
        }
        camara.transform.position = transform.position + offset;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0 || verticalInput != 0)
        {
            CambiarDireccion(horizontalInput, verticalInput);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CambiarDireccion2();
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

    void CambiarDireccion2()
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


    void CambiarDireccion(float horizontalInput, float verticalInput)
    {
        // Definir direcciones basadas en las teclas de flecha
        Vector3 direccionDeseada = Vector3.zero;
        if (horizontalInput > 0)
        {
            direccionDeseada += Vector3.right;
        }
        else if (horizontalInput < 0)
        {
            direccionDeseada += -Vector3.right;
        }

        if (verticalInput > 0)
        {
            direccionDeseada += Vector3.forward;
        }
        else if (verticalInput < 0)
        {
            direccionDeseada += -Vector3.forward;
        }

        // Normalizar la dirección deseada para evitar movimientos más rápidos en diagonal
        if (direccionDeseada != Vector3.zero)
        {
            direccionDeseada.Normalize();
        }

        // Actualizar la dirección actual solo si se ha presionado alguna tecla de dirección
        if (direccionDeseada != Vector3.zero)
        {
            DireccionActual = direccionDeseada;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // Estrella pequeña
        if (other.gameObject.CompareTag("Estrella"))
        {
            Destroy(other.gameObject);        
            puntos += 1;
            sonidoEstrella.PlayOneShot(sonidoEstrella.clip);
            contador.text = "Puntos: " + puntos;   
        }
    }

    void ActualizarContadorVidas()  
    {
        contadorVidas.text = "Vidas: " + vidas.ToString();
    }

    public void PerderVida()
    {
        vidas--;
        ActualizarContadorVidas();
        if(vidas <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    
}
