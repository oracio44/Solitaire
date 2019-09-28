using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDroppable
{
    bool CheckDrop(CardGameObject card);
    void DoDrop(CardGameObject cardGO);
}
