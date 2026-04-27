using UnityEngine;

public class d_DetectorSuperclass
{
    public virtual void d_PerformDetection()
    {
        Debug.Log("Look BC, the exectution changed :)");
    }
}

public class d_NoiseMaker : d_DetectorSuperclass
{
    public override void d_PerformDetection()
    {
        Debug.Log("This is the virtual overide of PerformDetection()");
    }
}

public class DynamicBinding : MonoBehaviour
{
    void Start()
    {
        d_DetectorSuperclass dynamicNoiseMaker = new d_NoiseMaker();

        dynamicNoiseMaker.d_PerformDetection(); 
    }
}