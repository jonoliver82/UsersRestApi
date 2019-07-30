using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UsersRestApi.UnitTest.Helpers
{
    public static class AssertHelpers
    {
        public static T IsOkResult<T>(ActionResult<T> result)
             where T : class
        {
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okObject = result.Result as OkObjectResult;

            Assert.IsInstanceOfType(okObject.Value, typeof(T));
            return okObject.Value as T;
        }
    }
}
