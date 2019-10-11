using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {



	public Button[] levelButtons;

	void Start ()
	{
		int levelReached = PlayerPrefs.GetInt("levelReached", 1);

		for (int i = 0; i < levelButtons.Length; i++)
		{
			if (i + 1 > levelReached)
				levelButtons[i].interactable = false;
		}
	}

    public void resetPlayerPrefs()
    {
        foreach (var button in levelButtons)
        {
            button.interactable = false;
        }

        levelButtons[0].interactable = true;

        //levelButtons[1].interactable = false;
        //levelButtons[2].interactable = false;
        //levelButtons[3].interactable = false;
        //levelButtons[4].interactable = false;

        PlayerPrefs.DeleteAll();
    }

}
