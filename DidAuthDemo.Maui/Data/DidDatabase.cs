using DidAuthDemo.Maui.Models;
using SQLite;

namespace DidAuthDemo.Maui.Data;

public class DidDatabase
{
    SQLiteAsyncConnection Database;

    public DidDatabase()
    {
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Did>();
    }

    public async Task<List<Did>> ListAsync()
    {
        await Init();
        return await Database.Table<Did>().ToListAsync();
    }

    public async Task<List<Did>> GetByKeyIdAsync(int keyId)
    {
        await Init();
        return await Database.Table<Did>().Where(x => x.KeyId == keyId).ToListAsync();
    }

    public async Task<Did> GetAsync(int id)
    {
        await Init();
        return await Database.Table<Did>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(Did item)
    {
        await Init();
        if (item.Id != 0)
            return await Database.UpdateAsync(item);
        else
            return await Database.InsertAsync(item);
    }

    public async Task<int> DeleteItemAsync(Did item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }
}
