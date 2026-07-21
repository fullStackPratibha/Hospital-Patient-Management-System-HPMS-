namespace HospitalManagementAPI.Exceptions
{
    public class DuplicatePhoneException : Exception
    {
        public DuplicatePhoneException(string message) 
        : base(message)
        {
        }
    }
}