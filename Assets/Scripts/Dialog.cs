using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Dialog : MonoBehaviour
{
    string CurrentDialog;
    public static Dialog instance { get; private set; }

    private TextMeshProUGUI TextBox;
    private Coroutine ActiveDialog;
    private readonly System.Collections.Generic.Queue<IEnumerator> DialogQueue = new();

    IEnumerator WriteDialog(string dialog, float speedMult = 1f)
    {
        yield return null;

        TextBox.text = dialog;
        TextBox.maxVisibleCharacters = 0;
        TextBox.alpha = 1f;

		for (int i = 0; i < dialog.Length; i++)
        {
            yield return new WaitForSeconds(0.07f * speedMult);
			TextBox.maxVisibleCharacters = i+1;
        }

        yield return new WaitForSeconds(dialog.Length / 20f + 0.5f);

        float progress = 0f;

        while (progress < 1f)
        {
            yield return null;
            progress = Mathf.Min(progress + Time.deltaTime * 3.5f * speedMult, 1f);
            TextBox.alpha = 1f - progress;
        }

		TextBox.text = string.Empty;
		TextBox.maxVisibleCharacters = 0;

		if (DialogQueue.Count > 0)
            ActiveDialog = StartCoroutine(DialogQueue.Dequeue());
        else
            ActiveDialog = null;

	}

    public static void QueueDialog(string dialog, float speedMult = 1f)
    {
        if (instance.ActiveDialog == null)
			instance.ActiveDialog = instance.StartCoroutine(instance.WriteDialog(dialog, speedMult));
        else
			instance.DialogQueue.Enqueue(instance.WriteDialog(dialog));
    }

    public static void QueueIfNotBusy(string dialog, float speedMult = 1f)
    {
        if (instance.ActiveDialog == null)
			instance.ActiveDialog = instance.StartCoroutine(instance.WriteDialog(dialog));
	}
    
	private void Awake()
	{
        instance = this;
	}

	private void Start()
	{
        TextBox = GetComponent<TextMeshProUGUI>();
		TextBox.text = string.Empty;
		TextBox.maxVisibleCharacters = 0;
		TextBox.alpha = 0f;
	}

	private void Update()
	{
        ((RectTransform)transform).localScale = new Vector3(Mathf.Sign(transform.parent.lossyScale.x), 1f, 1f);
	}
}
