using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WorldAlarm : MonoBehaviour {

    [SerializeField]
    private Light2D _light;

    [SerializeField]
    private bool _alarmOn;
    public bool IsAlarmOn(){ return _alarmOn; }

    private float _red;

    // Variable utilizada para apagar la alarma,
    // Solo se apagará cuando el invoker que detecto al player
    // Invoue que lo ha perdido de vista
    // Haciendo que el último en verlo, en el caso de ser muchos que lo detecten
    // Será el que diga si lo ha dejado de ver y apague la alarma
    private Transform _detector;

    public static event Action AlarmActivated;
    public static event Action AlarmDesactivated;

    void OnEnable(){
        Enemy.EnemyFound += StartAlarm;
        Enemy.EnemyLost += StopAlarm;
    }
    
    void OnDisable(){
        Enemy.EnemyFound -= StartAlarm;
        Enemy.EnemyLost -= StopAlarm;
    }

    void Awake(){
        _light = GetComponent<Light2D>();
    }

    void Start(){
        _red = _light.color.r;
        _alarmOn = false;
        _detector = transform;
    }

    void Update(){
        _light.color = new Color(((Time.timeSinceLevelLoad % 0.75f <= 0.375f && _alarmOn) ? 1.0f : _red), _light.color.g, _light.color.b);
    }

    // Método para iniciar la alarma
    // @param transform parent -> Enemigo que lo detecta
    void StartAlarm(Transform parent){
        _detector = parent;
        _alarmOn = true;
        AlarmActivated?.Invoke();
    }

    // Método para parar la alarma
    // @param transform parent -> Enemigo que lo deja de detectar
    void StopAlarm(Transform parent){
        if (parent == _detector) {
            _alarmOn = false;
            _detector = transform;
            AlarmDesactivated?.Invoke();
        }
    }

}
