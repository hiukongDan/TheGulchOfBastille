using UnityEngine;
using UnityEngine.UI;

public class ButtonGroup : MonoBehaviour
{
    // Start is called before the first frame update

    private Button[] btns;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
