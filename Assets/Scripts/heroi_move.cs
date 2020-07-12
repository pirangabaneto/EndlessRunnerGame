using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heroi_move : MonoBehaviour {

	public bool face = true; //facing right
	public Transform heroiT; //to turn
	public float vel = 5f; //to run
	private float forca = 25.5f; //to jump
	public bool pulando = false;
	public bool tocandoChao = false; 
	public Animator anim;
	//public bool vivo = true;
	public Rigidbody2D ninjaRB;
	public bool isUpsideDown = false;
	//deal with time 
	float timer = 0.0f; //in secs
	private float nextActionTime;
    public AudioClip pulo;
    public AudioClip trocaGravidade;
    public AudioClip palavraCerta;
    public AudioClip palavraErrada;
    public AudioClip gameOver;
    public AudioSource playerAudio;


	//Relacionado ao input de texto
	string[] palavras;
	private GameObject txt;
	private Text textoVisivel, textoNaoVisivel, textoDistancia;
	private GameObject ifgo;
	private InputField inputF; 

	public bool escrever = false;
	public string resposta;

	//tempo pra troca de gravidade
	public float trocarGravidadePalvra;
	public float trocarGravidadeAleatorio;
	private float tempMin = 2.0f;
	private float tempMax = 5.0f;

	//contador
	private float distancia = 0;

	//etbilu
	private bool encontroEt = false;
  
	void Start () {
		heroiT.GetComponent<Transform> ();
		//escala = heroiT.localScale;
		ninjaRB.GetComponent<Rigidbody2D> ();
		ninjaRB.GetComponent<Rigidbody2D> ().gravityScale = 10f;
		nextActionTime =  Random.Range(tempMin, tempMax);;
		trocarGravidadeAleatorio =  Random.Range(tempMin, tempMax);
		palavras = new string[] {"moon", "space", "astronaut","outer space", "sky","firmamento","sky dome","infinity","empire",
			"uranus","world","horizon","universe","beyond","earth","stars","supernova","mars", "sun"};
        playerAudio = GetComponent<AudioSource>();

		ifgo = GameObject.Find("InputField");
		inputF = ifgo.GetComponent<InputField>();

		txt = GameObject.Find ("textoVisivel");
		textoVisivel = txt.GetComponent<Text> ();

		txt = GameObject.Find ("textoNaoVisivel");
		textoNaoVisivel = txt.GetComponent<Text> ();

		txt = GameObject.Find ("textoDistancia");
		textoDistancia = txt.GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {
		//calculando distancia
		if(!escrever){
			distancia += Time.smoothDeltaTime * 1.5f;	
		}
		textoDistancia.text = distancia.ToString("f0");

		timer += Time.deltaTime;
		if (timer >= nextActionTime && tocandoChao && !escrever) {
			//pra pedir input
			StartCoroutine (readInput ());
		}
    }

	public void lendoEntrada(){
		if (escrever) {

		inputF.Select ();
		textoNaoVisivel.text = inputF.text;
		}
	}

	public IEnumerator readInput(){
		
		escrever = true;
		//definir quantos secs esperar
		inputF.Select ();

		textoVisivel.text = palavras[Random.Range(0, palavras.Length-1)];
		textoNaoVisivel.text = "";
		inputF.text = "";
		yield return new WaitForSeconds(textoVisivel.text.Length * 0.5f);
		if (textoVisivel.text == textoNaoVisivel.text) {
			textoVisivel.text = "Acertou mizeravi";
			if (encontroEt) {
				anim.SetBool ("andar", false);
				anim.SetBool ("idle", false);
				anim.SetBool ("pular", true);
				pular ();
			}
            playerAudio.PlayOneShot(palavraCerta, 1.0f);
		} else {
			textoVisivel.text = "Errrrrrrou";
            playerAudio.PlayOneShot(palavraErrada, 1.0f);
			//reiniciar o game




        }
		textoNaoVisivel.text = "";
		inputF.text = "";
		timer = 0;
		nextActionTime = Random.Range(tempMin, tempMax);
		escrever = false;

		//trocar gravidade dps de responder palavra

		trocarGravidadePalvra = Random.Range(0.0f, 1.0f);
		if (trocarGravidadePalvra >= 0.5f) {
			upsideDown ();
		}
	}

	void upsideDown(){
		ninjaRB.gravityScale = ninjaRB.gravityScale* -1;
		forca = forca * -1;
		transform.Rotate (new Vector3 (0, 0, 180));
		isUpsideDown = !isUpsideDown;
		Flip ();
        playerAudio.PlayOneShot(trocaGravidade, 1.0f);
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
			pulando = false;
		}
	}

	void OnTriggerEnter2D(Collider2D outro){
		if (outro.gameObject.CompareTag ("etBilu")) {
			encontroEt = true;
			StartCoroutine (readInput ());
		}
	}
	void OnTriggerExit2D(Collider2D outro){
		encontroEt = false;
		anim.SetBool ("andar", true);
		anim.SetBool ("idle", false);
		anim.SetBool ("pular", false);
	}

	void OnCollisionExit2D(Collision2D outro){
		if (outro.gameObject.CompareTag ("chao")) {
			tocandoChao = false;
		}
	}

	//pra fazer ele pular
	public void pular(){
		ninjaRB.AddForce (new Vector2 (10, forca), ForceMode2D.Impulse);
		pulando = true;
	}
}
