using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour
{
    [SerializeField] private Transform _gunPivot;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private SpriteRenderer _gunSprite;
    [SerializeField] private LayerMask _levelMask;
    private Vector2 _defaultOffset;

    private void Awake()
    {
        _defaultOffset = _gunSprite.transform.localPosition;
    }

    void OnEnable()
    {
        _gunSprite.enabled = true;
    }

    void OnDisable()
    {
        _gunSprite.enabled = false;
    }

    private void Update()
    {
        Aim();
        Offset();
    }

    private void Offset()
    {
        RaycastHit2D hit = Physics2D.Raycast(_gunPivot.position, _gunPivot.right, 1.35f, _levelMask);
        if (hit)
        {
            float offset = Vector2.Distance(hit.point, _firePoint.position);
            _gunSprite.transform.localPosition = new Vector3(_defaultOffset.x - offset, _gunSprite.transform.localPosition.y, 0);
        }
        else
        {
            _gunSprite.transform.localPosition = new Vector3(_defaultOffset.x, _gunSprite.transform.localPosition.y, 0);
        }
    }

    private void Aim()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPosition.z = 0;
        _gunPivot.right = (mouseWorldPosition - _gunPivot.position).normalized;
        if (Vector2.Dot(Vector2.up, _gunPivot.up) < 0)
        {
            _gunSprite.flipY = true;
            _gunSprite.transform.localPosition = new Vector3(_defaultOffset.x, -1 * _defaultOffset.y, 0);
        }
        else
        {
            _gunSprite.flipY = false;
            _gunSprite.transform.localPosition = new Vector3(_defaultOffset.x, _defaultOffset.y, 0);
        }
    }
}