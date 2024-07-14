using Microsoft.Data.Sqlite;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AmusedToDeath.Backend.Services;

public class DbService(IConfiguration configuration)
{

    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Data Source=db.sqlite";

    public async Task Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await connection.ExecuteAsync($@"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Officer INTEGER DEFAULT 0,
                BattleTag TEXT NOT NULL,
                PrimaryCharacterId INTEGER,
                AddedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                ChangedDate DATETIME DEFAULT CURRENT_TIMESTAMP
            );

            CREATE TABLE IF NOT EXISTS Characters (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Realm TEXT NOT NULL,
                Class TEXT NOT NULL,
                Level INTEGER NOT NULL,
                ItemLevel INTEGER NOT NULL,
                AddedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                ChangedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                OwnerId INTEGER,
                FOREIGN KEY (OwnerId) REFERENCES Users(Id)
            );

            CREATE TABLE IF NOT EXISTS Raids (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Date DATE NOT NULL DEFAULT CURRENT_TIMESTAMP,
                Gold INTEGER NOT NULL DEFAULT 0,
                Paid INTEGER NOT NULL DEFAULT 0,
                Comment TEXT,
                AddedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                ChangedDate DATETIME DEFAULT CURRENT_TIMESTAMP
            );

            CREATE TABLE IF NOT EXISTS RaidAttendance (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                RaidId INTEGER,
                CharacterId INTEGER,
                BossesKilled INTEGER NOT NULL DEFAULT 0,
                Paid INTEGER NOT NULL DEFAULT 0,
                AddedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                ChangedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                FOREIGN KEY (RaidId) REFERENCES Raids(Id),
                FOREIGN KEY (CharacterId) REFERENCES Characters(Id)
            );

            CREATE TABLE IF NOT EXISTS Applications (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Class TEXT NOT NULL,
                Spec TEXT NOT NULL,
                Realm TEXT NOT NULL,
                InterfaceUrl TEXT NOT NULL,
                LogsUrl TEXT NOT NULL,
                Comment TEXT,
                Alts TEXT,
                CangeKey TEXT NOT NULL,
                AddedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                ChangedDate DATETIME DEFAULT CURRENT_TIMESTAMP
            );
            
        ");
    }

    public async Task<IEnumerable<T>> GetAll<T>() where T : class
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.GetAllAsync<T>();
    }

    public async Task<T> Get<T>(int id) where T : class
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.GetAsync<T>(id);
    }

    public async Task<int> Insert<T>(T entity) where T : class
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.InsertAsync(entity);
    }

    public async Task<bool> Update<T>(T entity) where T : class
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.UpdateAsync(entity);
    }

    public async Task<bool> Delete<T>(T entity) where T : class
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.DeleteAsync(entity);
    }

    public async Task<IEnumerable<T>> GetByQuery<T>(string query) where T : class
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.QueryAsync<T>(query);
    }

    public async Task<IEnumerable<T>> GetByQuery<T>(string query, object parameters) where T : class
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.QueryAsync<T>(query, parameters);
    }

    public async Task<T?> GetByQuerySingle<T>(string query, object parameters) where T : class
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
    }
}