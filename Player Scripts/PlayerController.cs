using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Adenovirus {

    // Player specific variables.
    public bool isPlayerControlLocked;

    void Start()
    {
        InitializeVirusPhysicsBodyCollider();
        isPlayerControlLocked = false;

        InitializeVirusSpriteRenderer();
        ChangeSpriteTo_BlueProtein();
        ChangeVirusSpeedToDefault();

        virionCount = 1;
    }

    private void FixedUpdate()
    {
        AllowPlayerMovementControl();
    }

    private void Update()
    {
        PlayerSpacebarControl();
    }

    private void AllowPlayerMovementControl()
    {
        // Allow player to move only if no animation is occuring.
        if (isPlayerControlLocked == false)
        {
            float playerMoveInput_Horizontal = Input.GetAxis("Horizontal");
            float playerMoveInput_Vertical = Input.GetAxis("Vertical");

            // Add force to virus object to move it via player control input.
            Vector2 movement = new Vector2(playerMoveInput_Horizontal, playerMoveInput_Vertical);
            VirusRigidBody2D.AddForce(movement * virusMovementSpeed);
        }
    }

    private void PlayerSpacebarControl()
    {
        // Enter cell with space
        if (Input.GetKeyDown(KeyCode.Space) && touchingInfectableCell == true && insideCell != true)
        {
            // Player sprite change to enveloped virus to enter cell.
            isPlayerControlLocked = true;
            ChangeSpriteToEnveloped();
            VirusColliderOff();
            ChangeVirusSpeedToEnterCell();
            PullVirusIntoCell();
        }

        if (Input.GetKeyDown(KeyCode.Space) && touchingCellNucleus == true && insideCell == true)
        {
            ChangeVirusColliderRadiusToDNA();
            ChangeSpriteToDNA();
        }
    }

    // Trigger Touching Infectable Cell.
    private void OnTriggerStay2D(Collider2D collidorInfectableCell)
    {
        // If colliding with an infectable cell wall.
        if (collidorInfectableCell.gameObject.CompareTag("Epithelial Cell") || collidorInfectableCell.gameObject.CompareTag("Cell Nucleus")) // Refactor latter "Cell Nucleus"
        {
            touchingInfectableCell = true;
            currentContactedObject = collidorInfectableCell.gameObject;

            Debug.Log("Touching Infectable Cell = true");
        }

        else
        {
            touchingInfectableCell = false;
        }
    }

    // Trigger Touching Cell Nucleus.
    private void OnTriggerEnter2D(Collider2D triggerCellNucleus)
    {
        Debug.Log("TriggerCellNucleus triggered.");

        if (triggerCellNucleus.gameObject.CompareTag("Cell Nucleus"))
        {
            touchingCellNucleus = true;

            currentContactedObject = triggerCellNucleus.gameObject;

            Debug.Log("Touching Cell Nucleus = true");
        }

        else
        {
            touchingCellNucleus = false;
        }
    }

    // Trigger exit Touching Infectable Cell.
    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingInfectableCell = false;
        isPlayerControlLocked = false;
        Debug.Log("Touching Infectable Cell = false");
    }

    private void PullVirusIntoCell()
    {
        // Determine vector between touched cell and player.
        var touchedCellHeading = currentContactedObject.transform.position - transform.position;
        var distanceToCellCenter = touchedCellHeading.magnitude;
        Debug.Log("Distance to touched cell center: " + distanceToCellCenter);

        startTimeVirusPullIntoCell = Time.time;
        float endTime = startTimeVirusPullIntoCell + 3;

        StartCoroutine(MoveVirusThroughCell(touchedCellHeading, endTime));
    }

    private IEnumerator MoveVirusThroughCell(Vector3 touchedCellHeading, float pullVirusAnimationEndTime)
    {
        while (Time.time < pullVirusAnimationEndTime)
        {
            // Apply force to player to pull player into cell. Need to do this only as long as it takes to move player into cell.
            VirusRigidBody2D.AddForce(touchedCellHeading * 0.03f);
            yield return null;
        }

        Debug.Log("Sucessfully exited MoveVirusThroughCell Coroutine.");

        // Player regains control once animation complete. Switch player to naked virus capsid sprite.
        VirusColliderOn();
        isPlayerControlLocked = false;
        ChangeVirusSpeedToDefault();
        ChangeSpriteToNakedCapsid();
        ChangeVirusColliderRadiusToAdenovirus();
        insideCell = true;
    }

    private void GivePlayerBackControl()
    {
        isPlayerControlLocked = false;
    }
}
