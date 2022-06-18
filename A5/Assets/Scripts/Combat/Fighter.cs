using System;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Entity
{
    public float CurrentHealth;
    public float MaxHealth = 100;

    private float BaseDefense=0;
    private float RoundDefense;
    public float Defense => BaseDefense + RoundDefense;

    private float BaseAttack=25;
    private float RoundAttack;
    public float Attack => BaseAttack + RoundAttack;

    private float BaseSpeed;
    private float RoundSpeed;
    public float Speed => BaseSpeed + RoundSpeed;

    public List<FightCommandTypes> PossibleCommands;

    public static Action OnChange;

    public static event Action<Entity> OnDie;

    void Awake() {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage) {

        float realDamage = damage - (BaseDefense + RoundDefense);
        realDamage = Mathf.Max(realDamage, 0);

        CurrentHealth -= realDamage;

        OnChange?.Invoke();

        if (CurrentHealth <= 0) Die();
    }

    public void Heal(float amount){
        CurrentHealth = Math.Min(CurrentHealth + amount, MaxHealth);
        OnChange?.Invoke();
    }

    private void Die() {
        OnDie?.Invoke(this);
        Destroy(gameObject);
    }

    public void AddDefense(float amount) {
        RoundDefense += amount;
        OnChange?.Invoke();
    }

    public void AddAttack(float amount) {
        RoundAttack += amount;
        OnChange?.Invoke();
    }

    public void AddDefensePermanent(float amount) {
        BaseDefense += amount;
        OnChange?.Invoke();
    }

    public void AddAttackPermanent(float amount) {
        BaseAttack += amount;
        OnChange?.Invoke();
    }

    public void AddSpeed(float amount) {
        RoundSpeed += amount;
        OnChange?.Invoke();
    }

    public void ResetFighter() {
        RoundDefense = 0;
        RoundAttack = 0;
        RoundSpeed = 0;
        OnChange?.Invoke();
    }

    public void CreatePossibleCommands(){

        PossibleCommands = new List<FightCommandTypes>();
        PossibleCommands.Add(FightCommandTypes.Attack);

        for (int i = 1; i < (int)FightCommandTypes.TOTAL; i++){
            if (UnityEngine.Random.Range(0, 2) == 0) PossibleCommands.Add((FightCommandTypes)i);
        }

    }
    
}

