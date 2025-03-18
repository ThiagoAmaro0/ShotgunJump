using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private float _rotationDuration;
    [SerializeField] private float _rotationAngle;
    [SerializeField] private float _scalePunch;
    [SerializeField] private AudioSource _landSound;
    [SerializeField] private ParticleSystem _deathParticle;
    [SerializeField] private AimController _aimController;
    [SerializeField] private ShootController _shootController;
    private HorizontalDirection _horizontalDirection;
    private VerticalDirection _verticalDirection;

    void Update()
    {
        UpdateHorizontalState();
        UpdateVerticalState();
    }

    private void UpdateVerticalState()
    {
        if (_playerRigidbody.linearVelocityY > 0.1f)
        {
            if (_verticalDirection != VerticalDirection.UP)
            {
                SetVerticalState(VerticalDirection.UP);
            }
        }
        else if (_playerRigidbody.linearVelocityY < -0.1f)
        {
            if (_verticalDirection != VerticalDirection.DOWN)
            {
                SetVerticalState(VerticalDirection.DOWN);
            }
        }
        else if (_playerRigidbody.linearVelocityY == 0)
        {
            if (_verticalDirection != VerticalDirection.NONE)
            {
                SetVerticalState(VerticalDirection.NONE);
            }
        }
    }

    private void UpdateHorizontalState()
    {
        if (_playerRigidbody.linearVelocityX > 0.1f)
        {
            if (_horizontalDirection != HorizontalDirection.RIGHT)
            {
                SetHorizontalState(HorizontalDirection.RIGHT);
            }
        }
        else if (_playerRigidbody.linearVelocityX < -0.1f)
        {
            if (_horizontalDirection != HorizontalDirection.LEFT)
            {
                SetHorizontalState(HorizontalDirection.LEFT);
            }
        }
        else if (_playerRigidbody.linearVelocityX == 0)
        {
            if (_horizontalDirection != HorizontalDirection.NONE)
            {
                SetHorizontalState(HorizontalDirection.NONE);
            }
        }
    }

    private void SetHorizontalState(HorizontalDirection state)
    {
        _horizontalDirection = state;
        switch (state)
        {
            case HorizontalDirection.NONE:
                break;
            case HorizontalDirection.LEFT:
                _playerRenderer.flipX = true;
                break;
            case HorizontalDirection.RIGHT:
                _playerRenderer.flipX = false;
                break;
        }
    }

    private void SetVerticalState(VerticalDirection state)
    {
        _verticalDirection = state;
        _playerRenderer.transform.localScale = Vector3.one;

        switch (state)
        {
            case VerticalDirection.NONE:
                _playerRenderer.transform.DOLocalRotate(Vector3.zero, _rotationDuration);
                if (_horizontalDirection == HorizontalDirection.NONE)
                {
                    _landSound.Play();
                    _playerRenderer.transform.DOPunchScale(new Vector3(1, _scalePunch, 1), _rotationDuration);
                }
                break;
            case VerticalDirection.UP:
                Vector3 angleUp = _playerRenderer.flipX ? new Vector3(0, 0, _rotationAngle) : new Vector3(0, 0, -_rotationAngle);
                _playerRenderer.transform.DOLocalRotate(angleUp, _rotationDuration);
                break;
            case VerticalDirection.DOWN:
                Vector3 angleDown = _playerRenderer.flipX ? new Vector3(0, 0, -_rotationAngle) : new Vector3(0, 0, _rotationAngle);
                _playerRenderer.transform.DOLocalRotate(angleDown, _rotationDuration);
                break;
        }
    }
    public void DieAnimation()
    {
        if (!_deathParticle.isPlaying)
        {
            _aimController.enabled = false;
            _shootController.enabled = false;
            _playerRenderer.enabled = false;
            _deathParticle.Play();
            StartCoroutine(Respawn());
        }
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(_deathParticle.main.duration);
        RespawnSystem.instance.Respawn();
        yield return new WaitForEndOfFrame();
        _deathParticle.Play();
        _playerRenderer.enabled = true;
        _aimController.enabled = true;
        _shootController.enabled = true;

    }

    private enum HorizontalDirection
    {
        NONE,
        LEFT,
        RIGHT
    }
    private enum VerticalDirection
    {
        NONE,
        UP,
        DOWN
    }
}
