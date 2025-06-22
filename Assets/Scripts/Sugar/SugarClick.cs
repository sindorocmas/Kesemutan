using UnityEngine;

public class SugarClick : MonoBehaviour
{
    [SerializeField] private LayerMask clickableLayers;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                mainCamera.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero,
                Mathf.Infinity,
                clickableLayers
            );

            if (hit.collider != null)
            {
                SugarDrop sugarDrop = hit.collider.GetComponent<SugarDrop>();

                if (sugarDrop != null)
                {
                    sugarDrop.Collect();
                }
            }
        }
    }
}
