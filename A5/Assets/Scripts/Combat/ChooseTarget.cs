using System;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTarget : MonoBehaviour {

    bool _choosing;
    private ISelectable _selected;
    public List<ISelectable> _possibleTargets;

    public CombatManager _combatManager;

    public static Action<Fighter> OnSelected;

    void OnEnable(){
        Fighter.OnDie += (Entity e) => { _selected = null; };
    }

    void OnDisable(){
        Fighter.OnDie -= (Entity e) => { _selected = null; };
    }
    
    public void StartChoose(ISelectable[] targets) {
        _possibleTargets = new List<ISelectable>();
        foreach (var target in targets) {
            _possibleTargets.Add(target);
        }
        _choosing = true;
    }

    public void StopChoose() {
        _choosing = false;
    }

    void Update() {
        
        if (!_choosing) return;

        if(_selected != null) _selected.UnSelect();

        _selected = FindEntity();

        if (_selected != null) {
            TrySelect(_selected);
            TryClick(_selected);
        }
        
    }

    private void UnselectAll() {
        foreach (ISelectable target in _possibleTargets) {
            target.UnSelect();
        }
    }

    private void TryClick(ISelectable entity) {
        if (Input.GetMouseButtonDown(0)) {
            if (IsSelectable(entity)) {
                entity.ClickedGood();
                _combatManager.TargetChosen(entity);
                StopChoose();
            } else {
                entity.ClickedBad();
            }
        }
    }

    private void TrySelect(ISelectable entity) {
        if (IsSelectable(entity)) {
            entity.HighlightGood();
            OnSelected?.Invoke((Fighter)entity);
        } else {
            entity.HighlightBad();
        }
    }

    bool IsSelectable(ISelectable entity) {
        return _possibleTargets.Contains(entity);
    }

    ISelectable FindEntity() {

        RaycastHit2D hitData = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hitData) {
            var target = hitData.collider.GetComponent<ISelectable>();
            if (target != null) 
                return target;
        }
        return null;
    }
}