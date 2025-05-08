using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchHandler : MonoBehaviour
{
    private CharacterController characterController;
    public Player player;

    void Start()
    {
        characterController= GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
     
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
            // Só pula se estiver no chão (igual ao teclado)
            Vector2 pos = player.transform.position;
            float groundDistance = Mathf.Abs(pos.y - player.groundHeight);

            if (characterController.isGrounded || groundDistance <= player.jumpGroundTreshhold)
            {
                player.Jump();
            }
        }
    }

    private void HandleTouchEnd(Finger finger)
    {
        if (player != null)
        {
            player.ReleaseJump();
        }
    }
}
