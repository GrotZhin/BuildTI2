
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using RWM;
using DG.Tweening;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{

    private CharacterController characterController;
    public Camera CAM;
    public PowerUp powerUp;
    public float gravity;
    public Vector2 speed;
    public int score = 0;
    public float maxXSpeed = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;
    public int trickPoint;

    public float jumpSpeed = 20;
    public float groundHeight = 10;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float maxHoldJump = 0.4f;
    public float jumpTimer = 0.0f;
    public float jumpGroundTreshhold = 1;

    float groundDistance;
    public bool assist = false;

    bool slider = false;
    float sliderTimer;
    public GameObject sekker;
    public bool sekkerInstantiate = false;

    //Animation
    public GameObject Ranna;
    public Animator Ranani;
    public GameObject JumpPP;
    public GameObject SlidePP;
    public GameObject PWPP;
    public Transform RannaT;
    Vector3 PP;
    public float fbkTimer;
    public float SldTimer;

    //reactions
    public GameObject comJ;
    public GameObject comS;
    public Transform ReDad;


    public bool isDead = false;
    public bool cheat = false;
    public bool isGrind = false;
    GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        powerUp = GameObject.Find("GM").GetComponent<PowerUp>();

        Ranani = Ranna.GetComponent<Animator>();
        Ranani.SetLayerWeight(0, 1);
        Ranani.SetLayerWeight(1, 0);

    }

    // Update is called once per frame
    private void Update()
    {
        PP = new Vector3(characterController.transform.position.x, characterController.transform.position.y - 0.7f, characterController.transform.position.z);
        Vector2 pos = transform.position;

        ChangeLayersWeight();
        if (characterController.isGrounded)
        {

            Ranani.SetBool("JumpTricks", false);
            
            Ranani.SetBool("FallBack1", true);
            fbkTimer += Time.deltaTime;
            if (fbkTimer >= 2)
            {
                Ranani.SetBool("FallBack1", false);
                fbkTimer = 0;
            }

        }

        if (!sekkerInstantiate)
        {
            if (speed.x <= 7)
            {
                sekkerInstantiate = true;
                ISekker();

            }

        }

        groundDistance = Mathf.Abs(pos.y - groundHeight);

        if (slider)
        {
            sliderTimer += Time.deltaTime;
            if (sliderTimer >= 1.3f)
            {
                characterController.height = 2.0f;
                slider = false;
                sliderTimer = 0;
                Ranani.SetBool("SlideTrick", false);
                Ranna.transform.position = new Vector3(transform.position.x, transform.position.y + 0.32f, transform.position.z);
                PP = new Vector3(characterController.transform.position.x, characterController.transform.position.y - 0.7f, characterController.transform.position.z);

            }

        }
       
        if (characterController.isGrounded || groundDistance <= jumpGroundTreshhold)
        {


            // Teclado
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();



            }
        }
        if (!slider)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Slide();

            }
        }
        // Teclado
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ReleaseJump();
        }
    }
    public void Jump()
    {
        assist = true;
        speed.y = Mathf.Sqrt(jumpSpeed * -2.0f * gravity);
        soundManager.PlaySound(SoundType.Jump);

        Ranani.SetBool("SlideTrick", false);
        Ranani.SetBool("FallBack", false);
        Ranani.SetInteger("JumpTrickIndex", Random.Range(0, 6));
        Ranani.SetBool("JumpTricks", true);
        Ranani.SetBool("GrindTrick", false);

        Instantiate(JumpPP, PP, Quaternion.identity);

    }
    public void Slide()
    {
        slider = true;
        Ranna.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        PP = new Vector3(characterController.transform.position.x, characterController.transform.position.y - 0.2f, characterController.transform.position.z);
        characterController.height = 0.7f;
        Instantiate(SlidePP, PP, Quaternion.identity, RannaT);
        Ranani.SetInteger("SlideTrickIndex", Random.Range(0, 3));
        Ranani.SetBool("SlideTrick", true);
        soundManager.PlaySound(SoundType.Slide);

    }


    public void ReleaseJump()
    {
        isHoldingJump = false;
    }

    private void FixedUpdate()
    {
    
        Vector2 pos = transform.position;

        if (isDead)
        {
            return;
        }

        if (pos.y <= 3 && cheat == false)

        {
            isDead = true;
            speed.x = 0;
            Debug.Log("cheat off");
        }
         if (pos.y <= 3 && cheat == true)
        {
            Debug.Log("cheat on");
            speed.y = groundHeight + 20;
            speed.x = 5;

        }
        if (isHoldingJump)
        {
            jumpTimer += Time.fixedDeltaTime;
            if (jumpTimer >= maxHoldJumpTime)
            {
                isHoldingJump = false;

            }
        }
        if (!characterController.isGrounded)
        {
            pos.y += speed.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                speed.y += gravity * Time.fixedDeltaTime;
            }
            Ranani.SetBool("FallBack1", false);
            Ranani.SetBool("JumpTricks", true);
            Ranani.SetBool("GrindTrick", false);

        }

        distance += pos.x * Time.fixedDeltaTime;

        if (characterController.isGrounded)
        {

            speed.x += acceleration * Time.fixedDeltaTime;
            float speedRatio = speed.x / maxXSpeed;
            acceleration = maxAcceleration * (1 - speedRatio);
            maxHoldJumpTime = maxHoldJump * speedRatio;

            if (speed.x >= maxXSpeed)
            {
                speed.x = maxXSpeed;
            }

        }
   
        characterController.Move(new Vector2(speed.x, speed.y) * Time.deltaTime);

    }
    void ISekker()
    {
        Vector2 pos = transform.position;
        Vector2 sekkerPos = new Vector2(pos.x - 5, pos.y);
        prefab = Instantiate(sekker, sekkerPos, Quaternion.identity);
    }

    public void HitObstacle(Obstacle obstacle)
    {
        obstacle.boxCollider.enabled = false;
        speed.x *= 0.8f;
        soundManager.PlaySound(SoundType.Hit);
        Ranani.SetTrigger("Hit");
        CAM.DOShakeRotation(0.3f, 4, 2, 1, true);
    }
    void HitPowerUp(Batery batery)
    {
        Destroy(batery.gameObject);
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Vector3 pos = transform.position;
        Ground ground = hit.collider.GetComponent<Ground>();
        Grind grind = hit.collider.GetComponent<Grind>();

        /* if (ground == null && hit.moveDirection == Vector3.up)
         {
             groundHeight = ground.groundHeight + 0.35f;
             pos.y = groundHeight;
             speed.y = 0;
             isGrind = false;
             Debug.Log(isGrind);
         }*/

        if (ground == null && hit.moveDirection == Vector3.right)
        {
            Debug.Log("atingiuX");
            speed.x = 5;
        }
        
       /*if (grind == null && hit.moveDirection == Vector3.up)
         {
             groundHeight = grind.groundHeight + 0.35f;
             pos.y = groundHeight;
             speed.y = 0;
             //transform.rotation = grind.transform.rotation;
             isGrind = true;
             Debug.Log(isGrind);
         }*/

    }
   
    void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            HitObstacle(obstacle);
        }

        if (other.gameObject.CompareTag("Glass"))
        {
            CAM.DOShakeRotation(0.3f, 4, 2, 1, true);
        }

        if (other.gameObject.CompareTag("Score"))
        {
            score += 10;

            int randob = Random.Range(0, 10);
            if (randob >= 5)
            {
                Instantiate(comJ, ReDad.position, Quaternion.identity, ReDad);
                soundManager.PlaySound(SoundType.Plamn);
            }
        }
        if (other.gameObject.CompareTag("SlideScore"))
        {
            score += 10;

            int randob = Random.Range(0, 10);
            if (randob >= 5)
            {
                Instantiate(comS, ReDad.position, Quaternion.identity, ReDad);
                soundManager.PlaySound(SoundType.Plamn);
            }
        }

        Batery batery = other.gameObject.GetComponent<Batery>();
        if (batery != null)
        {
            HitPowerUp(batery);
            powerUp.BateryFill();
            Instantiate(PWPP, transform.position, Quaternion.identity, RannaT);
        }
    }
    
    public void Cheat()
    {
        cheat = !cheat;

    }

    #region animations

    public void ChangeLayersWeight()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {

            Ranani.SetLayerWeight(1, 1);
            Ranani.SetLayerWeight(0, 0);
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {

            Ranani.SetLayerWeight(1, 0);
            Ranani.SetLayerWeight(0, 1);
        }

    }


    #endregion



}
