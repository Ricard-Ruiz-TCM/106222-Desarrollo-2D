using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EntityManager))]
[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    //public Vector2 MovementInput => _movementInput;
    //private Vector2 _movementInput;

    private EntityManager _entityManager;

    private void Start()
    {
        _entityManager = GetComponent<EntityManager>();
    }


    private void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        var command = new MoveCommand(_entityManager.ActiveEntity,v);
        Invoker.AddCommand(command);
    }

    private void OnChangeColor(InputValue value){
        var command = new ChangeColorCommand(_entityManager.ActiveEntity);
        Invoker.AddCommand(command);
    }

    private void OnChangeSize(InputValue value){
        var command = new ChangeSizeCommand(_entityManager.ActiveEntity);
        Invoker.AddCommand(command);
    }
  
    private void OnNextPlayer()
    {
        _entityManager.SetNextEntity();
    }

    private void OnUndo()
    {
        Invoker.Undo();
    }

    private void OnRedo()
    {
        Invoker.Redo();
    }
}
