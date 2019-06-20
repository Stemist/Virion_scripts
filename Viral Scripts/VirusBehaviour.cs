using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Virus : MonoBehaviour {

    #region variables
    public Rigidbody2D VirusRigidBody2D;
    protected CircleCollider2D CircleCollider;
    protected GameObject currentContactedObject;
    protected GameObject BasicInfectableCell;
    protected float startTimeVirusPullIntoCell;

    // State variables.
    protected bool insideCell;
    public bool touchingInfectableCell;
    protected bool touchingCellNucleus;
    protected bool replicating;
    protected int VirusCurrentInteractState;
    protected int VirusCurrentProteinState;

    protected int virionCount;

    // Movement variables.
    protected float virusMovementSpeed;
    protected float radiusCollider;
    protected float defaultVirusSpeed = 100;
    protected float enterCellVirusSpeed = 10;

    // Size variables.
    protected float radiusDNA = 0.5f;
    protected float radiusAdenovirus = 4.55f;

    // Sprite variables.
    public SpriteRenderer spriteRenderer;
    public Sprite dnaSprite;
    #endregion

    // Constructor
    public Virus()
    {
        
    }

    // Enums
    protected enum VirusProteinStates
    {
        canEnterEpethilialCell,
        canEnterImmuneCell,
        canEnterAllAnimalCell,
        canEnterBacterialCell,
    }


    // Init Methods
    protected void InitializeVirusPhysicsBodyCollider()
    {
        VirusRigidBody2D = GetComponent<Rigidbody2D>();
        CircleCollider = GetComponent<CircleCollider2D>();
    }

    protected void InitializeVirusSpriteRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
            Debug.Log("No sprite found.");
    }

    // Sprite Methods
    protected abstract void ChangeSpriteToEnveloped();

    protected abstract void ChangeSpriteTo_BlueProtein();

    protected abstract void ChangeSpriteTo_WhiteProtein();

    protected abstract void ChangeSpriteTo_RedProtein();
    
    protected virtual void ChangeSpriteToDNA()
    {
        spriteRenderer.sprite = dnaSprite;
    }

    // Physics Methods
    protected virtual void VirusColliderOn()
    {
        CircleCollider.enabled = true;
    }

    protected virtual void VirusColliderOff()
    {
        CircleCollider.enabled = false;
    }

    protected void ChangeVirusColliderRadiusToDNA()
    {
        GetComponent<CircleCollider2D>().radius = radiusDNA;
    }

    protected void ChangeVirusSpeedToDefault()
    {
        virusMovementSpeed = defaultVirusSpeed;
    }

    protected void ChangeVirusSpeedToEnterCell()
    {
        virusMovementSpeed = enterCellVirusSpeed;
    }

    // A virus touches a contact replication enzyme complex to start replication.
    private void OnTriggerEnter2D(Collider2D triggerReplication)
    {
        Debug.Log("Virus replication complex triggered.");

        if (triggerReplication.gameObject.CompareTag("DNAReplicateProtein") || replicating == false)
        {
            replicating = true;
            Debug.Log("Replication of virus {0}", virionCount);
            currentContactedObject = triggerReplication.gameObject;
        }

        else
        {
            
        }
    }
}

public class Adenovirus : Virus {

    // Sprite references.
    public Sprite envelopedAdenovirusSprite;
    public Sprite blue_InfectEpethelial_AdenovirusSprite;
    public Sprite white_InfectImmune_AdenovirusSprite;
    public Sprite red_InfectBCell_AdenovirusSprite;
    public Sprite nakedAdenovirusCapsidSprite;
    public Sprite ReplicationAdenovirus;

    // Constructor.
    public Adenovirus()
    {
        
    }

    public void SpawnAdenovirus()
    {
        //Instantiate(prefab)
    }

    protected override void ChangeSpriteTo_BlueProtein()
    {
        spriteRenderer.sprite = blue_InfectEpethelial_AdenovirusSprite;
    }

    protected override void ChangeSpriteTo_WhiteProtein()
    {
        spriteRenderer.sprite = white_InfectImmune_AdenovirusSprite;
    }

    protected override void ChangeSpriteTo_RedProtein()
    {
        spriteRenderer.sprite = red_InfectBCell_AdenovirusSprite;
    }

    protected override void ChangeSpriteToEnveloped()
    {
        spriteRenderer.sprite = envelopedAdenovirusSprite;
    }

    public void ChangeSpriteToNakedCapsid()
    {
        spriteRenderer.sprite = nakedAdenovirusCapsidSprite;
    }

    protected void ChangeSpriteToReplicationComplex()
    {
        spriteRenderer.sprite = ReplicationAdenovirus;
    }

    public void ChangeVirusColliderRadiusToAdenovirus()
    {
        GetComponent<CircleCollider2D>().radius = radiusAdenovirus;
    }

}