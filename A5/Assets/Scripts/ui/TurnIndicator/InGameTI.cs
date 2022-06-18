using UnityEngine;
using UnityEngine.UI;

public class InGameTI : MonoBehaviour {

    public EntityManager EntityManager;

    public void Move(Entity obj){
        transform.position = obj.gameObject.transform.position;
    }

    void Update(){
        Move(EntityManager.ActiveEntity);
    }

}
