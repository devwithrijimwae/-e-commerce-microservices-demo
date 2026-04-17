using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using ProductApi.Application.Dtos;
using ProductApi.Application.Dtos.Conversions;
using ProductApi.Application.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text;

namespace ProductApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProduct productInterface) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            //get all product from repo
            var products = await productInterface.GetAllAsync();
            if (!products.Any())

                return NotFound("No product detected in the database");

            //convert data from entity to dto and return
            var (_, list) = ProductConversion.FromEntity(null!, products);
            return list!.Any() ? Ok(list) : NotFound("No product detected in the database");

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            //get single product from repo
            var product = await productInterface.FindByIdAsync(id);
            if (product is null)
                return NotFound("No product detected in the database");

            //convert from entity to dto and return
            var (_product, _) = ProductConversion.FromEntity(product, null!);
            return _product is not null ? Ok(_product) : NotFound("Product not Found");
        }

        [HttpPost]
        public async Task<ActionResult<Response>> CreateProduct(ProductDTO product)
        {
            //check model state if all data annotations are passed
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            //convert to entity
            var getEntity = ProductConversion.ToEntity(product);
            var response = await productInterface.CreateAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateProduct(ProductDTO product)
        {
            //check model state if all data annotations are passed
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            //convert to entity
            var getEntity = ProductConversion.ToEntity(product);
            var response = await productInterface.UpdateAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteProduct(ProductDTO product)
        {
            //Convert to entity
            var getEntity = ProductConversion.ToEntity(product);
            var response = await productInterface.DeleteAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        public override StatusCodeResult StatusCode([ActionResultStatusCode] int statusCode)
        {
            return base.StatusCode(statusCode);
        }

        public override ObjectResult StatusCode([ActionResultStatusCode] int statusCode, [ActionResultObjectValue] object? value)
        {
            return base.StatusCode(statusCode, value);
        }

        public override ContentResult Content(string content)
        {
            return base.Content(content);
        }

        public override ContentResult Content(string content, string contentType)
        {
            return base.Content(content, contentType);
        }

        public override ContentResult Content(string content, string contentType, Encoding contentEncoding)
        {
            return base.Content(content, contentType, contentEncoding);
        }

        public override ContentResult Content(string content, MediaTypeHeaderValue? contentType)
        {
            return base.Content(content, contentType);
        }

        public override NoContentResult NoContent()
        {
            return base.NoContent();
        }

        public override OkResult Ok()
        {
            return base.Ok();
        }

        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            return base.Ok(value);
        }

        public override RedirectResult Redirect([StringSyntax("Uri")] string url)
        {
            return base.Redirect(url);
        }

        public override RedirectResult RedirectPermanent([StringSyntax("Uri")] string url)
        {
            return base.RedirectPermanent(url);
        }

        public override RedirectResult RedirectPreserveMethod([StringSyntax("Uri")] string url)
        {
            return base.RedirectPreserveMethod(url);
        }

        public override RedirectResult RedirectPermanentPreserveMethod([StringSyntax("Uri")] string url)
        {
            return base.RedirectPermanentPreserveMethod(url);
        }

        public override LocalRedirectResult LocalRedirect([StringSyntax("Uri", new[] { UriKind.Relative })] string localUrl)
        {
            return base.LocalRedirect(localUrl);
        }

        public override LocalRedirectResult LocalRedirectPermanent([StringSyntax("Uri", new[] { UriKind.Relative })] string localUrl)
        {
            return base.LocalRedirectPermanent(localUrl);
        }

        public override LocalRedirectResult LocalRedirectPreserveMethod([StringSyntax("Uri", new[] { UriKind.Relative })] string localUrl)
        {
            return base.LocalRedirectPreserveMethod(localUrl);
        }

        public override LocalRedirectResult LocalRedirectPermanentPreserveMethod([StringSyntax("Uri", new[] { UriKind.Relative })] string localUrl)
        {
            return base.LocalRedirectPermanentPreserveMethod(localUrl);
        }

        public override RedirectToActionResult RedirectToAction()
        {
            return base.RedirectToAction();
        }

        public override RedirectToActionResult RedirectToAction(string? actionName)
        {
            return base.RedirectToAction(actionName);
        }

        public override RedirectToActionResult RedirectToAction(string? actionName, object? routeValues)
        {
            return base.RedirectToAction(actionName, routeValues);
        }

        public override RedirectToActionResult RedirectToAction(string? actionName, string? controllerName)
        {
            return base.RedirectToAction(actionName, controllerName);
        }

        public override RedirectToActionResult RedirectToAction(string? actionName, string? controllerName, object? routeValues)
        {
            return base.RedirectToAction(actionName, controllerName, routeValues);
        }

        public override RedirectToActionResult RedirectToAction(string? actionName, string? controllerName, string? fragment)
        {
            return base.RedirectToAction(actionName, controllerName, fragment);
        }

        public override RedirectToActionResult RedirectToAction(string? actionName, string? controllerName, object? routeValues, string? fragment)
        {
            return base.RedirectToAction(actionName, controllerName, routeValues, fragment);
        }

        public override RedirectToActionResult RedirectToActionPreserveMethod(string? actionName = null, string? controllerName = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToActionPreserveMethod(actionName, controllerName, routeValues, fragment);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName)
        {
            return base.RedirectToActionPermanent(actionName);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName, object? routeValues)
        {
            return base.RedirectToActionPermanent(actionName, routeValues);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName, string? controllerName)
        {
            return base.RedirectToActionPermanent(actionName, controllerName);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName, string? controllerName, string? fragment)
        {
            return base.RedirectToActionPermanent(actionName, controllerName, fragment);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName, string? controllerName, object? routeValues)
        {
            return base.RedirectToActionPermanent(actionName, controllerName, routeValues);
        }

        public override RedirectToActionResult RedirectToActionPermanent(string? actionName, string? controllerName, object? routeValues, string? fragment)
        {
            return base.RedirectToActionPermanent(actionName, controllerName, routeValues, fragment);
        }

        public override RedirectToActionResult RedirectToActionPermanentPreserveMethod(string? actionName = null, string? controllerName = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToActionPermanentPreserveMethod(actionName, controllerName, routeValues, fragment);
        }

        public override RedirectToRouteResult RedirectToRoute(string? routeName)
        {
            return base.RedirectToRoute(routeName);
        }

        public override RedirectToRouteResult RedirectToRoute(object? routeValues)
        {
            return base.RedirectToRoute(routeValues);
        }

        public override RedirectToRouteResult RedirectToRoute(string? routeName, object? routeValues)
        {
            return base.RedirectToRoute(routeName, routeValues);
        }

        public override RedirectToRouteResult RedirectToRoute(string? routeName, string? fragment)
        {
            return base.RedirectToRoute(routeName, fragment);
        }

        public override RedirectToRouteResult RedirectToRoute(string? routeName, object? routeValues, string? fragment)
        {
            return base.RedirectToRoute(routeName, routeValues, fragment);
        }

        public override RedirectToRouteResult RedirectToRoutePreserveMethod(string? routeName = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToRoutePreserveMethod(routeName, routeValues, fragment);
        }

        public override RedirectToRouteResult RedirectToRoutePermanent(string? routeName)
        {
            return base.RedirectToRoutePermanent(routeName);
        }

        public override RedirectToRouteResult RedirectToRoutePermanent(object? routeValues)
        {
            return base.RedirectToRoutePermanent(routeValues);
        }

        public override RedirectToRouteResult RedirectToRoutePermanent(string? routeName, object? routeValues)
        {
            return base.RedirectToRoutePermanent(routeName, routeValues);
        }

        public override RedirectToRouteResult RedirectToRoutePermanent(string? routeName, string? fragment)
        {
            return base.RedirectToRoutePermanent(routeName, fragment);
        }

        public override RedirectToRouteResult RedirectToRoutePermanent(string? routeName, object? routeValues, string? fragment)
        {
            return base.RedirectToRoutePermanent(routeName, routeValues, fragment);
        }

        public override RedirectToRouteResult RedirectToRoutePermanentPreserveMethod(string? routeName = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToRoutePermanentPreserveMethod(routeName, routeValues, fragment);
        }

        public override RedirectToPageResult RedirectToPage(string pageName)
        {
            return base.RedirectToPage(pageName);
        }

        public override RedirectToPageResult RedirectToPage(string pageName, object? routeValues)
        {
            return base.RedirectToPage(pageName, routeValues);
        }

        public override RedirectToPageResult RedirectToPage(string pageName, string? pageHandler)
        {
            return base.RedirectToPage(pageName, pageHandler);
        }

        public override RedirectToPageResult RedirectToPage(string pageName, string? pageHandler, object? routeValues)
        {
            return base.RedirectToPage(pageName, pageHandler, routeValues);
        }

        public override RedirectToPageResult RedirectToPage(string pageName, string? pageHandler, string? fragment)
        {
            return base.RedirectToPage(pageName, pageHandler, fragment);
        }

        public override RedirectToPageResult RedirectToPage(string pageName, string? pageHandler, object? routeValues, string? fragment)
        {
            return base.RedirectToPage(pageName, pageHandler, routeValues, fragment);
        }

        public override RedirectToPageResult RedirectToPagePermanent(string pageName)
        {
            return base.RedirectToPagePermanent(pageName);
        }

        public override RedirectToPageResult RedirectToPagePermanent(string pageName, object? routeValues)
        {
            return base.RedirectToPagePermanent(pageName, routeValues);
        }

        public override RedirectToPageResult RedirectToPagePermanent(string pageName, string? pageHandler)
        {
            return base.RedirectToPagePermanent(pageName, pageHandler);
        }

        public override RedirectToPageResult RedirectToPagePermanent(string pageName, string? pageHandler, string? fragment)
        {
            return base.RedirectToPagePermanent(pageName, pageHandler, fragment);
        }

        public override RedirectToPageResult RedirectToPagePermanent(string pageName, string? pageHandler, object? routeValues, string? fragment)
        {
            return base.RedirectToPagePermanent(pageName, pageHandler, routeValues, fragment);
        }

        public override RedirectToPageResult RedirectToPagePreserveMethod(string pageName, string? pageHandler = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToPagePreserveMethod(pageName, pageHandler, routeValues, fragment);
        }

        public override RedirectToPageResult RedirectToPagePermanentPreserveMethod(string pageName, string? pageHandler = null, object? routeValues = null, string? fragment = null)
        {
            return base.RedirectToPagePermanentPreserveMethod(pageName, pageHandler, routeValues, fragment);
        }

        public override FileContentResult File(byte[] fileContents, string contentType)
        {
            return base.File(fileContents, contentType);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, bool enableRangeProcessing)
        {
            return base.File(fileContents, contentType, enableRangeProcessing);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, string? fileDownloadName)
        {
            return base.File(fileContents, contentType, fileDownloadName);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, string? fileDownloadName, bool enableRangeProcessing)
        {
            return base.File(fileContents, contentType, fileDownloadName, enableRangeProcessing);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(fileContents, contentType, lastModified, entityTag);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(fileContents, contentType, lastModified, entityTag, enableRangeProcessing);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(fileContents, contentType, fileDownloadName, lastModified, entityTag);
        }

        public override FileContentResult File(byte[] fileContents, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(fileContents, contentType, fileDownloadName, lastModified, entityTag, enableRangeProcessing);
        }

        public override FileStreamResult File(Stream fileStream, string contentType)
        {
            return base.File(fileStream, contentType);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, bool enableRangeProcessing)
        {
            return base.File(fileStream, contentType, enableRangeProcessing);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, string? fileDownloadName)
        {
            return base.File(fileStream, contentType, fileDownloadName);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, string? fileDownloadName, bool enableRangeProcessing)
        {
            return base.File(fileStream, contentType, fileDownloadName, enableRangeProcessing);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(fileStream, contentType, lastModified, entityTag);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(fileStream, contentType, lastModified, entityTag, enableRangeProcessing);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(fileStream, contentType, fileDownloadName, lastModified, entityTag);
        }

        public override FileStreamResult File(Stream fileStream, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(fileStream, contentType, fileDownloadName, lastModified, entityTag, enableRangeProcessing);
        }

        public override VirtualFileResult File(string virtualPath, string contentType)
        {
            return base.File(virtualPath, contentType);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, bool enableRangeProcessing)
        {
            return base.File(virtualPath, contentType, enableRangeProcessing);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, string? fileDownloadName)
        {
            return base.File(virtualPath, contentType, fileDownloadName);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, string? fileDownloadName, bool enableRangeProcessing)
        {
            return base.File(virtualPath, contentType, fileDownloadName, enableRangeProcessing);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(virtualPath, contentType, lastModified, entityTag);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(virtualPath, contentType, lastModified, entityTag, enableRangeProcessing);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.File(virtualPath, contentType, fileDownloadName, lastModified, entityTag);
        }

        public override VirtualFileResult File(string virtualPath, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.File(virtualPath, contentType, fileDownloadName, lastModified, entityTag, enableRangeProcessing);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType)
        {
            return base.PhysicalFile(physicalPath, contentType);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, bool enableRangeProcessing)
        {
            return base.PhysicalFile(physicalPath, contentType, enableRangeProcessing);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, string? fileDownloadName)
        {
            return base.PhysicalFile(physicalPath, contentType, fileDownloadName);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, string? fileDownloadName, bool enableRangeProcessing)
        {
            return base.PhysicalFile(physicalPath, contentType, fileDownloadName, enableRangeProcessing);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.PhysicalFile(physicalPath, contentType, lastModified, entityTag);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.PhysicalFile(physicalPath, contentType, lastModified, entityTag, enableRangeProcessing);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag)
        {
            return base.PhysicalFile(physicalPath, contentType, fileDownloadName, lastModified, entityTag);
        }

        public override PhysicalFileResult PhysicalFile(string physicalPath, string contentType, string? fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag, bool enableRangeProcessing)
        {
            return base.PhysicalFile(physicalPath, contentType, fileDownloadName, lastModified, entityTag, enableRangeProcessing);
        }

        public override UnauthorizedResult Unauthorized()
        {
            return base.Unauthorized();
        }

        public override UnauthorizedObjectResult Unauthorized([ActionResultObjectValue] object? value)
        {
            return base.Unauthorized(value);
        }

        public override NotFoundResult NotFound()
        {
            return base.NotFound();
        }

        public override NotFoundObjectResult NotFound([ActionResultObjectValue] object? value)
        {
            return base.NotFound(value);
        }

        public override BadRequestResult BadRequest()
        {
            return base.BadRequest();
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object? error)
        {
            return base.BadRequest(error);
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] ModelStateDictionary modelState)
        {
            return base.BadRequest(modelState);
        }

        public override UnprocessableEntityResult UnprocessableEntity()
        {
            return base.UnprocessableEntity();
        }

        public override UnprocessableEntityObjectResult UnprocessableEntity([ActionResultObjectValue] object? error)
        {
            return base.UnprocessableEntity(error);
        }

        public override UnprocessableEntityObjectResult UnprocessableEntity([ActionResultObjectValue] ModelStateDictionary modelState)
        {
            return base.UnprocessableEntity(modelState);
        }

        public override ConflictResult Conflict()
        {
            return base.Conflict();
        }

        public override ConflictObjectResult Conflict([ActionResultObjectValue] object? error)
        {
            return base.Conflict(error);
        }

        public override ConflictObjectResult Conflict([ActionResultObjectValue] ModelStateDictionary modelState)
        {
            return base.Conflict(modelState);
        }

        public override ObjectResult Problem(string? detail = null, string? instance = null, int? statusCode = null, string? title = null, string? type = null)
        {
            return base.Problem(detail, instance, statusCode, title, type);
        }

        public override ActionResult ValidationProblem([ActionResultObjectValue] ValidationProblemDetails descriptor)
        {
            return base.ValidationProblem(descriptor);
        }

        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            return base.ValidationProblem(modelStateDictionary);
        }

        public override ActionResult ValidationProblem()
        {
            return base.ValidationProblem();
        }

        public override ActionResult ValidationProblem(string? detail = null, string? instance = null, int? statusCode = null, string? title = null, string? type = null, [ActionResultObjectValue] ModelStateDictionary? modelStateDictionary = null)
        {
            return base.ValidationProblem(detail, instance, statusCode, title, type, modelStateDictionary);
        }

        public override CreatedResult Created()
        {
            return base.Created();
        }

        public override CreatedResult Created(string? uri, [ActionResultObjectValue] object? value)
        {
            return base.Created(uri, value);
        }

        public override CreatedResult Created(Uri? uri, [ActionResultObjectValue] object? value)
        {
            return base.Created(uri, value);
        }

        public override CreatedAtActionResult CreatedAtAction(string? actionName, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtAction(actionName, value);
        }

        public override CreatedAtActionResult CreatedAtAction(string? actionName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtAction(actionName, routeValues, value);
        }

        public override CreatedAtActionResult CreatedAtAction(string? actionName, string? controllerName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtAction(actionName, controllerName, routeValues, value);
        }

        public override CreatedAtRouteResult CreatedAtRoute(string? routeName, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtRoute(routeName, value);
        }

        public override CreatedAtRouteResult CreatedAtRoute(object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtRoute(routeValues, value);
        }

        public override CreatedAtRouteResult CreatedAtRoute(string? routeName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.CreatedAtRoute(routeName, routeValues, value);
        }

        public override AcceptedResult Accepted()
        {
            return base.Accepted();
        }

        public override AcceptedResult Accepted([ActionResultObjectValue] object? value)
        {
            return base.Accepted(value);
        }

        public override AcceptedResult Accepted(Uri uri)
        {
            return base.Accepted(uri);
        }

        public override AcceptedResult Accepted(string? uri)
        {
            return base.Accepted(uri);
        }

        public override AcceptedResult Accepted(string? uri, [ActionResultObjectValue] object? value)
        {
            return base.Accepted(uri, value);
        }

        public override AcceptedResult Accepted(Uri uri, [ActionResultObjectValue] object? value)
        {
            return base.Accepted(uri, value);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName)
        {
            return base.AcceptedAtAction(actionName);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName, string? controllerName)
        {
            return base.AcceptedAtAction(actionName, controllerName);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName, [ActionResultObjectValue] object? value)
        {
            return base.AcceptedAtAction(actionName, value);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName, string? controllerName, [ActionResultObjectValue] object? routeValues)
        {
            return base.AcceptedAtAction(actionName, controllerName, routeValues);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.AcceptedAtAction(actionName, routeValues, value);
        }

        public override AcceptedAtActionResult AcceptedAtAction(string? actionName, string? controllerName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.AcceptedAtAction(actionName, controllerName, routeValues, value);
        }

        public override AcceptedAtRouteResult AcceptedAtRoute([ActionResultObjectValue] object? routeValues)
        {
            return base.AcceptedAtRoute(routeValues);
        }

        public override AcceptedAtRouteResult AcceptedAtRoute(string? routeName)
        {
            return base.AcceptedAtRoute(routeName);
        }

        public override AcceptedAtRouteResult AcceptedAtRoute(string? routeName, object? routeValues)
        {
            return base.AcceptedAtRoute(routeName, routeValues);
        }

        public override AcceptedAtRouteResult AcceptedAtRoute(object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.AcceptedAtRoute(routeValues, value);
        }

        public override AcceptedAtRouteResult AcceptedAtRoute(string? routeName, object? routeValues, [ActionResultObjectValue] object? value)
        {
            return base.AcceptedAtRoute(routeName, routeValues, value);
        }

        public override ChallengeResult Challenge()
        {
            return base.Challenge();
        }

        public override ChallengeResult Challenge(params string[] authenticationSchemes)
        {
            return base.Challenge(authenticationSchemes);
        }

        public override ChallengeResult Challenge(AuthenticationProperties properties)
        {
            return base.Challenge(properties);
        }

        public override ChallengeResult Challenge(AuthenticationProperties properties, params string[] authenticationSchemes)
        {
            return base.Challenge(properties, authenticationSchemes);
        }

        public override ForbidResult Forbid()
        {
            return base.Forbid();
        }

        public override ForbidResult Forbid(params string[] authenticationSchemes)
        {
            return base.Forbid(authenticationSchemes);
        }

        public override ForbidResult Forbid(AuthenticationProperties properties)
        {
            return base.Forbid(properties);
        }

        public override ForbidResult Forbid(AuthenticationProperties properties, params string[] authenticationSchemes)
        {
            return base.Forbid(properties, authenticationSchemes);
        }

        public override SignInResult SignIn(ClaimsPrincipal principal)
        {
            return base.SignIn(principal);
        }

        public override SignInResult SignIn(ClaimsPrincipal principal, string authenticationScheme)
        {
            return base.SignIn(principal, authenticationScheme);
        }

        public override SignInResult SignIn(ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            return base.SignIn(principal, properties);
        }

        public override SignInResult SignIn(ClaimsPrincipal principal, AuthenticationProperties properties, string authenticationScheme)
        {
            return base.SignIn(principal, properties, authenticationScheme);
        }

        public override SignOutResult SignOut()
        {
            return base.SignOut();
        }

        public override SignOutResult SignOut(AuthenticationProperties properties)
        {
            return base.SignOut(properties);
        }

        public override SignOutResult SignOut(params string[] authenticationSchemes)
        {
            return base.SignOut(authenticationSchemes);
        }

        public override SignOutResult SignOut(AuthenticationProperties properties, params string[] authenticationSchemes)
        {
            return base.SignOut(properties, authenticationSchemes);
        }

        public override Task<bool> TryUpdateModelAsync<TModel>(TModel model)
        {
            return base.TryUpdateModelAsync(model);
        }

        public override Task<bool> TryUpdateModelAsync<TModel>(TModel model, string prefix)
        {
            return base.TryUpdateModelAsync(model, prefix);
        }

        public override Task<bool> TryUpdateModelAsync<TModel>(TModel model, string prefix, IValueProvider valueProvider)
        {
            return base.TryUpdateModelAsync(model, prefix, valueProvider);
        }

        public override Task<bool> TryUpdateModelAsync(object model, Type modelType, string prefix)
        {
            return base.TryUpdateModelAsync(model, modelType, prefix);
        }

        public override bool TryValidateModel(object model)
        {
            return base.TryValidateModel(model);
        }

        public override bool TryValidateModel(object model, string? prefix)
        {
            return base.TryValidateModel(model, prefix);
        }
    }
}