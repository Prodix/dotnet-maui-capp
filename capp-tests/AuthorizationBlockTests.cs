using CAPP;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace capp_tests
{
    public class AuthorizationBlockTests
    {
        [Fact]
        public async Task AccessToServerTest()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(Constants.serverLink);

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode, "Ќе удалось подключитьс€ к серверу");
        }

        [Fact]
        public void UsernameValidTestOne()
        {
            string username = "admin";

            bool result = (username.Length < 5) ? false : true;

            Assert.True(result);
        }

        [Fact]
        public void EmailValidTestOne()
        {
            string email = "prodixfg@gmail.com";
            bool result = true;

            if (email.EndsWith('.'))
            {
                result = false;
            }

            MailAddress address = new MailAddress(email);

            Assert.True(result);
            Assert.Equal(email, address.Address);
        }

        [Fact]
        public void EmailValidTestTwo()
        {
            string email = "fsdf";
            MailAddress address;
            bool result = true;

            if (email.EndsWith('.'))
            {
                result = false;
            }

            Assert.ThrowsAny<Exception>(() =>
            {
                address = new MailAddress(email);
            });
            Assert.True(result);
        }

        [Fact]
        public void PasswordValidTestOne()
        {
            string password = "admin";
            bool result = false;

            if (password.Length < 8)
            {
                result = false;
            }
            else
            {
                if (!Regex.Match(password, @"[0-9]").Success)
                {
                    result = false;
                }
                else if (!Regex.Match(password, @"[%$&^#@*!.,/;:]").Success)
                {
                    result = false;
                }
                else if (!Regex.Match(password, @"[A-Z]").Success)
                {
                    result = false;
                }
                else if (Regex.Match(password, @"[а-€ј-я]").Success)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }

            Assert.False(result);
        }

        [Fact]
        public void PasswordValidTestTwo()
        {
            string password = "asdF123$";
            bool result = false;

            if (password.Length < 8)
            {
                result = false;
            }
            else
            {
                if (!Regex.Match(password, @"[0-9]").Success)
                {
                    result = false;
                }
                else if (!Regex.Match(password, @"[%$&^#@*!.,/;:]").Success)
                {
                    result = false;
                }
                else if (!Regex.Match(password, @"[A-Z]").Success)
                {
                    result = false;
                }
                else if (Regex.Match(password, @"[а-€ј-я]").Success)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }

            Assert.True(result);
        }

        [Fact]
        public void PasswordValidTestThree()
        {
            string password = "asdF123$к";
            bool result = false;

            if (password.Length < 8)
            {
                result = false;
            }
            else
            {
                if (!Regex.Match(password, @"[0-9]").Success)
                {
                    result = false;
                }
                else if (!Regex.Match(password, @"[%$&^#@*!.,/;:]").Success)
                {
                    result = false;
                }
                else if (!Regex.Match(password, @"[A-Z]").Success)
                {
                    result = false;
                }
                else if (Regex.Match(password, @"[а-€ј-я]").Success)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }

            Assert.False(result);
        }

        [Fact]
        public void BMITestOne()
        {
            float weight = 65f;
            float height = 190f;
            bool result = false;

            if (weight < 18.5 * (height / 100.0 * (height / 100.0)))
            {
                result = true;
            }

            Assert.True(result);
        }

        [Fact]
        public void BMITestTwo()
        {
            float weight = 65f;
            float height = 160f;
            bool result = false;

            if (weight > 18.5 * (height / 100.0 * (height / 100.0)) && weight < 30 * (height / 100.0 * (height / 100.0)))
            {
                result = true;
            }

            Assert.True(result);
        }

        [Fact]
        public void BMITestThree()
        {
            float weight = 65f;
            float height = 140f;
            bool result = false;

            if (weight > 30 * (height / 100.0 * (height / 100.0)))
            {
                result = true;
            }

            Assert.True(result);
        }
    }
}