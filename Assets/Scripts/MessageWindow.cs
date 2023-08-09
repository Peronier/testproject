using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindow : MonoBehaviour
{
    public Text text;
    public float maxPerFrameH = 0.5f;
    public float maxPerFrameV = 1.0f;
    private bool isAdding = false;
    private bool isFalling = false;

    void Start()
    {

    }

    void Update()
    {
        if (isAdding)
        {
            MessageAnimation anim;
            if (!isFalling)
            {
                anim = transform.GetChild(transform.childCount - 1).GetComponent<MessageAnimation>();
                isAdding = !anim.MoveMessage(transform.position + new Vector3(0, 0, 0), maxPerFrameH);
                return;
            }

            for (int i = 0; i < transform.childCount - 1; i++)
            {
                anim = transform.GetChild(i).GetComponent<MessageAnimation>();
                if (anim.IsDeleting()) continue;
                isFalling = !anim.MoveMessage(transform.position + new Vector3(0, -100 * (transform.childCount - i - 1), 0), maxPerFrameV);
            }
        }
        else ShowMessage();
    }

    /**
     * ���b�Z�[�W��ǉ��i�\���j����
     */
    private void ShowMessage()
    {
        if (Message.getCount() > 0)
        {
            isAdding = true;
            isFalling = transform.childCount > 0;
            string m = Message.get();
            Text msg = Instantiate(text, transform);
            msg.transform.position = transform.position + new Vector3(-400, 0, 0);
            msg.text = m;
        }
    }
}
