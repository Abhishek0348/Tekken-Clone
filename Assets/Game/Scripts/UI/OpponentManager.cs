using UnityEngine;

public class OpponentManager : MonoBehaviour
{
    public GameObject[] opponents;

    private void Start()
    {
        if (opponents.Length == 0)
        {
            Debug.LogError("No opponents found!");
            return;
        }
        
            ActivateRandomOpponent();
        
    }

    void ActivateRandomOpponent()
    {
        int randomIndex = Random.Range(0, opponents.Length);
        
        for (int i = 0; i < opponents.Length; i++)
        {
            if (i == randomIndex)
            {
                opponents[i].SetActive(true);
            }
            else
            {
                opponents[i].SetActive(false);
            }
        }
    }
}
