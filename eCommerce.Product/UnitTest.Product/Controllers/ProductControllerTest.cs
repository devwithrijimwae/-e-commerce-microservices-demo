using eCommerce.SharedLibary.Response;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.Dtos;
using ProductApi.Application.Interfaces;
using ProductApi.Presentation.Controllers;
using ProductApi.Domain.Entities;

namespace UnitTest.ProductEntity.Controllers
{
    public class ProductControllerTest
    {
        private readonly IProduct productInterface;
        private readonly ProductController productController;

        public ProductControllerTest()
        {
            //set up the dependencies 
            productInterface = A.Fake<IProduct>();

            //set up system under test - SUT
            productController = new ProductController(productInterface);
        }

        //Get All Products
        [Fact]
        public async Task GetProduct_WhenProductExits_RetuenResponseWithProducts()
        {
            //Arange
            var products = new List<Product>()
            {
                new(){Id = 1, Name = "Product 1", Quantity = 10, Price = 100.70m},
                new(){Id = 2, Name = "Product 2", Quantity = 110, Price = 1004.70m},
             };

            //set up fake response for GetAllAsync Method
            A.CallTo(() => productInterface.GetAllAsync()).Returns(products);

            //Act
            var result = await productController.GetProducts();

            //Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);

            var returnedProducts = okResult.Value as IEnumerable<ProductDTO>;
            returnedProducts.Should().NotBeNull();
            returnedProducts.Should().HaveCount(2);
            returnedProducts!.First().Name.Should().Be("1");
            returnedProducts!.Last().Name.Should().Be("2");


        }

        [Fact]
        public async Task GetProduct_WhenNoProductExits_ReturnNotFound()
        {
            //Arrange
            var products = new List<Product>();
            //set up fake response for GetAllAsync Method
            A.CallTo(() => productInterface.GetAllAsync()).Returns(products);
            //Act
            var result = await productController.GetProducts();
            //Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);

            var message = notFoundResult.Value as string;
            message.Should().Be("No product detected in the database");

        }
        [Fact]
        public async Task GetProduct_WhenModelStateIsInvalid_ReturnBadRequest()
        {
            //Arrange
            var productDTO = new ProductDTO(1, "Product 1", 34, 67.95m);
            productController.ModelState.AddModelError("Name", "Required");

            //Act
            var result = await productController.CreateProduct(productDTO);

            //Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        }
        [Fact]

        public async Task CreateProduct_WhenCreateSuccessful_ReturnOkResponse()
        {

            //Arange 
            var productDTO = new ProductDTO(1, "Product 1", 34, 67.95m);
            var response = new Response(true, "created");

            //Act 
            A.CallTo(() => productInterface.CreateAsync(A<Product>.Ignored)).Returns(response);
            var result = await productController.CreateProduct(productDTO);

            //Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);

            var returnedResult = okResult.Value as Response;
            returnedResult!.Message.Should().Be("Created");
            returnedResult!.Flag.Should().BeTrue();
        }

        [Fact]
        public async Task CreateProduct_WhenCreateFails_ReturnBadRequestResponse()
        {
            //Arrange 
            var productDTO = new ProductDTO(1, "Product 1", 34, 67.95m);
            var response = new Response(false, "Failed");

            //Act
            A.CallTo(() => productInterface.CreateAsync(A<Product>.Ignored)).Returns(response);
            var result = await productController.CreateProduct(productDTO);


            //Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

            var responseResult = badRequestResult.Value as Response;
            badRequestResult.Should().NotBeNull();
            responseResult!.Message.Should().Be("Failed");
            responseResult!.Flag.Should().BeFalse();


        }
        [Fact]
        public async Task UpdateProduct_WhenUpdateSuccessful_ReturnOkResponse()
        {
            //Arrange
            var productDTO = new ProductDTO(1, "Product 1", 34, 67.95m);
            var response = new Response(false, "Failed");
            //Act
            A.CallTo(() => productInterface.CreateAsync(A<Product>.Ignored)).Returns(response);
            var result = await productController.UpdateProduct(productDTO);
            //Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
            var responseResult = okResult.Value as Response;
            responseResult!.Message.Should().Be("Update");
            responseResult!.Flag.Should().BeTrue();
        }
    }
}




