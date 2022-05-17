using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public Text m_TypingText;
    public string m_Message { get; set; }
    public float m_speed = 0.07f;
    public bool isTyping { get; set; } = false;

    public IEnumerator Typing()
    {
        if (m_TypingText == null)
            m_TypingText = gameObject.GetComponent<Text>();

        isTyping = true;
        for (int i = 0; i < m_Message.Length; i++)
        {
            m_TypingText.text = m_Message.Substring(0, i + 1);
            yield return new WaitForSeconds(m_speed);
        }
        isTyping = false;
    }
}
