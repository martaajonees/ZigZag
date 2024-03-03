using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choquebola : MonoBehaviour
{
    public GameObject particulas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable() {
        if(!this.gameObject.scene.isLoaded) return;
        Vector3 posactual = new Vector3(transform.position.x, 0.5f, transform.position.z);
        Instantiate(particulas, posactual, particulas.transform.rotation);
    }
    void OnDestroy()
    {
        Invoke("DestroyParticulas", 0.1f);
    }

    void DestroyParticulas()
    {
        Destroy(particulas);
    }
}
