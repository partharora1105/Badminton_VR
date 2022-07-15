using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class UiManager : MonoBehaviour
{
    [SerializeField] private InputActionReference buttonReference = null;
    [SerializeField] private InputActionReference triggerReference = null;
    [SerializeField] private InputActionReference gripReference = null;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Opponent;
    [SerializeField] private TextMeshProUGUI[] textFields = new TextMeshProUGUI[4];
    [SerializeField] private Image[] bgFields = new Image[4];
    [SerializeField] private AudioSource audioSource;
    private int[] levelNumbers = { 2, 2, 1, 1};
    private int currentTextIndex;
    private string[] levels = new string[3];

    private void Awake()
    {
        buttonReference.action.started += UiDisplay;
        triggerReference.action.started += goDown;
        gripReference.action.started += changeLevel;
    }

    private void Start()
    {
        UI.SetActive(false);
        levels[0] = "Low";
        levels[1] = "Medium";
        levels[2] = "High";
    }
    private void OnDestroy()
    {
        buttonReference.action.started -= UiDisplay;
        triggerReference.action.started -= goDown;
        gripReference.action.started -= changeLevel;
    }

    private void UiDisplay(InputAction.CallbackContext context)
    {
        
        if (!UI.activeSelf)
        {
            bgFields[0].color = new Color(0, 0.854902f, 0.7568f);
            for (int i = 1; i < 4; i++)
            {
                bgFields[i].color = new Color(0.38f, 0.38f, 0.38f);
            }
            currentTextIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                textFields[i].text = levels[levelNumbers[i] - 1];
            }
            audioSource.Play();
        }
        UI.SetActive(!UI.activeSelf);

    }
    private void goDown(InputAction.CallbackContext context)
    {
        if (UI.activeSelf)
        {
            bgFields[currentTextIndex].color = new Color(0.38f , 0.38f, 0.38f);
            currentTextIndex += (currentTextIndex == 3) ? -3 : 1;
            bgFields[currentTextIndex].color = new Color(0, 0.854902f, 0.7568f);
            audioSource.Play();
        }
    }
    private void changeLevel(InputAction.CallbackContext context)
    {
        if (UI.activeSelf)
        {
            int currentLevel = levelNumbers[currentTextIndex];
            currentLevel += (currentLevel == 3) ? -2 : 1;
            levelNumbers[currentTextIndex] = currentLevel;
            textFields[currentTextIndex].text = levels[levelNumbers[currentTextIndex] - 1];
            if (currentTextIndex == 0)
            {
                Opponent.GetComponent<Opponent>().setPrecision(currentLevel);
                levelNumbers[0] = (int)Opponent.GetComponent<Opponent>().getPrecision();
                //textFields[currentTextIndex + 1].text = levels[currentLevel - 1] = (Opponent.GetComponent<Opponent>().getPrecision()).ToString();
            }
            else if (currentTextIndex == 1)
            {
                Opponent.GetComponent<Opponent>().setSpeed(currentLevel);
                levelNumbers[1] = (int) Opponent.GetComponent<Opponent>().getSpeed();
            }
            else if (currentTextIndex == 2)
            {
                Opponent.GetComponent<Opponent>().setStamina(currentLevel);
                levelNumbers[2] = (int) Opponent.GetComponent<Opponent>().getStamina();
            }
            else if (currentTextIndex == 3)
            {
                Opponent.GetComponent<Opponent>().setStrength(currentLevel);
                levelNumbers[3] = (int) Opponent.GetComponent<Opponent>().getStrength();
            }
            audioSource.Play();
        }
    }



}
