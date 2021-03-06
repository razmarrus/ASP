﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PointSystem.Models;
 
namespace PointSystem.Controllers
{

    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<AspNetUser> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AspNetUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        public IActionResult Index() => View(_roleManager.Roles.ToList());


        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        public IActionResult Create() => View();
        [HttpPost]

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }


        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        public IActionResult UserList() => View(_userManager.Users.ToList());


        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            AspNetUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            AspNetUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}
