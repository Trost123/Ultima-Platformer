using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    private int _currentLevel = 1;

    private TextMeshProUGUI _levelTF;
    // Start is called before the first frame update
    void Start()
    {
        _levelTF = GetComponent<TextMeshProUGUI>();
    }

    public void LevelUp()
    {
        _currentLevel++;
        _levelTF.text = "Level: " + _currentLevel;
    }
}
