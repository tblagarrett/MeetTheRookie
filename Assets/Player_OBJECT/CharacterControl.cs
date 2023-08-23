using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [HideInInspector]
    public Player_Interaction interaction;

    public float playerSpeed=5f;
    private Vector3 target;
    private Vector3 cameraTarget;
    public bool arrowKeys = true;
    public bool inventoryOpen = false;
    //public Inventory inventory;
    public GameObject inventoryParent;
    public GameObject cam;
    public Animator animator;
    public float colliderOffset;
    
    public KeyCode inventoryKey = KeyCode.I;
    // Start is called before the first frame update

    public GameObject floor;
    public bool ignoringInputs;
    void Start()
    {
        interaction = GetComponentInChildren<Player_Interaction>();

        target = transform.position;
        cameraTarget=cam.transform.position;
        inventoryParent.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        colliderOffset = GetComponent<BoxCollider2D>().offset.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(!ignoringInputs){
            InputHandler();
        }
    }
    

    #region <<INPUT>>
    public void InputHandler(){
        if (Input.GetKeyDown(inventoryKey))
        {
            inventoryOpen = !inventoryOpen;
            inventoryParent.SetActive(inventoryOpen);
            Debug.Log("SETTING INVENTORY PARENT TO: " + inventoryOpen);
            //set inventory active
        }

        if(!inventoryOpen){
            //HandleMovement();
            if(Input.GetKeyDown(KeyCode.E)){
                interaction.InteractWithObject();
            }
        }
    }


    #endregion
    #region <<MOVEMENT>>
    //RIGHT POSIVE LEFT NEGATIVE
    //SPRITE FACES LEFT BY DEFAULT
    public void HandleMovement(Vector2 input){
        Vector3 MoveDirection = Vector2.zero;
        MoveDirection.x = input.x;
        target.x+=MoveDirection.x * playerSpeed*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target , playerSpeed*Time.deltaTime);
        Vector2 offset = new Vector2(GetComponent<BoxCollider2D>().offset.x, GetComponent<BoxCollider2D>().offset.y);

        switch (input.x){
            case -1: //walk left
                GetComponentInChildren<SpriteRenderer>().flipX = false;
                offset.x= colliderOffset;
                GetComponent<BoxCollider2D>().offset=offset;
                animator.Play("walking");
                break;
            case 1: //walk right
                GetComponentInChildren<SpriteRenderer>().flipX = true;
                offset.x= colliderOffset*-1;
                GetComponent<BoxCollider2D>().offset=offset;
                animator.Play("walking");
                break;
            case 0: //no walk
                animator.Play("idle");
                break;
        }
    }

    public void MoveUpLadder(Vector3 position){
        ignoringInputs = true;
        //LOGIC FOR ANIMATION HERE
        transform.position = position;
        target.y = transform.position.y;
        ignoringInputs = false;
    }
    public void MoveDownLadder(Vector3 position){
        ignoringInputs = true;
        //LOGIC FOR ANIMATION HERE
        transform.position = position;
        target.y = transform.position.y;
        ignoringInputs = false;
    }
    #endregion

}
