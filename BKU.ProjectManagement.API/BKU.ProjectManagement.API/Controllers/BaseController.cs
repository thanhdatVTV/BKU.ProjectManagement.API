using BKU.ProjectManagement.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BKU.ProjectManagement.API.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    public abstract class BaseController : SharedBaseController
    {
        protected internal new const string BasePath = SharedBaseController.BasePath;
    }
}
