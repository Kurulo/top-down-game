using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerMoving
{
    private PlayerStateMachine _player;

    public PlayerMoving(PlayerStateMachine player)
    {
        _player = player;
    }

    public void MovePlayer(Vector2 direction, float speedMultiplier = 0f)
    {
        float smoothedSpeed;

        if (speedMultiplier == 0f)
            smoothedSpeed = _player.PlayerSettings.MovingSpeed * Time.deltaTime;
        else
            smoothedSpeed = _player.PlayerSettings.MovingSpeed * speedMultiplier * Time.deltaTime;

        Vector3 moveDirection = new Vector3(direction.x, 0f, direction.y);
        moveDirection = _player.SlopeVelocity(moveDirection);

        _player.CharacterController.Move(moveDirection * smoothedSpeed);
    }

    public void RotatePlayer(Vector2 direction)
    {
        float smoothRotation = _player.PlayerSettings.RotatingSpeed * Time.deltaTime;

        Vector3 rotateDirection = new Vector3(direction.x, 0f, direction.y);

        if (rotateDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
            _player.SelfTransform.rotation = Quaternion.RotateTowards(_player.SelfTransform.rotation, rotation, smoothRotation);
        }
    }
}
