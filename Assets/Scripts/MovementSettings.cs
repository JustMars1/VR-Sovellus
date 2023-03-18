using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementSettings : MonoBehaviour
{
    public enum MovementMethod
    {
        TeleportSnapTurn,
        ContinuousMoveTurn
    }

    [SerializeField] MovementMethod movementMethod = MovementMethod.ContinuousMoveTurn;
    [SerializeField] Button teleportSnapTurnButton;
    [SerializeField] Button continuousMoveTurnButton;

    [SerializeField] SnapTurnProviderBase snapTurn;
    [SerializeField] TeleportationProvider teleportation;
    [SerializeField] ContinuousMoveProviderBase continuousMove;
    [SerializeField] ContinuousTurnProviderBase continuousTurn;

    void Awake()
    {
        teleportSnapTurnButton.onClick.AddListener(SetTeleportSnapTurn);
        continuousMoveTurnButton.onClick.AddListener(SetContinuousMoveTurn);
        UpdateMovementMethod();
    }

    void SetTeleportSnapTurn()
    {
        movementMethod = MovementMethod.TeleportSnapTurn;
        UpdateMovementMethod();
    }

    void SetContinuousMoveTurn()
    {
        movementMethod = MovementMethod.ContinuousMoveTurn;
        UpdateMovementMethod();
    }

    void UpdateMovementMethod()
    {
        teleportSnapTurnButton.targetGraphic.color = movementMethod == MovementMethod.TeleportSnapTurn ? Color.green : Color.white;
        continuousMoveTurnButton.targetGraphic.color = movementMethod == MovementMethod.ContinuousMoveTurn ? Color.green : Color.white;

        teleportation.enabled = movementMethod == MovementMethod.TeleportSnapTurn;
        snapTurn.enabled = movementMethod == MovementMethod.TeleportSnapTurn;

        continuousMove.enabled = movementMethod == MovementMethod.ContinuousMoveTurn;
        continuousTurn.enabled = movementMethod == MovementMethod.ContinuousMoveTurn;
    }
}
