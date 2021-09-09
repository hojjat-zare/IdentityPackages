﻿using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;



namespace RoleBaseApi.RegisterEndpoints
{
    public class Register : BaseAsyncEndpoint
        .WithRequest<RegisterRequest>
        .WithResponse<RegisterResponse>
    {

        private SignInManager<ApplicationUser> _signInManager{ get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        public IEmailSender _emailSender{ get; set; }

        public Register(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager,IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }


        [HttpPost("api/Register")]
        [SwaggerOperation(
            Summary = "Register a user",
            Description = "Register a user",
            OperationId = "Register",
            Tags = new[] { "RegisterEndpoints" })
        ]
        public override async Task<ActionResult<RegisterResponse>> HandleAsync(RegisterRequest Input, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return BadRequest(ModelState);

        }
    }
}
