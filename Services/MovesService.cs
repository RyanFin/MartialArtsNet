using MartialArtsNet.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MartialArtsNet.Services;

public class MovesService{
    private readonly IMongoCollection<MoveSetDTO> _movesCollection;

    public MovesService(IOptions<MartialArtsDatabaseSettings> martialArtsDatabaseSettings){
        var mongoClient = new MongoClient(martialArtsDatabaseSettings.Value.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(martialArtsDatabaseSettings.Value.DatabaseName);
        _movesCollection = mongoDatabase.GetCollection<MoveSetDTO>(martialArtsDatabaseSettings.Value.CollectionName);
    }

    // public async Task<List<MoveSetDTO>> GetAsync() {await _movesCollection.Find(_ => true).ToListAsync();}

    public async Task<List<MoveSetDTO>> GetAsync(){
        return await _movesCollection.Find(_ => true).ToListAsync();
    }

    public async Task<MoveSetDTO> GetAsync(string id) =>
        await _movesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(MoveSetDTO newMove){
        await _movesCollection.InsertOneAsync(newMove);
        return;
    }

    public async Task UpdateAsync(string id, MoveSetDTO updatedMove) =>
        await _movesCollection.ReplaceOneAsync(x => x.Id == id, updatedMove);

    public async Task RemoveAsync(string id) =>
        await _movesCollection.DeleteOneAsync(x => x.Id == id);


    public async Task AddToMovesAsync(string id, string moveId){
        FilterDefinition<MoveSetDTO> filter = Builders<MoveSetDTO>.Filter.Eq("Id", id);
        UpdateDefinition<MoveSetDTO> update = Builders<MoveSetDTO>.Update.AddToSet<string>("moveId", moveId);
        await _movesCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id){
        FilterDefinition<MoveSetDTO> filter = Builders<MoveSetDTO>.Filter.Eq("Id", id);
        await _movesCollection.DeleteOneAsync(filter);
        return;
    }

}