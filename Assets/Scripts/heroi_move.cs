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
	public float nextActionTime = 10.0f;
  
	void Start () {
		heroiT.GetComponent<Transform> ();
		ninjaRB.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= nextActionTime && tocandoChao) {
			upsideDown ();
			timer = 0;
			nextActionTime = Random.Range(0.0f, 25.0f);
		}
		if (tocandoChao) {
			if (isUpsideDown) {
				transform.Translate (new Vector2 (-vel * Time.deltaTime, 0));
				anim.SetBool ("andar", true);
				anim.SetBool ("idle", false);
			} else {
				transform.Translate (new Vector2 (vel * Time.deltaTime, 0));
				anim.SetBool ("andar", true);
				anim.SetBool ("idle", false);
			}	
		} else {
			anim.SetBool ("andar", false);
			anim.SetBool ("idle", true);
		}
    }

	void upsideDown(){
		transform.Translate (new Vector2 (0, 0));
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
