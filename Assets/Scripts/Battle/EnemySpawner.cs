using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    //list berisi Scriptable Object wave
    [SerializeField] private List<WaveData> waves;

    //untuk iterasikan wave yang akan dimulai
    private int currentWaveIndex = -1;
    private bool isSpawning = false;

    //untuk cek jumlah enemy yang hidup --> menentukan wave selesai atau belum
    private int totalEnemiesInWave;
    private int enemiesRemaining;
    public static EnemySpawner instance;

    [Header("UI Management")] 
    public UIManager ui;
    public PlayerBase playerBase;

    private void Start()
    {
        ui.hideUpgrade();
        ui.hideSummary();
        instance = this;
        // Mulai wave pertama otomatis
        StartNextWave();
    }

    private void Update()
    {
        // Jika wave selesai dan player menekan spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartNextWave();
        }

    }

    public void StartNextWave()
    {
        if (isSpawning) return;

        isSpawning = true;
        //tutup upgrade panel
        ui.hideUpgrade();
        //lanjut ke wave selanjutnya di list
        currentWaveIndex++;

        Debug.Log("Memulai Wave " + (currentWaveIndex + 1));

        if (currentWaveIndex < waves.Count)
        {
            //lanjut ke index wave berikutnya di list
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));

            if (SugarManager.instance.totalSugar < 50)
            {
                //negative feedback --> kompensasi sugar di awal wave
                SugarManager.instance.AddSugar(75);
            }
            else
            {

                SugarManager.instance.AddSugar(25);
            }
        }
        else
        {
            Debug.Log("Semua wave selesai!");
        }
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        Debug.Log("Memulai Wave " + (currentWaveIndex + 1));
        totalEnemiesInWave = 0;
        foreach (var action in wave.actions)
        {
            totalEnemiesInWave += action.count;
        }
        enemiesRemaining = totalEnemiesInWave;
        ui.UpdateEnemyProgress(0, totalEnemiesInWave);
        
        Debug.Log("Enemy remaining" + enemiesRemaining);
        Debug.Log("Total enemies in wave " + totalEnemiesInWave);
        foreach (var action in wave.actions)
        {
            yield return new WaitForSeconds(action.delayBeforeAction);

            for (int i = 0; i < action.count; i++)
            {
                SpawnEnemy(action.enemyPrefab);
                yield return new WaitForSeconds(action.spawnInterval);
            }
        }

        isSpawning = false;
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

    public void EnemyDied()
    {
        enemiesRemaining--;

        int enemiesKilled = totalEnemiesInWave - enemiesRemaining;
        ui.UpdateEnemyProgress(enemiesKilled, totalEnemiesInWave);
        
        Debug.Log("Enemy remaining" + enemiesRemaining);
        Debug.Log("Total enemies in wave " + totalEnemiesInWave);

        //wave dicek selesai atau tidak berdasarkan status spawning dan jumlah enemy
        if (enemiesRemaining <= 0)
        {
            WaveFinished();
        }
    }

    private void WaveFinished()
    {
        isSpawning = false;
        if (currentWaveIndex < waves.Count - 1)
        {
            //tampilkan upgrade panel setelah delay untuk kumpulkan sugar
            StartCoroutine(ShowUpgradePanel());
        }
        else
        {
            ui.showSummary(countCoins(), countStars());
            int totalCoins = PlayerPrefs.GetInt("Coins", 0) + countCoins();
            PlayerPrefs.SetInt("Coins", totalCoins);
        }

        Debug.Log("Wave " + (currentWaveIndex + 1) + " selesai");
        Debug.Log("Wave total: " + (waves.Count));
    }

    IEnumerator ShowUpgradePanel()
    {
        yield return new WaitForSeconds(3f);
        ui.showUpgrade();
    }

    private int countStars()
    {
        float healthPercent = playerBase.getHealthPercent();
        if (healthPercent > 0.8)
        {
            return 3;
        }
        else if (healthPercent > 0.5)
        {
            return 2;
        }
        else if (healthPercent > 0.2)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private int countCoins()
    {
        return SugarManager.instance.totalSugar;
    }
}