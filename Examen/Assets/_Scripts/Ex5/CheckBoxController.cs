using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBoxController : MonoBehaviour
{
    public Toggle SmellToggle;
    public Toggle HearToggle;
    public Toggle SeeToggle;

    public EnemySenses EnemySenses;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SmellToggle.isOn = EnemySenses.CanSmell;
        HearToggle.isOn = EnemySenses.CanHear;
        SeeToggle.isOn = EnemySenses.CanSee;
    }
}
