using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public float tempMin = 0.0f;
	public float tempMax = 2.0f;
  
	void Start () {
		heroiT.GetComponent<Transform> ();
		ninjaRB.GetComponent<Rigidbody2D> ();

		nextActionTime = 2;
	}
	
	// Update is called once per frame
	void Update () {
		//upsideDown ();
		timer += Time.deltaTime;
		if (timer >= nextActionTime) {
			upsideDown ();
			timer = 0;
			nextActionTime = Random.Range(tempMin, tempMax);
		}
			
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
