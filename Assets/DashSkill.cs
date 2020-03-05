using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : MonoBehaviour
{
    public float dashSpeed;
    public float maxDashDuration;
    private float _dashDuration;

    private Rigidbody _playerRb;
    private Player _player;
    public bool isDashing;
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            _playerRb.velocity += _player.transform.right * ((_player.facingRight ? 1 : -1) * (dashSpeed * Time.deltaTime));

            _dashDuration += Time.deltaTime;
            if (_dashDuration > maxDashDuration)
            {
                _playerRb.velocity = Vector3.zero;
                _dashDuration = 0;
                isDashing = false;
            }
        }

        if (Input.GetButtonDown("Fire2") || CustomInputs.DashPressed)
        {
            CustomInputs.DashPressed = false;
            StartDash();
        }
    }

    public void StartDash()
    {
        isDashing = true;
    }
}
