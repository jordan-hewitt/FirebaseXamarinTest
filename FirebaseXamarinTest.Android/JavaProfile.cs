using System;
namespace FirebaseXamarinTest.Droid
{
    public class JavaProfile : Java.Lang.Object
    {
        private string firstName;
        private string lastName;
        private int birthYear;

        public JavaProfile() { }

        public JavaProfile(string firstName, string lastName, int birthYear)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthYear = birthYear;
        }

        public string getFirstName()
        {
            return firstName;
        }

        public void setFirstName(string firstName)
        {
            this.firstName = firstName;
        }

        public string getLastName()
        {
            return lastName;
        }

        public void setLastName(string lastName)
        {
            this.lastName = lastName;
        }

        public int getBirthYear()
        {
            return birthYear;
        }

        public void setBirthYear(int birthYear)
        {
            this.birthYear = birthYear;
        }
    }
}
