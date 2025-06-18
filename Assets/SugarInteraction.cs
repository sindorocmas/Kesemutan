using UnityEngine;
using TMPro;


public class SugarInteraction : MonoBehaviour
{
    public static int totalSugar = 0;
    public static TMP_Text sugarUIText;

    public float lifetime = 100.0f;
    
    private Rigidbody2D rb;
    private bool hasLanded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasLanded && other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            hasLanded = true;
            
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnMouseDown()
    {
        totalSugar += (int)(1 * DropManager.getMultiplier());

        if (sugarUIText != null)
        {
            sugarUIText.text = "Sugar: " + totalSugar;
        }
        else
        {
            Debug.LogWarning("Sugar UI Text has not been assigned!");
        }

        Destroy(gameObject);
    }
}
