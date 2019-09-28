using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    void Click(Vector2 ClickPoint);
    void Activate();
}
