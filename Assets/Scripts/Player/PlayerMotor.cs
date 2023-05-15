using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.UI;


public class PlayerMotor : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public bool respawned = false;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool sprinting;
    private bool crouching;
    private float crouchTimer;
    private bool lerpCrouch = true;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public Button openInventory;
    public InventoryManager inventoryManager;


    public Button pause;
    public Button play;

    private bool isPaused = false;
    private bool insideHaram = false;


    // Start is called before the first frame update
    void Start()
    {
        controller  = GetComponent<CharacterController>();

        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if(crouching)
            {
                speed = 1f;
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                speed = 5f;
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if(p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

        if(respawned)
        {
          if(Input.GetKeyDown(KeyCode.G) && !isPaused) // gia na kanei delete to respawn message apo to screen
          {
            textComponent.text = string.Empty;
            respawned = false;
          }
        }

        if(Input.GetKeyDown(KeyCode.Tab) && !isPaused)
        {
            isPaused = true;
            openInventory.onClick.Invoke();
        }

        PauseGame();
        
        isGrounded = controller.isGrounded;
        Respawn();
    }

    //receive the inputs for InputManager.cs and apply them to character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if(sprinting)
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Respawn()
    {
        if(transform.position.y < -30 && !isGrounded) // otan ginetai teleport prepei na ginei turn off/on o controller
        {
            Vector3 checkPoint = new Vector3(-2, 5, -5); // gia ta kanonika checkpoints pinakas Vector 3 kai analoga ti fasi tou paixnidiou tha kanei teleport se auto to index
            controller.enabled = false;
            transform.position = checkPoint;
            controller.enabled = true;

            //textComponent.enabled = true;
            textComponent.text = "Respawned \n (Press G)";
            //Debug.Log(textComponent.text);
            respawned = true;
        }

    }

    public void SeenPlayer()
    {
        
        Vector3 checkPoint = new Vector3(437, 11, 487); // gia ta kanonika checkpoints pinakas Vector 3 kai analoga ti fasi tou paixnidiou tha kanei teleport se auto to index
        controller.enabled = false;
        transform.position = checkPoint;
        controller.enabled = true;
        //textComponent.enabled = true;
        //textComponent.text = "You were seen !";
        //StartCoroutine(ShowMessage());

    }

     public IEnumerator ShowMessage()
    {
         
          yield return new WaitForSeconds(2);
          textComponent.text = string.Empty;
    }

    public void CantMove()
    {
        //PausePanel.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Movement : Disabled");
    }

    public void CanMove()
    {
        //PausePanel.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Movement : Enabled");
    }

    public void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Debug.Log("PauseGame");
            pause.onClick.Invoke();
        }
        
    }


    public void Pause()
    {
        //PausePanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Continue()
    {
        //PausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        
    }

    public void InteractHaram()
    {
        if(!inventoryManager.HasKeY())
        {
            textComponent.enabled = true;
            textComponent.text = "Find the Key by the Docks";
            StartCoroutine(ShowMessage());
            return;
        }

        if(!insideHaram)
        {
            insideHaram = true;
            Vector3 checkPoint = new Vector3((float)441,(float)14,(float)553.5); // gia ta kanonika checkpoints pinakas Vector 3 kai analoga ti fasi tou paixnidiou tha kanei teleport se auto to index
            controller.enabled = false;
            transform.position = checkPoint;
            controller.enabled = true;
        }
        else
        {
            insideHaram = false;
            Vector3 checkPoint = new Vector3((float)441,(float)11,(float)553.5); // gia ta kanonika checkpoints pinakas Vector 3 kai analoga ti fasi tou paixnidiou tha kanei teleport se auto to index
            controller.enabled = false;
            transform.position = checkPoint;
            controller.enabled = true;
        }
    }

    public void InteractSilversmith()
    {
        if(!inventoryManager.HasCoins())
        {
            textComponent.enabled = true;
            textComponent.text = "You don't have something to melt yet";
            StartCoroutine(ShowMessage());
            return;
        }
    }


}
