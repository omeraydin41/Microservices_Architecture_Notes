using MongoDB.Bson.Serialization.Attributes;

namespace UdemyNewMicroservice.Discount.Api.Repositories;

public class BaseEntity
{
    public Guid Id { get; set; }
}