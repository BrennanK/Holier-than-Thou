using UnityEngine;

namespace StoreServices.Core.Achievements
{
    [System.Serializable]
    public class AchievementInstance
    {
        [System.NonSerialized]
        private Achievement achievementReference;

        [SerializeField]
        private string achievementInternalID;
        public string AchievementInternalID
        {
            get
            {
                return achievementInternalID;
            }
        }

        public string AchievementName
        {
            get
            {
                return achievementReference.name;
            }
        }

        public string AchievementDescription
        {
            get
            {
                return achievementReference.achievementDescription;
            }
        }

        public float GoalValue
        {
            get
            {
                return achievementReference.goalValue;
            }
        }

        [SerializeField]
        private float currentProgress;

        [SerializeField]
        private bool isCurrentlyHidden;

        [SerializeField]
        private long lastModification = 0;

        [SerializeField]
        private bool isCompletedAlready = false;
        public bool AlreadyCompleted
        {
            get
            {
                return isCompletedAlready;
            }
            set
            {
                isCompletedAlready = value;
            }
        }

        public bool Complete
        {
            get
            {
                return currentProgress >= achievementReference.goalValue;
            }
        }

        public float ProgressInPercentage
        {
            get
            {
                return Mathf.Clamp((currentProgress / achievementReference.goalValue), 0, 1);
            }
        }

        public float CurrentProgress
        {
            get
            {
                return currentProgress;
            }
            set
            {
                lastModification = System.DateTime.Now.Ticks;
                isCurrentlyHidden = false;
                currentProgress = value;
            }
        }

        public AchievementInstance(Achievement _achievementReference)
        {
            achievementReference = _achievementReference;
            achievementInternalID = _achievementReference.internalAchievementID;
            currentProgress = 0;
            isCurrentlyHidden = _achievementReference.isAchievementHidden;
            lastModification = 0;
            isCompletedAlready = false;
        }

        public AchievementInstance(Achievement _achievementReference, float _currentProgress, bool _isCurrentlyHidden, long _lastModification, bool _isCompletedAlready)
        {
            achievementReference = _achievementReference;
            achievementInternalID = _achievementReference.internalAchievementID;
            currentProgress = _currentProgress;
            isCurrentlyHidden = _isCurrentlyHidden;
            lastModification = _lastModification;
            isCompletedAlready = _isCompletedAlready;
        }

        public void SetAchievementReference(Achievement _achievementReference)
        {
            achievementReference = _achievementReference;
        }

        public override string ToString()
        {
            return $"Achievement {achievementReference.name} - Progress: {currentProgress}/{achievementReference.goalValue} - Last Modification: {lastModification}";
        }
    }
}
