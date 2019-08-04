using System;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using UserInformation.WebService.Models;


namespace UserService.NUnit.WebService.Client.Test
{
    [TestFixture]
    public class ClientTest
    {
        const string BaseAddress = "http://localhost:900/Import";

        [Test]
        public void PostNullObject_Response_BadRequest()
        {
            SyncProfileRequest nullRequest = null;
            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(BaseAddress, nullRequest);

                Assert.AreEqual(response.Result.StatusCode,
                    HttpStatusCode.BadRequest);
            }
        }

        [Test]

        public void PostObjectWithNullUserID_Response_NoContent()
        {
            var nullUserIdObjectRequest =
                new SyncProfileRequest
                {
                    CountryIsoCode = "RU",
                    AdvertisingOptIn = true
                };

            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(BaseAddress, nullUserIdObjectRequest);

                Assert.AreEqual(response.Result.StatusCode, HttpStatusCode.NoContent);
            }
        }


        [Test]
        public void PostObjectWithGuidEmptyUserID_Response_NoContent()
        {
            var emptyGuidUserIdObjectRequest =
                new SyncProfileRequest
                {
                    UserId = Guid.Empty,
                    CountryIsoCode = "RU",
                    AdvertisingOptIn = true
                };

            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(BaseAddress, emptyGuidUserIdObjectRequest);

                Assert.AreEqual(response.Result.StatusCode, HttpStatusCode.NoContent);
            }
        }


        [Test]
        [TestCase("RUS")]
        public void PostUserWithNotValidCountryISOCode_Response_BadRequest(string notValidCode)
        {
            var userWithNoValidCountryIsoCode = new SyncProfileRequest
            {
                UserId = Guid.NewGuid(),
                RequestId = Guid.NewGuid(),
                CountryIsoCode = notValidCode,
            };

            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(BaseAddress, userWithNoValidCountryIsoCode);

                Assert.AreEqual(response.Result.StatusCode, HttpStatusCode.BadRequest);
            }
        }


        [Test]
        [TestCase("enn-ENN")]
        public void PostUserWithNotValidLocaleISOCode_Response_BadRequest(string notValidLocalCode)
        {
            var userWithNoValidLocaleIsoCode = new SyncProfileRequest
            {
                UserId = Guid.NewGuid(),
                RequestId = Guid.NewGuid(),
                Locale = notValidLocalCode,
            };

            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(BaseAddress, userWithNoValidLocaleIsoCode);

                Assert.AreEqual(response.Result.StatusCode, HttpStatusCode.BadRequest);
            }
        }
    }
}
