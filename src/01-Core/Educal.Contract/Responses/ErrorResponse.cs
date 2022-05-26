using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educal.Contract.Responses
{
    public class ErrorResponse
    {
        public List<string> Errors  { get; set; } = new List<string> ();
        public ErrorResponse()
        {
            Errors = new List<string>();
        }
        public ErrorResponse(string error)
        {
            Errors.Add(error);
        }
        public ErrorResponse(List<string> errors)
        {
            Errors = errors;
        }
    }
}