using SweetCake.Data;
using SweetCake.ViewModel;

namespace SweetCake.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl (HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
