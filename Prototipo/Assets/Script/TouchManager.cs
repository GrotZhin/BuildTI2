using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchHandler : MonoBehaviour
{
    public Player player;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();

        Touch.onFingerDown += HandleTouchStart;
        Touch.onFingerUp += HandleTouchEnd;
    }

    private void OnDisable()
    {
        Touch.onFingerDown -= HandleTouchStart;
        Touch.onFingerUp -= HandleTouchEnd;

        EnhancedTouchSupport.Disable();
    }

    private void HandleTouchStart(Finger finger)
    {
        if (player != null)
        {            
            Vector2 pos = player.transform.position;
            float groundDistance = Mathf.Abs(pos.y - player.groundHeight);

            if (player.characterController.isGrounded || groundDistance <= player.jumpGroundTreshhold)
            {
                player.Jump();
            }
        }
    }

    private void HandleTouchEnd(Finger finger)
    {
        if (player != null)
        {
            //player.ReleaseJump();
        }
    }
}