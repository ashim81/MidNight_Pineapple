using UnityEngine;

public class DetectorSuperclass : MonoBehaviour
// I need to use MonoBehaviour becuase I use its functionality all over my childclass.
// 
//      If I was not then I could use:
//
//      DetectorSuperclass d_NoiseMaker = new NoiseMaker();

{
    public virtual void PerformDetection()
    {
        Debug.Log("Look BC, the exectution changed :)");
    }
}