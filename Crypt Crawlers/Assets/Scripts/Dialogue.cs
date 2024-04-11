using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    private bool isTyping;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F)) {
            if (textComponent.text == lines[index]) {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            gameObject.SetActive(false);
        }
    }

    void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        isTyping = true;
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    void NextLine() {
        if (!isTyping) {
            if (index < lines.Length - 1) {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine()); 
            }
            else {
                index = 0; // Reset index to start from the beginning
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialogue();
            gameObject.SetActive(true);
        }
    }
}
