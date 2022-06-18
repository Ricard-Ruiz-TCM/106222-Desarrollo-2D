using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    public bool CanSmell => SmellDetector.InRange;
    public bool CanHear => NoiseDetector.InRange && NoiseDetector.NotBlocked;
    public bool CanSee => VisionDetector.InRange && VisionDetector.NotBlocked && VisionDetector.InRange && VisionDetector.InFOV;

    VisionDetector VisionDetector;
    SmellDetector SmellDetector;
    NoiseDetector NoiseDetector;
    // Start is called before the first frame update
    void Start()
    {
        VisionDetector = GetComponent<VisionDetector>();
        SmellDetector = GetComponent<SmellDetector>();
        NoiseDetector = GetComponent<NoiseDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        VisionDetector.NotBlocked = CanHear;
    }
}
