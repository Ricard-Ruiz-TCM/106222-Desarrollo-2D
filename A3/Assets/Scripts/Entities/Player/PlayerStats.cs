using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    // Observer para controlar cuando vamos a morir
    public static event Action OnDie;

    public int Defense => _defense;
    public int Health => _health;

    [SerializeField]
    private int _health;
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _defense;

    void OnEnable(){
        FoodItem.OnFoodEaten += Heal;
        PotionItem.OnPotionUsed += Heal;
    }

    void OnDisable(){
        FoodItem.OnFoodEaten -= Heal;
        PotionItem.OnPotionUsed -= Heal;
    }

    void Start(){
        _health = 165;
        _maxHealth = _health;
        _defense = CalcDefense();
    }

    // Método parasaber si necesitamos ser curados
    // @return bool true -> sí | false -> no
    public bool NeedToHeal(){
        return (_health != _maxHealth);
    }

    // Método para calcular la defensa del player seg´n sus piezas de equipo
    private int CalcDefense() {
        int def = 0;
        for (int i = 0; i < GetComponent<Player>().Equip.Length; i++){
            def += ((EquipmentItem)GetComponent<Player>().Equip.GetSlot(i).GetItem()).Arm;
        }
        return def;
    }

    // Método para curarse
    // @param int hp -> cantidad a curarse
    public void Heal(int hp){
        _health = Mathf.Min(_maxHealth, _health += hp);
    }

    // Método para recibir daño
    // @param int dmg -> daño
    public void TakeDamage(int dmg){
        _health -= dmg;
        if (_health <= 0) OnDie?.Invoke();
    }

}
