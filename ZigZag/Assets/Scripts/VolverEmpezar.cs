using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReiniciarJuego : MonoBehaviour
{
    public Button botonReiniciar;

    void Start()
    {
        
        botonReiniciar.onClick.AddListener(Reiniciar);
    }

    
    public void Reiniciar()
    {
        SceneManager.LoadScene("EscenaInicio");
    }
}
