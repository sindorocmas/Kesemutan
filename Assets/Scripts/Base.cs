using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int hp = 500;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgradeBase(int amount)
    {
        hp += amount;
    }

    public int getBaseHP() => hp;
}
