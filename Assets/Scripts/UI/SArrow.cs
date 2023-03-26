using UnityEngine;
using UnityEngine.UI;

public class SArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;

    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip pressSound;
    private RectTransform rect;
    private int curruntPos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Move Arrow
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);

        //Press option
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Press();
    }
    private void ChangePosition(int _change)
    {
        curruntPos += _change;

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if (curruntPos < 0)
            curruntPos = options.Length - 1;
        else if (curruntPos > options.Length - 1)
            curruntPos = 0;

        rect.position = new Vector3(rect.position.x, options[curruntPos].position.y, 0);
    }

    private void Press()
    {
        SoundManager.instance.PlaySound(pressSound);

        options[curruntPos].GetComponent<Button>().onClick.Invoke();
    }
}
