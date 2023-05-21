using CAPP;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace capp_tests
{
    public class AuthorizationBlockTests
    {

        [Fact]
        public void BMITestOne()
        {
            float weight = 65f;
            float height = 190f;

            bool result = weight < 18.5 * (height / 100.0 * (height / 100.0)) ? true : false;

            Assert.True(result);
        }

        [Fact]
        public void BMITestTwo()
        {
            float weight = 65f;
            float height = 160f;

            bool result = weight > 18.5 * (height / 100.0 * (height / 100.0)) ? true : false;

            Assert.True(result);
        }

        [Fact]
        public void BMITestThree()
        {
            float weight = 65f;
            float height = 140f;

            bool result = weight > 30 * (height / 100.0 * (height / 100.0)) ? true : false;

            Assert.True(result);
        }
    }
}