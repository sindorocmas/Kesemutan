using UnityEngine;

public class DropManager : MonoBehaviour
{
    public GameObject sugarDrop;

    //jumlah yang didrop
    private int itemNum;

    //enemy type untuk berapa yang didrop
    private float enemyNum;

    //multiplier yang bisa di upgrade
    private static float dropMultiplier;
    private static float dropMultiplierDiff;
    private Transform enemyPos;
    public static int totalSugarDropped = 0;

    private void Start()
    {
        enemyPos = GetComponent<Transform>();
        dropMultiplier = 5;
        dropMultiplierDiff = 1.5f;
    }

    public void upgradeDropRate()
    {
        dropMultiplier *= dropMultiplierDiff;
        if (dropMultiplierDiff > 1.2)
        {
            dropMultiplierDiff -= 0.1f;
        }
        Debug.Log("Drop mutliplier rn: " + dropMultiplier);
    }

    public void DropItem()
    {
        enemyNum = Random.Range(1, 4);
        Debug.Log("Random Enemy Number is " + enemyNum);

        int itemsToDropThisCall = 0;

        switch (enemyNum)
        {
            case 1:
                itemsToDropThisCall = 1;
                break;
            case 2:
                itemsToDropThisCall = 2;
                break;
            case 3:
                itemsToDropThisCall = 3;
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
                Instantiate(sugarDrop, transform.position, Quaternion.identity);
            }
            int sugarAdded = (int)(itemsToDropThisCall * dropMultiplier);
            Debug.Log("Total dropped : " + sugarAdded);
        }
    }

    public static float getMultiplier()
    {
        return dropMultiplier;
    }
}
