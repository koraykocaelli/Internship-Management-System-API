using Microsoft.AspNetCore.Http;

namespace InternshipManagementSystem.Application.ViewModels
{
   public class ResponseModel
   {
      public ResponseModel(bool isSuccess, string message, object data, int statusCodeNumber)
      {
            
         IsSuccess = isSuccess;
         Message = message;
         Data = data;
         StatusCode = statusCodeNumber;
         
      }
      public ResponseModel()
      {

      }

      public bool IsSuccess { get; set; }
      public string Message { get; set; }
      public object Data {    get; set; }
      public  int StatusCode { get; set;}

   }
   
}
