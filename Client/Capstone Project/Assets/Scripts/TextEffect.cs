using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public Text m_TypingText;
    public string m_Message { get; set; }
    public float m_speed = 0.03f;

    bool isTyping = false;

    // Start is called before the first frame update
    void Start()
    {
        m_TypingText = gameObject.GetComponent<Text>();
        m_Message = "테스트테스트테스트테스트테스트테스트테스트테스트테스트테스트테스트테스트";
        StartCoroutine(Typing());
    }

    public IEnumerator Typing()
    {
        isTyping = true;
        for (int i = 0; i < m_Message.Length; i++)
        {
            m_TypingText.text = m_Message.Substring(0, i + 1);
            yield return new WaitForSeconds(m_speed);
        }
        isTyping = false;
    }
}
