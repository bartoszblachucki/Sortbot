using UnityEngine;

[RequireComponent(typeof(RailMovement))]
[RequireComponent(typeof(Animator))]
public class RoboticArm : MonoBehaviour
{
    [SerializeField] private Transform heldObjectPositionTransform;
    [SerializeField] private Transform objectProximitySensor;
    [SerializeField] private LayerMask pickableLayers;
    [SerializeField] private LayerMask pickableStandLayers;
    
    
    private Animator _animator;
    private static readonly int PickUpTrigger = Animator.StringToHash("PickUp");
    private static readonly int PutDownTrigger = Animator.StringToHash("PutDown");
    private static readonly int HoldingAnObjectBool = Animator.StringToHash("HoldingAnObject");
    private static readonly int GoToSleepTrigger = Animator.StringToHash("GoToSleep");
    private static readonly int WakeUpTrigger = Animator.StringToHash("WakeUp");
    
    private GameObject _heldGameObject;
    public GameObject HeldGameObject => _heldGameObject;

    public bool IsSleeping { get; private set;  } = false;
    public bool IsReady => IsIdle();
    private bool _busy = true;


    public float ArmOffset { get; private set; }
    private RailMovement _railMovement;

    void Start()
    {
        _railMovement = GetComponent<RailMovement>();
        _animator = GetComponent<Animator>();
        ArmOffset = transform.position.z - heldObjectPositionTransform.position.z;
    }

    public void StartPickUpSequence()
    {
        _busy = true;
        _animator.SetTrigger(PickUpTrigger);
    }

    public void StartPutDownSequence()
    {
        _busy = true;
        _animator.SetTrigger(PutDownTrigger);
    }

    public void StartMovingTowards(Vector3 position)
    {
        _railMovement.MoveTo(position);
    }
    
    public bool IsIdle()
    {
        return (IsAnimationPlaying(_animator, "RoboticArmIdle") ||
                IsAnimationPlaying(_animator, "RoboticArmIdleWithBall")) && !_busy && !_railMovement.Moving;
    }

    private static bool IsAnimationPlaying(Animator animator, string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            return true;
        return false;
    }
    
    private void TryToPickUp()
    {
        bool holdingAnObject = false;

        if (IsPickableInFront(out var objectInFront))
        {

            AttachObject(objectInFront);
            holdingAnObject = true;
        }

        _animator.SetBool(HoldingAnObjectBool, holdingAnObject);
    }

    private void TryToPutDown()
    {

        if (_heldGameObject == null)
            return;

        if (IsPickableStandInFront(out var pickableStand))
        {
            if (pickableStand.IsEmpty())
            {
                pickableStand.SetSortable(_heldGameObject);
                _heldGameObject = null;
            }
        }
    }

    private void OnAnimationFinish()
    {
        _busy = false;
    }

    private void AttachObject(GameObject objectToPickUp)
    {
        objectToPickUp.transform.SetParent(heldObjectPositionTransform);
        objectToPickUp.transform.localPosition = Vector3.zero;

        _heldGameObject = objectToPickUp;
    }

    private bool IsPickableInFront(out GameObject objectInFront)
    {
        Debug.DrawRay(objectProximitySensor.position, objectProximitySensor.forward * 2, Color.red, 2);
        
        if (Physics.Raycast(objectProximitySensor.position, objectProximitySensor.forward, out RaycastHit hit, 2, pickableLayers))
        {
            objectInFront = hit.collider.gameObject;
            return true;
        }

        objectInFront = null;
        return false;
    }

    private bool IsPickableStandInFront(out Stand stand)
    {
        Debug.DrawRay(objectProximitySensor.position, objectProximitySensor.forward * 4, Color.red, 2);
        
        if (Physics.Raycast(objectProximitySensor.position, objectProximitySensor.forward, out RaycastHit hit, 4, pickableStandLayers))
        {
            if (hit.collider.TryGetComponent(out stand))
            {
                return true;
            }
        }

        stand = null;
        return false;
    }

    public void GoToSleep()
    {
        IsSleeping = true;
        _animator.SetTrigger(GoToSleepTrigger);
    }

    public void WakeUp()
    {
        IsSleeping = false;
        _animator.SetTrigger(WakeUpTrigger);
    }
}
