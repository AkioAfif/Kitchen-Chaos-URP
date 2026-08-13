using UnityEngine;
using UnityEngineInternal;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        forward,
        forwardInverted
    };

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.forward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.forwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
        
    }
}
    