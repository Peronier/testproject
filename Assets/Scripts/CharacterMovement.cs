using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Grid grid;
    public Animator animator;
    private Vector3Int currentCell;

    void Start()
    {
        currentCell = grid.WorldToCell(transform.position);
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector3Int targetCell = currentCell + new Vector3Int(Mathf.RoundToInt(horizontalInput), Mathf.RoundToInt(verticalInput), 0);
            if (IsCellWalkable(targetCell))
            {
                currentCell = targetCell;
            }
        }

        Vector3 targetPosition = grid.GetCellCenterWorld(currentCell);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // �ړ����͂�����ꍇ�ɕ������[�V�������Đ�
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            animator.SetBool("IsSprint", true);
        }
        else
        {
            animator.SetBool("IsSprint", false);
        }
    }

    bool IsCellWalkable(Vector3Int cell)
    {
        // �����ňړ��\�ȃZ�����ǂ����̔�����s��������ǉ�����
        // �Ⴆ�΁A�^�C���̏��⑼�̃I�u�W�F�N�g�̈ʒu�Ȃǂ��Q�Ƃ��Ĕ��肷�邱�Ƃ��l������
        return true;
    }
}
