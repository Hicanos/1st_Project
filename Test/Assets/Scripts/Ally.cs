using UnityEngine;

public class Ally : MonoBehaviour, IDamagable
{
    public void TakePhysicalDamage(int amount)
    {
        Debug.Log($"아군 유닛이 {amount}의 피해를 입었습니다.");
    }
}