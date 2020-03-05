using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public GameEvent LevelUpEvent;
    public GameEvent GameOverEvent;
    public static Player Instance;
    public Transform fist;
    public float playerSpeed;
    public float jumpHeight;
    public float climbSpeed;
    public bool isTouchingGround;
    public float fallMultiplier;
    public Weapon currentWeapon;

    private Rigidbody _rigidBody;
    private Transform _playerModel;

    private float _invulnerableTimer;

    public bool facingRight = true;
    public bool isClimbing = true;

    public int Health = 20;

    public GameEvent OnPlayerHurtEvent;

    // Start is called before the first frame update
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();

        #region Singleton

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        #endregion
    }

    void Start()
    {
        _playerModel = transform.Find("PlayerModel");
        if (Math.Abs(transform.rotation.y) > 0.1f)
        {
            facingRight = false;
        }

        // SetWeapon(Weapon.WeaponType.Melee);
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
        _invulnerableTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
        }
    }

    #region Player movement, jump and attack.

    void ManageInput()
    {
        #region Player movement

        var inputX = CustomInputs.XAxis + Input.GetAxis("Horizontal"); //we can use both touch and kb\m

        _rigidBody.velocity = new Vector3(inputX * playerSpeed, _rigidBody.velocity.y, 0);

        if (inputX > 0) //Turn player right
        {
            if (!facingRight)
            {
                if (!isClimbing)
                {
                    _playerModel.Rotate(Vector3.up, 180f);
                }

                facingRight = true;
            }
        }
        else if (inputX < 0) //Turn player left
        {
            if (facingRight)
            {
                if (!isClimbing)
                {
                    _playerModel.Rotate(Vector3.up, 180f);
                }

                facingRight = false;
            }
        }

        #endregion

        #region Player jumping

        bool hodlingJump = Input.GetButton("Jump") || CustomInputs.JumpPressed;
        if (hodlingJump && isTouchingGround) //Make player jump
        {
            _rigidBody.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
            isTouchingGround = false;
        }

        if (isClimbing && hodlingJump)
        {
            transform.Translate(0, climbSpeed * Time.deltaTime, 0);
        }
        else
        {
            bool falling = _rigidBody.velocity.y < 0;
            if (falling || !hodlingJump)
            {
                _rigidBody.velocity += Vector3.down *
                                       (fallMultiplier * ((falling && !hodlingJump) ? 1.5f : 1f) *
                                        Time.deltaTime
                                       ); //make player fall faster, and even faster if he's not holding "up"
            }
        }

        #endregion

        #region Player attack

        if (Input.GetButton("Fire1") || CustomInputs.AttackPressed)
        {
            if (currentWeapon)
            {
                currentWeapon.Attack();
            }
        }

        #endregion
    }

    #endregion

    public void SetWeapon(Weapon.WeaponType newWeaponType)
    {
        if (currentWeapon)
        {
            if (Equals(newWeaponType, currentWeapon.weaponType))
            {
                return;
            } //do not re-spawn weapon if we pick up the same

            //remove current weapon
            Destroy(currentWeapon.gameObject);
            currentWeapon = null;
        }

        //set new weapon
        switch (newWeaponType)
        {
            case Weapon.WeaponType.Unarmed:
                break;
            case Weapon.WeaponType.Melee:
                currentWeapon = Instantiate(WeaponManager.Instance.meleeWeapon, fist);
                (currentWeapon as Sword)?.Init();
                break;
            case Weapon.WeaponType.Ranged:
                currentWeapon = Instantiate(WeaponManager.Instance.rangedWeapon, fist);
                (currentWeapon as Gun)?.Init();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newWeaponType), newWeaponType, null);
        }
    }

    public void StartClimbing(Ladder ladder)
    {
        isClimbing = true;
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        _playerModel.rotation = Quaternion.Euler(0, -90f, 0);
    }

    public void StopClimbing(Ladder ladder)
    {
        isClimbing = false;
        //_rigidBody.velocity = Vector3.zero;
        _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        if (transform.position.y > ladder.transform.position.y + 6f) //3f is ladder height. TODO: Ladder height calculation.
        {
            transform.Translate(0, 0, 2f);
            LevelUpEvent.Raise();
        }

        if (facingRight)
        {
            _playerModel.rotation = Quaternion.identity;
        }
        else
        {
            _playerModel.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }

    public void Hurt(int amount)
    {
        if (_invulnerableTimer <= 0 || amount > 666) //because lava deals 999
        {
            Health -= amount;
            if (Health <= 0)
            {
                Health = 0;
                Destroy(gameObject);
            }

            OnPlayerHurtEvent.Raise();
            _invulnerableTimer = 0.5f;
        }
    }

    private void OnDestroy()
    {
        GameOverEvent.Raise();
        Time.timeScale = 0;
    }
}