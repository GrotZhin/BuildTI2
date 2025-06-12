using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.InputSystem.EnhancedTouch;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] Player player;
    [SerializeField] Tricks tricks;
  

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
        touchPressAction.performed += TouchPressed;
        touchPressAction.canceled += TouchReleased;
        touchPressAction.performed += TouchStarted;
        touchPressAction.canceled += TouchEnded;
    }
    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
        touchPressAction.canceled -= TouchReleased;
        touchPressAction.performed -= TouchStarted;
        touchPressAction.canceled -= TouchEnded;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 postition = touchPositionAction.ReadValue<Vector2>();


        if (player != null)
        {
            if (player.characterController.isGrounded && postition.x > 0 && postition.x < 1000 && postition.y < 1000 && postition.y > 500)
            {
                player.Jump();
            }
            if (player.characterController.isGrounded && postition.x > 0 && postition.x < 1000 && postition.y < 500 && postition.y > 0)
            {
                player.Slide();
            }
        }
    }
    private void TouchReleased(InputAction.CallbackContext context)
    {
        if (player != null)
        {
            player.ReleaseJump();
        }
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
        if (swipeDirection.magnitude > 50)
        {
            Debug.Log(swipeDirection.normalized);
            SwipeDirectionX(swipeDirection, 0.9f);
            SwipeDirectionY(swipeDirection, 0.9f);

        }
    }
    private void SwipeDirectionX(Vector2 swipeDirection, float normalizedX)
    {
        if (swipeDirection.normalized.x > normalizedX)
        {
            Debug.Log("direita");
            tricks.TrickCerto(3);
           
        }
        if (swipeDirection.normalized.x < -normalizedX)
        {
            Debug.Log("esquerda");
            tricks.TrickCerto(2);
            
        }
       
    }
    private void SwipeDirectionY(Vector2 swipeDirection, float normalizedY)
    {
        if (swipeDirection.normalized.y > normalizedY)
        {
            
            Debug.Log("cima");
            tricks.TrickCerto(0);
            
        }
        if (swipeDirection.normalized.y < -normalizedY)
        {
            Debug.Log("baixo");
            tricks.TrickCerto(1);
           
        }
      
    }

}