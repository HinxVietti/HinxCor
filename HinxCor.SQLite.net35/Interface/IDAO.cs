/// <summary>
/// database access operator
/// </summary>
public interface IDAO
{
    hTable<T> CreateTable<T>(string tableName, params hHeader[] headers);
    void Insert<T>(hTable<T> table, hRow<T> row);
    int Delete<T>(hTable<T> table, hFilter<T> row);
    int Drop<T>(hTable<T> table);
    hResult<T> Serach<T>(hTable<T> table, hRow<T> row);
    hResult<T> Update<T>(hTable<T> table, hRow<T> row);
}


