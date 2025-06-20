using UnityEngine;

public class independentRotation : MonoBehaviour
{
    private Quaternion initialWorldRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialWorldRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = initialWorldRotation;
    }
}
