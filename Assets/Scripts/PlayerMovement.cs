using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Edir;


public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public EDir direction = EDir.Up;
    public float speed = 0.9f;
    public float speedDampTime = 0.1f;

    private readonly int hashSpeedPara = Animator.StringToHash("Speed");

    public Pos2D grid;

    public int maxFrame = 100;
    private int currentFrame = 0;
    private Pos2D newGrid;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (currentFrame == 0)
        {
            EDir d = KeyToDir();
            if (d == EDir.Pause)
                animator.SetFloat(hashSpeedPara, 0.0f, speedDampTime, Time.deltaTime);
            else
            {
                direction = d;
                Message.add(direction.ToString());
                transform.rotation = DirToRotation(direction);
                newGrid = GetNewGrid(grid, direction);
                grid = Move(grid, newGrid, ref currentFrame);
            }
        }
        else grid = Move(grid, newGrid, ref currentFrame);
    }

    /**
    * ���͂��ꂽ�L�[�ɑΉ����������Ԃ�
    */
    private EDir KeyToDir()
    {
        if (!Input.anyKey)
        {
            return EDir.Pause;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return EDir.Left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return EDir.Up;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return EDir.Right;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            return EDir.Down;
        }
        return EDir.Pause;
    }

    /**
    * �����ŗ^����ꂽ�����ɑΉ������]�̃x�N�g����Ԃ�
    */
    private Quaternion DirToRotation(EDir d)
    {
        Quaternion r = Quaternion.Euler(0, 0, 0);
        switch (d)
        {
            case EDir.Left:
                r = Quaternion.Euler(0, 270, 0); break;
            case EDir.Up:
                r = Quaternion.Euler(0, 0, 0); break;
            case EDir.Right:
                r = Quaternion.Euler(0, 90, 0); break;
            case EDir.Down:
                r = Quaternion.Euler(0, 180, 0); break;
        }
        return r;
    }

    /**
    * �O���b�h���W�����[���h���W�ɕϊ�
    */
    private float ToWorldX(int xgrid)
    {
        return xgrid * 2;
    }

    private float ToWorldZ(int zgrid)
    {
        return zgrid * 2;
    }

    /**
    * ���[���h���W���O���b�h���W�ɕϊ�
    */
    private int ToGridX(float xworld)
    {
        return Mathf.FloorToInt(xworld / 2);
    }

    private int ToGridZ(float zworld)
    {
        return Mathf.FloorToInt(zworld / 2);
    }

    /**
    * �⊮�Ōv�Z���Đi��
    */
    private Pos2D Move(Pos2D currentPos, Pos2D newPos, ref int frame)
    {
        float px1 = ToWorldX(currentPos.x);
        float pz1 = ToWorldZ(currentPos.z);
        float px2 = ToWorldX(newPos.x);
        float pz2 = ToWorldZ(newPos.z);
        frame += 1;
        float t = (float)frame / maxFrame;
        float newX = px1 + (px2 - px1) * t;
        float newZ = pz1 + (pz2 - pz1) * t;
        transform.position = new Vector3(newX, 0, newZ);
        animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltaTime);
        if (maxFrame == frame)
        {
            frame = 0;
            return newPos;
        }
        return currentPos;
    }

    /**
    * ���݂̍��W(position)�ƈړ�����������(d)��n����
    * �ړ���̍��W���擾
    */
    private Pos2D GetNewGrid(Pos2D position, EDir d)
    {
        GameObject newObject = new GameObject("NewPos2D");
        Pos2D newP = newObject.AddComponent<Pos2D>();
        newP.x = position.x;
        newP.z = position.z;
        switch (d)
        {
            case EDir.Left:
                newP.x -= 2; break;
            case EDir.Up:
                newP.z += 2; break;
            case EDir.Right:
                newP.x += 2; break;
            case EDir.Down:
                newP.z -= 2; break;
        }
        Destroy(newObject);
        return newP;
    }

    //�C���X�y�N�^�[�̒l���ς�����Ƃ��ɌĂяo�����
     void OnValidate()
    {
        if(grid.x != ToGridX(transform.position.x) || grid.z != ToGridZ(transform.position.z))
        {
            transform.position = new Vector3(ToWorldX(grid.x), 0, ToWorldZ(grid.z));
        }
        if (direction != RotationToDir(transform.rotation))
        {
            transform.rotation = DirToRotation(direction);
        }
    }

    /**
     * �����ŗ^����ꂽ��]�̃x�N�g���ɑΉ����������Ԃ�
     */
    private EDir RotationToDir(Quaternion r)
    {
        float y = r.eulerAngles.y;
        if(y < 45)
        {
            return EDir.Up;
        }
        else if(y < 135)
        {
            return EDir.Right;
        }
        else if(y < 225)
        {
            return EDir.Down;
        }
        else if(y < 315)
        {
            return EDir.Left;
        }

        return EDir.Up;
    }


}
