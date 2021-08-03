using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] Camera mainCamera = null;
    Rigidbody2D shipRigidBody2D = null;
    Transform shootingPoint;
    [SerializeField]GameObject engineThrustAnimatorObject = null;
    SpriteRenderer engineThrustsSprite;
    Animator engineThrustAnimator;
    [SerializeField]Transform leftBorder = null, rightBorder = null, topBorder = null, bottomBorder = null;
    [SerializeField]HUDController hudController = null;
    SoundController soundController;
    AudioSource shipThrustsAudioSource;
    float shipSizeOffSet;
    [SerializeField] float movSpeed = 0, rotationSpeed = 0;
    float xAxisDirection, yAxisDirection;
    [SerializeField] float boostForce = 0, boostDelay = 0;
    float boostingTime;
    bool isBoosting, canBoost;
    public bool shipHaveBoost { get; private set; }
    ShipSelectorController shipSelectorController;

    void Awake(){
        //Only for debug purposes so i can start the main game scene without going on the menu before
        //Have to remove this try-catch model later, cannot not have a ShipSelector Instance
        try{
            shipSelectorController = GameObject.FindGameObjectWithTag("ShipSelectorController").GetComponent<ShipSelectorController>();
            if(shipSelectorController.currentShipTypeIndex == 1){
                shipHaveBoost = false;
            }else{
                shipHaveBoost = true;
            }
        }catch (Exception e){
            Debug.Log("Missing ship selector controller object \n" + e.Message);
        }
        
        //Debug.Log(shipHaveBoost);
        //shipHaveBoost = true;

        shipRigidBody2D = GetComponent<Rigidbody2D>();   
        shootingPoint = GetComponentInChildren<Transform>(); 
        engineThrustsSprite = GameObject.FindGameObjectWithTag("ShipThrusts").GetComponent<SpriteRenderer>();

        //movSpeed = 8f;
        //rotationSpeed = 300f;
        shipSizeOffSet = 0.5f;

        //boostForce = 8f;
        //boostDelay = 3f;
        boostingTime = 0.3f;
        canBoost = true;

        engineThrustAnimator = engineThrustAnimatorObject.GetComponent<Animator>();
        //engineThrustAnimatorObject.SetActive(false);
        engineThrustsSprite.enabled = false;
        
        shipThrustsAudioSource = GetComponent<AudioSource>();
        shipThrustsAudioSource.enabled = false;
        soundController = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundController>();

        //Activate as the game start
        AdjustMovementLimitationBordersBasedOnCamera();
    }
    
    void Update(){
        //If want to use translate based movement, uncomment the line bellow and comment the one in FixedUpdate()
        //MovementInputUpdateChecker();

        RotationInputUpdateChecker();
        BoostInputUpdateChecker();
    }

    void FixedUpdate(){
        //If want to use physics based movement, uncomment the line bellow and comment the one in Update()
        MovementInputUpdateChecker();

        //Adjust at game runtime
        AdjustMovementLimitationBordersBasedOnCamera();
    }

    void MovementInputUpdateChecker(){
        //Function defined for checking movement either on Update or FixedUpdate
        xAxisDirection = Input.GetAxis("Horizontal");
        yAxisDirection = Input.GetAxis("Vertical");
        if(yAxisDirection > 0 || isBoosting){
            MoveShip(yAxisDirection);
            //engineThrustAnimatorObject.SetActive(true);
            shipRigidBody2D.drag = 3f; 
            shipRigidBody2D.angularDrag = 1f;
            engineThrustsSprite.enabled = true;
            shipThrustsAudioSource.enabled = true;
        }else{
            //engineThrustAnimatorObject.SetActive(false);
            // Maybe the linear drag can be increased over some little time, instead of being this value always when the ship is stopped
            shipRigidBody2D.drag = 0.3f;
            shipRigidBody2D.angularDrag = 0.05f;
            engineThrustsSprite.enabled = false;
            shipThrustsAudioSource.enabled = false;
        }
    }

    void RotationInputUpdateChecker(){
        if(xAxisDirection != 0){
            RotateShip(xAxisDirection);
        }else{
            RotateShip(0f);
        }
    }

    void BoostInputUpdateChecker(){
        if(isBoosting){
            hudController.DecreaseBoostBar(Time.deltaTime * 4);
        }else{
            hudController.IncreaseBoostBar(Time.deltaTime/(boostDelay + 1.3f));
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(canBoost && shipHaveBoost){
                StartCoroutine(BoostShip(boostForce));
            }
        }
    }

    void MoveShip(float yDirection){
        Vector2 shipDirectionVector = new Vector2(0f, yDirection * movSpeed * Time.deltaTime);

        //Using Translate (Non-Physics based)
        shipRigidBody2D.transform.Translate(shipDirectionVector, Space.Self);
        
        
        //Using Rigidbody (Physics based)
        //--Using Force--
        shipRigidBody2D.AddForce(transform.up * movSpeed); 
        
        //--Using Velocity--
        //shipRigidBody2D.velocity += new Vector2(0f, yDirection * movSpeed * Time.deltaTime); 
        
        //--Using MovePosition--
        //shipRigidBody2D.MovePosition((Vector2)transform.position + shipDirectionVector); 
        
        //Clampping (Only works for non physics based movement)
        float xPosClampped = Mathf.Clamp(transform.position.x, leftBorder.position.x + shipSizeOffSet, rightBorder.position.x - shipSizeOffSet);
        float yPosClampped = Mathf.Clamp(transform.position.y, bottomBorder.position.y + shipSizeOffSet, topBorder.position.y - shipSizeOffSet);
        transform.position = new Vector2(xPosClampped, yPosClampped);
    }

    void RotateShip(float zRotationAngle){
        Vector3 shipRotationVector = new Vector3(0f, 0f, -zRotationAngle * rotationSpeed * Time.deltaTime); 
        shipRigidBody2D.transform.Rotate(shipRotationVector);
    }

    void AdjustMovementLimitationBordersBasedOnCamera(){
        //Funcion for adjusting the limitation borders of the ship's movement with the camera size
        
        //float verticalHeightSeen = mainCamera.orthographicSize * 2.0f;
        //float horizontalHeightSeen = mainCamera.orthographicSize * (Screen.width / Screen.height);
        //Debug.Log(verticalHeightSeen);
        //Debug.Log(horizontalHeightSeen);

        //leftBorder.position = new Vector2(-horizontalHeightSeen - 2.5f, 0f);
        //rightBorder.position = new Vector2(+horizontalHeightSeen + 2.5f, 0f);
        //topBorder.position = new Vector2(0f, +verticalHeightSeen/2f);
        //bottomBorder.position = new Vector2(0f, -verticalHeightSeen/2f);

        Vector2 cameraPosition = mainCamera.ViewportToWorldPoint(new Vector3(1,1,mainCamera.nearClipPlane));
        
        //Debug.Log(cameraPosition);
        //Debug.Log(cameraPosition.x);
        //Debug.Log(-cameraPosition.x);
        //Debug.Log(cameraPosition.y);
        //Debug.Log(-cameraPosition.y);

        //Call adjustements for the borders limits scale
        AdjustBordersScaleBasedOnCamera(cameraPosition);

        leftBorder.position = new Vector2(-cameraPosition.x, 0f);
        rightBorder.position = new Vector2(cameraPosition.x, 0f);
        topBorder.position = new Vector2(0f, cameraPosition.y);
        bottomBorder.position = new Vector2(0f, -cameraPosition.y);
    }

    void AdjustBordersScaleBasedOnCamera(Vector2 cameraPosition){
        float fixedBordersWidth = 10f;

        float xAxisTransformBordersDistance = cameraPosition.x * 2;
        float yAxisTransformBordersDistance = cameraPosition.y * 2;

        float xBordersScaleBasedOnDistance = xAxisTransformBordersDistance * 100;
        float yBordersScaleBasedOnDistance = yAxisTransformBordersDistance * 100;

        //Debug.Log(leftBorder.localScale);
        //Debug.Log(rightBorder.localScale);
        //Debug.Log(topBorder.localScale);
        //Debug.Log(bottomBorder.localScale);
        
        leftBorder.localScale = new Vector3(fixedBordersWidth, yBordersScaleBasedOnDistance, 1f);
        rightBorder.localScale = new Vector3(fixedBordersWidth, yBordersScaleBasedOnDistance, 1f);
        topBorder.localScale = new Vector3(fixedBordersWidth, xBordersScaleBasedOnDistance, 1f);
        bottomBorder.localScale = new Vector3(fixedBordersWidth, xBordersScaleBasedOnDistance, 1f);
    }

    IEnumerator BoostShip(float boostForce){
        soundController.playSFX("shipBoost");
        isBoosting = true;
        engineThrustAnimator.SetBool("isBoosting", true);
        Vector2 boostThrust = transform.up * boostForce;
        shipRigidBody2D.AddForce(boostThrust, ForceMode2D.Impulse);
        yield return new WaitForSeconds(boostingTime);
        isBoosting = false;
        canBoost = false;
        yield return new WaitForSeconds(1.3f);
        engineThrustAnimator.SetBool("isBoosting", false);
        yield return new WaitForSeconds(boostDelay);
        soundController.playSFX("shipBoostRecharge");
        canBoost = true;
    }
}
