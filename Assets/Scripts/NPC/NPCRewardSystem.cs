using UnityEngine;

public class NPCRewardSystem : MonoBehaviour
{
    [SerializeField] private GameObject rewardOne;
    [SerializeField] private GameObject rewardTwo;
    [SerializeField] private Transform rewardOnePopUpLocation;
    [SerializeField] private Transform rewardTwoPopUpLocation;

    public int correctAnswers = 0;

    public void GiveReward()
    {
        GameObject reward = Instantiate(
            rewardOne,
            rewardOnePopUpLocation.position,
            Quaternion.identity);

        if (correctAnswers > 1)
        {
            Instantiate(
                rewardTwo,
                rewardTwoPopUpLocation.position,
                Quaternion.identity);
        }
    }
}
