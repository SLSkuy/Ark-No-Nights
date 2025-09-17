using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private PlayerMovementFSM _movementFSM;

    private void Awake()
    {
        _movementFSM = new PlayerMovementFSM();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        _movementFSM.HandleInput();
        _movementFSM.Update();
    }

    private void FixedUpdate()
    {
        _movementFSM.FixedUpdate();
    }
}
