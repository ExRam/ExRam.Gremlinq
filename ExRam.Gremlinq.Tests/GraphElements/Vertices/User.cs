using System;

namespace ExRam.Gremlinq.Tests
{
    public class User : Authority
    {
        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public DateTimeOffset RegistrationDate { get; set; }
    }
}