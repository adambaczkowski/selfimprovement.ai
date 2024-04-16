namespace LS.ServiceClient;

public class Header
{
    public Header(string key, string value)
        : this(key, new[] { value })
    {
    }

    public Header(string key, IEnumerable<string> values)
    {
        Key = key;
        Values = values;
    }

    public string Key { get; set; }
    public IEnumerable<string> Values { get; set; }
}