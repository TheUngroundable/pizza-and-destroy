using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    public int money;
    private TextMesh text;

    public void Start()
    {
        text = GetComponent<TextMesh>();
        transform.LookAt(UnityEngine.Camera.main.transform.position);


        if (money > 0)
        {
            text.color = Color.green;
            text.text = "+" + money.ToString() + "$";
        }
        else if (money < 0)
        {
            text.color = Color.red;
            text.text = money.ToString() + "$";
        }
        else
        {
            text.text = "";
        }

        Destroy(gameObject, 3f);
    }

}
