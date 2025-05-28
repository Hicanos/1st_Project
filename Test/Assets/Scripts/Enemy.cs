using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public void TakePhysicalDamage(int amount)
    {
        Debug.Log($"적 유닛이 {amount}의 피해를 입었습니다.");
    }
}