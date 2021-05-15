using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrintVariable : MonoBehaviour
{
    public Text text;
    public IntVariable variable;

    private void Update()
    {
        text.text = variable.Value.ToString();
    }
}
