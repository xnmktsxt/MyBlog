using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    //对AccountController开启登陆认证
    [Authorize]
    public class AccountController : Controller
    {
        //登陆与注册主要用到的两个核心服务
        private SignInManager<User> _signManager;
        private UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signManager = signInManager;
            _userManager = userManager;
        }

        //允许在未登录的状态下对Login页面的请求
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl="")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        //允许在未登录的状态下对Login表单的提交
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //表单数据验证成功的情况下，对用户进行登陆
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.UserName,
                    model.Password, model.RememberMe, false);

                
                //登陆成功后，根据之前记录的url进行重定向，如果为空，就定向到/Home/Index页面
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                
                //ReturnUrl为空的情况下，给其赋值空字符串
                if(string.IsNullOrEmpty(model.ReturnUrl))
                {
                    model.ReturnUrl = "";
                }
            }

            //表单数据验证未通过显示错误提示
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        //退出登陆
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //只允许已登录的用户对注册页面的请求
        //[AllowAnonymous]
        [HttpGet]
        public ViewResult Signup()
        {
            return View();
        }

        //注册表单提交
        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Signup(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);

                //注册成功自动登陆，并重定向到Home/Index。否则提示错误描述。
                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }
    }

    //用来传递Signup.View表单数据的Model
    public class RegisterViewModel
    {
        [Required, MaxLength(64)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }

    //用来传递Login.View表单数据的Model
    public class LoginViewModel
    {
        [StringLength(64, MinimumLength = 4)]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "password cannot be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
