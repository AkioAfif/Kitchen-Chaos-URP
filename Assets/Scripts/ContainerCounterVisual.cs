using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{

    private const string OPEN_CLOSE = "OpenClose"; 
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent <Animator> ();
    }

    private void Start()
    {
        containerCounter.onPlayerGrabbedOnject += ContainerCounter_onPlayerGrabbedOnject;
    }

    private void ContainerCounter_onPlayerGrabbedOnject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
