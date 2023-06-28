using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSilde : MonoBehaviour
{
    private int m_ColCount = 0;
    // Start is called before the first frame update
    public bool State()
    {
        return m_ColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        m_ColCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_ColCount--;
    }
}
