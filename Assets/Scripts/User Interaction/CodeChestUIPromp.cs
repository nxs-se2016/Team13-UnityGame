using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class CodeChestUISetup : MonoBehaviour
{
    public void SetupUI()
    {
        // Create panel
        GameObject panel = new GameObject("CodePanel");
        panel.transform.SetParent(transform);
        RectTransform panelRect = panel.AddComponent<RectTransform>();
        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.9f);
        
        // Set panel size and position
        panelRect.anchorMin = new Vector2(0.5f, 0.5f);
        panelRect.anchorMax = new Vector2(0.5f, 0.5f);
        panelRect.sizeDelta = new Vector2(300, 100);
        panelRect.anchoredPosition = Vector2.zero;

        // Create input fields
        for (int i = 0; i < 4; i++)
        {
            GameObject inputObj = new GameObject($"Input_{i + 1}");
            inputObj.transform.SetParent(panel.transform);
            RectTransform inputRect = inputObj.AddComponent<RectTransform>();
            TMP_InputField input = inputObj.AddComponent<TMP_InputField>();
            
            // Position input field
            inputRect.anchorMin = new Vector2(0, 0.5f);
            inputRect.anchorMax = new Vector2(0, 0.5f);
            inputRect.sizeDelta = new Vector2(50, 50);
            inputRect.anchoredPosition = new Vector2(60 + (i * 60), 0);
            
            // Add required visual components
            GameObject textArea = new GameObject("Text Area");
            textArea.transform.SetParent(inputObj.transform);
            RectTransform textAreaRect = textArea.AddComponent<RectTransform>();
            textAreaRect.anchorMin = Vector2.zero;
            textAreaRect.anchorMax = Vector2.one;
            textAreaRect.sizeDelta = Vector2.zero;
            
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(textArea.transform);
            RectTransform textRect = textObj.AddComponent<RectTransform>();
            TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.sizeDelta = Vector2.zero;
            
            // Setup input field
            input.textViewport = textAreaRect;
            input.textComponent = tmp;
            input.characterLimit = 1;
            input.contentType = TMP_InputField.ContentType.IntegerNumber;
        }
    }
}