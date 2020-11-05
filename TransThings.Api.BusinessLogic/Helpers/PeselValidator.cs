using System;
using System.Linq;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class PeselValidator
    {
        private const char male = 'M';
        private const char female = 'K';
        public static bool Validate(string peselNumber, char gender, DateTime birthdayDate)
        {
            // RRMMDDPPPPK - construction of pesel number
            // RR - last two digits of year birth
            // MM - month of year (depends on century)
            // DD - day of birth
            // PPPP - last digit is gender, even for female, odd for male
            // K - check digit

            if (peselNumber.Length != 11)
                return false;

            var lastTwoDigitsOfYearOfBirth = peselNumber.Substring(0, 2);
            var monthOfBirth = Int32.Parse(peselNumber.Substring(2, 2));
            var dayOfBirth = peselNumber.Substring(4, 2);

            // RR
            if (lastTwoDigitsOfYearOfBirth != birthdayDate.Year.ToString().Substring(2, 2))
                return false;

            // MM
            if (birthdayDate.Year >= 1800 && birthdayDate.Year <= 1899)
            {
                if (monthOfBirth != birthdayDate.Month + 80)
                    return false;
            }

            else if (birthdayDate.Year >= 1900 && birthdayDate.Year <= 1999)
            {
                if (monthOfBirth != birthdayDate.Month)
                    return false;
            }

            else if (birthdayDate.Year >= 2000 && birthdayDate.Year <= 2099)
            {
                if (monthOfBirth != birthdayDate.Month + 20)
                    return false;
            }

            else if (birthdayDate.Year >= 2100 && birthdayDate.Year <= 2199)
            {
                if (monthOfBirth != birthdayDate.Month + 40)
                    return false;
            }

            // DD
            if (Int32.Parse(dayOfBirth) != birthdayDate.Day)
                return false;

            // PPPP
            if (Int32.Parse(peselNumber[9].ToString()) % 2 == 0 && gender != female)
                return false;

            if (Int32.Parse(peselNumber[9].ToString()) % 2 == 1 && gender != male)
                return false;

            int tmp = Int32.Parse(peselNumber[0].ToString());

            // K
            int checkSum = (9*Int32.Parse(peselNumber[0].ToString()) + 7*Int32.Parse(peselNumber[1].ToString()) + 3*Int32.Parse(peselNumber[2].ToString())
                + Int32.Parse(peselNumber[3].ToString()) + 9*Int32.Parse(peselNumber[4].ToString()) + 7*Int32.Parse(peselNumber[5].ToString())
                + 3*Int32.Parse(peselNumber[6].ToString()) + Int32.Parse(peselNumber[7].ToString()) + 9*Int32.Parse(peselNumber[8].ToString())
                + 7*Int32.Parse(peselNumber[9].ToString()))%10;
            if (checkSum > 9)
                checkSum %= 10;

            if (checkSum != Int32.Parse(peselNumber[10].ToString()))
                return false;

            return true;
        }
    }
}
