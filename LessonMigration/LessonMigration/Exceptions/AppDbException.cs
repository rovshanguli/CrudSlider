using System;

namespace LessonMigration.Exceptions
{
    public class AppDbException : Exception
    {
        public AppDbException(string message) : base(message) 
        {
        }
        
    }
}
