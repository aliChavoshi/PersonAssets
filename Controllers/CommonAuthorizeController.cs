using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonAssets.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class CommonAuthorizeController : Controller;