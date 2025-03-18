using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShootController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private ParticleSystem _shootParticle;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private InputAction _fireInput;
    [SerializeField] private float _jumpForce = 450f;
    [SerializeField] private float _rechargeTime = 1f;
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _rechargeSound;
    private float _nextFireTime;

    private void OnEnable()
    {
        _fireInput.Enable();
        _fireInput.started += Fire;
    }

    private void OnDisable()
    {
        _fireInput.Disable();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (Time.time > _nextFireTime)
        {
            CameraNoise.instance?.Shake(.25f);
            _shootSound.Play();
            StartCoroutine(PlayRechargeSound());
            _shootParticle.Play();
            _nextFireTime = Time.time + _rechargeTime;
            _playerRigidbody.linearVelocity = Vector2.zero;
            _playerRigidbody.AddForce(-_firePoint.right * _jumpForce);
            Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        }
    }

    private IEnumerator PlayRechargeSound()
    {
        yield return new WaitForSeconds(_rechargeTime);
        _rechargeSound.Play();
    }
}
