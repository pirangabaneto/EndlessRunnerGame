using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManagerChao : MonoBehaviour
{
    public GameObject chaoPrefab; // Pegar o prefab do terreno
    public Vector2 spawnPos = new Vector2(30, -10); // Posição que o terreno vai aparecer
    private float startDelay = 2.0f; // Tempo em segundos até que o terreno comece a aparecer
    private float repeatRate = 4.0f; // Tempo em segundos para o terreno ser spwanado de novo
    private heroi_move heroi_move_script;
    // Start is called before the first frame update

    void Start()
    {
        heroi_move_script = GameObject.Find("ninjaPlayer").GetComponent<heroi_move>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate); //Chama o método "SpawnObstacle" a partir de X segundos e cada Y segundos
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
            Instantiate(chaoPrefab, spawnPos, chaoPrefab.transform.rotation); //Instancia o prefab do terreno, na posição escolhida
    }
}