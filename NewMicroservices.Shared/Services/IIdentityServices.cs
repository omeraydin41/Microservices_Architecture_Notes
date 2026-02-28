namespace NewMicroservices.Shared.Services
{
    public interface IIdentityServices
    {
        public Guid GetUserId { get;}//get var sadece eerişileblir ve değiştirilemez 
        public string GetUserName { get;}

    }
}