using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class DiePanel : MonoBehaviour {

    public CanvasGroup thisGroup;


    public void setPanel()
    {
        thisGroup.alpha = 1;
        thisGroup.interactable = true;
        thisGroup.blocksRaycasts = true;
    }

    public void OnClickGoHome()
    {
        GameData.useMoney(GameData.getMoney() / 10);
        SceneManager.LoadScene("shop");
    }

}
