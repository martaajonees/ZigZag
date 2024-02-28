using UnityEngine;
using UnityEngine.UI;

public class AsignadorSalir : MonoBehaviour
{
    public Button botonSalir;

    void Start()
    {
        
        botonSalir.onClick.AddListener(Salir);
    }
    
    public void Salir()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

}
