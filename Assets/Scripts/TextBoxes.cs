using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxes : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    public string[] welcomeLines;
    public string[] firstDeathLines;

    public MainManager MainManager;
    public float textSpeed;
    public string[] currentLines;

    [SerializeField]private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        Debug.Log("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == currentLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = currentLines[index];
            }
        }
    }

    public void StartDialogue()
    {
        switch (MainManager.Instance.deathCount)
        {
            case 0:
                currentLines = welcomeLines;
                break;
            case 1:
                currentLines = firstDeathLines;
                break;
        }
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in currentLines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
            if (new List<char>(currentLines[index].ToCharArray()).IndexOf(c) == currentLines[index].ToCharArray().Length - 1)
            {
                textComponent.text = currentLines[index];
            }
        }
    }

    void NextLine()
    {
        if (index < currentLines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            MainManager.Instance.paused = false;
            gameObject.SetActive(false);
        }
    }
}
