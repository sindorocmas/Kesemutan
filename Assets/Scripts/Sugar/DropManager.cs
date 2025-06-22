using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager instance;
    public GameObject sugarDrop;

    //jumlah yang didrop
    private int itemNum;

    //enemy type untuk berapa yang didrop
    private float enemyNum;

    //multiplier yang bisa di upgrade
    [SerializeField] private float force;
    [SerializeField] private float dropMultiplier = 5;
    private float dropMultiplierDiff;
    public int dropMultiplierPrice;
    private int dropUpgradeLevel;
    public int totalSugarDropped = 0;

    private void Start()
    {
        instance = this;
        dropMultiplierDiff = 1.5f;
        dropMultiplierPrice = 50;
        dropUpgradeLevel = 1;
    }

    public void upgradeDropRate()
    {
        if (SugarManager.instance.SpendSugar(dropMultiplierPrice))
        {
            if (dropUpgradeLevel < 3)
            {
                dropMultiplier *= dropMultiplierDiff;

                if (dropMultiplierDiff > 1.2)
                {
                    dropMultiplierDiff -= 0.1f;
                }

                dropMultiplierPrice += 50;
                dropUpgradeLevel++;
            }
            else
            {
                Debug.Log("Reached maximum drop rate level: 3");
                SugarManager.instance.AddSugar(dropMultiplierPrice);
            }
        }

        Debug.Log("Drop mutliplier rn: " + dropMultiplier);
    }

    public void DropItem(Vector3 enemyPosition, EnemyType enemyType)
    {
        int itemsToDropThisCall = 0;

        switch (enemyType)
        {
            case EnemyType.Grasshopper:
                itemsToDropThisCall = 1;
                break;

            case EnemyType.Spider:
                itemsToDropThisCall = 3;
                break;

            case EnemyType.Scorpion:
                itemsToDropThisCall = 5;
                break;

            default:
                itemsToDropThisCall = 0;
                Debug.Log("Enemy not recognized");
                break;
        }

        if (itemsToDropThisCall > 0)
        {
            Debug.Log("Dropping " + itemsToDropThisCall + " sugar cube.");
            for (int i = 0; i < itemsToDropThisCall; i++)
            {
                GameObject newDrop = Instantiate(sugarDrop, enemyPosition, Quaternion.identity);
                Rigidbody2D rb = newDrop.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    float randomX = Random.Range(-0.5f, 0.5f);
                    float randomY = Random.Range(0.8f, 1.5f);
                    Vector2 randomDirection = new Vector2(randomX, randomY).normalized;

                    rb.AddForce(randomDirection * force, ForceMode2D.Impulse);
                }

            }
            int sugarAdded = (int)(itemsToDropThisCall * dropMultiplier);
            Debug.Log("Total dropped : " + sugarAdded);
        }
    }

    public float GetDropValue()
    {
        return dropMultiplier;
    }
}
