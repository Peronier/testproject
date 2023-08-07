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

        // 移動入力がある場合に歩きモーションを再生
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
        // ここで移動可能なセルかどうかの判定を行う処理を追加する
        // 例えば、タイルの情報や他のオブジェクトの位置などを参照して判定することが考えられる
        return true;
    }
}
