using UnityEngine;

public class EnemySeek : EnemySee {

    [SerializeField]
    private bool _seek;

    void OnEnable(){
        EnemyCollision.Close2Wall += ChangeDirection;
        GameManager.Reset += ResetPos;
        WorldAlarm.AlarmActivated += () => { _seek = true; };
        WorldAlarm.AlarmDesactivated += () => { _seek = false; };
    }

    void OnDisable(){
        EnemyCollision.Close2Wall -= ChangeDirection;
        GameManager.Reset -= ResetPos;
        WorldAlarm.AlarmActivated -= () => { _seek = true; };
        WorldAlarm.AlarmDesactivated -= () => { _seek = false; };
    }

    void FixedUpdate(){
        Move();
        EntityDetection();
        // Solo queremos reiniciar el nivel si el EnemySekk está "Close2Player"
        // Por eso este if v se ejecuta aquí y no en EnemyCollision.cs
        if (GetComponent<EnemyCollision>().IsClose2Player()) EnemyCollision.Close2Player?.Invoke();
    }

    // Método de movimiento nuevo para el EnemySeek, permitiendo seguir al player cuando lo detecta
    public override void Move(){
        if (_seek){
            if (EntityIsVisible(_entity)){
                transform.position += (_entity.transform.position - transform.position) * _speed * Time.deltaTime;
                Vector3 diff = _entity.position - transform.position;
                diff.Normalize();
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
            } else {
                base.Move();
            }
        } else {
            base.Move();
        }
    }

}