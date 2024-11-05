using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CodeChest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _lockedPrompt = "Enter Code (E)";
    [SerializeField] private string _unlockedPrompt = "Open (E)";
    [SerializeField] private GameObject _codePanel; // This should be the parent panel containing both image and inputs
    [SerializeField] private TMP_InputField[] _codeInputs = new TMP_InputField[4];
    [SerializeField] private int[] _correctCode = new int[4];
    [SerializeField] private Image _panelImage;
    
    [SerializeField] private Color _defaultColor = new Color(0, 0, 0, 0.9f);
    [SerializeField] private Color _correctColor = new Color(0, 1, 0, 0.9f);
    [SerializeField] private Color _wrongColor = new Color(1, 0, 0, 0.9f);
    
    private bool _isUnlocked = false;
    private bool _isPanelOpen = false;
    private bool _hasBeenOpened = false;

    public string InteractionPrompt 
    { 
        get => _isPanelOpen ? "" : (_hasBeenOpened ? "" : (_isUnlocked ? _unlockedPrompt : _lockedPrompt));
    }

    private void Start()
    {
        _codePanel.SetActive(false);
        _panelImage.color = _defaultColor;
        
        for (int i = 0; i < _codeInputs.Length; i++)
        {
            int index = i;
            _codeInputs[i].characterLimit = 1;
            _codeInputs[i].contentType = TMP_InputField.ContentType.IntegerNumber;
            _codeInputs[i].onValueChanged.AddListener((string value) => OnInputValueChanged(index, value));
            _codeInputs[i].onSelect.AddListener((string value) => OnInputSelected(index));
        }
    }

    private void OnInputSelected(int index)
    {
        if (!string.IsNullOrEmpty(_codeInputs[index].text))
        {
            _codeInputs[index].text = "";
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (_hasBeenOpened)
        {
            return false;
        }

        if (_isUnlocked)
        {
            Debug.Log("Chest Opened");
            ClosePanel();
            _hasBeenOpened = true;
            return true;
        }
        
        _isPanelOpen = !_isPanelOpen;
        _codePanel.SetActive(_isPanelOpen);
        
        if (_isPanelOpen)
        {
            _panelImage.color = _defaultColor;
            ClearAllInputs();
            _codeInputs[0].Select();
            _codeInputs[0].ActivateInputField();
        }
        
        return true;
    }

    private void OnInputValueChanged(int index, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            if (index > 0)
            {
                _codeInputs[index - 1].Select();
                _codeInputs[index - 1].ActivateInputField();
            }
            return;
        }

        if (index < _codeInputs.Length - 1)
        {
            _codeInputs[index + 1].Select();
            _codeInputs[index + 1].ActivateInputField();
        }

        bool allFilled = true;
        int[] currentCode = new int[4];
        
        for (int i = 0; i < _codeInputs.Length; i++)
        {
            if (string.IsNullOrEmpty(_codeInputs[i].text))
            {
                allFilled = false;
                break;
            }
            currentCode[i] = int.Parse(_codeInputs[i].text);
        }

        if (allFilled)
        {
            CheckCode(currentCode);
        }
    }

    private void CheckCode(int[] inputCode)
    {
        bool isCorrect = true;
        for (int i = 0; i < 4; i++)
        {
            if (inputCode[i] != _correctCode[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            _isUnlocked = true;
            Debug.Log("Correct Code!");
            _panelImage.color = _correctColor;
            StartCoroutine(CloseAfterDelay(0.5f));
        }
        else
        {
            Debug.Log("Wrong Code!");
            _panelImage.color = _wrongColor;
            StartCoroutine(ResetInputsAfterDelay(0.5f));
        }
    }

    private System.Collections.IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClosePanel();
    }

    private System.Collections.IEnumerator ResetInputsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _panelImage.color = _defaultColor;
        ClearAllInputs();
        _codeInputs[0].Select();
        _codeInputs[0].ActivateInputField();
    }

    private void ClearAllInputs()
    {
        foreach (var input in _codeInputs)
        {
            input.text = "";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPanelOpen)
        {
            ClosePanel();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            for (int i = 0; i < _codeInputs.Length; i++)
            {
                if (_codeInputs[i].isFocused)
                {
                    if (string.IsNullOrEmpty(_codeInputs[i].text) && i > 0)
                    {
                        _codeInputs[i - 1].text = "";
                        _codeInputs[i - 1].Select();
                        _codeInputs[i - 1].ActivateInputField();
                    }
                    break;
                }
            }
        }
    }

    public void ClosePanel()
    {
        _codePanel.SetActive(false);
        _isPanelOpen = false;
    }

    public void OnInteractionExit()
    {
        if (_isPanelOpen)
        {
            ClosePanel();
        }
    }
}