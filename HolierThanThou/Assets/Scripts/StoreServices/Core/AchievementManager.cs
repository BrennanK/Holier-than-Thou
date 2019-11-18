using StoreServices.Core.Achievements;
using System.Linq;
using UnityEngine;
using System;

namespace StoreServices
{
    public class AchievementManager : MonoBehaviour
    {
        public static AchievementManager instance;
        private const string ACHIEVEMENTS_SAVE_STRING = "SAVED_ACHIEVEMENTS";
        public Achievement[] allAchievements;

        private PlayerCustomization pCustomizer;

        [SerializeField] private AchievementInstance[] achievementInstances;
        public AchievementInstance[] AchievementInstances
        {
            get
            {
                return achievementInstances;
            }
        }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // Create all achievement instances and/or check for persistence
            achievementInstances = new AchievementInstance[allAchievements.Length];

            if(PlayerPrefs.HasKey(ACHIEVEMENTS_SAVE_STRING))
            {
                LoadAchievements();

            }
            else
            {
                for(int i = 0; i < allAchievements.Length; i++)
                {
                    achievementInstances[i] = new AchievementInstance(allAchievements[i]);
                }
            }

            pCustomizer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCustomization>();
        }

        private void LoadAchievements()
        {
            for(int i = 0; i < allAchievements.Length; i++)
            {
                achievementInstances[i] = JsonUtility.FromJson<AchievementInstance>(PlayerPrefs.GetString($"{ACHIEVEMENTS_SAVE_STRING}_{allAchievements[i].internalAchievementID}"));
                achievementInstances[i].SetAchievementReference(allAchievements[i]);

            }
        }

        public void UpdateAllAchievements(PlayerProfile _profileIncrement)
        {
            //Games Completed Achievements
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_first_timer, _profileIncrement.gamesPlayed);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_10_Down, _profileIncrement.gamesPlayed);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_four_score, _profileIncrement.gamesPlayed);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_half_a_century, _profileIncrement.gamesPlayed);


            //Games won 
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_holy, _profileIncrement.gamesWon);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_going_pro, _profileIncrement.gamesWon);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_pretty_good, _profileIncrement.gamesWon);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_try_hard, _profileIncrement.gamesWon);


            //Other
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_cant_touch_me, _profileIncrement.hitSomebody);
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_alley_oop, _profileIncrement.AlleyOop);
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_denied, _profileIncrement.denied);
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_not_very_holy, _profileIncrement.playerLast);
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_score, _profileIncrement.scoredGoal);
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_middle_of_pack, _profileIncrement.placedFourth);
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_no_hands, _profileIncrement.usedPowerUp);
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_grounded, _profileIncrement.hasJumped);


            PersistAchievements();
        }

        private void TrackIncrementalAchievementUsingInternalID(string _achievementID, float _incrementValue)
        {
            AchievementInstance achievementBeingUpdated = AchievementInstances.Where((achievement) =>
            {
                return achievement.AchievementInternalID == _achievementID;
            }).FirstOrDefault();

            if(achievementBeingUpdated.AlreadyCompleted)
            {
                return;
            }

            achievementBeingUpdated.CurrentProgress += _incrementValue;

            if(achievementBeingUpdated.Complete)
            {
                achievementBeingUpdated.AlreadyCompleted = true;
            }
        }

        private void TrackStandardAchievementUsingInternalID(string _achievementID, bool _isComplete)
        {
            AchievementInstance achievementBeingUpdated = achievementInstances.Where((achievement) =>
            {
                return achievement.AchievementInternalID == _achievementID;
            }).FirstOrDefault();

            if(achievementBeingUpdated.AlreadyCompleted || !_isComplete)
            {
                return;
            }

            achievementBeingUpdated.CurrentProgress += 1;

            if(achievementBeingUpdated.Complete)
            {
                achievementBeingUpdated.AlreadyCompleted = true;
            }
        }

        public void ClaimAchievement(string _achievementID, int _reward)
        {
            AchievementInstance achievementBeingUpdated = achievementInstances.Where((achievement) =>
            {
                return achievement.AchievementInternalID == _achievementID;
            }).FirstOrDefault();

            if(achievementBeingUpdated.AlreadyClaimed)
            {
                Debug.Log("Achievement already claimed");
                return;
            }

            if (achievementBeingUpdated.Claimable)
            {
                pCustomizer.addCurrency(_reward);
                achievementBeingUpdated.AlreadyClaimed = true;
                Debug.Log("Money added:" + _reward);
                PersistAchievements();
            }
            else
            {
                Debug.Log("Achievement not completed yet");
            }



        }


        public void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        private void PersistAchievements()
        {
            PlayerPrefs.SetInt(ACHIEVEMENTS_SAVE_STRING, 1);
            for(int i = 0; i < achievementInstances.Length; i++)
            {
                PlayerPrefs.SetString($"{ACHIEVEMENTS_SAVE_STRING}_{achievementInstances[i].AchievementInternalID}", JsonUtility.ToJson(achievementInstances[i]));
            }
        }

    }
}
