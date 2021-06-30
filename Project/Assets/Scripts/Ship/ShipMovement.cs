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

    void Awake(){
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
        xAxisDirection = Input.GetAxis("Horizontal");
        yAxisDirection = Input.GetAxis("Vertical");
        if(yAxisDirection > 0 || isBoosting){
            MoveShip(yAxisDirection);
            //engineThrustAnimatorObject.SetActive(true);
            engineThrustsSprite.enabled = true;
            shipThrustsAudioSource.enabled = true;
        }else{
            //engineThrustAnimatorObject.SetActive(false);
            engineThrustsSprite.enabled = false;
            shipThrustsAudioSource.enabled = false;
        }
        
        if(xAxisDirection != 0){
            RotateShip(xAxisDirection);
        }else{
            RotateShip(0f);
        }

        if(isBoosting){
            hudController.DecreaseBoostBar(Time.deltaTime * 4);
        }else{
            hudController.IncreaseBoostBar(Time.deltaTime/(boostDelay + 1.3f));
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(canBoost){
                StartCoroutine(BoostShip(boostForce));
            }
        }
    }

    void FixedUpdate(){
        //Adjust at game runtime
        AdjustMovementLimitationBordersBasedOnCamera();
    }

    void MoveShip(float yDirection){
        Vector2 shipDirectionVector = new Vector2(0f, yDirection * movSpeed * Time.deltaTime);
        shipRigidBody2D.transform.Translate(shipDirectionVector, Space.Self);
        
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
