using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoubleSlidingDoor : Door
{
    private Animator _animator;
    private static readonly int CloseTriggerName = Animator.StringToHash("Close");
    private static readonly int OpenTriggerName = Animator.StringToHash("Open");
    
    private AudioSource _audioSource;
    [SerializeField] private AudioClip lockingSound;
    [SerializeField] private AudioClip unlockingSound;
    [SerializeField] private AudioClip openingSound;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Open()
    {
        _audioSource.pitch = 0.7f;
        _animator.SetTrigger(OpenTriggerName);
    }

    public override void Close()
    {
        _audioSource.pitch = 1f;
        _animator.SetTrigger(CloseTriggerName);
    }

    public void OnOpened()
    {
        IsOpen = true;
    }

    public void OnClosed()
    {
        IsOpen = false;
    }

    public void OnUnlock()
    {
        _audioSource.PlayOneShot(unlockingSound);
    }

    public void OnUnlocked()
    {
        _audioSource.PlayOneShot(openingSound);
    }

    public void OnLock()
    {
        _audioSource.PlayOneShot(lockingSound);
    }
}
