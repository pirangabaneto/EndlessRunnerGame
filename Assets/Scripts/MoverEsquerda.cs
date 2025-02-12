﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEsquerda : MonoBehaviour {

    public float velocidade = 10.0f;
    private heroi_move heroi_move_script;
    private float limite = -20; //Limite da tela 

    // Use this for initialization
    void Start () {
        heroi_move_script = GameObject.Find("ninjaPlayer").GetComponent<heroi_move>(); 
    }
	
	// Update is called once per frame
	void Update () {
		if (!heroi_move_script.escrever) {
			//animacao do personagem
			heroi_move_script.anim.SetBool ("andar", true);
			heroi_move_script.anim.SetBool ("idle", false);
			transform.Translate (Vector2.left * velocidade * Time.deltaTime);

			if (transform.position.x < limite && !heroi_move_script.tocandoChao && !heroi_move_script.pulando) { // Destroi o chão se ele estiver fora do limite da tela
				Destroy (gameObject);
			}
		} else {
			heroi_move_script.anim.SetBool ("andar", false);
			heroi_move_script.anim.SetBool ("idle", true);
		}
            

    }

}
