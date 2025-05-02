using SC.Services.RewardAPI.Message;

namespace SC.Services.RewardAPI.Service.IService
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardsMessage rewardsMessage);
    }
}
