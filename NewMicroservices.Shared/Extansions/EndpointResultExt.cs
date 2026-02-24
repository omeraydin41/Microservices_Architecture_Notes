using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroservices.Shared.Extansions
{
    public static class ResultExt
    {
        //controller endpoint Iaction result mınımal api için TResult
        public static IResult ToGenericResult<T>(this ServiceResult<T> result)
        {
            return result.Status switch
            {
                HttpStatusCode.OK => Results.Ok(result.Data),

                HttpStatusCode.Created=> Results.Created(result.UrlAsCreated,result.Data),

                HttpStatusCode.NotFound => Results.NotFound(),

                HttpStatusCode.BadRequest => Results.Problem(result.Fail!),
                //HttpStatusCode.NoContent => Results.NoContent(),
                _ => Results.Problem(result.Fail!)
                //Problem hatanın detayını veririz ServiceResult içindeki Fail propertysinden //null olamaz 



            };
        }

        public static IResult ToGenericResult(this ServiceResult result)
        {
            return result.Status switch
            {
                

                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.NotFound => Results.NotFound(),

                HttpStatusCode.BadRequest => Results.Problem(result.Fail!),
                _ => Results.Problem(result.Fail!)
                //Problem hatanın detayını veririz ServiceResult içindeki Fail propertysinden //null olamaz 



            };
        }


    }
}
