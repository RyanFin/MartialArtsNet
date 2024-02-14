using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MartialArtsNet.Models;

public class MoveSet{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id {get; set;}
    public string? Name {get; set;}
    public string? BeltRequired {get; set;}
    // example of an attribute that we don't want to transfer to the response
    public string? Secret{get; set;}
}

// Had to remove the old MoveSetController file before updating Controllor
public class MoveSetDTO{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id {get; set;}
    public string? Name {get; set;}
    public string? BeltRequired {get; set;}
}