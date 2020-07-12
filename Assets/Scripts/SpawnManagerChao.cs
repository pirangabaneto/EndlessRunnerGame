using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManagerChao : MonoBehaviour
{
    public GameObject chaoPrefab; // Pegar o prefab do terreno
    public Vector2 spawnPos = new Vector2(30, -10); // Posi��o que o terreno vai aparecer
	private float startDelay = 2.0f; // Tempo em segundos at� que o terreno comece a aparecer
	private float repeatRate = 1.8f; // Tempo em segundos para o terreno ser spwanado de novo
    public heroi_move heroi_move_script;
    // Start is called before the first frame update

    void Start()
    {
        heroi_move_script = GameObject.Find("ninjaPlayer").GetComponent<heroi_move>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate); //Chama o m�todo "SpawnObstacle" a partir de X segundos e cada Y segundos
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
		if (!heroi_move_script.escrever) {
            Instantiate(chaoPrefab, spawnPos, chaoPrefab.transform.rotation); //Instancia o prefab do terreno, na posi��o escolhida
		}
	}
}