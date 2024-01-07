using Game.Tasks;
using Logging;
using UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private readonly Logger m_LOG = new Logger(new LogHandler());
    private const string LOGTag = "HUD";

    public IntegrityIndicator iIndicator;
    public TextPanel uiTextBox;
    public TimerPanel timerPanel;
    public UINavigator navigator;

    private Camera cam;

    private bool initializedCorrectly = false;

    // Start is called before the first frame update
    void Start()
    {
        if (iIndicator != null && uiTextBox != null && timerPanel != null && navigator != null)
        {
            initializedCorrectly = true;
            m_LOG.Log(LOGTag, "HUD Objects set correctly!");
            
        }
        else
        {
            m_LOG.Log(LOGTag, "HUD Objects not set correctly!");
        }

        cam = gameObject.GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        navigator.UpdateMarkers(gameObject.GetComponentInParent<RectTransform>());
    }

    /// <summary>
    /// Update the integrity value for the HUD indicator.
    /// Values are set as percentages between 0.0 and 1.0.
    /// </summary>
    /// <param name="percentage">Value to set indicator to.</param>
    public void UpdateIntegrityIndicator(float percentage)
    {
        if (initializedCorrectly)
        {
            iIndicator.changeBar(percentage);
        }
    }

    /// <summary>
    /// Update the HUD TextPanel text.
    /// String can be of any length, the Panel subdivides the string into pages.
    /// </summary>
    /// <param name="text">Text to set</param>
    public void ChangeText(string text)
    {
        if (initializedCorrectly)
        {
            uiTextBox.DisplayText(text);
        }
    }

    /// <summary>
    /// Dismiss the current text and free the panel for the next test.
    /// Should be called in between ChangeText() calls, so the player gets notified of new text.
    /// </summary>
    public void DismissText()
    {
        if (initializedCorrectly)
        {
            uiTextBox.DismissText();
        }
    }

    /// <summary>
    /// Update the Timer on the HUD.
    /// Values must be given as a float in format  seconds.millis
    /// The Panel will calculate remaining minutes
    /// </summary>
    /// <param name="val">Value to set timer to</param>
    public void UpdateTime(float val)
    {
        timerPanel.UpdateTime(val);
    }


    public void registerNewTask(GameTask task, Vector3 spawnLocation, ObjectiveMarker.TaskType type)
    {
        navigator.InitializeMarker(task, spawnLocation, type);
        
        task.GameObjectDestroyed += (gameTask) =>
        {
            navigator.DismissMarker(gameTask);
        };
    }
    
    
    public void ChangeNavigation(bool active)
    {
        navigator.gameObject.SetActive(active);
    }
    

    public Camera ParentCamera()
    {
        return cam;
    }
}
