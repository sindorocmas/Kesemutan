using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject grasshopperPrefab;
    public GameObject spiderPrefab;
    public GameObject scorpionPrefab;

    private int currentWave = 0;
    private bool isSpawning = false;
    private bool waveCompleted = true; // Flag untuk menandai wave selesai

    private void Start()
    {
        // Pindahkan spawner ke kanan layar
        float spawnX = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x;
        transform.position = new Vector2(spawnX, transform.position.y);

        // Mulai wave pertama otomatis
        StartNextWave();
    }

    private void Update()
    {
        // Jika wave selesai dan player menekan spacebar
        if (waveCompleted && Input.GetKeyDown(KeyCode.Space))
        {
            StartNextWave();
        }
    }

    public void StartNextWave()
    {
        if (isSpawning) return;

        waveCompleted = false;
        currentWave++;
        isSpawning = true;

        Debug.Log("Memulai Wave " + currentWave);

        switch (currentWave)
        {
            case 1:
                StartCoroutine(SpawnWave1());
                break;
            case 2:
                StartCoroutine(SpawnWave2());
                break;
            case 3:
                StartCoroutine(SpawnWave3());
                break;
            default:
                Debug.Log("Semua wave selesai!");
                waveCompleted = true;
                break;
        }
    }

    IEnumerator SpawnWave1()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy(grasshopperPrefab);
            yield return new WaitForSeconds(2f);
        }
        WaveFinished();
    }

    IEnumerator SpawnWave2()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy(grasshopperPrefab);
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < 2; i++)
        {
            SpawnEnemy(spiderPrefab);
            yield return new WaitForSeconds(3f);
        }
        WaveFinished();
    }

    IEnumerator SpawnWave3()
    {
        for (int i = 0; i < 8; i++)
        {
            SpawnEnemy(grasshopperPrefab);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(2f);

        SpawnEnemy(spiderPrefab);
        yield return new WaitForSeconds(2f);

        SpawnEnemy(scorpionPrefab);
        yield return new WaitForSeconds(4f);

        SpawnEnemy(spiderPrefab);
        WaveFinished();
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.transform.localScale = new Vector3(
            -Mathf.Abs(enemy.transform.localScale.x),
            enemy.transform.localScale.y,
            enemy.transform.localScale.z
        );
    }

    private void WaveFinished()
    {
        isSpawning = false;
        waveCompleted = true;
        Debug.Log("Wave " + currentWave + " selesai! Tekan SPACE untuk wave berikutnya");
    }
}