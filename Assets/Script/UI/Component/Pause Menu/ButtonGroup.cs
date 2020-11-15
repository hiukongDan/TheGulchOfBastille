using UnityEngine;
using UnityEngine.UI;

public class ButtonGroup : MonoBehaviour
{
    // Start is called before the first frame update

    private Button[] btns;

    private GameManager gm;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void OnEnable()
    {
        if(btns == null)
        {
            btns = GetComponentsInChildren<Button>();
        }

        if(btns != null)
        {
            btns[0].Select();
        }
    }

    public void SelectBtn(Button btn)
    {

    }
}
