using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    public float MovementSpeed = 5f;
    public float JumpPower = 10f;
    public float CoyoteTime = 0.5f;
	public float JumpCooldown = 0.25f;
	private int _Coins = 0;
	public int Coins { get => _Coins; set { _Coins = value; CoinsText.text = "Coins: " + _Coins; } }
	public bool Grounded = true;
	public bool Jumping = false;
	public LayerMask WorldLayer;

	public TextMeshProUGUI CoinsText;

	public float IdleAnimSpeed = 2f;
	[FormerlySerializedAs("IdleSprites")] public Sprite[] MovementSprites;
	//public ParticleSystem DirtParticles;
	
	private Rigidbody2D rigid;
	private CapsuleCollider2D ccollider;
	private SpriteRenderer spriteRenderer;
	private InputAction moveAction;
	private InputAction jumpAction;
	private Vector3 startScale;

	public static Player instance;
	public new static CapsuleCollider2D collider;

	private void Awake()
	{
		instance = this;
	}

	void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ccollider = GetComponent<CapsuleCollider2D>();
		collider = ccollider;
        spriteRenderer = GetComponent<SpriteRenderer>();
		moveAction = InputSystem.actions.FindAction("Move");
		jumpAction = InputSystem.actions.FindAction("Jump");
		startScale = transform.localScale;

		Dialog.QueueDialog("I AM ALIVE!!!!!!!!!");
	}

	private float JumpMoment = 0;
	private float CoyoteMoment = 0;

	private void Update()
	{
		if (Mathf.Abs(rigid.linearVelocityX) > 0.5f)
		spriteRenderer.sprite = MovementSprites[(int)(Time.time * IdleAnimSpeed) % MovementSprites.Length];
		else
			spriteRenderer.sprite = MovementSprites[0];

		if (!Jumping && (Grounded || Time.time - CoyoteMoment <= CoyoteTime) && Time.time - JumpMoment >= JumpCooldown && jumpAction.WasPressedThisFrame())
		{
			Jumping = true;
			JumpMoment = Time.time;
			rigid.linearVelocityY += JumpPower;
			Dialog.QueueIfNotBusy("Wahoo!", 1.5f);
		}
	}

	private void FixedUpdate()
	{
		bool wasGrounded = Grounded;
		Grounded = Physics2D.Raycast(transform.position, -transform.up, ccollider.size.y * transform.lossyScale.y + 0.01f, WorldLayer).collider != null;

		//if (!wasGrounded && Grounded && rigid.linearVelocityY < -0.5f)
			//DirtParticles.Emit((int)Mathf.Min(Mathf.Abs(rigid.linearVelocityY) * 2f, 30f));

		if (Grounded && Jumping && Time.time - JumpMoment >= JumpCooldown)
			Jumping = false;

		if (wasGrounded && !Grounded)
			CoyoteMoment = Time.fixedTime;

		float moveDir = moveAction.ReadValue<Vector2>().x;
		rigid.linearVelocityX = moveDir * MovementSpeed;

		if (moveDir > 0f)
			transform.localScale = new Vector3(-startScale.x, startScale.y, startScale.z);
		else if (moveDir < 0f)
			transform.localScale = startScale;

		//ParticleSystem.EmissionModule emission = DirtParticles.emission;
		//emission.enabled = (moveDir > 0f || moveDir < 0f) && Grounded;
	}
}
