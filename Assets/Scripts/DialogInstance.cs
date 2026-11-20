using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogInstance : MonoBehaviour
{
    string CurrentDialog;

    public bool Busy => ActiveDialog != null;
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

    public void QueueDialog(string dialog, float speedMult = 1f)
    {
        if (ActiveDialog == null)
			ActiveDialog = StartCoroutine(WriteDialog(dialog, speedMult));
        else
			DialogQueue.Enqueue(WriteDialog(dialog));
    }

    public void QueueIfNotBusy(string dialog, float speedMult = 1f)
    {
        if (ActiveDialog == null)
			ActiveDialog = StartCoroutine(WriteDialog(dialog));
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
        transform.localScale = new Vector3(Mathf.Sign(transform.parent.localScale.x), 1f, 1f);
	}
}
