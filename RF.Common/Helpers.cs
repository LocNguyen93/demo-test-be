namespace RF.Common
{
    public static class Helpers
    {
        public static (bool success, int fromMonth, int fromYear, int toMonth, int toYear) ParseFromTo(string from, string to)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                return (false, 0, 0, 0, 0);

            var f = from.Split('-');
            var t = to.Split('-');
            if (f.Length != 2 || t.Length != 2)
                return (false, 0, 0, 0, 0);

            try
            {
                return (true, int.Parse(f[0]), int.Parse(f[1]), int.Parse(t[0]), int.Parse(t[1]));
            }
            catch (System.Exception)
            {
                return (false, 0, 0, 0, 0);
            }
        }

        public static bool ValidateMonthRange(int fromMonth, int fromYear, int toMonth, int toYear, int min = 0, int max = 11)
        {
            var monthDifference = (toYear - fromYear) * 12 + (toMonth - fromMonth);
            return monthDifference >= min && monthDifference <= max;
        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
