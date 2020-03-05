using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private TextMeshProUGUI _textField;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player")?.GetComponent<Player>();
        _textField = GetComponent<TextMeshProUGUI>();
    }

    public void Refresh()
    {
        _textField.text = "Health: " + _player.Health;
    }
}
