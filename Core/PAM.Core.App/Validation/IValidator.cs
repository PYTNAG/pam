namespace PAM.Core.App.Validation
{
    public interface IValidator { }
    
    public interface IValidator<T> : IValidator
    {
        public T Validate(T obj);
    }
}