using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    [SerializeField] private Text sugarText; // Gunakan [SerializeField] untuk private variable yang perlu di-assign
    public int sugar = 20;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateSugarUI(); // Update UI saat awal
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SpendSugar(int amount)
    {
        if (sugar >= amount)
        {
            sugar -= amount;
            UpdateSugarUI();
            return true;
        }
        return false;
    }

    public void AddSugar(int amount)
    {
        sugar += amount;
        UpdateSugarUI();
    }

    private void UpdateSugarUI()
    {
        if (sugarText != null) // Null check penting!
        {
            sugarText.text = "Sugar: " + sugar;
        }
        else
        {
            Debug.LogWarning("Sugar Text UI not assigned!");
        }
    }
    public void GameOver()
    {
        Debug.Log("Game Over! Keluar aplikasi...");

        // Beri waktu untuk efek/sound selesai
        Time.timeScale = 0.3f; // Slow motion effect
        Invoke("QuitApplication", 1f);
    }

    private void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}