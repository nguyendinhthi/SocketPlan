
namespace Edsa.AutoCadProxy
{
    public class Result<T>
    {
        public Result(bool success, T value)
        {
            this.Success = success;
            this.Value = value;
        }

        public T Value { get; set; }

        public bool Success { get; set; }
    }
}
