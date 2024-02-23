using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para añadir el botón
using UnityEngine.SceneManagement; // Para añadir la escena que se va a cargar
using Unity.VisualScripting; 



public class Cuenta_atras : MonoBehaviour
{
    //Publicas
    public Button boton;
    public Image imagen;
    public Sprite[] imagenes;
    // Start is called before the first frame update
    void Start()
    {
        boton.onClick.AddListener(Empezar);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Debe cambiar la imagen para la cuenta atrás
    void Empezar()
    {
        imagen.GameObject().SetActive(true); // Imagen Visible
        boton.GameObject().SetActive(true); // Botón Visible
        StartCoroutine(CuentaAtras());
    }

    IEnumerator CuentaAtras()
    {
        for(int i = 0; i < imagenes.Length; i++)
        {
            imagen.sprite = imagenes[i];
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Nivel1");
    }
}
