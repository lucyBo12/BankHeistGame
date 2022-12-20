using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;
using Cinemachine;

/**
 * Character movement behavior and animation handling is
 * based in this class. Uses 'Player' input actions to determine
 * object orientation, location and animation playback per frame.
 * 
 * External component requirements:
 *  - CharacterController
 *  - Animator
 * 
 * author: Joseph Denby
 * email: jd744@kent.ac.uk
 */
[RequireComponent(typeof(CharacterController), typeof(Character))]
public class CharacterLocomotion : NetworkBehaviour
{
    //Variables
    public CharacterController controller;
    public WeightModifier weight;
    public Animator animator;
    public float speed = 2f;

    private Character character => GetComponent<Character>();

    //Properties
    public float Angle { get; private set; }
    public float CurrentSpeed { get; private set; }
    public bool isAiming => PlayerActions.Aim.IsPressed();
    public bool IsCrouching => PlayerActions.Crouch.IsPressed();
    public bool IsMoving => controller.velocity != Vector3.zero;
    public CinemachineVirtualCamera VirtualCamera => GameObject.FindGameObjectWithTag("VirtualCamera")?.GetComponent<CinemachineVirtualCamera>();
    private InputMaster.PlayerActions PlayerActions => GameManager.Input.Player;


    private void Start() {
        PlayerActions.Enable();

        if (!VirtualCamera) return;

        VirtualCamera.ForceCameraPosition((transform.position - (Vector3.back * 5)), VirtualCamera.transform.rotation);
        VirtualCamera.Follow = LobbyManager.Self().transform;
        VirtualCamera.LookAt = LobbyManager.Self().transform;
    }

    //Called automatically every frame
    private void Update() {
        if (!IsOwner) return;

        //Move/AnimateCharacter both use Movement input
        Vector2 input = PlayerActions.Movement.ReadValue<Vector2>();
        Move(input);
        AnimateCharacter();

        //Get mouse position and pass to Aim function
        Vector2 mousePos = PlayerActions.Look.ReadValue<Vector2>();
        Aim(mousePos);

        if (IsHost) return;
        ulong id = NetworkManager.Singleton.LocalClient.ClientId;
        UpdateHostServerRpc(id, input);
    }

    /**
     * Moves character object using CharacterController componet as well 
     * as altering move speed spending on if the player is crouching or aiming. 
     * 
     * @param Vector2
     */
    private void Move(Vector2 input) {
        //If aiming/crouching speed should be lowered
        CurrentSpeed = IsCrouching || isAiming ? speed * 0.5f : speed;

        //Move Transform toward input over currentSpeed
        controller.Move((new Vector3(input.x, 0, input.y) * CurrentSpeed) * Time.deltaTime);
    }

    /**
     * Takes current mouse position on screen and rotates character
     * to face its location in world space. Sets 'angle' property to the 
     * calculated floating point result between 0 - 360.
     * 
     * @param Vector2
     */
    private void Aim(Vector2 mousePosition) {
        //Get GameObject position in screen space
        var center = Camera.main.WorldToScreenPoint(transform.position);

        //Offset the difference
        mousePosition.x = mousePosition.x - center.x;
        mousePosition.y = mousePosition.y - center.y;

        //Apply Tan to get rotation in radians and convert to degrees
        Angle = Mathf.Atan2(mousePosition.x, mousePosition.y) * Mathf.Rad2Deg;
        
        //Offset to ensure absolute value (0-360)
        Angle = Angle < 0 ? Angle + 360 : Angle;

        //Apply calculated angled to Transform
        transform.rotation = Quaternion.Euler(0, Angle, 0);

        //Apply Aim weights
        weight.SetWeight(character.inventoryManager.ActiveWeaponType(), isAiming);
    }

    /**
     * Updates Animator component parameters based on 
     * CharacterLocomotion properties and given input parameter.
     * 
     * @param Vector2
     */
    private void AnimateCharacter() {
        //Animator speed
        animator.speed = isAiming && !IsCrouching ? 0.66f : 1f;

        //Animator layers
        animator.SetLayerWeight(1, isAiming ? 1 : 0);
        animator.SetLayerWeight(2, isAiming ? 1 : 0);

        //Bool logic
        animator.SetBool("isAiming", isAiming);
        animator.SetBool("crouch", IsCrouching);

        //Rotation inverse logic
        var movement_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        var moveDirection = new Vector3(movement_input.x, 0, movement_input.y);
        moveDirection = transform.InverseTransformDirection(moveDirection);
        animator.SetFloat("inputx", moveDirection.x, 0.1f, Time.deltaTime);
        animator.SetFloat("inputy", moveDirection.z, 0.1f, Time.deltaTime);
    }

    [ServerRpc]
    private void UpdateHostServerRpc(ulong id, Vector2 input) {
        if (!IsHost) return;

        NetworkObject netObj = NetworkManager.Singleton.ConnectedClients[id].PlayerObject;
        netObj.GetComponent<Animator>().SetFloat("inputx", input.x);
        netObj.GetComponent<Animator>().SetFloat("inputy", input.y);
    }


}
