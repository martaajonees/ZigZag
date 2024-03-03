using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Jugador : MonoBehaviour
{
    // publico
    public Camera camara;
    public float velocidad = 7.0f;
    public Text contadorPuntos;
    public AudioSource sonidoEstrella;
    // privado
    private Vector3 offset;
    private Vector3 DireccionActual;
    private Rigidbody rb;
    private int puntos = 0;
    private int resetHeight = -10;
    private Vector3 posicionInicial; // Guarda la posición inicial del jugador

    void Start()
    {
        string EscenaActual = SceneManager.GetActiveScene().name;
        rb = GetComponent<Rigidbody>();
        offset = camara.transform.position - transform.position;
        posicionInicial = transform.position; // Guarda la posición inicial
    }

    void Update()
    {
        camara.transform.position = transform.position + offset;
        if(transform.position.y < resetHeight)
        {
            PerderVida();
            transform.position = posicionInicial;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Si el jugador presiona alguna tecla de movimiento
        if (horizontalInput != 0 || verticalInput != 0)
        {
            CambiarDireccion(horizontalInput, verticalInput);
        }
        // Si el jugador presiona la barra espaciadora
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CambiarDireccion2();
        }

        transform.Translate(DireccionActual * velocidad * Time.deltaTime);
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

        if (direccionDeseada != Vector3.zero)
        {
            DireccionActual = direccionDeseada;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Estrella"))
        {
            Destroy(other.gameObject);
            puntos += 1;
            sonidoEstrella.PlayOneShot(sonidoEstrella.clip);
            contadorPuntos.text = "Puntos: " + puntos;
        
        } else if (other.gameObject.CompareTag("Estrella2"))
        {
            Destroy(other.gameObject);
            puntos += 3;
            sonidoEstrella.PlayOneShot(sonidoEstrella.clip);
            contadorPuntos.text = "Puntos: " + puntos;
        }

    }

    public void PerderVida()
    {
       
        SceneManager.LoadScene("GameOver");
    }
    
    public void GuardarDatosAlFinalDeLaEscena()
    {
        PlayerPrefs.SetInt("contadorPuntos", puntos);
    }

}
