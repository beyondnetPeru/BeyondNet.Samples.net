namespace BeyondNet.Demo.Polly.App.Interfaces
{
    public interface ITableRuleResponseConverter
    {
        T[] Convert<T>(string result);
    }
}
