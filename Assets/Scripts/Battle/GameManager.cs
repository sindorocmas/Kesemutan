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
        }
        else
        {
            Destroy(gameObject);
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