using UnityEngine;

public class EnemyManagerTemp : MonoBehaviour
{
    public float speed = 5f;
    public GameObject dropManager;
    public DropManager getItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        getItem = dropManager.GetComponent<DropManager>();
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyDestroyer"))
        {
            getItem.DropItem();
            Debug.Log("Dropped sugar cubes");
            Destroy(gameObject);
        }
    }
}
