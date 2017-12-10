using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using RichmondGroupTechnicalTask.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace RGTT.Tests
{
    [TestClass]
    public class FateControllerTests
    {
        [TestMethod]
        public void _EnsureThatApiIsUpAndReturnsProperHttpStatusCode()
        {
            var fateController = new FateController();
            
            IHttpActionResult actionResult = fateController.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<String>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<string>>));
        }
    }
}
