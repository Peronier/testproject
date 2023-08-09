using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Edir;

public static class DirUtil
{

    /**
    * ���͂��ꂽ�L�[�ɑΉ����������Ԃ�
    */
    public static EDir KeyToDir()
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
    public static Quaternion DirToRotation(EDir d)
    {
        Quaternion r = Quaternion.Euler(0, 0, 0);
        switch (d)
        {
            case EDir.Left:
                r = Quaternion.Euler(0, 90, 0); break;
            case EDir.Up:
                r = Quaternion.Euler(0, 180, 0); break;
            case EDir.Right:
                r = Quaternion.Euler(0, 270, 0); break;
            case EDir.Down:
                r = Quaternion.Euler(0, 0, 0); break;
        }
        return r;
    }

    /**
     * �����ŗ^����ꂽ��]�̃x�N�g���ɑΉ����������Ԃ�
     */
    public static EDir RotationToDir(Quaternion r)
    {
        float y = r.eulerAngles.y;
        if (y < 45)
        {
            return EDir.Down;
        }
        else if (y < 135)
        {
            return EDir.Left;
        }
        else if (y < 225)
        {
            return EDir.Up;
        }
        else if (y < 315)
        {
            return EDir.Right;
        }

        return EDir.Down;
    }

    /**
    * ���݂̍��W(position)�ƈړ�����������(d)��n����
    * �ړ���̍��W���擾
    */
    public static Pos2D GetNewGrid(Pos2D position, EDir d)
    {
        GameObject newObject = new GameObject("NewPos2D");
        Pos2D newP = newObject.AddComponent<Pos2D>();
        newP.x = position.x;
        newP.z = position.z;
        switch (d)
        {
            case EDir.Left:
                newP.x += 2; break;
            case EDir.Up:
                newP.z -= 2; break;
            case EDir.Right:
                newP.x -= 2; break;
            case EDir.Down:
                newP.z += 2; break;
        }
       
        return newP;
    }
}
