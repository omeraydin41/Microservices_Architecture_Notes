using MongoDB.Bson.Serialization.Attributes;

namespace NewMicroservice.Discount.Api.Repositories;

public class BaseEntity
{
    public Guid Id { get; set; }
}