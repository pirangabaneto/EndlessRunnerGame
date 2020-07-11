using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heroi_move : MonoBehaviour {

	public bool face = true; //facing right
	public Transform heroiT; //to turn
	public float vel = 5f; //to run
	public float forca = 6.5f; //to jump
	public bool tocandoChao = false; 
	public Animator anim;
	//public bool vivo = true;
	public Rigidbody2D ninjaRB;
	public bool isUpsideDown = false;
	//deal with time 
	float timer = 0.0f; //in secs
	private float nextActionTime;


	//Relacionado ao input de texto
	string[] palavras;
	int numChaves;
	public Text textoVisivel, textoNaoVisivel;
	public InputField inputF;

	public bool escrever = false;
	public string resposta;

	//tempo pra troca de gravidade
	public float trocarGravidadePalvra;
	public float trocarGravidadeAleatorio;
	public float tempMin = 0.0f;
	public float tempMax = 2.0f;
  
	void Start () {
		heroiT.GetComponent<Transform> ();
		ninjaRB.GetComponent<Rigidbody2D> ();
		nextActionTime = 2;
		trocarGravidadeAleatorio = 10.0f;
		palavras = new string[] {"lua", "espaco", "astronauta","espaço sideral", "céu","firmamento","abóbada celeste","infinito","empíreo",
			"uranograma","mundo","horizonte","universo","alturas","manto","estrelado","nadir","azimute"};
	}
	
	// Update is called once per frame
	void Update () {
		//upsideDown ();
		timer += Time.deltaTime;
		if (timer >= nextActionTime && tocandoChao && !escrever) {
			//pra pedir input
			StartCoroutine (readInput ());
		}
			
    }

	public void lendoEntrada(){
		inputF.Select ();
		textoNaoVisivel.text = inputF.text;
	}

	public IEnumerator readInput(){
		escrever = true;
		//definir quantos secs esperar
		inputF.Select ();

		textoVisivel.text = palavras[Random.Range(0, palavras.Length-1)];//(Random.Range (0, 2)).ToString();//palavras [];
		yield return new WaitForSeconds(textoVisivel.text.Length);
		if (textoVisivel.text == textoNaoVisivel.text) {
			textoVisivel.text = "Acertou mizeravi";
		} else {
			textoVisivel.text = "Errrrrrrou";

		}
		textoNaoVisivel.text = "";
		inputF.text = "";
		timer = 0;
		nextActionTime = Random.Range(tempMin, tempMax);
		escrever = false;

		//trocar gravidade aleatoriamente
		/*
		trocarGravidadePalvra = Random.Range(0.0f, 1.0f);
		if (trocarGravidadePalvra >= 0.5) {
			upsideDown ();
		}*/
	}

	void upsideDown(){
		ninjaRB.gravityScale = ninjaRB.gravityScale* -1;
		transform.Rotate (new Vector3 (0, 0, 180));
		isUpsideDown = !isUpsideDown;
		Flip ();
	}

	void Flip(){
		face = !face;

		Vector3 escala = heroiT.localScale;
		escala.x = escala.x * -1;
		heroiT.localScale = escala;
	}

	void OnCollisionEnter2D(Collision2D outro){
		if (outro.gameObject.CompareTag ("chao")) {
			tocandoChao = true;
		}

		if(outro.gameObject.CompareTag("bala")){
			upsideDown ();
		}
	}

	void OnCollisionExit2D(Collision2D outro){
		if (outro.gameObject.CompareTag ("chao")) {
			tocandoChao = false;
		}
	}
}
