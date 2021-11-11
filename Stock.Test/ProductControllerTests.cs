using NUnit.Framework;
using Stock.Api.DTOs;
using System;

namespace Stock.Test
{
    public class ProductControllerTests
    {
        [Test]
        public void When_ProductControllerCreated_Expect_PostMethodWithParameter()
        {
            var className = "Stock.Api.Controllers.ProductController, Stock.Api";
            var methodName = "Post";
            var productDTOParameter = "Stock.Api.DTOs.ProductDTO, Stock.Api";
            var parametersType = new Type[] { Type.GetType(productDTOParameter) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProductControllerCreated_Expect_GetMethodWhitParameter()
        {
            var className = "Stock.Api.Controllers.ProductController, Stock.Api";
            var methodName = "Get";
            var parametersType = new Type[] { typeof(string) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProductControllerCreated_Expect_GetMethodWhitoutParameter()
        {
            var className = "Stock.Api.Controllers.ProductController, Stock.Api";
            var methodName = "Get";

            Assert.True(HasMethod(className, methodName));
        }

        [Test]
        public void When_ProductControllerCreated_Expect_PutMethodWithTwoParameters()
        {
            var className = "Stock.Api.Controllers.ProductController, Stock.Api";
            var methodName = "Put";
            var productDTOParameter = "Stock.Api.DTOs.ProductDTO, Stock.Api";
            var parametersType = new Type[] { typeof(string), Type.GetType(productDTOParameter) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProductControllerCreated_Expect_DeleteMethodWhitParameter()
        {
            var className = "Stock.Api.Controllers.ProductController, Stock.Api";
            var methodName = "Delete";
            var parametersType = new Type[] { typeof(string) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        [Test]
        public void When_ProductControllerCreated_Expect_SearchMethodWithParameter()
        {
            var className = "Stock.Api.Controllers.ProductController, Stock.Api";
            var methodName = "Search";
            var productDTOParameter = "Stock.Api.DTOs.ProductSearchDTO, Stock.Api";
            var parametersType = new Type[] { Type.GetType(productDTOParameter) };

            Assert.True(HasMethod(className, methodName, parametersType));
        }

        private bool? HasMethod(string className, string methodName)
        {
            return Type.GetType(className).GetMethod(methodName, new Type[] { }) != null;
        }

        private bool? HasMethod(string className, string methodName, Type[] types)
        {
            return Type.GetType(className).GetMethod(methodName, types) != null;
        }
    }
}