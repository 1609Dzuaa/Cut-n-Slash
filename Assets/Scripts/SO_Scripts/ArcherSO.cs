using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/ArcherStats")]
public class ArcherSO : EnemiesSO
{
    [Header("Withdrawn Related")]
    [SerializeField] Vector2 _withdrawnForce;
    [Tooltip("Khoảng cách mà khi Player ở đủ gần" +
        " thì nó sẽ Withdrawn")]
    [SerializeField] Vector2 _withdrawnableRange;
    [SerializeField, Tooltip("Khoảng thgian để có thể Withdrawn tiếp")] float _withdrawnDelay;

    [Header("Teleport Related"), Tooltip("Archer sẽ Tele nếu bị Player dồn vào góc chết:" +
        " Phía sau có tường hoặc bờ vực")]
    [SerializeField] Vector2 _check2Size;
    [SerializeField] float _teleDist;

    public Vector2 WithdrawnForce { get => _withdrawnForce; }

    public Vector2 WithdrawnableRange { get => _withdrawnableRange; }

    public float WithdrawnDelay { get => _withdrawnDelay; }

    public Vector2 Check2Size { get => _check2Size; }

    public float TeleDist { get => _teleDist; }
}
