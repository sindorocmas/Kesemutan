using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        UIManager.instance.showLosePopup();
    }
}