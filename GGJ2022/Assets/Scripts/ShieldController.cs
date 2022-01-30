using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private GameObject _heldShield;
    [SerializeField] private GameObject _thrownShield;

    private Rigidbody2D _thrownShieldRigidBody;
    private CircleCollider2D _thrownShieldCollider;

    private ShieldCollider _shieldCollider;

    [SerializeField] private float _shieldDamage = 10f;
    [SerializeField] private float _pickUpDelay = 1.5f;
    [SerializeField] private float _timeSinceThrown;

    public bool IsHeld = true;
    public bool CanBePickedUp() => (!IsHeld && ( _timeSinceThrown > _pickUpDelay || _thrownShieldRigidBody.velocity.magnitude < 0.5f ));


    private void Awake()
    {
        _thrownShieldRigidBody = _thrownShield.GetComponent<Rigidbody2D>();
        _thrownShieldCollider = _thrownShield.GetComponent<CircleCollider2D>();

        _shieldCollider = _thrownShield.GetComponent<ShieldCollider>();
    }


    void Start()
    {
        
    }

    public void SetShieldDamage(int damage)
    {
        _shieldCollider.SetDamage(damage);
    }

    public void PickUp(Transform playerTransform)
    {
        _thrownShield.transform.parent = playerTransform;
        _thrownShield.SetActive(false);
        _heldShield.SetActive(true);

        IsHeld = true;
    }

    public void Remove()
    {
        _thrownShield.SetActive(false);
        _heldShield.SetActive(false);

        IsHeld = false;
    }


    public void Throw(Vector2 direction, Vector2 throwSpawn)
    {
        _thrownShield.transform.localPosition = throwSpawn;
        _thrownShield.transform.localRotation = Quaternion.Euler(0, 0, 0);

        _thrownShield.SetActive(true);
        _thrownShieldCollider.isTrigger = false;
        _thrownShield.transform.parent = null;

        //Debug.Log(_thrownShield.transform.position + " " + direction);
        _thrownShield.transform.up = direction;
        _heldShield.SetActive(false);

        IsHeld = true;
        

        Vector2 force = new(0, 10);
        _thrownShieldRigidBody.AddRelativeForce(force, ForceMode2D.Impulse);

        IsHeld = false;
        _timeSinceThrown = 0f;
    }


    void Update()
    {
        if (IsHeld)
        {
            return;
        }

        Debug.Log(_thrownShieldRigidBody.velocity.magnitude);
        _timeSinceThrown += Time.deltaTime;

        var shieldSpeed = _thrownShieldRigidBody.velocity.magnitude;
        if (shieldSpeed < 1f)
        {
            _thrownShieldRigidBody.velocity = new Vector2(0, 0);
        }
    }

}