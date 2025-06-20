using UnityEngine;

public class SugarDrop : MonoBehaviour
{
    [Header("Settings")]
    public float lifetime = 15.0f; 
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    void OnMouseDown()
    {
        int value = (int)(1 * DropManager.getMultiplier());
        SugarManager.instance.AddSugar(value);

        Destroy(gameObject);
    }
}