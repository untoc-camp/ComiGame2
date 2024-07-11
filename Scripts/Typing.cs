using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI components
using System.Text; // Required for StringBuilder

public class Typing : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textComponent; // Public field to assign the Text component in the inspector

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TypeTextEffect("정커미 키우기")); // Start typing effect when the game starts
    }

    // Update is called once per frame
    void Update()
    {
        // Here you can check for other inputs or update other components dynamically if needed
    }

    // Coroutine for typing effect
    IEnumerator TypeTextEffect(string text)
    {
        textComponent.text = string.Empty; // Clear the text component before starting the typing effect

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            stringBuilder.Append(text[i]);
            textComponent.text = stringBuilder.ToString(); // Update the text component with the current string
            yield return new WaitForSeconds(0.5f); // Delay to simulate the typing effect
        }
    }
}
