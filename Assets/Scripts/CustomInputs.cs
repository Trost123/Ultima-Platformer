using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CustomInputs : MonoBehaviour
{
    public Slider movementSlider;

    public static float XAxis;
    public static bool AttackPressed;
    public static bool JumpPressed;
    public static bool DashPressed;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void ResetSliderOnPointerUp()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (movementSlider)
        {
            XAxis = (movementSlider.value - 0.5f) * 2;
        }
    }

    public void SetAttack(bool value)
    {
        AttackPressed = value;
    }
    
    public void SetJump(bool value)
    {
        JumpPressed = value;
    }

    public void SetDash(bool value)
    {
        DashPressed = value;
    }
}
