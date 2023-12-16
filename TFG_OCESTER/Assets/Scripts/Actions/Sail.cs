using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Sail : MonoBehaviour
{
    [SerializeField] private BoxCollider2D colliderRight;
    [SerializeField] private BoxCollider2D colliderLeft;
    [SerializeField] private BoxCollider2D colliderUp;
    [SerializeField] private BoxCollider2D colliderDown;
    public SailDirection startingSailDirection;
    private Animator _boatAnim;
    private GameObject _player;
    private bool _sailRight;
    
    void Start()
    {
        colliderLeft.enabled = false;
        _boatAnim = gameObject.GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        if (startingSailDirection == SailDirection.RIGHT)
            _sailRight = true;
        else
        {
            _sailRight = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            MovementController.Instance.ToggleMovement();
            other.transform.position = transform.position;
            other.transform.parent = transform;
            HideColliders();
            PlaySailAnim();
        }
    }

    private void SetFinalPosition()
    {
        _player.transform.parent = null;
        MovementController.Instance.ToggleMovement();
        ActivateColliders();
        ToggleSailDirection();
    }

    private void ActivateColliders()
    {
        colliderDown.enabled = true;
        colliderLeft.enabled = true;
        colliderRight.enabled = true;
        colliderUp.enabled = true;
        
        if (_sailRight)
        {
            colliderUp.enabled = false;
        }
        else
        {
            colliderLeft.enabled = false;
        }
    }

    private void HideColliders()
    {
        colliderDown.enabled = false;
        colliderLeft.enabled = false;
        colliderRight.enabled = false;
        colliderUp.enabled = false;
    }

    private void PlaySailAnim()
    {
        if (_sailRight)
        {
            _boatAnim.Play("SailRight");    
        }
        else
        {
            _boatAnim.Play("SailLeft");
        }
    }

    private void ToggleSailDirection()
    {
        _sailRight = !_sailRight;
    }

    public enum SailDirection
    {
        RIGHT,
        LEFT,
    }
}
