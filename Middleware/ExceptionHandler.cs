using LibraryAPI.Exceptions;

namespace LibraryAPI.Middleware
{
    public class ExceptionHandler : IMiddleware 
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next) 
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                context.Response.WriteAsync(ex.Message);
            }
            catch (BadRequestException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.WriteAsync(ex.Message);
            }
            
            catch (AccountNotVerifiedException ex)
            {
                context.Response.StatusCode = 403;
                context.Response.WriteAsync(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = 401;
                context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.WriteAsync("Something went wrong");
            }
            
                

            

        }
    }
}
