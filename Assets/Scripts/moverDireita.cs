using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverDireita : MonoBehaviour {

    public float velocidade = 10.0f;
    private heroi_move heroi_move_script;
    private float limite = -15; //Limite da tela 

    // Use this for initialization
    void Start () {
        heroi_move_script = GameObject.Find("ninjaPlayer").GetComponent<heroi_move>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);

        if (transform.position.x < limite && gameObject.CompareTag("chao")) // Destroi o chão se ele estiver fora do limite da tela
        {
            Destroy(gameObject);
        }
    }
}
