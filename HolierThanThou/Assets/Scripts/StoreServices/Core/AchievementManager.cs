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


            //Crowns collected
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_my_shiny, _profileIncrement.crownsCollected);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_more_shiny, _profileIncrement.crownsCollected);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_pardon_shines, _profileIncrement.crownsCollected);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_shine_get, _profileIncrement.crownsStolen);


            //Crowns stolen
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_dethroned, _profileIncrement.crownsStolen);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_take_that, _profileIncrement.crownsStolen);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_phantom_thief, _profileIncrement.crownsStolen);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_royal_klepto, _profileIncrement.crownsStolen);


            //Last Ball Rolling
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_got_the_skills, _profileIncrement.LBRWins);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_survivor, _profileIncrement.LBRWins);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_tried_and_true, _profileIncrement.LBRWins);


            //Points scored
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_bump, _profileIncrement.bump);
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_do_da_bump, _profileIncrement.doDaBump);
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_mo_bump, _profileIncrement.moBump);
            TrackIncrementalAchievementUsingInternalID(InternalIDs.achievement_down_to_georgia, _profileIncrement.georgiaGoals);


            //Score with Crowns
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_all_hail, _profileIncrement.allHail);
            TrackStandardAchievementUsingInternalID(InternalIDs.achievement_make_way, _profileIncrement.makeWay);


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
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCustomization>().addCurrency(_reward);
                achievementBeingUpdated.AlreadyClaimed = true;
                Debug.Log("Money added:" + _reward);
                GameObject.FindObjectOfType<AchievementUIManager>().UpdateAllAchievements();
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
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCustomization>().ResetAll();

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
