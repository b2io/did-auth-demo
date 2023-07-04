using DidAuthDemo.Maui.Models;
using SQLite;

namespace DidAuthDemo.Maui.Data;

public class CredentialDatabase
{
    SQLiteAsyncConnection Database;

    public CredentialDatabase()
    {
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Credential>();
    }
    public async Task<List<Credential>> ListAsync()
    {
        await Init();
        return await Database.Table<Credential>().ToListAsync();
    }

    public async Task<Credential> GetAsync(int id)
    {
        await Init();
        return await Database.Table<Credential>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(Credential item)
    {
        await Init();
        if (item.Id != 0)
            return await Database.UpdateAsync(item);
        else
            return await Database.InsertAsync(item);
    }

    public async Task<int> DeleteItemAsync(Credential item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }
}
