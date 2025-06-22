    using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float keyboardPanSpeed = 5;
    [SerializeField] private InputActionReference moveAction;

    void Start()
    {

    }

    void onEnable()
    {
        moveAction.action.Enable();
    }

    void onDisable()
    {
        moveAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = moveAction.action.ReadValue<Vector2>();
        float horizontalInput = inputVector.x;
        Vector3 moveAmount = new Vector3(horizontalInput, 0, 0) * keyboardPanSpeed * Time.deltaTime;
        cameraTarget.position += moveAmount;
    }
}
