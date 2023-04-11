namespace Mc2.CrudTest.Shared.Common
{
    public class ResponseModel<T>
    {        
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Output { get; set; }
      
    }
}
