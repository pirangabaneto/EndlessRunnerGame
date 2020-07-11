using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManagerTeto : MonoBehaviour
{
    public GameObject tetoPrefab; // Pegar o prefab do terreno
    public Vector2 spawnPos = new Vector2(30, 4); // Posi��o que o teto vai aparecer
	private float startDelay = 2.0f; // Tempo em segundos at� que o teto comece a aparecer
    private float repeatRate = 5.0f; // Tempo em segundos para o teto ser spwanado de novo
    private heroi_move heroi_move_script;
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
            Instantiate(tetoPrefab, spawnPos, tetoPrefab.transform.rotation); //Instancia o prefab do terreno, na posi��o escolhida
		}
	}
}