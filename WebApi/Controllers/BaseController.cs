using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class BaseController:ControllerBase
    {
        private IMediator _mediator;

        //set edildi mi edilmişse onu döndür yok ise HttpContext üzerinden RequestServislerinde GetServis ile injektionlara bak bulduğunu set et. 
        //IoC ortamındaki karşılığını getiriyor.
        //Bu IoC kaydı için de extension yazdım. Her katman özelinde bunu yazdım
        //Application>ApplicationServiceRegistration adında class ile yaptım.
        protected IMediator? Mediator => _mediator??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
