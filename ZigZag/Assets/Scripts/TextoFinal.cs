using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoFinal : MonoBehaviour
{
    public Text texto;
    private int puntuacion;
    
    // Start is called before the first frame update
    void Start()
    {
        puntuacion = PlayerPrefs.GetInt("contadorPuntos", 0);
        texto.text = "Puntos: " + puntuacion;
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = "Puntos: " + puntuacion;
    }
}
