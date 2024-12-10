using FufelMarketBackend.Data;
using FufelMarketBackend.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FufelMarketBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController(AppDbContext context, IMailService mailService, EmailTemplateService emailTemplateService, ISender mediator) : ControllerBase
{
    [HttpPost("sendVerificationCode")]
    public async Task<ActionResult> SendCode(string email)
    {
        var code = mailService.GenerateConfirmationCode();
        var body = await emailTemplateService
            .GetEmailBodyAsync("SendVerificationCode.html",
                new Dictionary<string, string>
                {
                   ["Code"] = code
                });
        await mailService.SendMailAsync(email, "Подтверждение входа", body);
        
        return Ok();
    }

    // [HttpPost("verifyCode")]
    // public async Task<ActionResult> VerifyCode()
    // {
    //     return Ok();
    // }
}