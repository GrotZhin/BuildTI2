using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchStarted;
        touchPressAction.canceled += TouchEnded;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchStarted;
        touchPressAction.canceled -= TouchEnded;
    }

    private void TouchStarted(InputAction.CallbackContext context)
    {
        startTouchPosition = touchPositionAction.ReadValue<Vector2>();
        isSwiping = true;
    }

    private void TouchEnded(InputAction.CallbackContext context)
    {
        endTouchPosition = touchPositionAction.ReadValue<Vector2>();
        isSwiping = false;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        Vector2 swipeDirection = endTouchPosition - startTouchPosition;
        if (swipeDirection.magnitude > 50) // Ajuste o valor conforme necessário
        {
            Debug.Log("Swipe detected: " + swipeDirection.normalized);
            Vector3 startWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(startTouchPosition.x, startTouchPosition.y, 10));
            Vector3 endWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(endTouchPosition.x, endTouchPosition.y, 10));
            Debug.DrawLine(startWorldPosition, endWorldPosition, Color.red, 2.0f);
            // Adicione aqui a lógica para o que deve acontecer após o swipe
        }
    }
}