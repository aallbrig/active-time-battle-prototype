using System.Collections.Generic;
using Controllers;
using ScriptableObjects;
using UnityEngine;

public class CameraAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private string _currentTrigger;
    private FighterController _fighter;

    private static class CameraStates
    {
        public static string DEFAULT = "Default";
        public static string VIEW_PLAYERS = "Look At Players";
        public static string VIEW_ENEMIES = "Look At Enemies";
    }

    public void PlayerFighterActive(FighterController fighter)
    {
        if (_currentTrigger == CameraStates.DEFAULT)
        {
            UpdateAnimationTrigger(CameraStates.VIEW_PLAYERS);
            _fighter = fighter;
        }
    }

    public void PlayerFighterActionSelected(FighterAction action)
    {
        if (_currentTrigger == CameraStates.VIEW_PLAYERS)
            UpdateAnimationTrigger(CameraStates.VIEW_ENEMIES);
    }

    public void PlayerInputComplete(FighterController fighter, FighterAction action, List<FighterController> targets)
    {
        if (fighter == _fighter) 
            UpdateAnimationTrigger(CameraStates.DEFAULT);
    }

    public void ResetCamera()
    {
        UpdateAnimationTrigger(CameraStates.DEFAULT);
    }

    private void UpdateAnimationTrigger(string triggerName)
    {
        if (_currentTrigger != null) _animator.SetBool(_currentTrigger, false);
        _animator.SetBool(triggerName, true);
        _currentTrigger = triggerName;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        UpdateAnimationTrigger(CameraStates.DEFAULT);
    }
}
