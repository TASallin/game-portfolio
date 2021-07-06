using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//require some things the bot control needs
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(AstroController))]
public class AstroRootMotionControl : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;
    private AstroController cinput;

    private Transform leftFoot;
    private Transform rightFoot;

    public float animationSpeed = 1.5f;
    public float rootMovementSpeed = 1.3f;
    public float rootTurnSpeed = 3f;

    //Useful if you implement jump in the future...
    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;


    private int groundContactCount = 0;

    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }

    void Awake()
    {
        Application.targetFrameRate = 30;
        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();

        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponent<AstroController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");
    }

    // Start is called before the first frame update
    void Start()
    {
        //example of how to get access to certain limbs
        leftFoot = this.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftLeg/mixamorig1:LeftFoot");
        rightFoot = this.transform.Find("mixamorig1:Hips/mixamorig1:RightUpLeg/mixamorig1:RightLeg/mixamorig1:RightFoot");

        if (leftFoot == null || rightFoot == null)
            Debug.Log("One of the feet could not be found");
    }

    // Update is called once per frame
    void Update()
    {
        float inputForward=0f;
        float inputTurn=0f;
        bool inputAction = false;
        bool doJump = false;
        bool jumpAction = false;

        if (cinput.enabled)
        {
            inputForward = cinput.Forward;
            inputTurn = cinput.Turn;
            inputAction = cinput.Action;
            jumpAction = cinput.Jump;
        }

        //onCollisionXXX() doesn't always work for checking if the character is grounded from a playability perspective
        //Uneven terrain can cause the player to become technically airborne, but so close the player thinks they're touching ground.
        //Therefore, an additional raycast approach is used to check for close ground.
        //This is good for allowing player to jump and not be frustrated that the jump button doesn't
        //work
        //bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);
        bool isGrounded = true;
        // get info about current animation
        var animState = anim.GetCurrentAnimatorStateInfo(0);

        if (inputAction)
        {
            Debug.Log("Action pressed");

            // if (buttonDistance <= buttonCloseEnoughForMatchDistance)
            // {
            //     if(buttonDistance <= buttonCloseEnoughForPressDistance &&
            //         buttonAngleDegrees <= buttonCloseEnoughForPressAngleDegrees)
            //     {
            //         Debug.Log("Button press initiated");

            //         doButtonPress = true;

            //     }
            //     else
            //     {
            //         // TODO UNCOMMENT THESE LINES FOR TARGET MATCHING
            //         Debug.Log("match to button initiated");
            //         doMatchToButtonPress = true;
            //     }

            // }
        }

        if (isGrounded && jumpAction)
        {
            Debug.Log("jumping");
            doJump = true;
        }

        // get info about current animation
        //var animState = anim.GetCurrentAnimatorStateInfo(0);
        // If the transition to button press has been initiated then we want
        // to correct the character position to the correct place
        // if ( animState.IsName("Jump") & !anim.IsInTransition(0) ) {
        //     if (buttonPressStandingSpot != null) {
        //         Debug.Log("Target matching correction started"); initalMatchTargetsAnimTime = animState.normalizedTime;
        //         var t = buttonPressStandingSpot.transform;
        //         anim.MatchTarget(t.position, t.rotation, AvatarTarget.Root,
        //             new MatchTargetWeightMask(new Vector3(1f, 0f, 1f), 1f), initalMatchTargetsAnimTime, exitMatchTargetsAnimTime);
        //     }
        // }

        anim.SetFloat("velx", inputTurn);
        anim.SetFloat("vely", inputForward);
        anim.SetBool("isFalling", !isGrounded);
        anim.SetBool("doJump", doJump);
        anim.speed = animationSpeed;

    }

    //This is a physics callback
    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {

            ++groundContactCount;

            // Generate an event that might play a sound, generate a particle effect, etc.
            //EventManager.TriggerEvent<PlayerLandsEvent, Vector3, float>(collision.contacts[0].point, collision.impulse.magnitude);

        }

    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {
            --groundContactCount;
        }

    }

    void OnAnimatorMove()
    {

        Vector3 newRootPosition;
        Quaternion newRootRotation;

        //bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);
        bool isGrounded = true;
        if (isGrounded)
        {
            //use root motion as is if on the ground
            newRootPosition = anim.rootPosition;
        }
        else
        {
            //Simple trick to keep model from climbing other rigidbodies that aren't the ground
            newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
        }

        //use rotational root motion as is
        newRootRotation = anim.rootRotation;

        //TODO Here, you could scale the difference in position and rotation to make the character go faster or slower

        this.transform.position = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMovementSpeed);
        this.transform.rotation = Quaternion.LerpUnclamped(this.transform.rotation, newRootRotation, rootTurnSpeed);

    }

    void OnAnimatorIK(int layerIndex)
    {
        if(anim) {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0);
            Debug.Log(astate);

            if(astate.IsName("ButtonPress")) {
                // float buttonWeight = anim.GetFloat("buttonClose");
                // // Set the look target position, if one has been assigned
                // if(buttonObject != null) {
                //     anim.SetLookAtWeight(buttonWeight);
                //     anim.SetLookAtPosition(buttonObject.transform.position);
                //     anim.SetIKPositionWeight(AvatarIKGoal.RightHand,buttonWeight);
                //     anim.SetIKPosition(AvatarIKGoal.RightHand, buttonObject.transform.position);
                // }
            } else {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                anim.SetLookAtWeight(0);
            }
        }
    }


}
