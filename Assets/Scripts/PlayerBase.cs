using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [Header("Unit Spawning")]
    public GameObject workerPrefab;
    public GameObject soldierPrefab;
    public GameObject dronePrefab;
    public Transform spawnPoint;

    [Header("Base Health")]
    public int maxHealth = 200;
    public int currentHealth;
    public float baseSize = 3f;
    public HealthBar healthBar;

    [Header("Effects")]
    public ParticleSystem damageEffect;
    public AudioClip damageSound;

    private void Start()
    {
        // Inisialisasi posisi base
        float baseX = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, 0)).x;
        transform.position = new Vector2(baseX, transform.position.y);

        // Setup spawn point jika belum ada
        if (spawnPoint == null)
        {
            CreateSpawnPoint();
        }

        // Inisialisasi health
        currentHealth = maxHealth;
        UpdateHealthUI();

        // Tambahkan collider otomatis
        SetupBaseCollider();
    }

    private void CreateSpawnPoint()
    {
        GameObject spawnObj = new GameObject("SpawnPoint");
        spawnPoint = spawnObj.transform;
        spawnPoint.SetParent(transform);
        spawnPoint.localPosition = new Vector2(1.5f, 0);
    }

    private void SetupBaseCollider()
    {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(baseSize, baseSize);
        collider.isTrigger = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        // Play effects
        if (damageEffect != null) damageEffect.Play();
        if (damageSound != null) AudioSource.PlayClipAtPoint(damageSound, transform.position);

        Debug.Log($"Base damaged! Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            BaseDestroyed();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.UpdateHealth(currentHealth, maxHealth);
        }
    }

    private void BaseDestroyed()
    {
        Debug.Log("Player Base Destroyed!");
        // Tambahkan efek ledakan/animasi
        Destroy(gameObject);

        // Trigger game over
        GameManager.instance.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyUnit enemy = collision.GetComponent<EnemyUnit>();
            if (enemy != null)
            {
                TakeDamage(enemy.attackDamage);
                //enemy.Die(); // Musuh hancur setelah menyerang base
            }
        }
    }

    // Unit Spawning Methods
    public void SpawnWorker()
    {
        if (GameManager.instance.SpendSugar(5))
        {
            Instantiate(workerPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public void SpawnSoldier()
    {
        if (GameManager.instance.SpendSugar(7))
        {
            Instantiate(soldierPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public void SpawnDrone()
    {
        if (GameManager.instance.SpendSugar(4))
        {
            Instantiate(dronePrefab, spawnPoint.position, Quaternion.identity);
        }
    }

   // public List<AntUnit> spawnedUnits = new List<AntUnit>();

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SpawnWorker();
        if (Input.GetKeyDown(KeyCode.Alpha2)) SpawnSoldier();
        if (Input.GetKeyDown(KeyCode.Alpha3)) SpawnDrone();
    }
}