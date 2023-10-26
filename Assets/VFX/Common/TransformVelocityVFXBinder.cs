using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class TransformVelocityVFXBinder : MonoBehaviour
{
    public Transform target;
    public string id = "Target";

    private VisualEffect vfx;
    private Vector3 previous;

    void Update()
    {
        if (vfx == null) vfx = GetComponent<VisualEffect>();
        if (vfx == null || target == null) return ;

        if (vfx.HasVector3(id+"Position")) vfx.SetVector3(id+"Position", target.position);
        if (vfx.HasVector3(id+"Velocity")) vfx.SetVector3(id+"Velocity", target.position - previous);
        
        previous = target.position;
    }
}