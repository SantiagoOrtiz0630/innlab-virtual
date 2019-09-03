using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{

    public GameObject m_HelpObject;
    private Button m_Button;

    private void Awake()
    {
        m_Button = GetComponentInChildren<Button>();

        m_Button.onClick.AddListener(taskOnClick);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void taskOnClick()
    {
        m_HelpObject.SetActive(!m_HelpObject.activeSelf);
    }
}
