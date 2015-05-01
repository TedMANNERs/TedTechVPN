using System.Security;
using FluentAssertions;
using NUnit.Framework;
using TedTechVpn.Core;

namespace TedTechVpn.CoreTest
{
    [TestFixture]
    public class PasswortProviderTest
    {
        [SetUp]
        public void Init()
        {
            _provider = new PasswordProvider();
        }

        private IPasswordProvider _provider;

        [Test]
        public void Hash()
        {
            //arrange
            byte[] salt = { 76, 38, 64, 235, 154 };
            SecureString password = new SecureString();
            foreach (char c in "1234")
            {
                password.AppendChar(c);
            }

            byte[] expectedHash =
            {
                77, 181, 159, 218, 11, 68, 102, 46, 4, 191, 206, 197, 224, 118, 75, 51, 28, 140, 222,
                158, 3, 22, 240, 211, 181, 189, 96, 8, 213, 215, 141, 15, 109, 216, 68, 191, 155, 186, 175, 239, 186, 63,
                28, 226, 114, 237, 5, 109, 179, 198, 57, 163, 139, 10, 88, 188, 206, 28, 241, 39, 121, 8, 70, 152
            };

            //act
            byte[] actualHash = _provider.Hash(password, salt);

            //assert
            actualHash.Should().BeEquivalentTo(expectedHash);
        }

        [Test]
        public void SecureStringToByteArray()
        {
            //arrange
            byte[] expectedBytes = {49, 50, 51, 52};
            SecureString password = new SecureString();
            foreach (char c in "1234")
            {
                password.AppendChar(c);
            }

            //act
            byte[] actualBytes = _provider.SecureStringToByteArray(password);

            //assert
            actualBytes.Should().BeEquivalentTo(expectedBytes);
        }
    }
}