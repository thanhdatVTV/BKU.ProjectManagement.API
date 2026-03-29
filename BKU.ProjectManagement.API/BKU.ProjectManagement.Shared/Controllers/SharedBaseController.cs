using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Shared.Controllers
{
    public class SharedBaseController : ControllerBase
    {
        protected internal const string BasePath = "api";
    }
}
