using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class NPC : MonoBehaviour
{
	public float IdleAnimSpeed = 2f;
	public float IdleAnimHeight = 0.5f;
	public float TalkAnimSpeed = 2f;
	public Sprite[] TalkSprites;
	
	private SpriteRenderer spriteRenderer;
	private DialogInstance dialog;

	private bool ConditionMet = false;
	private Vector3 startPosition;

	public void MeetCondition()
	{
		ConditionMet = true;
	}

	void Start()
    {
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
		dialog = transform.Find("Canvas/Dialog").GetComponent<DialogInstance>();
		startPosition = transform.position;
	}

	private void Update()
	{
		transform.position = new Vector3(startPosition.x, startPosition.y + Mathf.Sin(Time.time * IdleAnimSpeed) * IdleAnimHeight, startPosition.z);

		if (dialog.Busy)
		{
			spriteRenderer.sprite = TalkSprites[(int)(Time.time * TalkAnimSpeed) % TalkSprites.Length];
		}
		else
		{
			spriteRenderer.sprite = TalkSprites[0];
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!dialog.Busy)
		{
			if (ConditionMet)
				dialog.QueueDialog("Yooo good work flipping the lever!");
			else
			{
				dialog.QueueDialog($"My name is {gameObject.name}!");
				dialog.QueueDialog("You should flip that lever dawg.");
			}
		}
	}
}
