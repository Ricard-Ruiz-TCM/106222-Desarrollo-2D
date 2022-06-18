using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour, ICollide {
    
    // Varaibles publicas declaradas en la interfaz con referencia directa a variables internas
    // ICollide
    public float CheckRadius => _checkRadius;
    public List<Transform> CheckPoints => _checkPoints;

    [SerializeField]
    private float _checkRadius;
    [SerializeField]
    private List<Transform> _checkPoints;

    [SerializeField]
    private LayerMask _layerKey;

    [SerializeField]
    private bool _isTouchingKey;
    public bool IsTouchingKey() { return _isTouchingKey; }

    // Observer para identificar si esta cerca de la llave
    public static Action PicKey;

    void Start(){
        _checkRadius = 0.35f;
        // CheckPoints asignados en el editor
        // Layers asignados en el editor
    }

    void FixedUpdate(){
        CheckKey();
        if (IsTouchingKey()) PicKey?.Invoke();
    }

    // Método para comproar la cercania con la llave
    private void CheckKey() {
        _isTouchingKey = CheckCollisions(_layerKey);
    }

    // Método de interfaz para comprobar las colisiones con x layer y todos los check points
    // @param LayerMask layer -> layer a comprobar
    // @return bool true -> hay colisión | false -> no hay colision
    public bool CheckCollisions(LayerMask layer) {
        return RCollision(layer, CheckPoints, 0);
    }

    // Método recursivo para gestionar todos los checkpoints y comprobar si hay colisiones con el layer
    // @param LayerMask layer -> layer a comprobar
    // @param List<Transform> list -> Lista de chekc points
    // @param int pos -> posición de la lista a comprobar
    // @return bool true -> hay colisión | false -> no hay colision
    public bool RCollision(LayerMask layer, List<Transform> list, int pos) {
        if (list.Count <= pos) return false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(list[pos].position, CheckRadius, layer);
        return (colliders.Length > 0) || RCollision(layer, list, pos + 1);
    }
}
