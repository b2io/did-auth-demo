using SQLite;
using DidAuthDemo.Maui.Models;

namespace DidAuthDemo.Maui.Data;

public class KeyDatabase
{
    SQLiteAsyncConnection Database;

    public KeyDatabase()
    {
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Key>();
    }
    public async Task<List<Key>> ListAsync()
    {
        await Init();
        return await Database.Table<Key>().ToListAsync();
    }

    public async Task<Key> GetAsync(int id)
    {
        await Init();
        return await Database.Table<Key>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(Key item)
    {
        await Init();
        if (item.Id != 0)
            return await Database.UpdateAsync(item);
        else
            return await Database.InsertAsync(item);
    }

    public async Task<int> DeleteItemAsync(Key item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }
}
