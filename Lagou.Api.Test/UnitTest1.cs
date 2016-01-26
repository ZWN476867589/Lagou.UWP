using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lagou.API.Methods;
using Lagou.API;

namespace Lagou.Api.Test {
    [TestClass]
    public class UnitTest1 {


        [TestMethod]
        public void TestMethod1() {

            var method = new Search() {
                City = "深圳",
                Key = "c#"
            };

            var a = ApiClient.Execute(method).Result;
        }

        [TestMethod]
        public void TestEvaluationList() {
            var method = new EvaluationList() {
                PositionID = 1178538
            };
            var a = ApiClient.Execute(method).Result;
        }

        [TestMethod]
        public void PositionListTest() {
            var method = new PositionList() {
                CompanyID = 3786,
                PositionType = API.Entities.PositionTypes.技术
            };

            var a = ApiClient.Execute(method).Result;
        }

        [TestMethod]
        public void MD5Test() {
            var str = MD5.GetHashString("aaa");
            Assert.AreEqual(str, "47BCE5C74F589F4867DBD57E9CA9F808");
        }

        [TestMethod]
        public void LoginTest() {
            var mth = new Login() {
                UserName = "gruan@asnum.com",
                Password = "aaabbbcc"
            };
            var a = ApiClient.Execute(mth).Result;
        }

        [TestMethod]
        public void CaptchaTest() {
            var mth = new GetCaptcha();
            var stm = ApiClient.Execute(mth).Result;
        }

        [TestMethod]
        public void CompanyTest() {
            var mth = new CompanyDetail() {
                CompanyID = 6368
            };

            var c = ApiClient.Execute(mth).Result;
        }
    }
}
