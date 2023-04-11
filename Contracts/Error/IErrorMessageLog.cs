namespace Mc2.CrudTest.Contracts.Error
{
    public interface IErrorMessageLog
    {
        bool LogError(string layerName, string className, string methodName, string msg);
    }
}
